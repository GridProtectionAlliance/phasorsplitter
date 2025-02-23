//******************************************************************************************************
//  ProxyConnection.cs - Gbtc
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
//  08/31/2013 - J. Ritchie Carroll
//       Generated original version of source code.
//
//******************************************************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using GSF;
using GSF.Communication;
using GSF.Configuration;
using GSF.Diagnostics;
using GSF.PhasorProtocols;

namespace StreamSplitter
{
    /// <summary>
    /// Defines a user control to configure <see cref="ProxyConnection"/> settings.
    /// </summary>
    public partial class ProxyConnectionEditor : UserControl
    {
        #region [ Members ]

        // Constants

        private const int DefaultPhasorProtocol = 1;

        // Events

        /// <summary>
        /// Sends notification that configuration data has changed.
        /// </summary>
        public event EventHandler ConfigurationChanged;

        /// <summary>
        /// Sends notification that user wants to apply changes.
        /// </summary>
        public event EventHandler ApplyChanges;

        /// <summary>
        /// Sends notification that the enabled state has changed.
        /// </summary>
        public event EventHandler<EventArgs<bool>> EnabledStateChanged;

        // Fields
        private MultiProtocolFrameParser m_frameParser;
        private readonly List<TextBox> m_udpDestinations;
        private ProxyConnection m_proxyConnection;
        private volatile bool m_updatingConnectionString;
        private bool m_updatingProxyConnection;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Creates a new <see cref="ProxyConnectionEditor"/> instance.
        /// </summary>
        public ProxyConnectionEditor()
        {
            InitializeComponent();

            m_udpDestinations = [textBoxUdpRebroadcast0];
            m_frameParser = new MultiProtocolFrameParser();

            // Initialize phasor protocol selection list
            foreach (object protocol in Enum.GetValues(typeof(PhasorProtocol)))
                comboBoxProtocol.Items.Add(((PhasorProtocol)protocol).GetFormattedProtocolName());

            comboBoxProtocol.SelectedIndex = DefaultPhasorProtocol;
        }

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Gets or sets property that determines if focus should be applied upon selection.
        /// </summary>
        public bool SelectionFocus { get; set; } = true;

        /// <summary>
        /// Gets or sets selected state for <see cref="ProxyConnectionEditor"/>.
        /// </summary>
        public bool Selected
        {
            set
            {
                if (value && SelectionFocus)
                    textBoxName.Focus();
            }
        }

        /// <summary>
        /// Gets a <see cref="StreamSplitter.ProxyConnection"/> instance for the current settings.
        /// </summary>
        public ProxyConnection ProxyConnection
        {
            get => m_proxyConnection;
            set
            {
                try
                {
                    m_updatingProxyConnection = true;

                    m_proxyConnection = value;

                    if (m_proxyConnection is null)
                        return;

                    ConnectionString = m_proxyConnection.ConnectionString;
                    m_frameParser.ConnectionParameters = m_proxyConnection.ConnectionParameters;
                    ID = m_proxyConnection.ID;
                    ConnectionState = m_proxyConnection.ConnectionState;
                    textBoxConnectionStatus.Text = "";
                }
                finally
                {
                    m_updatingProxyConnection = false;
                }
            }
        }

        /// <summary>
        /// Gets or sets ID for <see cref="ProxyConnectionEditor"/>.
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Gets or sets connection string value for the <see cref="ProxyConnectionEditor"/>.
        /// </summary>
        public string ConnectionString
        {
            get => textBoxConnectionString.Text;
            set => textBoxConnectionString.Text = value;
        }

        /// <summary>
        /// Gets or sets connection parameters for this <see cref="ProxyConnectionEditor"/>.
        /// </summary>
        public IConnectionParameters ConnectionParameters
        {
            get => m_frameParser.ConnectionParameters;
            set
            {
                Dictionary<string, string> allSettings = textBoxConnectionString.Text.ToNonNullString().ParseKeyValuePairs();

                if (!allSettings.TryGetValue("sourceSettings", out string connectionString) || string.IsNullOrWhiteSpace(connectionString))
                    return;

                m_frameParser.ConnectionString = connectionString;
                m_frameParser.ConnectionParameters = value;
                m_frameParser.TryInitializeFrameParser();

                // Update property grid to reflect possible changes
                propertyGridProtocolParameters.SelectedObject = m_frameParser.ConnectionParameters;
            }
        }

        /// <summary>
        /// Gets or sets <see cref="ConnectionState"/> for the <see cref="ProxyConnectionEditor"/> controlling the connection state icon.
        /// </summary>
        public ConnectionState ConnectionState
        {
            get
            {
                if (pictureBoxRed.Visible)
                    return ConnectionState.Disconnected;

                if (pictureBoxYellow.Visible)
                    return ConnectionState.ConnectedNoData;

                if (pictureBoxGreen.Visible)
                    return ConnectionState.Connected;

                return ConnectionState.Disabled;
            }
            set
            {
                // Don't update controls if value hasn't changed
                if (value == ConnectionState)
                    return;

                pictureBoxRed.Visible = false;
                pictureBoxYellow.Visible = false;
                pictureBoxGreen.Visible = false;
                pictureBoxGray.Visible = false;

                switch (value)
                {
                    case ConnectionState.Disconnected:
                        pictureBoxRed.Visible = true;
                        break;
                    case ConnectionState.ConnectedNoData:
                        pictureBoxYellow.Visible = true;
                        break;
                    case ConnectionState.Connected:
                        pictureBoxGreen.Visible = true;
                        break;
                    default:
                        pictureBoxGray.Visible = true;
                        break;
                }
            }
        }

        /// <summary>
        /// Updates connection state for the <see cref="ProxyConnectionEditor"/>.
        /// </summary>
        public string ConnectionStatus
        {
            set
            {
                // Don't update controls unless text has changed
                if (string.Compare(textBoxConnectionStatus.Text, value, StringComparison.Ordinal) == 0)
                    return;

                textBoxConnectionStatus.Text = value;
                textBoxConnectionStatus.SelectionStart = textBoxConnectionStatus.Text.ToNonNullString().Length;
                textBoxConnectionStatus.SelectionLength = 0;
                textBoxConnectionStatus.ScrollToCaret();

                textBoxConnectionStatus.Refresh();
            }
        }

        #endregion

        #region [ Methods ]

        // Handle connection string manipulations

        private string GenerateConnectionString()
        {
            Dictionary<string, string> allSettings = textBoxConnectionString.Text.ToNonNullString().ParseKeyValuePairs();
            Dictionary<string, string> sourceSettings = allSettings.TryGetValue("sourceSettings", out string setting) ? setting.ParseKeyValuePairs() : new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            Dictionary<string, string> proxySettings = allSettings.TryGetValue("proxySettings", out setting) ? setting.ParseKeyValuePairs() : new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            // Update source connection information
            TransportProtocol sourceTransportProtocol = (TransportProtocol)tabControlSourceConnectionType.SelectedIndex;
            sourceSettings["transportProtocol"] = sourceTransportProtocol.ToString();

            if (!sourceSettings.ContainsKey("interface"))
                sourceSettings["interface"] = "0.0.0.0";

            sourceSettings.Remove("server");
            sourceSettings.Remove("localport");

            switch (sourceTransportProtocol)
            {
                case TransportProtocol.Tcp:
                    if (checkBoxTcpSourceIsListener.Checked)
                    {
                        sourceSettings["port"] = textBoxTcpListeningPort.Text;
                        sourceSettings["isListener"] = "true";
                        sourceSettings.Remove("server");
                    }
                    else
                    {
                        sourceSettings["server"] = textBoxTcpConnection.Text;
                        sourceSettings.Remove("isListener");
                        sourceSettings.Remove("port");
                    }

                    sourceSettings.Remove("remoteport");
                    break;
                case TransportProtocol.Udp:
                    sourceSettings["localport"] = textBoxUdpListeningPort.Text;
                    sourceSettings.Remove("isListener");

                    if (checkBoxUseRemoteUdp.Checked)
                    {
                        string[] parts = textBoxRemoteUdpConnection.Text.Split(':');

                        if (parts.Length != 2)
                        {
                            sourceSettings.Remove("server");
                            sourceSettings.Remove("remoteport");
                        }
                        else
                        {
                            sourceSettings["server"] = parts[0].Trim();

                            if (ushort.TryParse(parts[1].Trim(), out ushort remotePort))
                                sourceSettings["remoteport"] = remotePort.ToString();
                            else
                                sourceSettings["remotePort"] = "0";
                        }
                    }
                    else
                    {
                        sourceSettings.Remove("server");
                        sourceSettings.Remove("remoteport");
                    }
                    break;
            }

            UpdateCommandChannelSettings(sourceSettings);

            // Update protocol information
            sourceSettings["phasorProtocol"] = ((PhasorProtocol)comboBoxProtocol.SelectedIndex).ToString();
            sourceSettings["accessID"] = textBoxIDCode.Text;

            // Update proxy connection information
            TransportProtocol proxyTransportProtocol = (TransportProtocol)tabControlProxyDestinationType.SelectedIndex;
            proxySettings["protocol"] = proxyTransportProtocol.ToString();
            proxySettings.Remove("transportProtocol");

            if (!proxySettings.ContainsKey("interface"))
                proxySettings["interface"] = "0.0.0.0";

            proxySettings.Remove("clients");

            switch (proxyTransportProtocol)
            {
                case TransportProtocol.Tcp:
                    if (checkBoxTcpProxyIsListener.Checked)
                    {
                        proxySettings["port"] = textBoxTcpPublisherListeningPort.Text;
                        proxySettings.Remove("useClientPublishChannel");
                        proxySettings.Remove("server");
                    }
                    else
                    {
                        proxySettings["server"] = textBoxTcpClientPublisherConnection.Text;
                        proxySettings["useClientPublishChannel"] = "true";
                        proxySettings.Remove("port");
                    }
                    break;
                case TransportProtocol.Udp:
                    proxySettings["port"] = "-1";
                    proxySettings["clients"] = string.Join(", ", m_udpDestinations.Select(textBox => textBox.Text).Where(value => !string.IsNullOrWhiteSpace(value)));
                    proxySettings.Remove("useClientPublishChannel");
                    break;
            }

            InjectMaxSendQueueSize(sourceSettings);
            InjectMaxSendQueueSize(proxySettings);

            allSettings.Clear();
            allSettings["sourceSettings"] = sourceSettings.JoinKeyValuePairs();
            allSettings["proxySettings"] = proxySettings.JoinKeyValuePairs();
            allSettings["enabled"] = checkBoxEnabled.Checked.ToString();
            allSettings["name"] = textBoxName.Text;

            return allSettings.JoinKeyValuePairs();
        }

        private void UpdateCommandChannelSettings(Dictionary<string, string> sourceSettings)
        {
            if (checkBoxUseAlternateTcpConnection.Checked)
            {
                if (sourceSettings.TryGetValue("commandChannel", out string setting))
                {
                    Dictionary<string, string> settings = setting.ParseKeyValuePairs();
                    settings["server"] = textBoxAlternateTcpConnection.Text;
                    InjectMaxSendQueueSize(settings);
                    sourceSettings["commandChannel"] = settings.JoinKeyValuePairs();
                }
                else
                {
                    string oldCommandChannel = checkBoxUseAlternateTcpConnection.Tag as string;

                    if (!string.IsNullOrWhiteSpace(oldCommandChannel))
                    {
                        sourceSettings["commandChannel"] = oldCommandChannel;
                        UpdateCommandChannelSettings(sourceSettings);
                    }
                    else
                    {
                        sourceSettings["commandChannel"] = $"transportProtocol=Tcp; server={textBoxAlternateTcpConnection.Text}; interface=0.0.0.0";
                    }
                }
            }
            else
            {
                if (!sourceSettings.TryGetValue("commandChannel", out string setting))
                    return;

                checkBoxUseAlternateTcpConnection.Tag = setting;
                sourceSettings.Remove("commandChannel");
            }
        }

        private void ParseConnectionString()
        {
            bool updateConnectionString = false;

            Dictionary<string, string> allSettings = textBoxConnectionString.Text.ToNonNullString().ParseKeyValuePairs();
            Dictionary<string, string> sourceSettings = allSettings.TryGetValue("sourceSettings", out string setting) ? setting.ParseKeyValuePairs() : new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            Dictionary<string, string> proxySettings = allSettings.TryGetValue("proxySettings", out setting) ? setting.ParseKeyValuePairs() : new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            InjectMaxSendQueueSize(sourceSettings);
            InjectMaxSendQueueSize(proxySettings);

            // Update source connection information

            if (!sourceSettings.TryGetValue("transportProtocol", out setting) || !Enum.TryParse(setting, out TransportProtocol sourceTransportProtocol) || (int)sourceTransportProtocol > 1)
                sourceTransportProtocol = TransportProtocol.Tcp;

            tabControlSourceConnectionType.SelectedIndex = (int)sourceTransportProtocol;

            switch (sourceTransportProtocol)
            {
                case TransportProtocol.Tcp:

                    if (sourceSettings.TryGetValue("isListener", out setting) && setting.ParseBoolean())
                    {
                        checkBoxTcpSourceIsListener.Checked = true;

                        if (sourceSettings.TryGetValue("port", out setting))
                            textBoxTcpListeningPort.Text = setting;
                    }
                    else
                    {
                        checkBoxTcpSourceIsListener.Checked = false;

                        if (sourceSettings.TryGetValue("server", out setting))
                            textBoxTcpConnection.Text = setting;
                    }
                    break;
                case TransportProtocol.Udp:
                    if (sourceSettings.TryGetValue("localport", out setting))
                        textBoxUdpListeningPort.Text = setting;

                    string remoteUdpConnection = null;

                    if (sourceSettings.TryGetValue("server", out setting))
                        remoteUdpConnection = setting.Trim();

                    if (sourceSettings.TryGetValue("remoteport", out setting) && ushort.TryParse(setting.Trim(), out ushort remotePort))
                        remoteUdpConnection = $"{remoteUdpConnection}:{remotePort}";
                    else
                        remoteUdpConnection = null;

                    if (string.IsNullOrWhiteSpace(remoteUdpConnection))
                    {
                        checkBoxUseRemoteUdp.Checked = false;
                    }
                    else
                    {
                        checkBoxUseRemoteUdp.Checked = true;
                        textBoxRemoteUdpConnection.Text = remoteUdpConnection;
                    }
                    break;
            }

            if (sourceSettings.TryGetValue("commandChannel", out setting))
            {
                Dictionary<string, string> settings = setting.ParseKeyValuePairs();

                checkBoxUseAlternateTcpConnection.Checked = true;

                if (settings.TryGetValue("server", out setting))
                    textBoxAlternateTcpConnection.Text = setting;
            }
            else
            {
                checkBoxUseAlternateTcpConnection.Checked = false;
            }

            // Update protocol information

            if (sourceSettings.TryGetValue("phasorProtocol", out setting) && Enum.TryParse(setting, out PhasorProtocol protocol))
                comboBoxProtocol.SelectedIndex = (int)protocol;
            else
                comboBoxProtocol.SelectedIndex = DefaultPhasorProtocol;

            if (sourceSettings.TryGetValue("accessID", out setting) && ushort.TryParse(setting, out ushort idCode))
                textBoxIDCode.Text = idCode.ToString();
            else
                textBoxIDCode.Text = "1";

            // Update proxy connection information

            if (!(proxySettings.TryGetValue("protocol", out setting) || proxySettings.TryGetValue("transportProtocol", out setting)) || !Enum.TryParse(setting, out TransportProtocol proxyTransportProtocol) || (int)proxyTransportProtocol > 1)
                proxyTransportProtocol = TransportProtocol.Tcp;

            tabControlProxyDestinationType.SelectedIndex = (int)proxyTransportProtocol;

            switch (proxyTransportProtocol)
            {
                case TransportProtocol.Tcp:
                    if (proxySettings.TryGetValue("useClientPublishChannel", out setting))
                    {
                        if (setting.ParseBoolean())
                        {
                            checkBoxTcpProxyIsListener.Checked = false;

                            if (proxySettings.TryGetValue("server", out setting))
                                textBoxTcpClientPublisherConnection.Text = setting;
                        }
                        else
                        {
                            checkBoxTcpProxyIsListener.Checked = true;

                            if (proxySettings.TryGetValue("port", out setting))
                                textBoxTcpPublisherListeningPort.Text = setting;
                        }
                    }
                    else
                    {
                        // If useClientPublishChannel setting is not defined, derive current
                        // server/client mode based on other connection string contents...
                        if (proxySettings.TryGetValue("port", out setting))
                        {
                            checkBoxTcpProxyIsListener.Checked = true;
                            textBoxTcpPublisherListeningPort.Text = setting;
                        }
                        else if (proxySettings.TryGetValue("server", out setting))
                        {
                            checkBoxTcpProxyIsListener.Checked = false;
                            textBoxTcpClientPublisherConnection.Text = setting;
                            updateConnectionString = true;
                        }
                        else
                        {
                            checkBoxTcpProxyIsListener.Checked = false;
                        }
                    }
                    break;
                case TransportProtocol.Udp:
                    if (proxySettings.TryGetValue("clients", out setting))
                    {
                        string[] clients = setting.Split(',').Where(value => !string.IsNullOrWhiteSpace(value)).ToArray();

                        while (m_udpDestinations.Count > clients.Length)
                            RemoveDestinationClient();

                        while (m_udpDestinations.Count < clients.Length)
                            AddDestinationClient();

                        for (int i = 0; i < clients.Length; i++)
                            m_udpDestinations[i].Text = clients[i].Trim();
                    }
                    break;
            }

            checkBoxEnabled.Checked = allSettings.TryGetValue("enabled", out setting) && setting.ParseBoolean();
            textBoxName.Text = allSettings.TryGetValue("name", out setting) ? setting : "New Proxy Connection";

            if (updateConnectionString)
                BeginInvoke(UpdateConnectionString);
        }

        private void ControlValueChanged(object sender, EventArgs e)
        {
            UpdateConnectionString();
        }

        private void UpdateConnectionString()
        {
            if (m_updatingConnectionString)
                return;

            m_updatingConnectionString = true;

            try
            {
                textBoxConnectionString.Text = GenerateConnectionString();
                
                if (m_proxyConnection is not null)
                    m_proxyConnection.ConnectionString = textBoxConnectionString.Text;

                OnConfigurationChanged();
            }
            finally
            {
                m_updatingConnectionString = false;
            }
        }

        private void textBoxConnectionString_TextChanged(object sender, EventArgs e)
        {
            if (m_updatingConnectionString)
                return;

            m_updatingConnectionString = true;

            try
            {
                ParseConnectionString();

                if (m_updatingProxyConnection)
                    return;

                if (m_proxyConnection is not null)
                    m_proxyConnection.ConnectionString = textBoxConnectionString.Text;

                OnConfigurationChanged();
            }
            finally
            {
                m_updatingConnectionString = false;
            }
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            OnApplyChanges();
        }

        // Add / remove UDP destinations

        private void AddDestinationClient()
        {
            TextBox newDestination = new()
            {
                Size = textBoxUdpRebroadcast0.Size,
                Margin = textBoxUdpRebroadcast0.Margin,
                Name = "textBoxUdpRebroadcast" + m_udpDestinations.Count,
                TabIndex = textBoxUdpRebroadcast0.TabIndex + m_udpDestinations.Count
            };

            newDestination.TextChanged += ControlValueChanged;
            newDestination.Validating += HandleHostPortValidation;

            toolTip.SetToolTip(newDestination, "Example: 233.123.123.123:5000");

            flowLayoutPanelUdpDestinations.Controls.Add(newDestination);

            m_udpDestinations.Add(newDestination);
            newDestination.Focus();
        }

        private void RemoveDestinationClient()
        {
            // Need to maintain one control at a minimum
            if (m_udpDestinations.Count < 2)
                return;

            TextBox destinationToRemove = m_udpDestinations.Last();
            destinationToRemove.TextChanged -= ControlValueChanged;
            destinationToRemove.Validating -= HandleHostPortValidation;
            m_udpDestinations.RemoveAt(m_udpDestinations.Count - 1);
            flowLayoutPanelUdpDestinations.Controls.Remove(destinationToRemove);
            UpdateConnectionString();
        }

        private void buttonAddUdpDestination_Click(object sender, EventArgs e)
        {
            AddDestinationClient();
        }

        private void buttonRemoveUdpDestination_Click(object sender, EventArgs e)
        {
            RemoveDestinationClient();
        }

        // Handle validations

        private void HandleInt16Validation(object sender, CancelEventArgs e)
        {
            if (sender is not TextBox textBox)
                return;

            if (IsValidInt16(textBox))
            {
                errorProvider.SetError(textBox, null);
            }
            else
            {
                errorProvider.SetError(textBox, "Must be number between 1 and 65535");
                e.Cancel = true;
            }
        }

        private void HandleHostPortValidation(object sender, CancelEventArgs e)
        {
            if (sender is not TextBox textBox)
                return;

            if (IsValidHostPort(textBox))
            {
                errorProvider.SetError(textBox, null);
            }
            else
            {
                errorProvider.SetError(textBox, "Must be formatted as \"host:port\" where port number is between 1 and 65535");
                e.Cancel = true;
            }
        }

        // Handle event interactions

        private void checkBoxEnabled_CheckedChanged(object sender, EventArgs e)
        {
            UpdateConnectionString();

            // Raise event to send service notification to "enable/disable" this proxy connection
            OnEnabledStateChanged(checkBoxEnabled.Checked);
        }

        // Handle dynamic enabling / disabling of controls

        private void checkBoxUseAlternateTcpConnection_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = checkBoxUseAlternateTcpConnection.Checked;

            labelAlternateTcpConnection.Enabled = isChecked;
            textBoxAlternateTcpConnection.Enabled = isChecked;
            labelAltermateTcpConnectionFormat.Enabled = isChecked;

            UpdateConnectionString();
        }

        private void checkBoxUseRemoteUdp_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxUseRemoteUdp.Checked)
            {
                checkBoxUseRemoteUdp.Location = checkBoxUseRemoteUdp.Location with { Y = 0 };
                textBoxRemoteUdpConnection.Visible = true;
                labelRemoteUdpConnectionFormat.Visible = true;
            }
            else
            {
                textBoxRemoteUdpConnection.Visible = false;
                labelRemoteUdpConnectionFormat.Visible = false;
                checkBoxUseRemoteUdp.Location = checkBoxUseRemoteUdp.Location with { Y = 22 };
            }

            UpdateConnectionString();
        }

        // Update phasor specific protocol parameters

        private void comboBoxProtocol_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateConnectionString();

            // Assign new protocol selection to frame parser - this will retrieve a default set
            // of connection parameters for the protocol if any are available
            m_frameParser.PhasorProtocol = (PhasorProtocol)comboBoxProtocol.SelectedIndex;
            propertyGridProtocolParameters.SelectedObject = m_frameParser.ConnectionParameters;
        }

        // Change TCP operational modes

        private void checkBoxTcpSourceIsListener_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxTcpSourceIsListener.Checked)
            {
                textBoxTcpListeningPort.Visible = true;
                textBoxTcpConnection.Visible = false;
                labelTcpSourceSettings.Text = "Listening Port:";
                labelTcpSourceFormat.Text = "Format: port";
            }
            else
            {
                textBoxTcpConnection.Visible = true;
                textBoxTcpListeningPort.Visible = false;
                labelTcpSourceSettings.Text = "Connect To:";
                labelTcpSourceFormat.Text = "Format: host:port";
            }

            UpdateConnectionString();
        }

        private void checkBoxTcpProxyIsListener_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxTcpProxyIsListener.Checked)
            {
                textBoxTcpPublisherListeningPort.Visible = true;
                textBoxTcpClientPublisherConnection.Visible = false;
                labelTcpProxySettings.Text = "Listening Port:";
                labelTcpProxyFormat.Text = "Format: port";
            }
            else
            {
                textBoxTcpClientPublisherConnection.Visible = true;
                textBoxTcpPublisherListeningPort.Visible = false;
                labelTcpProxySettings.Text = "Connect To:";
                labelTcpProxyFormat.Text = "Format: host:port";
            }

            UpdateConnectionString();
        }

        /// <summary>
        /// Raises the <see cref="ConfigurationChanged"/> event.
        /// </summary>
        protected virtual void OnConfigurationChanged() => 
            ConfigurationChanged?.Invoke(this, EventArgs.Empty);

        /// <summary>
        /// Raise the <see cref="ApplyChanges"/> event.
        /// </summary>
        protected virtual void OnApplyChanges() => 
            ApplyChanges?.Invoke(this, EventArgs.Empty);

        /// <summary>
        /// Raises the <see cref="EnabledStateChanged"/> event.
        /// </summary>
        /// <param name="newState">New enabled state.</param>
        protected virtual void OnEnabledStateChanged(bool newState) => 
            EnabledStateChanged?.Invoke(this, new EventArgs<bool>(newState));

        #endregion

        #region [ Static ]

        // Static Fields
        private static string s_maxSendQueueSize;

        // Static Methods
        private static bool IsValidHostPort(object sender)
        {
            if (sender is not TextBox textBox)
                return false;

            string text = textBox.Text;

            if (string.IsNullOrWhiteSpace(text))
                return textBox.Name.StartsWith("textBoxUdpRebroadcast");

            int lastColonIndex = text.LastIndexOf(':');

            if (lastColonIndex > 0 && text.Length > 2)
                return IsValidInt16(text.Substring(lastColonIndex + 1));

            return false;
        }

        private static bool IsValidInt16(object sender)
        {
            if (sender is TextBox textBox)
                return IsValidInt16(textBox.Text);

            return false;
        }

        private static bool IsValidInt16(string text) =>
            string.IsNullOrWhiteSpace(text) || ushort.TryParse(text, out ushort _);

        internal static void InjectMaxSendQueueSize(Dictionary<string, string> settings)
        {
            if (s_maxSendQueueSize is null)
            {
                const string DefaultMaxSendQueueSize = "10000";

                try
                {
                    // Make sure setting exists within system settings section of config file
                    ConfigurationFile configFile = ConfigurationFile.Current;
                    CategorizedSettingsElementCollection systemSettings = configFile.Settings["systemSettings"];
                    systemSettings.Add("MaxSendQueueSize", DefaultMaxSendQueueSize, "Defines the max send queue size for socket connections. Set to -1 for no limit.");

                    s_maxSendQueueSize = systemSettings["MaxSendQueueSize"].ValueAs(DefaultMaxSendQueueSize);
                }
                catch (Exception ex)
                {
                    Logger.SwallowException(ex);
                    s_maxSendQueueSize = DefaultMaxSendQueueSize;
                }
            }

            settings["maxSendQueueSize"] = s_maxSendQueueSize;
        }

        #endregion
    }
}
