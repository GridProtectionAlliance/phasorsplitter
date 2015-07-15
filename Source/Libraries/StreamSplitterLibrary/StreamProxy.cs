//******************************************************************************************************
//  StreamSplitter.cs - Gbtc
//
//  Copyright © 2010, Grid Protection Alliance.  All Rights Reserved.
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
using GSF.Parsing;
using GSF.PhasorProtocols;
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
        private string m_sourceSettings;
        private string m_proxySettings;
        private MultiProtocolFrameParser m_frameParser;
        private IServer m_publishChannel;
        private TcpClient m_clientBasedPublishChannel;
        private Timer m_dataStreamMonitor;
        private volatile IConfigurationFrame m_configurationFrame;
        private int m_lastConfigurationPublishMinute;
        private bool m_configurationFramePublished;
        private long m_receivedConfigurationFrames;
        private readonly StreamProxyStatus m_streamProxyStatus;
        private readonly ConcurrentDictionary<Guid, string> m_connectionIDCache;
        private long m_startTime;
        private long m_stopTime;
        private long m_bytesReceived;
        private long m_totalBytesSent;
        private volatile bool m_enabled;
        private bool m_disposed;

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

            m_frameParser.ConnectionAttempt += m_frameParser_ConnectionAttempt;
            m_frameParser.ConnectionEstablished += m_frameParser_ConnectionEstablished;
            m_frameParser.ConnectionException += m_frameParser_ConnectionException;
            m_frameParser.ConnectionTerminated += m_frameParser_ConnectionTerminated;
            m_frameParser.ConfigurationChanged += m_frameParser_ConfigurationChanged;
            m_frameParser.ParsingException += m_frameParser_ParsingException;
            m_frameParser.ReceivedConfigurationFrame += m_frameParser_ReceivedConfigurationFrame;
            m_frameParser.ReceivedFrameBufferImage += m_frameParser_ReceivedFrameBufferImage;

            // Create data stream monitoring timer
            m_dataStreamMonitor = new Timer();
            m_dataStreamMonitor.Elapsed += m_dataStreamMonitor_Elapsed;
            m_dataStreamMonitor.Interval = 10000.0D;
            m_dataStreamMonitor.AutoReset = true;
            m_dataStreamMonitor.Enabled = false;

            // Create a new connection ID cache
            m_connectionIDCache = new ConcurrentDictionary<Guid, string>();

            // Set stream proxy ID
            m_id = proxyConnection.ID;

            // Maintain a current status for the stream proxy
            m_streamProxyStatus = new StreamProxyStatus(m_id);

            // Initialize stream splitter based on new proxy connection settings
            ProxyConnection = proxyConnection;
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
                if ((object)value == null)
                    throw new ArgumentNullException("value", "Cannot update StreamSplitter configuration: provided ProxyConnection is null.");

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
        public StreamProxyStatus StreamProxyStatus
        {
            get
            {
                return m_streamProxyStatus;
            }
        }

        /// <summary>
        /// Gets or sets name associated with the <see cref="StreamProxy"/>.
        /// </summary>
        public string Name
        {
            get
            {
                return m_name.ToNonNullString("<undefined>");
            }
            set
            {
                m_name = value;
            }
        }

        /// <summary>
        /// Gets or sets ID of the <see cref="StreamProxy"/>.
        /// </summary>
        public Guid ID
        {
            get
            {
                return m_id;
            }
            set
            {
                m_id = value;
            }
        }

        /// <summary>
        /// Gets or sets connection string used for <see cref="MultiProtocolFrameParser"/> data source.
        /// </summary>
        public string SourceSettings
        {
            get
            {
                return m_sourceSettings;
            }
            set
            {
                m_sourceSettings = value;
            }
        }

        /// <summary>
        /// Gets or sets connection string for TCP or UCP socket instance used to proxy synchrophasor frames.
        /// </summary>
        public string ProxySettings
        {
            get
            {
                return m_proxySettings;
            }
            set
            {
                m_proxySettings = value;
            }
        }

        /// <summary>
        /// Gets or sets the current enabled state of <see cref="StreamProxy"/>.
        /// </summary>
        /// <returns>Current enabled state of <see cref="StreamProxy"/>.</returns>
        /// <remarks>
        /// Splitter must be started by calling <see cref="Start"/> method or setting
        /// <c><see cref="Enabled"/> = true</c>) before data splitting will begin.
        /// </remarks>
        public virtual bool Enabled
        {
            get
            {
                return m_enabled;
            }
            set
            {
                if (value)
                    Start();
                else
                    Stop();
            }
        }

        /// <summary>
        /// Gets the total number of bytes sent to clients of this <see cref="StreamProxy"/>.
        /// </summary>
        public long TotalBytesSent
        {
            get
            {
                return m_totalBytesSent;
            }
        }

        /// <summary>
        /// Gets the UTC time the <see cref="StreamProxy"/> was started.
        /// </summary>
        public Ticks StartTime
        {
            get
            {
                return m_startTime;
            }
        }

        /// <summary>
        /// Gets the UTC time the <see cref="StreamProxy"/> was stopped.
        /// </summary>
        public Ticks StopTime
        {
            get
            {
                return m_stopTime;
            }
        }

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
                StringBuilder status = new StringBuilder();

                status.AppendFormat("        Stream Splitter ID: {0}", ID);
                status.AppendLine();
                status.AppendFormat("    Total process run time: {0}", RunTime);
                status.AppendLine();
                status.AppendFormat("          Total bytes sent: {0}", m_totalBytesSent);
                status.AppendLine();

                if ((object)m_dataStreamMonitor != null)
                {
                    status.AppendFormat("No data reconnect interval: {0} seconds", Ticks.FromMilliseconds(m_dataStreamMonitor.Interval).ToSeconds().ToString("0.000"));
                    status.AppendLine();
                }

                if ((object)m_configurationFrame != null)
                {
                    status.AppendFormat("  Configuration frame size: {0} bytes", m_configurationFrame.BinaryLength);
                    status.AppendLine();
                }

                if ((object)m_frameParser != null)
                    status.Append(m_frameParser.Status);

                if ((object)m_publishChannel != null)
                {
                    status.AppendLine();
                    status.AppendLine("Publication Channel Status".CenterText(50));
                    status.AppendLine("--------------------------".CenterText(50));
                    status.Append(m_publishChannel.Status);

                    TcpServer tcpPublishChannel = m_publishChannel as TcpServer;

                    if ((object)tcpPublishChannel != null)
                    {
                        Guid[] clientIDs = tcpPublishChannel.ClientIDs;

                        if (clientIDs != null && clientIDs.Length > 0)
                        {
                            status.AppendLine();
                            status.AppendFormat("TCP publish channel has {0} connected clients:\r\n\r\n", clientIDs.Length);

                            for (int i = 0; i < clientIDs.Length; i++)
                            {
                                status.AppendFormat("    {0}) {1}\r\n", i + 1, GetConnectionID(tcpPublishChannel, clientIDs[i]));
                            }

                            status.AppendLine();
                        }
                    }
                }

                if ((object)m_clientBasedPublishChannel != null)
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
            get
            {
                return m_publishChannel as UdpServer;
            }
            set
            {
                UdpServer udpPublishChannel = m_publishChannel as UdpServer;

                if ((object)m_publishChannel != null && (object)udpPublishChannel == null)
                {
                    // Trying to dispose non-UDP publication channel - nothing to do...
                    if ((object)value == null)
                        return;

                    // Publish channel is currently TCP, detach from TCP events
                    TcpPublishChannel = null;
                }

                if ((object)udpPublishChannel != null)
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

                if ((object)udpPublishChannel != null)
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
            get
            {
                return m_publishChannel as TcpServer;
            }
            set
            {
                TcpServer tcpPublishChannel = m_publishChannel as TcpServer;

                if ((object)m_publishChannel != null && (object)tcpPublishChannel == null)
                {
                    // Trying to dispose non-TCP publication channel - nothing to do...
                    if ((object)value == null)
                        return;

                    // Publish channel is currently UDP, detach from UDP events
                    UdpPublishChannel = null;
                }

                if ((object)tcpPublishChannel != null)
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

                if ((object)tcpPublishChannel != null)
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
            get
            {
                return m_clientBasedPublishChannel;
            }
            set
            {
                if ((object)m_clientBasedPublishChannel != null)
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

                if ((object)m_clientBasedPublishChannel != null)
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
            if (!m_disposed)
            {
                try
                {
                    if (disposing)
                    {
                        TcpPublishChannel = null;
                        UdpPublishChannel = null;
                        TcpClientPublishChannel = null;

                        if ((object)m_frameParser != null)
                        {
                            m_frameParser.ConnectionAttempt -= m_frameParser_ConnectionAttempt;
                            m_frameParser.ConnectionEstablished -= m_frameParser_ConnectionEstablished;
                            m_frameParser.ConnectionException -= m_frameParser_ConnectionException;
                            m_frameParser.ConnectionTerminated -= m_frameParser_ConnectionTerminated;
                            m_frameParser.ConfigurationChanged -= m_frameParser_ConfigurationChanged;
                            m_frameParser.ParsingException -= m_frameParser_ParsingException;
                            m_frameParser.ReceivedConfigurationFrame -= m_frameParser_ReceivedConfigurationFrame;
                            m_frameParser.ReceivedFrameBufferImage -= m_frameParser_ReceivedFrameBufferImage;
                            m_frameParser.Dispose();
                            m_frameParser = null;
                        }

                        if ((object)m_dataStreamMonitor != null)
                        {
                            m_dataStreamMonitor.Elapsed -= m_dataStreamMonitor_Elapsed;
                            m_dataStreamMonitor.Dispose();
                            m_dataStreamMonitor = null;
                        }
                    }
                }
                finally
                {
                    m_disposed = true;  // Prevent duplicate dispose.

                    if ((object)Disposed != null)
                        Disposed(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Initializes the <see cref="StreamProxy"/>.
        /// </summary>
        public void Initialize()
        {
            // Update connection information for the multi-protocol frame parser
            m_frameParser.ConnectionString = SourceSettings;

            Dictionary<string, string> sourceSettings = m_frameParser.ConnectionString.ParseKeyValuePairs();
            Dictionary<string, string> proxySettings = ProxySettings.ParseKeyValuePairs();
            string setting;

            // TODO: These should be optionally picked up from connection string inside of MPFP

            // Apply other settings as needed
            if (sourceSettings.TryGetValue("accessID", out setting))
                m_frameParser.DeviceID = ushort.Parse(setting);

            if (sourceSettings.TryGetValue("simulateTimestamp", out setting))
                m_frameParser.InjectSimulatedTimestamp = setting.ParseBoolean();

            if (sourceSettings.TryGetValue("allowedParsingExceptions", out setting))
                m_frameParser.AllowedParsingExceptions = int.Parse(setting);

            if (sourceSettings.TryGetValue("parsingExceptionWindow", out setting))
                m_frameParser.ParsingExceptionWindow = Ticks.FromSeconds(double.Parse(setting));

            if (sourceSettings.TryGetValue("skipDisableRealTimeData", out setting))
                m_frameParser.SkipDisableRealTimeData = setting.ParseBoolean();

            if (proxySettings.TryGetValue("useClientPublishChannel", out setting) && setting.ParseBoolean())
            {
                // Create a new client based publication channel (for reverse TCP connections)
                TcpClientPublishChannel = ClientBase.Create(ProxySettings) as TcpClient;

                if (m_clientBasedPublishChannel != null)
                    m_clientBasedPublishChannel.MaxConnectionAttempts = -1;
            }
            else
            {
                // Create a new server based publication channel
                IServer publicationServer = ServerBase.Create(ProxySettings);
                TcpPublishChannel = publicationServer as TcpServer;
                UdpPublishChannel = publicationServer as UdpServer;
            }
        }

        /// <summary>
        /// Starts the <see cref="StreamProxy"/>, if it is not already running.
        /// </summary>
        public void Start()
        {
            // Make sure we are stopped before attempting to start
            if (Enabled)
                Stop();

            // Start publication server
            if ((object)m_publishChannel != null)
                m_publishChannel.Start();

            if ((object)m_clientBasedPublishChannel != null)
                m_clientBasedPublishChannel.ConnectAsync();

            // Start multi-protocol frame parser
            if ((object)m_frameParser != null)
                m_frameParser.Start();

            if (!m_enabled)
            {
                m_stopTime = 0;
                m_startTime = DateTime.UtcNow.Ticks;

                // Start real-time frame publication
                m_enabled = true;
            }
        }

        /// <summary>
        /// Stops the <see cref="StreamProxy"/>.
        /// </summary>
        public void Stop()
        {
            m_streamProxyStatus.ConnectionState = ConnectionState.Disabled;

            // Stop data stream monitor
            if ((object)m_dataStreamMonitor != null)
                m_dataStreamMonitor.Enabled = false;

            if (m_enabled)
            {
                m_enabled = false;
                m_stopTime = DateTime.UtcNow.Ticks;
            }

            // Stop multi-protocol frame parser
            if ((object)m_frameParser != null)
                m_frameParser.Stop();

            // Stop publication server
            if ((object)m_publishChannel != null)
                m_publishChannel.Stop();

            if ((object)m_clientBasedPublishChannel != null)
                m_clientBasedPublishChannel.Disconnect();

            m_bytesReceived = 0;
            m_receivedConfigurationFrames = 0;
        }

        /// <summary>
        /// Sends the specified <see cref="DeviceCommand"/> to the current device connection.
        /// </summary>
        /// <param name="command"><see cref="DeviceCommand"/> to send to connected device.</param>
        public void SendCommand(DeviceCommand command)
        {
            if ((object)m_frameParser != null)
            {
                if ((object)m_frameParser.SendDeviceCommand(command) != null)
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
                m_streamProxyStatus.AppendStatusMessage(status);

                if ((object)StatusMessage != null)
                    StatusMessage(this, new EventArgs<string>(status));
            }
            catch (Exception ex)
            {
                // We protect our code from consumer thrown exceptions
                OnProcessException(new InvalidOperationException(string.Format("Exception in consumer handler for StatusMessage event: {0}", ex.Message), ex));
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

                m_streamProxyStatus.AppendStatusMessage(status);

                if ((object)StatusMessage != null)
                    StatusMessage(this, new EventArgs<string>(status));
            }
            catch (Exception ex)
            {
                // We protect our code from consumer thrown exceptions
                OnProcessException(new InvalidOperationException(string.Format("Exception in consumer handler for StatusMessage event: {0}", ex.Message), ex));
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
            m_streamProxyStatus.AppendStatusMessage("ERROR: " + processException.Message);

            if ((object)ProcessException != null)
                ProcessException(this, new EventArgs<Exception>(processException));
        }

        /// <summary>
        /// Gets connection ID (i.e., IP:Port) for specified <paramref name="clientID"/>.
        /// </summary>
        /// <param name="server">Server connection of associated <paramref name="clientID"/>.</param>
        /// <param name="clientID">Guid of client for ID lookup.</param>
        /// <returns>Connection ID (i.e., IP:Port) for specified <paramref name="clientID"/>.</returns>
        protected virtual string GetConnectionID(IServer server, Guid clientID)
        {
            string connectionID;

            if (((object)server == null || clientID.Equals(Guid.Empty)) && (object)m_clientBasedPublishChannel != null)
                return m_clientBasedPublishChannel.ServerUri;

            if (!m_connectionIDCache.TryGetValue(clientID, out connectionID))
            {
                // Attempt to lookup remote connection identification for logging purposes
                try
                {
                    IPEndPoint remoteEndPoint = null;
                    TcpServer commandChannel = server as TcpServer;
                    TransportProvider<Socket> tcpClient;
                    TransportProvider<EndPoint> udpClient;

                    if ((object)commandChannel != null)
                    {
                        if (commandChannel.TryGetClient(clientID, out tcpClient))
                            remoteEndPoint = tcpClient.Provider.RemoteEndPoint as IPEndPoint;
                    }
                    else
                    {
                        UdpServer dataChannel = server as UdpServer;

                        if ((object)dataChannel != null && dataChannel.TryGetClient(clientID, out udpClient))
                            remoteEndPoint = udpClient.Provider as IPEndPoint;
                    }

                    if ((object)remoteEndPoint != null)
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
            }

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
                ICommandFrame commandFrame;

                // Attempt to interpret data received from a client as a command frame
                switch (m_frameParser.PhasorProtocol)
                {
                    case PhasorProtocol.IEEEC37_118V2:
                    case PhasorProtocol.IEEEC37_118V1:
                    case PhasorProtocol.IEEEC37_118D6:
                        commandFrame = new GSF.PhasorProtocols.IEEEC37_118.CommandFrame(commandBuffer, 0, length);
                        break;
                    case PhasorProtocol.IEEE1344:
                        commandFrame = new GSF.PhasorProtocols.IEEE1344.CommandFrame(commandBuffer, 0, length);
                        break;
                    case PhasorProtocol.SelFastMessage:
                        commandFrame = new GSF.PhasorProtocols.SelFastMessage.CommandFrame(commandBuffer, 0, length);
                        break;
                    case PhasorProtocol.IEC61850_90_5:
                        commandFrame = new GSF.PhasorProtocols.IEC61850_90_5.CommandFrame(commandBuffer, 0, length);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(string.Format("Protocol \"{0}\" does not support commands.", m_frameParser.PhasorProtocol));
                }

                switch (commandFrame.Command)
                {
                    case DeviceCommand.SendConfigurationFrame1:
                    case DeviceCommand.SendConfigurationFrame2:
                        // Reset received configuration frame counter
                        m_receivedConfigurationFrames = 0;

                        if ((object)m_configurationFrame != null)
                        {
                            if ((object)m_publishChannel != null)
                                m_publishChannel.SendToAsync(clientID, m_configurationFrame.BinaryImage(), 0, m_configurationFrame.BinaryLength);
                            else if ((object)m_clientBasedPublishChannel != null)
                                m_clientBasedPublishChannel.SendAsync(m_configurationFrame.BinaryImage(), 0, m_configurationFrame.BinaryLength);

                            OnStatusMessage("Received request for \"{0}\" from \"{1}\" - frame was returned.", commandFrame.Command, connectionID);
                        }

                        break;
                    case DeviceCommand.EnableRealTimeData:
                    case DeviceCommand.DisableRealTimeData:
                        // We ignore these commands without message, these commands are normally sent by synchrophasor devices
                        // but we do not allow stream control in a proxy situation
                        break;
                    default:
                        OnStatusMessage("Request for \"{0}\" from \"{1}\" was ignored - device command is unsupported by stream splitter.", commandFrame.Command, connectionID);
                        break;
                }
            }
            catch (Exception ex)
            {
                OnProcessException(new InvalidOperationException(string.Format("Remotely connected device \"{0}\" sent an unrecognized data sequence to the concentrator, no action was taken. Exception details: {1}", connectionID, ex.Message), ex));
            }
        }

        // Thread procedure used to proxy data to the user implemented device command handler
        private void DeviceCommandHandlerProc(object state)
        {
            EventArgs<Guid, byte[], int> e = state as EventArgs<Guid, byte[], int>;

            if ((object)e != null)
                DeviceCommandHandler(e.Argument1, GetConnectionID(m_publishChannel, e.Argument1), e.Argument2, e.Argument3);
        }

        #region [ Frame Parser Event Handlers ]

        // Redistribute received data.
        private void m_frameParser_ReceivedFrameBufferImage(object sender, EventArgs<FundamentalFrameType, byte[], int, int> e)
        {
            if ((object)m_publishChannel == null && (object)m_clientBasedPublishChannel == null)
                return;

            byte[] image;
            int offset, length;

            // Track total number of received configuration frames - data source could be auto-sending config frames every minute
            if (e.Argument1 == FundamentalFrameType.ConfigurationFrame)
                m_receivedConfigurationFrames++;

            // As soon as we start receiving data frames and a config frame exists, we change proxy connection status to "Connected" instead of "ConnectedNoData"
            if (m_streamProxyStatus.ConnectionState == ConnectionState.ConnectedNoData && e.Argument1 == FundamentalFrameType.DataFrame && (object)m_configurationFrame != null)
                m_streamProxyStatus.ConnectionState = ConnectionState.Connected;

            // Send the configuration frame at the top of each minute if publication channel is UDP and source is not auto-sending configuration frames
            if (m_receivedConfigurationFrames < 2 && m_publishChannel is UdpServer && (object)m_configurationFrame != null)
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
                        m_publishChannel.MulticastAsync(image, 0, image.Length);
                        m_totalBytesSent += image.Length;

                        // Sleep for a moment between config frame and data frame transmissions
                        Thread.Sleep(1);
                    }
                }
            }

            // Publish binary image for frame 
            image = e.Argument2;
            offset = e.Argument3;
            length = e.Argument4;

            // We republish exactly what we receive to the current destinations
            if ((object)m_publishChannel != null)
                m_publishChannel.MulticastAsync(image, offset, length);
            else if ((object)m_clientBasedPublishChannel != null)
                m_clientBasedPublishChannel.SendAsync(image, offset, length);

            // We track bytes received so that connection can be restarted if data is not flowing
            m_bytesReceived += length;
            m_totalBytesSent += length;
        }

        private void m_frameParser_ReceivedConfigurationFrame(object sender, EventArgs<IConfigurationFrame> e)
        {
            // Cache latest configuration frame that was received
            m_configurationFrame = e.Argument;

            OnStatusMessage("Received configuration frame at {0:yyyy-MM-dd HH:mm:ss.fff}", DateTime.UtcNow);
        }

        private void m_frameParser_ParsingException(object sender, EventArgs<Exception> e)
        {
            OnProcessException(e.Argument);
        }

        private void m_frameParser_ConnectionException(object sender, EventArgs<Exception, int> e)
        {
            OnProcessException(new InvalidOperationException(string.Format("Connection attempt {0} failed due to exception: {1}", e.Argument2, e.Argument1.Message), e.Argument1));
            m_streamProxyStatus.ConnectionState = ConnectionState.Disconnected;
        }

        private void m_frameParser_ConnectionEstablished(object sender, EventArgs e)
        {
            OnStatusMessage("Initiating {0} {1} based connection...", m_frameParser.PhasorProtocol.GetFormattedProtocolName(), m_frameParser.TransportProtocol.ToString().ToUpper());
            m_streamProxyStatus.ConnectionState = ConnectionState.ConnectedNoData;

            // Enable data stream monitor for connections that support commands
            if ((object)m_dataStreamMonitor != null)
                m_dataStreamMonitor.Enabled = m_frameParser.DeviceSupportsCommands;
        }

        private void m_frameParser_ConnectionAttempt(object sender, EventArgs e)
        {
            OnStatusMessage("Attempting connection...");
            m_streamProxyStatus.ConnectionState = ConnectionState.Disconnected;
        }

        private void m_frameParser_ConnectionTerminated(object sender, EventArgs e)
        {
            OnStatusMessage("Connection terminated.");
            m_streamProxyStatus.ConnectionState = ConnectionState.Disconnected;
        }

        private void m_frameParser_ConfigurationChanged(object sender, EventArgs e)
        {
            OnStatusMessage("NOTICE: Configuration has changed, requesting new configuration frame...");

            // Reset data stream monitor to allow time for non-cached reception of new configuration frame...
            if ((object)m_dataStreamMonitor != null && m_dataStreamMonitor.Enabled)
            {
                m_dataStreamMonitor.Stop();
                m_dataStreamMonitor.Start();
            }

            // Reset received configuration frame counter
            m_receivedConfigurationFrames = 0;

            SendCommand(DeviceCommand.SendConfigurationFrame2);
        }

        private void m_dataStreamMonitor_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (m_bytesReceived == 0 && (m_frameParser.DeviceSupportsCommands || m_frameParser.ConnectionIsMulticast || m_frameParser.ConnectionIsListener))
            {
                // If we've received no data in the last time-span, we restart connect cycle...
                m_dataStreamMonitor.Enabled = false;
                OnStatusMessage("\r\nNo data received in {0} seconds, restarting connect cycle...\r\n", (m_dataStreamMonitor.Interval / 1000.0D).ToString("0.0"));
                Start();
            }

            m_bytesReceived = 0;
        }

        #endregion

        #region [ UDP Publish Channel Event Handlers ]

        private void udpPublishChannel_ClientConnectingException(object sender, EventArgs<Exception> e)
        {
            Exception ex = e.Argument;
            OnProcessException(new InvalidOperationException(string.Format("Exception occurred while client attempting to connect to UDP publication channel: {0}", ex.Message), ex));
        }

        private void udpPublishChannel_ReceiveClientDataComplete(object sender, EventArgs<Guid, byte[], int> e)
        {
            // Queue up device command handling on a different thread since this will often
            // engage sending data back on the same command channel and we want this async
            // thread to complete gracefully...
            if ((object)m_publishChannel == null)
                ThreadPool.QueueUserWorkItem(DeviceCommandHandlerProc, e);
        }

        private void udpPublishChannel_SendClientDataException(object sender, EventArgs<Guid, Exception> e)
        {
            Exception ex = e.Argument2;

            if (ex is SocketException)
                OnProcessException(new InvalidOperationException(string.Format("Socket exception occurred on the UDP publication channel while attempting to send client data to \"{0}\": {1}", GetConnectionID(m_publishChannel, e.Argument1), ex.Message), ex));
            else
                OnProcessException(new InvalidOperationException(string.Format("UDP publication channel exception occurred while sending client data to \"{0}\": {1}", GetConnectionID(m_publishChannel, e.Argument1), ex.Message), ex));
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
            string connectionID;

            OnStatusMessage("Client \"{0}\" disconnected from TCP publication channel.", GetConnectionID(m_publishChannel, clientID));

            m_connectionIDCache.TryRemove(clientID, out connectionID);
        }

        private void tcpPublishChannel_ClientConnectingException(object sender, EventArgs<Exception> e)
        {
            Exception ex = e.Argument;
            OnProcessException(new InvalidOperationException(string.Format("Socket exception occurred while client was attempting to connect to TCP publication channel: {0}", ex.Message), ex));
        }

        private void tcpPublishChannel_ReceiveClientDataComplete(object sender, EventArgs<Guid, byte[], int> e)
        {
            // Queue up derived class device command handling on a different thread
            ThreadPool.QueueUserWorkItem(DeviceCommandHandlerProc, e);
        }

        private void tcpPublishChannel_SendClientDataException(object sender, EventArgs<Guid, Exception> e)
        {
            Exception ex = e.Argument2;

            if (ex is SocketException)
                OnProcessException(new InvalidOperationException(string.Format("Socket exception occurred on the TCP publication channel while attempting to send client data to \"{0}\": {1}", GetConnectionID(m_publishChannel, e.Argument1), ex.Message), ex));
            else
                OnProcessException(new InvalidOperationException(string.Format("TCP publication channel exception occurred while sending client data to \"{0}\": {1}", GetConnectionID(m_publishChannel, e.Argument1), ex.Message), ex));
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
        }

        private void tcpClientBasedPublishChannel_ConnectionException(object sender, EventArgs<Exception> e)
        {
            Exception ex = e.Argument;
            OnProcessException(new InvalidOperationException(string.Format("Socket exception occurred while TCP publishing client was attempting to connect to TCP listening server channel \"{0}\": {1}", m_clientBasedPublishChannel.ServerUri, ex.Message), ex));
        }

        private void tcpClientBasedPublishChannel_ReceiveDataComplete(object sender, EventArgs<byte[], int> e)
        {
            // Queue up derived class device command handling on a different thread
            ThreadPool.QueueUserWorkItem(DeviceCommandHandlerProc, new EventArgs<Guid, byte[], int>(Guid.Empty, e.Argument1, e.Argument2));
        }

        private void tcpClientBasedPublishChannel_SendDataException(object sender, EventArgs<Exception> e)
        {
            Exception ex = e.Argument;

            if (ex is SocketException)
                OnProcessException(new InvalidOperationException(string.Format("Socket exception occurred on the TCP publication client channel while attempting to send client data to TCP listening server \"{0}\": {1}", m_clientBasedPublishChannel.ServerUri, ex.Message), ex));
            else
                OnProcessException(new InvalidOperationException(string.Format("TCP publication client channel exception occurred while sending client data to TCP listening server \"{0}\": {1}", m_clientBasedPublishChannel.ServerUri, ex.Message), ex));
        }

        #endregion

        #endregion
    }
}
