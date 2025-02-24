//******************************************************************************************************
//  StreamSplitter.cs - Gbtc
//
//  Copyright © 2015, Grid Protection Alliance.  All Rights Reserved.
//
//  Licensed to the Grid Protection Alliance (GPA) under one or more contributor license agreements. See
//  the NOTICE file distributed with this work for additional information regarding copyright ownership.
//  The GPA licenses this file to you under the Eclipse Public License -v 1.0 (the "License"); you may
//  not use this file except in compliance with the License. You may obtain a copy of the License at:
//
//      http://www.opensource.org/licenses/eclipse-1.0.php
//
//  Unless agreed to in writing, the subject software distributed under the License is distributed on an
//  "AS-IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. Refer to the
//  License for the specific language governing permissions and limitations.
//
//  Code Modification History:
//  ----------------------------------------------------------------------------------------------------
//  09/04/2013 - J. Ritchie Carroll
//       Generated original version of source code.
//
//******************************************************************************************************

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Timers;
using GSF;
using GSF.Communication;
using GSF.Configuration;
using GSF.Diagnostics;
using GSF.Parsing;
using GSF.PhasorProtocols;
using GSF.PhasorProtocols.IEEEC37_118;
using GSF.Threading;
using GSF.Units;
using TcpClient = GSF.Communication.TcpClient;
using Timer = System.Timers.Timer;

namespace StreamSplitter
{
    /// <summary>
    /// Represents the source synchrophasor and proxy socket connection used to split a stream of data.
    /// </summary>
    public class StreamProxy : ISupportLifecycle, IProvideStatus, IComparable<StreamProxy>
    {
        #region [ Members ]

        // Constants
        private const double DefaultDataMonitorInterval = 10000.0D;
        private const double DefaultSocketErrorReportingInterval = 10.0D;

        // Events

        /// <summary>
        /// Provides status messages to consumer.
        /// </summary>
        /// <remarks>
        /// <see cref="EventArgs{T}.Argument"/> is new status message.
        /// </remarks>
        public event EventHandler<EventArgs<string>> StatusMessage;

        /// <summary>
        /// This event is raised if there is an exception encountered during processing.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <see cref="EventArgs{T}.Argument"/> is the <see cref="Exception"/> encountered while processing.
        /// </para>
        /// <para>
        /// Processing will not stop for any exceptions thrown by the user function, but any captured exceptions will be exposed through this event.
        /// </para>
        /// </remarks>
        public event EventHandler<EventArgs<Exception>> ProcessException;

        /// <summary>
        /// This event is raised when <see cref="StreamProxy"/> is disposed.
        /// </summary>
        public event EventHandler Disposed;

        // Fields
        private string m_name;
        private Guid m_id;
        private MultiProtocolFrameParser m_frameParser;
        private ushort[] m_accessIDList;
        private IServer m_publishChannel;
        private TcpClient m_clientBasedPublishChannel;
        private Timer m_dataStreamMonitor;
        private volatile IConfigurationFrame m_configurationFrame;
        private volatile ConfigurationFrame3 m_configurationFrame3;
        private int m_lastConfigurationPublishMinute;
        private bool m_configurationFramePublished;
        private long m_receivedConfigurationFrames;
        private readonly ConcurrentDictionary<Guid, string> m_connectionIDCache;
        private readonly object m_startStopLock;
        private long m_startTime;
        private long m_stopTime;
        private long m_bytesReceived;
        private readonly double m_socketErrorReportingInterval;
        private int m_lastSocketErrorNumber;
        private long m_lastSocketErrorTime;
        private string m_connectionInfo;
        private volatile bool m_enabled;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Creates a new <see cref="StreamProxy"/> based on a <see cref="ProxyConnection"/> configuration.
        /// </summary>
        /// <param name="proxyConnection"><see cref="ProxyConnection"/> used to configure the new <see cref="StreamProxy"/>.</param>
        public StreamProxy(ProxyConnection proxyConnection)
        {
            // Create new synchrophasor data source parser
            m_frameParser = new MultiProtocolFrameParser();

            // We are not here to validate/use data, just here to proxy it along...
            m_frameParser.CheckSumValidationFrameTypes = CheckSumValidationFrameTypes.NoFrames;
            m_frameParser.AutoStartDataParsingSequence = true;
            m_frameParser.MaximumConnectionAttempts = 1;

            m_frameParser.ConnectionAttempt += m_frameParser_ConnectionAttempt;
            m_frameParser.ConnectionEstablished += m_frameParser_ConnectionEstablished;
            m_frameParser.ConnectionException += m_frameParser_ConnectionException;
            m_frameParser.ConnectionTerminated += m_frameParser_ConnectionTerminated;
            m_frameParser.ConfigurationChanged += m_frameParser_ConfigurationChanged;
            m_frameParser.ParsingException += m_frameParser_ParsingException;
            m_frameParser.ExceededParsingExceptionThreshold += m_frameParser_ExceededParsingExceptionThreshold;
            m_frameParser.ReceivedConfigurationFrame += m_frameParser_ReceivedConfigurationFrame;
            m_frameParser.ReceivedFrameBufferImage += m_frameParser_ReceivedFrameBufferImage;
            m_frameParser.ServerStarted += m_frameParser_ServerStarted;
            m_frameParser.ServerStopped += m_frameParser_ServerStopped;
            m_frameParser.ServerIndexUpdated += m_frameParser_ServerIndexUpdated;

            // Create data stream monitoring timer
            m_dataStreamMonitor = new Timer();
            m_dataStreamMonitor.Elapsed += m_dataStreamMonitor_Elapsed;
            m_dataStreamMonitor.Interval = DefaultDataMonitorInterval;
            m_dataStreamMonitor.AutoReset = true;
            m_dataStreamMonitor.Enabled = false;

            // Create a new connection ID cache
            m_connectionIDCache = new ConcurrentDictionary<Guid, string>();

            // Create lock object for Start() and Stop() methods
            m_startStopLock = new object();

            // Set stream proxy ID
            m_id = proxyConnection.ID;

            // Maintain a current status for the stream proxy
            StreamProxyStatus = new StreamProxyStatus(m_id);

            // Initialize stream splitter based on new proxy connection settings
            ProxyConnection = proxyConnection;

            // Read any needed configuration settings
            try
            {
                ConfigurationFile configFile = ConfigurationFile.Current;
                CategorizedSettingsElementCollection systemSettings = configFile.Settings["systemSettings"];
                m_socketErrorReportingInterval = systemSettings["SocketErrorReportingInterval"].ValueAs(DefaultSocketErrorReportingInterval);
            }
            catch (Exception)
            {
                m_socketErrorReportingInterval = DefaultSocketErrorReportingInterval;
            }
        }

        /// <summary>
        /// Releases the unmanaged resources before the <see cref="StreamProxy"/> object is reclaimed by <see cref="GC"/>.
        /// </summary>
        ~StreamProxy()
        {
            Dispose(false);
        }

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Updates <see cref="StreamProxy"/> configuration based on assigned <see cref="ProxyConnection"/>.
        /// </summary>
        public ProxyConnection ProxyConnection
        {
            set
            {
                if (value is null)
                    throw new ArgumentNullException(nameof(value), "Cannot update StreamSplitter configuration: provided ProxyConnection is null.");

                // Validate that this is correct proxy connection
                if (value.ID != m_id)
                    throw new InvalidOperationException("Cannot update StreamSplitter configuration: provided ProxyConnection does not have the correct ID.");

                Name = value.Name;
                SourceSettings = value.SourceSettings;
                ProxySettings = value.ProxySettings;
                Enabled = false;
                Initialize();

                if (value.Enabled)
                    Enabled = true;
            }
        }

        /// <summary>
        /// Gets the current proxy connection status.
        /// </summary>
        public StreamProxyStatus StreamProxyStatus { get; }

        /// <summary>
        /// Gets or sets name associated with the <see cref="StreamProxy"/>.
        /// </summary>
        public string Name
        {
            get => m_name.ToNonNullString("<undefined>");
            set => m_name = value;
        }

        /// <summary>
        /// Gets or sets ID of the <see cref="StreamProxy"/>.
        /// </summary>
        public Guid ID
        {
            get => m_id;
            set => m_id = value;
        }

        /// <summary>
        /// Gets the access ID (a.k.a., ID code) for this device connection. Value is often necessary in order to make a connection to some phasor protocols.
        /// </summary>
        /// <remarks>
        /// This value can mutate when configured with multiple values, i.e., where an alternate access ID code is specified for a target device connection, e.g.:
        /// <c>accessID=95; server=192.168.1.10:4712,192.168.1.12:4712/96,192.168.2.10:4712,192.168.2.12:4712/96</c>
        /// In this example both <c>192.168.1.10:4712</c> and <c>192.168.2.10:4712</c> use the configured access ID of 95, but
        /// <c>192.168.1.12:4712/96</c> and <c>192.168.2.12:4712/96</c> specify an access ID of 96.
        /// </remarks>
        public ushort AccessID
        {
            get
            {
                if (ServerIndex >= 0 && ServerIndex < m_accessIDList?.Length)
                    return m_accessIDList[ServerIndex];

                return m_accessIDList?.Length > 0 ? m_accessIDList[0] : (ushort)1;
            }
        }

        private int ServerIndex => m_frameParser?.ServerIndex ?? 0;

        /// <summary>
        /// Gets connection info for proxy.
        /// </summary>
        public string ConnectionInfo
        {
            get
            {
                string connectionInfo = m_frameParser?.ConnectionInfo;

                if (connectionInfo is not null && connectionInfo.StartsWith("tcp://", StringComparison.Ordinal))
                    connectionInfo = $"{connectionInfo}/{AccessID}";

                if (m_accessIDList.Length > 1)
                    connectionInfo = $"{connectionInfo} [IP #{ServerIndex + 1}]";

                return connectionInfo;
            }
        }

        /// <summary>
        /// Gets or sets connection string used for <see cref="MultiProtocolFrameParser"/> data source.
        /// </summary>
        public string SourceSettings { get; set; }

        /// <summary>
        /// Gets or sets connection string for TCP or UCP socket instance used to proxy synchrophasor frames.
        /// </summary>
        public string ProxySettings { get; set; }

        /// <summary>
        /// Gets or sets the current enabled state of <see cref="StreamProxy"/>.
        /// </summary>
        /// <returns>Current enabled state of <see cref="StreamProxy"/>.</returns>
        /// <remarks>
        /// Splitter must be started by calling <see cref="Start"/> method or setting
        /// <c><see cref="Enabled"/> = true</c> before data splitting will begin.
        /// </remarks>
        public virtual bool Enabled
        {
            get => m_enabled;
            set
            {
                if (value)
                    Start();
                else
                    Stop();
            }
        }

        /// <summary>
        /// Gets a flag that indicates whether the object has been disposed.
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Gets the total number of bytes sent to clients of this <see cref="StreamProxy"/>.
        /// </summary>
        public long TotalBytesSent { get; private set; }

        /// <summary>
        /// Gets the UTC time the <see cref="StreamProxy"/> was started.
        /// </summary>
        public Ticks StartTime => m_startTime;

        /// <summary>
        /// Gets the UTC time the <see cref="StreamProxy"/> was stopped.
        /// </summary>
        public Ticks StopTime => m_stopTime;

        /// <summary>
        /// Gets the total amount of time, in seconds, that the <see cref="StreamProxy"/> has been active.
        /// </summary>
        public Time RunTime
        {
            get
            {
                Ticks processingTime = 0;

                if (m_startTime > 0)
                {
                    if (m_stopTime > 0)
                        processingTime = m_stopTime - m_startTime;
                    else
                        processingTime = DateTime.UtcNow.Ticks - m_startTime;
                }

                if (processingTime < 0)
                    processingTime = 0;

                return processingTime.ToSeconds();
            }
        }

        /// <summary>
        /// Returns a detailed status of this <see cref="StreamProxy"/>.
        /// </summary>
        public string Status
        {
            get
            {
                StringBuilder status = new();

                status.AppendLine($"        Stream Splitter ID: {ID:N0}");
                status.AppendLine($"    Total process run time: {RunTime.ToString(2)}");
                status.AppendLine($"          Total bytes sent: {TotalBytesSent:N0}");

                if (m_dataStreamMonitor is not null)
                    status.AppendLine($"No data reconnect interval: {Ticks.FromMilliseconds(m_dataStreamMonitor.Interval).ToSeconds():N3} seconds");

                if (m_configurationFrame is not null)
                    status.AppendLine($"  Configuration frame size: {m_configurationFrame.BinaryLength:N0} bytes");

                if (m_configurationFrame3 is not null)
                    status.AppendLine($" Configuration frame3 size: {m_configurationFrame3.BinaryLength:N0} bytes");

                if (m_frameParser is not null)
                    status.Append(m_frameParser.Status);

                if (m_publishChannel is not null)
                {
                    status.AppendLine();
                    status.AppendLine("Publication Channel Status".CenterText(50));
                    status.AppendLine("--------------------------".CenterText(50));
                    status.Append(m_publishChannel.Status);

                    TcpServer tcpPublishChannel = m_publishChannel as TcpServer;

                    Guid[] clientIDs = tcpPublishChannel?.ClientIDs;

                    if (clientIDs is { Length: > 0 })
                    {
                        status.AppendLine();
                        status.Append($"TCP publish channel has {clientIDs.Length:N0} connected clients:\r\n\r\n");

                        for (int i = 0; i < clientIDs.Length; i++)
                            status.Append($"    {i + 1}) {GetConnectionID(tcpPublishChannel, clientIDs[i])}\r\n");

                        status.AppendLine();
                    }
                }

                if (m_clientBasedPublishChannel is not null)
                {
                    status.AppendLine();
                    status.AppendLine("Publication Channel Status".CenterText(50));
                    status.AppendLine("--------------------------".CenterText(50));
                    status.Append(m_clientBasedPublishChannel.Status);
                }

                return status.ToString();
            }
        }

        /// <summary>
        /// Gets or sets reference to <see cref="UdpServer"/> publication channel, attaching and/or detaching to events as needed.
        /// </summary>
        protected UdpServer UdpPublishChannel
        {
            get => m_publishChannel as UdpServer;
            set
            {
                UdpServer udpPublishChannel = m_publishChannel as UdpServer;

                if (m_publishChannel is not null && udpPublishChannel is null)
                {
                    // Trying to dispose non-UDP publication channel - nothing to do...
                    if (value is null)
                        return;

                    // Publish channel is currently TCP, detach from TCP events
                    TcpPublishChannel = null;
                }

                if (udpPublishChannel is not null)
                {
                    // Detach from events on existing data channel reference
                    udpPublishChannel.ClientConnectingException -= udpPublishChannel_ClientConnectingException;
                    udpPublishChannel.ReceiveClientDataComplete -= udpPublishChannel_ReceiveClientDataComplete;
                    udpPublishChannel.SendClientDataException -= udpPublishChannel_SendClientDataException;
                    udpPublishChannel.ServerStarted -= udpPublishChannel_ServerStarted;
                    udpPublishChannel.ServerStopped -= udpPublishChannel_ServerStopped;

                    if (udpPublishChannel != value)
                        udpPublishChannel.Dispose();
                }

                // Assign new UDP publish channel reference
                udpPublishChannel = value;

                if (udpPublishChannel is not null)
                {
                    // Detach any existing client based publish channels
                    TcpClientPublishChannel = null;

                    // Attach to events on new data channel reference
                    udpPublishChannel.ClientConnectingException += udpPublishChannel_ClientConnectingException;
                    udpPublishChannel.ReceiveClientDataComplete += udpPublishChannel_ReceiveClientDataComplete;
                    udpPublishChannel.SendClientDataException += udpPublishChannel_SendClientDataException;
                    udpPublishChannel.ServerStarted += udpPublishChannel_ServerStarted;
                    udpPublishChannel.ServerStopped += udpPublishChannel_ServerStopped;
                }

                m_publishChannel = udpPublishChannel;
            }
        }

        /// <summary>
        /// Gets or sets reference to <see cref="TcpServer"/> publication channel, attaching and/or detaching to events as needed.
        /// </summary>
        protected TcpServer TcpPublishChannel
        {
            get => m_publishChannel as TcpServer;
            set
            {
                TcpServer tcpPublishChannel = m_publishChannel as TcpServer;

                if (m_publishChannel is not null && tcpPublishChannel is null)
                {
                    // Trying to dispose non-TCP publication channel - nothing to do...
                    if (value is null)
                        return;

                    // Publish channel is currently UDP, detach from UDP events
                    UdpPublishChannel = null;
                }

                if (tcpPublishChannel is not null)
                {
                    // Detach from events on existing command channel reference
                    tcpPublishChannel.ClientConnected -= tcpPublishChannel_ClientConnected;
                    tcpPublishChannel.ClientDisconnected -= tcpPublishChannel_ClientDisconnected;
                    tcpPublishChannel.ClientConnectingException -= tcpPublishChannel_ClientConnectingException;
                    tcpPublishChannel.ReceiveClientDataComplete -= tcpPublishChannel_ReceiveClientDataComplete;
                    tcpPublishChannel.SendClientDataException -= tcpPublishChannel_SendClientDataException;
                    tcpPublishChannel.ServerStarted -= tcpPublishChannel_ServerStarted;
                    tcpPublishChannel.ServerStopped -= tcpPublishChannel_ServerStopped;

                    if (tcpPublishChannel != value)
                        tcpPublishChannel.Dispose();
                }

                // Assign new TCP publish channel reference
                tcpPublishChannel = value;

                if (tcpPublishChannel is not null)
                {
                    // Detach any existing client based publish channels
                    TcpClientPublishChannel = null;

                    // Attach to events on new command channel reference
                    tcpPublishChannel.ClientConnected += tcpPublishChannel_ClientConnected;
                    tcpPublishChannel.ClientDisconnected += tcpPublishChannel_ClientDisconnected;
                    tcpPublishChannel.ClientConnectingException += tcpPublishChannel_ClientConnectingException;
                    tcpPublishChannel.ReceiveClientDataComplete += tcpPublishChannel_ReceiveClientDataComplete;
                    tcpPublishChannel.SendClientDataException += tcpPublishChannel_SendClientDataException;
                    tcpPublishChannel.ServerStarted += tcpPublishChannel_ServerStarted;
                    tcpPublishChannel.ServerStopped += tcpPublishChannel_ServerStopped;
                }

                m_publishChannel = tcpPublishChannel;
            }
        }

        /// <summary>
        /// Gets or sets reference to <see cref="TcpClient"/> publication channel, attaching and/or detaching to events as needed.
        /// </summary>
        protected TcpClient TcpClientPublishChannel
        {
            get => m_clientBasedPublishChannel;
            set
            {
                if (m_clientBasedPublishChannel is not null)
                {
                    // Detach from events on existing command channel reference
                    m_clientBasedPublishChannel.ConnectionEstablished -= tcpClientBasedPublishChannel_ConnectionEstablished;
                    m_clientBasedPublishChannel.ConnectionTerminated -= tcpClientBasedPublishChannel_ConnectionTerminated;
                    m_clientBasedPublishChannel.ConnectionException -= tcpClientBasedPublishChannel_ConnectionException;
                    m_clientBasedPublishChannel.ReceiveDataComplete -= tcpClientBasedPublishChannel_ReceiveDataComplete;
                    m_clientBasedPublishChannel.SendDataException -= tcpClientBasedPublishChannel_SendDataException;

                    if (m_clientBasedPublishChannel != value)
                        m_clientBasedPublishChannel.Dispose();
                }

                // Assign new TCP client based publish channel reference
                m_clientBasedPublishChannel = value;

                if (m_clientBasedPublishChannel is not null)
                {
                    // Detach any existing server based publish channels
                    UdpPublishChannel = null;
                    TcpPublishChannel = null;

                    // Attach to events on new command channel reference
                    m_clientBasedPublishChannel.ConnectionEstablished += tcpClientBasedPublishChannel_ConnectionEstablished;
                    m_clientBasedPublishChannel.ConnectionTerminated += tcpClientBasedPublishChannel_ConnectionTerminated;
                    m_clientBasedPublishChannel.ConnectionException += tcpClientBasedPublishChannel_ConnectionException;
                    m_clientBasedPublishChannel.ReceiveDataComplete += tcpClientBasedPublishChannel_ReceiveDataComplete;
                    m_clientBasedPublishChannel.SendDataException += tcpClientBasedPublishChannel_SendDataException;
                }
            }
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Releases all the resources used by the <see cref="StreamProxy"/> object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="StreamProxy"/> object and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed)
                return;

            try
            {
                if (!disposing)
                    return;

                TcpPublishChannel = null;
                UdpPublishChannel = null;
                TcpClientPublishChannel = null;

                if (m_frameParser is not null)
                {
                    m_frameParser.ConnectionAttempt -= m_frameParser_ConnectionAttempt;
                    m_frameParser.ConnectionEstablished -= m_frameParser_ConnectionEstablished;
                    m_frameParser.ConnectionException -= m_frameParser_ConnectionException;
                    m_frameParser.ConnectionTerminated -= m_frameParser_ConnectionTerminated;
                    m_frameParser.ConfigurationChanged -= m_frameParser_ConfigurationChanged;
                    m_frameParser.ParsingException -= m_frameParser_ParsingException;
                    m_frameParser.ExceededParsingExceptionThreshold -= m_frameParser_ExceededParsingExceptionThreshold;
                    m_frameParser.ReceivedConfigurationFrame -= m_frameParser_ReceivedConfigurationFrame;
                    m_frameParser.ReceivedFrameBufferImage -= m_frameParser_ReceivedFrameBufferImage;
                    m_frameParser.Dispose();
                    m_frameParser = null;
                }

                if (m_dataStreamMonitor is not null)
                {
                    m_dataStreamMonitor.Elapsed -= m_dataStreamMonitor_Elapsed;
                    m_dataStreamMonitor.Dispose();
                    m_dataStreamMonitor = null;
                }
            }
            finally
            {
                IsDisposed = true;  // Prevent duplicate dispose.
                Disposed?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Initializes the <see cref="StreamProxy"/>.
        /// </summary>
        public void Initialize()
        {
            // Update connection information for the multiprotocol frame parser
            m_frameParser.ConnectionString = SourceSettings;

            Dictionary<string, string> sourceSettings = m_frameParser.ConnectionString.ParseKeyValuePairs();
            Dictionary<string, string> proxySettings = ProxySettings.ParseKeyValuePairs();

            // Apply custom data monitoring interval
            if (sourceSettings.TryGetValue("dataMonitorInterval", out string setting) && double.TryParse(setting, out double value) && m_dataStreamMonitor is not null)
                m_dataStreamMonitor.Interval = value;

            // TODO: These should be optionally picked up from connection string inside MPFP

            // Apply other settings as needed
            if (!sourceSettings.TryGetValue("accessID", out setting) || string.IsNullOrWhiteSpace(setting) || !ushort.TryParse(setting, out ushort defaultAccessID))
                defaultAccessID = 1;

            m_frameParser.DeviceID = defaultAccessID;

            // Parse any defined access IDs from server list, this assumes TCP connection since this is currently the only connection type that supports multiple end points
            if (sourceSettings.TryGetValue("server", out setting) && !string.IsNullOrWhiteSpace(setting))
            {
                List<string> serverList = [];
                List<ushort> accessIDList = [];
                string[] servers = setting.Split([','], StringSplitOptions.RemoveEmptyEntries);

                foreach (string server in servers)
                {
                    string[] parts = server.Split(['/'], StringSplitOptions.RemoveEmptyEntries);

                    if (parts.Length < 2 || !ushort.TryParse(parts[1], out ushort accessID))
                        accessID = defaultAccessID;

                    serverList.Add(parts[0].Trim());
                    accessIDList.Add(accessID);
                }

                sourceSettings["server"] = string.Join(",", serverList);
                m_frameParser.ConnectionString = sourceSettings.JoinKeyValuePairs();
                m_accessIDList = accessIDList.Count == 0 ? [defaultAccessID] : accessIDList.ToArray();
            }
            else
            {
                m_accessIDList = [defaultAccessID];
            }

            if (sourceSettings.TryGetValue("simulateTimestamp", out setting))
                m_frameParser.InjectSimulatedTimestamp = setting.ParseBoolean();

            if (sourceSettings.TryGetValue("allowedParsingExceptions", out setting))
                m_frameParser.AllowedParsingExceptions = int.Parse(setting);

            if (sourceSettings.TryGetValue("parsingExceptionWindow", out setting))
                m_frameParser.ParsingExceptionWindow = Ticks.FromSeconds(double.Parse(setting));

            if (sourceSettings.TryGetValue("skipDisableRealTimeData", out setting))
                m_frameParser.SkipDisableRealTimeData = setting.ParseBoolean();

            const int DefaultMaxQueueSize = 1000;
            int maxQueueSize;

            try
            {
                // Make sure default service settings exist
                ConfigurationFile configFile = ConfigurationFile.Current;

                // System settings
                CategorizedSettingsElementCollection systemSettings = configFile.Settings["systemSettings"];
                systemSettings.Add("MaxSendQueueSize", DefaultMaxQueueSize.ToString(), "Maximum send queue size for TCP connections.");

                if (!int.TryParse(systemSettings["MaxSendQueueSize"].ValueAsString(DefaultMaxQueueSize.ToString()), out maxQueueSize))
                    maxQueueSize = DefaultMaxQueueSize;
            }
            catch (Exception ex)
            {
                Logger.SwallowException(ex);
                maxQueueSize = DefaultMaxQueueSize;
            }

            if (proxySettings.TryGetValue("useClientPublishChannel", out setting) && setting.ParseBoolean())
            {
                // Create a new client based publication channel (for reverse TCP connections)
                TcpClientPublishChannel = ClientBase.Create(ProxySettings) as TcpClient;

                if (m_clientBasedPublishChannel is not null)
                {
                    m_clientBasedPublishChannel.MaxConnectionAttempts = -1;
                    m_clientBasedPublishChannel.MaxSendQueueSize = maxQueueSize;
                }
            }
            else
            {
                // Create a new server based publication channel
                IServer publicationServer = ServerBase.Create(ProxySettings);
                TcpPublishChannel = publicationServer as TcpServer;
                UdpPublishChannel = publicationServer as UdpServer;

                if (TcpPublishChannel is not null)
                    TcpPublishChannel.MaxSendQueueSize = maxQueueSize;
            }
        }

        /// <summary>
        /// Starts the <see cref="StreamProxy"/>, if it is not already running.
        /// </summary>
        public void Start()
        {
            lock (m_startStopLock)
            {
                // Make sure we are stopped before attempting to start
                if (Enabled)
                    Stop();

                try
                {
                    // Start publication server
                    m_publishChannel?.Start();

                    m_clientBasedPublishChannel?.ConnectAsync();

                    // Start multi-protocol frame parser
                    m_frameParser?.Start();

                    if (m_enabled)
                        return;

                    m_stopTime = 0;
                    m_startTime = DateTime.UtcNow.Ticks;

                    // Start real-time frame publication
                    m_enabled = true;
                }
                catch (Exception ex)
                {
                    OnProcessException(new InvalidOperationException($"Failed to start stream proxy: {ex.Message}", ex));
                }
            }
        }

        /// <summary>
        /// Stops the <see cref="StreamProxy"/>.
        /// </summary>
        public void Stop()
        {
            lock (m_startStopLock)
            {
                try
                {
                    StreamProxyStatus.ConnectionState = ConnectionState.Disabled;

                    // Stop data stream monitor
                    if (m_dataStreamMonitor is not null)
                        m_dataStreamMonitor.Enabled = false;

                    if (m_enabled)
                    {
                        m_enabled = false;
                        m_stopTime = DateTime.UtcNow.Ticks;
                    }

                    // Stop multi-protocol frame parser
                    m_frameParser?.Stop();

                    // Stop publication server
                    m_publishChannel?.Stop();

                    m_clientBasedPublishChannel?.Disconnect();

                    m_bytesReceived = 0;
                    m_receivedConfigurationFrames = 0;
                }
                catch (Exception ex)
                {
                    OnProcessException(new InvalidOperationException($"Failed to stop stream proxy: {ex.Message}", ex));
                }
            }
        }

        /// <summary>
        /// Sends the specified <see cref="DeviceCommand"/> to the current device connection.
        /// </summary>
        /// <param name="command"><see cref="DeviceCommand"/> to send to connected device.</param>
        public void SendCommand(DeviceCommand command)
        {
            if (m_frameParser is not null)
            {
                if (m_frameParser.SendDeviceCommand(command) is not null)
                    OnStatusMessage("Sent device command \"{0}\"...", command);
            }
            else
            {
                OnStatusMessage("Failed to send device command \"{0}\", no frame parser is defined.", command);
            }
        }

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has the following meanings:
        /// <list type="table">
        ///     <listheader>
        ///         <term>Value</term>
        ///         <term>Meaning</term>
        ///     </listheader>
        ///     <item>
        ///         <description>Less than zero</description>
        ///         <description>This object is less than the <paramref name="other"/> parameter.</description>
        ///     </item>
        ///     <item>
        ///         <description>Zero</description>
        ///         <description>This object is equal to <paramref name="other"/>.</description>
        ///     </item>
        ///     <item>
        ///         <description>Greater than zero</description>
        ///         <description>This object is greater than <paramref name="other"/>.</description>
        ///     </item>
        /// </list>
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public int CompareTo(StreamProxy other)
        {
            return m_id.CompareTo(other.m_id);
        }

        /// <summary>
        /// Raises the <see cref="StatusMessage"/> event.
        /// </summary>
        /// <param name="status">New status message.</param>
        protected virtual void OnStatusMessage(string status)
        {
            try
            {
                StreamProxyStatus.AppendStatusMessage(status);
                StatusMessage?.Invoke(this, new EventArgs<string>(status));
            }
            catch (Exception ex)
            {
                // We protect our code from consumer thrown exceptions
                OnProcessException(new InvalidOperationException($"Exception in consumer handler for StatusMessage event: {ex.Message}", ex));
            }
        }

        /// <summary>
        /// Raises the <see cref="StatusMessage"/> event with a formatted status message.
        /// </summary>
        /// <param name="formattedStatus">Formatted status message.</param>
        /// <param name="args">Arguments for <paramref name="formattedStatus"/>.</param>
        /// <remarks>
        /// This overload combines string.Format and SendStatusMessage for convenience.
        /// </remarks>
        protected virtual void OnStatusMessage(string formattedStatus, params object[] args)
        {
            try
            {
                string status = string.Format(formattedStatus, args);
                StreamProxyStatus.AppendStatusMessage(status);
                StatusMessage?.Invoke(this, new EventArgs<string>(status));
            }
            catch (Exception ex)
            {
                // We protect our code from consumer thrown exceptions
                OnProcessException(new InvalidOperationException($"Exception in consumer handler for StatusMessage event: {ex.Message}", ex));
            }
        }

        /// <summary>
        /// Raises the <see cref="ProcessException"/> event.
        /// </summary>
        /// <param name="processException">Exception to send to <see cref="ProcessException"/> event.</param>
        /// <remarks>
        /// Allows derived classes to raise a processing exception.
        /// </remarks>
        protected virtual void OnProcessException(Exception processException)
        {
            StreamProxyStatus.AppendStatusMessage("ERROR: " + processException.Message);
            ProcessException?.Invoke(this, new EventArgs<Exception>(processException));
        }

        /// <summary>
        /// Gets connection ID (i.e., IP:Port) for specified <paramref name="clientID"/>.
        /// </summary>
        /// <param name="server">Server connection of associated <paramref name="clientID"/>.</param>
        /// <param name="clientID">Guid of client for ID lookup.</param>
        /// <returns>Connection ID (i.e., IP:Port) for specified <paramref name="clientID"/>.</returns>
        protected virtual string GetConnectionID(IServer server, Guid clientID)
        {

            if ((server is null || clientID.Equals(Guid.Empty)) && m_clientBasedPublishChannel is not null)
                return m_clientBasedPublishChannel.ServerUri;

            if (m_connectionIDCache.TryGetValue(clientID, out string connectionID))
                return connectionID;

            // Attempt to lookup remote connection identification for logging purposes
            try
            {
                IPEndPoint remoteEndPoint = server switch
                {
                    TcpServer commandChannel when commandChannel.TryGetClient(clientID, out TransportProvider<Socket> tcpClient) && tcpClient is not null => tcpClient.Provider?.RemoteEndPoint as IPEndPoint,
                    UdpServer dataChannel when dataChannel.TryGetClient(clientID, out TransportProvider<EndPoint> udpClient) && udpClient is not null => udpClient.Provider as IPEndPoint,
                    _ => null
                };

                if (remoteEndPoint is not null)
                {
                    if (remoteEndPoint.AddressFamily == AddressFamily.InterNetworkV6)
                        connectionID = "[" + remoteEndPoint.Address + "]:" + remoteEndPoint.Port;
                    else
                        connectionID = remoteEndPoint.Address + ":" + remoteEndPoint.Port;

                    // Cache value for future lookup
                    m_connectionIDCache.TryAdd(clientID, connectionID);
                    ThreadPool.QueueUserWorkItem(LookupHostName, new Tuple<Guid, string, IPEndPoint>(clientID, connectionID, remoteEndPoint));
                }
            }
            catch (Exception ex)
            {
                OnProcessException(new InvalidOperationException("Failed to lookup remote end-point connection information for client data transmission due to exception: " + ex.Message, ex));
            }

            if (string.IsNullOrEmpty(connectionID))
                connectionID = "unavailable";

            return connectionID;
        }

        private void LookupHostName(object state)
        {
            try
            {
                Tuple<Guid, string, IPEndPoint> parameters = (Tuple<Guid, string, IPEndPoint>)state;
                IPHostEntry ipHost = Dns.GetHostEntry(parameters.Item3.Address);

                if (!string.IsNullOrWhiteSpace(ipHost.HostName))
                    m_connectionIDCache[parameters.Item1] = ipHost.HostName + " (" + parameters.Item2 + ")";
            }
            catch
            {
                // Just ignoring possible DNS lookup failures...
            }
        }

        /// <summary>
        /// Handles incoming commands from devices connected over the command channel.
        /// </summary>
        /// <param name="clientID">Guid of client that sent the command.</param>
        /// <param name="connectionID">Remote client connection identification (i.e., IP:Port).</param>
        /// <param name="commandBuffer">Data buffer received from connected client device.</param>
        /// <param name="length">Valid length of data within the buffer.</param>
        protected virtual void DeviceCommandHandler(Guid clientID, string connectionID, byte[] commandBuffer, int length)
        {
            try
            {
                ICommandFrame commandFrame = m_frameParser.PhasorProtocol switch
                {
                    PhasorProtocol.IEEEC37_118V2 or PhasorProtocol.IEEEC37_118V1 or PhasorProtocol.IEEEC37_118D6 => new CommandFrame(commandBuffer, 0, length),
                    PhasorProtocol.IEEE1344 => new GSF.PhasorProtocols.IEEE1344.CommandFrame(commandBuffer, 0, length),
                    PhasorProtocol.SelFastMessage => new GSF.PhasorProtocols.SelFastMessage.CommandFrame(commandBuffer, 0, length),
                    PhasorProtocol.IEC61850_90_5 => new GSF.PhasorProtocols.IEC61850_90_5.CommandFrame(commandBuffer, 0, length),
                    _ => throw new ArgumentOutOfRangeException($"Protocol \"{m_frameParser.PhasorProtocol}\" does not support commands."),
                };

                switch (commandFrame.Command)
                {
                    case DeviceCommand.SendConfigurationFrame1:
                    case DeviceCommand.SendConfigurationFrame2:
                        // Reset received configuration frame counter
                        m_receivedConfigurationFrames = 0;

                        if (m_configurationFrame is not null)
                        {
                            if (m_publishChannel is not null)
                                m_publishChannel.SendToAsync(clientID, m_configurationFrame.BinaryImage(), 0, m_configurationFrame.BinaryLength);
                            else
                                m_clientBasedPublishChannel?.SendAsync(m_configurationFrame.BinaryImage(), 0, m_configurationFrame.BinaryLength);

                            OnStatusMessage("Received request for \"{0}\" from \"{1}\" - frame was returned.", commandFrame.Command, connectionID);
                        }
                        break;
                    case DeviceCommand.SendConfigurationFrame3:
                        // Reset received configuration frame counter
                        m_receivedConfigurationFrames = 0;

                        if (m_configurationFrame3 is not null)
                        {
                            void publishFrame(byte[] frameImage)
                            {
                                if (m_publishChannel is not null)
                                    m_publishChannel.SendToAsync(clientID, frameImage, 0, frameImage.Length);
                                else
                                    m_clientBasedPublishChannel?.SendAsync(frameImage, 0, frameImage.Length);
                            }

                            foreach (byte[] frame in m_configurationFrame3.BinaryImageFrames)
                                publishFrame(frame);

                            OnStatusMessage("Received request for \"{0}\" from \"{1}\" - frame was returned.", commandFrame.Command, connectionID);
                        }
                        break;
                    case DeviceCommand.EnableRealTimeData:
                    case DeviceCommand.DisableRealTimeData:
                        // We ignore these commands without message, these commands are normally sent by synchrophasor devices,
                        // but we do not allow stream control in a proxy situation
                        break;
                    default:
                        OnStatusMessage("Request for \"{0}\" from \"{1}\" was ignored - device command is unsupported by stream splitter.", commandFrame.Command, connectionID);
                        break;
                }
            }
            catch (Exception ex)
            {
                OnProcessException(new InvalidOperationException($"Remotely connected device \"{connectionID}\" sent an unrecognized data sequence to the concentrator, no action was taken. Exception details: {ex.Message}", ex));
            }
        }

        // Thread procedure used to proxy data to the user implemented device command handler
        private void DeviceCommandHandlerProc(object state)
        {
            if (state is EventArgs<Guid, byte[], int> e)
                DeviceCommandHandler(e.Argument1, GetConnectionID(m_publishChannel, e.Argument1), e.Argument2, e.Argument3);
        }

        private void m_dataStreamMonitor_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (m_bytesReceived == 0 && (m_frameParser.DeviceSupportsCommands || m_frameParser.ConnectionIsMulticast || m_frameParser.ConnectionIsListener))
            {
                // If we've received no data in the last time-span, we restart connect cycle...
                m_dataStreamMonitor.Enabled = false;
                OnStatusMessage("\r\nNo data received in {0} seconds, restarting connect cycle...\r\n", (m_dataStreamMonitor.Interval / 1000.0D).ToString("0.0"));

                // Ask to move to next server, if available
                m_frameParser.RequestNextServerIndex();
                Start();
            }

            m_bytesReceived = 0;
        }

        private bool HandleException(Exception ex)
        {
            int i = 0;

            while (i < 5)
            {
                if (ex is SocketException socketEx)
                {
                    if (m_lastSocketErrorNumber == socketEx.ErrorCode)
                    {
                        // Suppress exception if we are within reporting interval
                        if (DateTime.UtcNow.Ticks - m_lastSocketErrorTime <= Ticks.FromSeconds(m_socketErrorReportingInterval))
                            return false;
                    }

                    m_lastSocketErrorNumber = socketEx.ErrorCode;
                    m_lastSocketErrorTime = DateTime.UtcNow.Ticks;
                }
                else if (ex.InnerException is not null)
                {
                    ex = ex.InnerException;
                    i++;
                    continue;
                }

                return true;
            }

            return false;
        }

        #region [ Frame Parser Event Handlers ]

        // Redistribute received data.
        private void m_frameParser_ReceivedFrameBufferImage(object sender, EventArgs<FundamentalFrameType, byte[], int, int> e)
        {
            if (m_publishChannel is null && m_clientBasedPublishChannel is null)
                return;

            byte[] image;

            // Track total number of received configuration frames - data source could be auto-sending config frames every minute
            if (e.Argument1 == FundamentalFrameType.ConfigurationFrame)
                m_receivedConfigurationFrames++;

            // As soon as we start receiving data frames and a config frame exists, we change proxy connection status to "Connected" instead of "ConnectedNoData"
            if (StreamProxyStatus.ConnectionState == ConnectionState.ConnectedNoData && e.Argument1 == FundamentalFrameType.DataFrame && m_configurationFrame is not null)
            {
                if (m_clientBasedPublishChannel is not null)
                {
                    if (m_clientBasedPublishChannel.CurrentState == ClientState.Connected)
                        StreamProxyStatus.ConnectionState = ConnectionState.Connected;
                }
                else
                {
                    StreamProxyStatus.ConnectionState = ConnectionState.Connected;
                }
            }

            // Send the configuration frame at the top of each minute if publication channel is UDP and source is not auto-sending configuration frames
            if (m_receivedConfigurationFrames < 2 && m_publishChannel is UdpServer && m_configurationFrame is not null)
            {
                DateTime currentTime = DateTime.UtcNow;

                if (currentTime.Second == 0)
                {
                    if (currentTime.Minute != m_lastConfigurationPublishMinute)
                    {
                        m_lastConfigurationPublishMinute = currentTime.Minute;
                        m_configurationFramePublished = false;
                    }

                    if (!m_configurationFramePublished)
                    {
                        // Publish binary image for configuration frame
                        m_configurationFramePublished = true;
                        m_configurationFrame.Timestamp = currentTime;

                        image = m_configurationFrame.BinaryImage();

                        try
                        {
                            m_publishChannel.MulticastAsync(image, 0, image.Length);
                        }
                        catch (Exception ex)
                        {
                            OnProcessException(new InvalidOperationException($"Server based publication channel exception during proxy output: {ex.Message}", ex));
                        }

                        TotalBytesSent += image.Length;

                        // Sleep for a moment between config frame and data frame transmissions
                        Thread.Sleep(1);
                    }
                }
            }

            // Publish binary image for frame 
            image = e.Argument2;
            int offset = e.Argument3;
            int length = e.Argument4;

            // We republish exactly what we receive to the current destinations
            if (m_publishChannel is not null)
            {
                try
                {
                    m_publishChannel.MulticastAsync(image, offset, length);
                }
                catch (Exception ex)
                {
                    OnProcessException(new InvalidOperationException($"Server based publication channel exception during proxy output: {ex.Message}", ex));
                }
            }
            else if (m_clientBasedPublishChannel is not null && m_clientBasedPublishChannel.CurrentState == ClientState.Connected)
            {
                try
                {
                    m_clientBasedPublishChannel.SendAsync(image, offset, length);
                }
                catch (Exception ex)
                {
                    OnProcessException(new InvalidOperationException($"TCP client based publication channel exception during proxy output: {ex.Message}", ex));
                }
            }

            // We track bytes received so that connection can be restarted if data is not flowing
            m_bytesReceived += length;
            TotalBytesSent += length;
        }

        private void m_frameParser_ReceivedConfigurationFrame(object sender, EventArgs<IConfigurationFrame> e)
        {
            // Cache latest configuration frame that was received
            if (e.Argument is ConfigurationFrame3 configFrame3)
            {
                m_configurationFrame3 = configFrame3;
                OnStatusMessage("Received configuration frame 3 at {0:yyyy-MM-dd HH:mm:ss.fff}", DateTime.UtcNow);
            }
            else
            {
                m_configurationFrame = e.Argument;
                OnStatusMessage("Received configuration frame at {0:yyyy-MM-dd HH:mm:ss.fff}", DateTime.UtcNow);
            }
        }

        private void m_frameParser_ParsingException(object sender, EventArgs<Exception> e)
        {
            OnProcessException(e.Argument);
        }

        private void m_frameParser_ExceededParsingExceptionThreshold(object sender, EventArgs e)
        {
            OnStatusMessage("Connection is being reset due to an excessive number of exceptions...\r\n");

            if (Enabled)
                Start();
        }

        private void m_frameParser_ConnectionException(object sender, EventArgs<Exception, int> e)
        {
            if (HandleException(e.Argument1))
                OnProcessException(new InvalidOperationException($"Connection attempt failed for {m_connectionInfo}: {e.Argument1.Message}", e.Argument1));

            StreamProxyStatus.ConnectionState = ConnectionState.Disconnected;
        }

        private void m_frameParser_ConnectionEstablished(object sender, EventArgs e)
        {
            OnStatusMessage($"Successfully connected to {m_connectionInfo}, initializing {m_frameParser.PhasorProtocol.GetFormattedProtocolName()} protocol...");
            StreamProxyStatus.ConnectionState = ConnectionState.ConnectedNoData;

            // Enable data stream monitor for connections that support commands
            if (m_dataStreamMonitor is not null)
                m_dataStreamMonitor.Enabled = m_frameParser.DeviceSupportsCommands;

            // Reinitialize proxy connection if needed...
            if (m_enabled && m_clientBasedPublishChannel is not null && m_clientBasedPublishChannel.CurrentState != ClientState.Connected)
            {
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    m_clientBasedPublishChannel.Disconnect();
                    m_clientBasedPublishChannel.ConnectAsync();
                });
            }
        }

        private void m_frameParser_ConnectionAttempt(object sender, EventArgs e)
        {
            // Cache connection info before possible failure in case connection switches to another target
            m_connectionInfo = ConnectionInfo;

            OnStatusMessage($"Attempting {m_frameParser.PhasorProtocol.GetFormattedProtocolName()} protocol connection to {m_connectionInfo}...");
            StreamProxyStatus.ConnectionState = ConnectionState.Disconnected;
        }

        private void m_frameParser_ConnectionTerminated(object sender, EventArgs e)
        {
            OnStatusMessage($"{m_frameParser.PhasorProtocol.GetFormattedProtocolName()} protocol connection to {m_connectionInfo} terminated.");
            StreamProxyStatus.ConnectionState = ConnectionState.Disconnected;

            // Reset proxy connection
            if (m_enabled)
                new Action(Start).DelayAndExecute(1000);
        }

        private void m_frameParser_ConfigurationChanged(object sender, EventArgs e)
        {
            OnStatusMessage("NOTICE: Configuration has changed, requesting new configuration frame...");

            // Reset data stream monitor to allow time for non-cached reception of new configuration frame...
            if (m_dataStreamMonitor is not null && m_dataStreamMonitor.Enabled)
            {
                m_dataStreamMonitor.Stop();
                m_dataStreamMonitor.Start();
            }

            // Reset received configuration frame counter
            m_receivedConfigurationFrames = 0;

            SendCommand(DeviceCommand.SendConfigurationFrame2);
            SendCommand(DeviceCommand.SendConfigurationFrame3);
        }

        private void m_frameParser_ServerStarted(object sender, EventArgs e)
        {
            OnStatusMessage("TCP server based data channel listener started.");
            StreamProxyStatus.ConnectionState = ConnectionState.Disconnected;
        }

        private void m_frameParser_ServerStopped(object sender, EventArgs e)
        {
            OnStatusMessage("TCP server based data channel listener stopped.");
            StreamProxyStatus.ConnectionState = ConnectionState.Disabled;
        }

        private void m_frameParser_ServerIndexUpdated(object sender, EventArgs e)
        {
            m_frameParser.DeviceID = AccessID;
        }

        #endregion

        #region [ UDP Publish Channel Event Handlers ]

        private void udpPublishChannel_ClientConnectingException(object sender, EventArgs<Exception> e)
        {
            Exception ex = e.Argument;

            if (HandleException(ex))
                OnProcessException(new InvalidOperationException($"Exception occurred while client attempting to connect to UDP publication channel: {ex.Message}", ex));
        }

        private void udpPublishChannel_ReceiveClientDataComplete(object sender, EventArgs<Guid, byte[], int> e)
        {
            // Queue up device command handling on a different thread since this will often
            // engage sending data back on the same command channel, and we want this async
            // thread to complete gracefully...
            if (m_publishChannel is null)
                ThreadPool.QueueUserWorkItem(DeviceCommandHandlerProc, e);
        }

        private void udpPublishChannel_SendClientDataException(object sender, EventArgs<Guid, Exception> e)
        {
            Exception ex = e.Argument2;

            if (!HandleException(ex))
                return;

            OnProcessException(ex is SocketException ?
                new InvalidOperationException($"Socket exception occurred on the UDP publication channel while attempting to send client data to \"{GetConnectionID(m_publishChannel, e.Argument1)}\": {ex.Message}", ex) :
                new InvalidOperationException($"UDP publication channel exception occurred while sending client data to \"{GetConnectionID(m_publishChannel, e.Argument1)}\": {ex.Message}", ex));
        }

        private void udpPublishChannel_ServerStarted(object sender, EventArgs e)
        {
            OnStatusMessage("UDP publication channel started.");
        }

        private void udpPublishChannel_ServerStopped(object sender, EventArgs e)
        {
            OnStatusMessage("UDP publication channel stopped.");
        }

        #endregion

        #region [ TCP Publish Channel Event Handlers ]

        private void tcpPublishChannel_ClientConnected(object sender, EventArgs<Guid> e)
        {
            OnStatusMessage("Client \"{0}\" connected to TCP publication channel.", GetConnectionID(m_publishChannel, e.Argument));
        }

        private void tcpPublishChannel_ClientDisconnected(object sender, EventArgs<Guid> e)
        {
            Guid clientID = e.Argument;
            OnStatusMessage("Client \"{0}\" disconnected from TCP publication channel.", GetConnectionID(m_publishChannel, clientID));
            m_connectionIDCache.TryRemove(clientID, out _);
        }

        private void tcpPublishChannel_ClientConnectingException(object sender, EventArgs<Exception> e)
        {
            Exception ex = e.Argument;

            if (HandleException(ex))
                OnProcessException(new InvalidOperationException($"Socket exception occurred while client was attempting to connect to TCP publication channel: {ex.Message}", ex));
        }

        private void tcpPublishChannel_ReceiveClientDataComplete(object sender, EventArgs<Guid, byte[], int> e)
        {
            // Queue up derived class device command handling on a different thread
            ThreadPool.QueueUserWorkItem(DeviceCommandHandlerProc, e);
        }

        private void tcpPublishChannel_SendClientDataException(object sender, EventArgs<Guid, Exception> e)
        {
            Exception ex = e.Argument2;

            if (!HandleException(ex))
                return;

            OnProcessException(ex is SocketException ?
                new InvalidOperationException($"Socket exception occurred on the TCP publication channel while attempting to send client data to \"{GetConnectionID(m_publishChannel, e.Argument1)}\": {ex.Message}", ex) :
                new InvalidOperationException($"TCP publication channel exception occurred while sending client data to \"{GetConnectionID(m_publishChannel, e.Argument1)}\": {ex.Message}", ex));
        }

        private void tcpPublishChannel_ServerStarted(object sender, EventArgs e)
        {
            OnStatusMessage("TCP publication channel started.");
        }

        private void tcpPublishChannel_ServerStopped(object sender, EventArgs e)
        {
            OnStatusMessage("TCP publication channel stopped.");
        }

        #endregion

        #region [ TCP Client Based Publish Channel Event Handlers ]

        private void tcpClientBasedPublishChannel_ConnectionEstablished(object sender, EventArgs e)
        {
            OnStatusMessage("TCP publishing client connected to TCP listening server channel \"{0}\".", m_clientBasedPublishChannel.ServerUri);
        }

        private void tcpClientBasedPublishChannel_ConnectionTerminated(object sender, EventArgs e)
        {
            OnStatusMessage("TCP publishing client disconnected from TCP listening server channel \"{0}\".", m_clientBasedPublishChannel.ServerUri);

            // Reinitialize client connection if it was just disconnected...
            if (!m_enabled || !m_frameParser.IsConnected)
                return;

            StreamProxyStatus.ConnectionState = ConnectionState.ConnectedNoData;

            new Action(() =>
            {
                if (m_clientBasedPublishChannel.CurrentState == ClientState.Disconnected)
                    m_clientBasedPublishChannel.ConnectAsync();
            })
            .DelayAndExecute(1000);
        }

        private void tcpClientBasedPublishChannel_ConnectionException(object sender, EventArgs<Exception> e)
        {
            Exception ex = e.Argument;

            if (HandleException(ex))
                OnProcessException(new InvalidOperationException($"Socket exception occurred while TCP publishing client was attempting to connect to TCP listening server channel \"{m_clientBasedPublishChannel.ServerUri}\": {ex.Message}", ex));
        }

        private void tcpClientBasedPublishChannel_ReceiveDataComplete(object sender, EventArgs<byte[], int> e)
        {
            // Queue up derived class device command handling on a different thread
            ThreadPool.QueueUserWorkItem(DeviceCommandHandlerProc, new EventArgs<Guid, byte[], int>(Guid.Empty, e.Argument1, e.Argument2));
        }

        private void tcpClientBasedPublishChannel_SendDataException(object sender, EventArgs<Exception> e)
        {
            Exception ex = e.Argument;

            if (!HandleException(ex))
                return;

            OnProcessException(ex is SocketException ?
                new InvalidOperationException($"Socket exception occurred on the TCP publication client channel while attempting to send client data to TCP listening server \"{m_clientBasedPublishChannel.ServerUri}\": {ex.Message}", ex) :
                new InvalidOperationException($"TCP publication client channel exception occurred while sending client data to TCP listening server \"{m_clientBasedPublishChannel.ServerUri}\": {ex.Message}", ex));
        }

        #endregion

        #endregion
    }
}
