//******************************************************************************************************
//  StreamSplitterManager.cs - Gbtc
//
//  Copyright © 2013, Grid Protection Alliance.  All Rights Reserved.
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
//  09/03/2013 - J. Ritchie Carroll
//       Generated original version of source code.
//
//******************************************************************************************************

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using GSF;
using GSF.IO;
using GSF.ServiceProcess;
using GSF.Windows.Forms;
using Timer = System.Timers.Timer;

namespace StreamSplitter
{
    /// <summary>
    /// Defines a form to manage proxy connections.
    /// </summary>
    public partial class StreamSplitterManager : Form
    {
        #region [ Members ]

        // Constants
        private const string ConfigurationFileName = "ProxyConnections.xml";
        private const int MaximumToolTipSize = 2048;

        // Fields
        private ProxyConnections m_proxyConnections;
        private ServiceConnection m_serviceConnection;
        private Timer m_refreshProxyStatusTimer;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Creates a new <see cref="StreamSplitterManager"/> instance.
        /// </summary>
        public StreamSplitterManager()
        {
            InitializeComponent();

            // Set service connection
            m_serviceConnection = new ServiceConnection();
            m_serviceConnection.StatusMessage += m_serviceConnection_StatusMessage;
            m_serviceConnection.ServiceResponse += m_serviceConnection_ServiceResponse;
            m_serviceConnection.ConnectionState += m_serviceConnection_ConnectionState;

            // Setup status auto-refresh timer
            m_refreshProxyStatusTimer = new Timer();
            m_refreshProxyStatusTimer.Elapsed += m_refreshProxyStatusTimer_Elapsed;
            m_refreshProxyStatusTimer.Interval = 2000;
            m_refreshProxyStatusTimer.AutoReset = true;

            // Attach to mouse wheel event
            flowLayoutPanelProxyConnections.MouseWheel += flowLayoutPanelProxyConnections_MouseWheel;

            LoadConfiguration();
        }

        #endregion

        #region [ Methods ]

        private void StreamSplitterManager_Load(object sender, EventArgs e)
        {
            this.RestoreLayout();

            // Start connection cycle
            if ((object)m_serviceConnection != null)
                m_serviceConnection.ConnectAsync();

            Show();
            Application.DoEvents();

            BeginInvoke((Action)bindingSource.MoveFirst);
        }

        private void StreamSplitterManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveConfiguration();
            this.SaveLayout();
        }

        private void StreamSplitterManager_Activated(object sender, EventArgs e)
        {
            ProxyConnection currentItem = bindingSource.Current as ProxyConnection;

            if ((object)currentItem != null && (object)currentItem.ProxyConnectionEditor != null)
                SelectProxyConnectionEditorControl(currentItem.ProxyConnectionEditor);

            flowLayoutPanelProxyConnections.Refresh();
        }

        private void flowLayoutPanelProxyConnections_MouseWheel(object sender, MouseEventArgs e)
        {
            Debug.WriteLine(flowLayoutPanelProxyConnections.VerticalScroll.Value);
            flowLayoutPanelProxyConnections.Refresh();
        }

        private void flowLayoutPanelProxyConnections_Scroll(object sender, ScrollEventArgs e)
        {
            Debug.WriteLine(flowLayoutPanelProxyConnections.VerticalScroll.Value);
            flowLayoutPanelProxyConnections.Refresh();
        }

        // Load existing configuration
        private void LoadConfiguration()
        {
            string filename = FilePath.GetAbsolutePath(ConfigurationFileName);

            try
            {
                Cursor = Cursors.WaitCursor;
                m_proxyConnections = ProxyConnections.LoadConfiguration(filename);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load configuration file: " + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                Cursor = Cursors.Default;
            }

            // Establish an editing user control for each proxy connection
            foreach (ProxyConnection connection in m_proxyConnections)
            {
                SplashScreen.SetStatus("Loading " + connection.Name + "...", true);

                // Editing-control creation happens after proxy connection exists and is in the binding list,
                // so we depend on the PropertyChanged event to attach to needed control events
                AddProxyConnectionEditorControl(connection);
            }

            // Attach to needed proxy connection list events
            m_proxyConnections.GotFocus += m_proxyConnections_GotFocus;
            m_proxyConnections.EnabledStateChanged += m_proxyConnections_EnabledStateChanged;
            m_proxyConnections.SaveRecord += m_proxyConnections_SaveRecord;
            m_proxyConnections.RemovingItem += m_proxyConnections_RemovingItem;

            bindingSource.DataSource = m_proxyConnections;
        }

        private void SaveConfiguration()
        {
            string filename = FilePath.GetAbsolutePath(ConfigurationFileName);

            try
            {
                Cursor = Cursors.WaitCursor;

                lock (m_proxyConnections)
                {
                    ProxyConnections.SaveConfiguration(m_proxyConnections, filename);
                }

                // Send notification to service that configuration changes may need to be applied
                if ((object)m_serviceConnection != null)
                    m_serviceConnection.SendCommand("ReloadConfig");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save configuration file: " + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void AddProxyConnectionEditorControl(ProxyConnection connection)
        {
            // Create a new editing control for the proxy connection
            ProxyConnectionEditor editorControl = new ProxyConnectionEditor
            {
                ProxyConnection = connection
            };

            flowLayoutPanelProxyConnections.Controls.Add(editorControl);

            bindingSource.MoveLast();
        }

        private void RemoveProxyConnectionEditorControl(ProxyConnection connection)
        {
            if ((object)connection != null)
            {
                ProxyConnectionEditor editorControl = connection.ProxyConnectionEditor;

                if ((object)editorControl != null)
                {
                    flowLayoutPanelProxyConnections.Controls.Remove(editorControl);
                    editorControl.Dispose();
                }
            }

            // Save changes to configuration when a record is removed
            BeginInvoke((Action)SaveConfiguration);
        }

        private void UnselectProxyConnectionEditorControls()
        {
            // Unselect all existing controls
            foreach (ProxyConnectionEditor control in flowLayoutPanelProxyConnections.Controls)
            {
                control.Selected = false;
            }
        }

        private void SelectProxyConnectionEditorControl(ProxyConnectionEditor editorControl)
        {
            // Select specified control
            if ((object)editorControl != null)
            {
                if (!editorControl.Selected)
                {
                    // Unselect all existing controls
                    UnselectProxyConnectionEditorControls();

                    editorControl.Selected = true;
                    flowLayoutPanelProxyConnections.ScrollControlIntoView(editorControl);

                    flowLayoutPanelProxyConnections.Refresh();
                }
            }
        }

        private void UpdateToolTip(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                return;

            string currentToolTip = Invoke((Func<string>)(() => toolTipEx.GetToolTip(statusStrip))) as string;

            if (currentToolTip == "...")
            {
                Invoke((Action)(() => toolTipEx.SetToolTip(statusStrip, status)));
                return;
            }

            currentToolTip += status;

            // Truncate from the left to maintain maximum tool-tip size
            if (currentToolTip.Length > MaximumToolTipSize)
                currentToolTip = currentToolTip.Substring(currentToolTip.Length - MaximumToolTipSize);

            Invoke((Action)(() => toolTipEx.SetToolTip(statusStrip, currentToolTip)));
        }

        private void bindingSource_AddingNew(object sender, System.ComponentModel.AddingNewEventArgs e)
        {
            ProxyConnection connection = new ProxyConnection
            {
                ConnectionString = "name=New Proxy Connection " + (bindingSource.Count + 1)
            };

            e.NewObject = connection;

            // Adding editing control happens before item is added to binding list so
            // control will be available when connection is inserted
            AddProxyConnectionEditorControl(connection);
        }

        private void bindingSource_PositionChanged(object sender, EventArgs e)
        {
            ProxyConnection currentItem = bindingSource.Current as ProxyConnection;

            if ((object)currentItem != null)
                SelectProxyConnectionEditorControl(currentItem.ProxyConnectionEditor);
        }

        private void m_proxyConnections_GotFocus(object sender, EventArgs<ProxyConnection> e)
        {
            if ((object)e != null && (object)e.Argument != null)
            {
                int position = bindingSource.IndexOf(e.Argument);

                if (position > -1 && bindingSource.Position != position)
                    bindingSource.Position = position;
            }
        }

        private void m_proxyConnections_SaveRecord(object sender, EventArgs<ProxyConnection> e)
        {
            // Save configuration when requested
            SaveConfiguration();
        }

        private void m_proxyConnections_EnabledStateChanged(object sender, EventArgs<ProxyConnection, bool> e)
        {
            // Save configuration when enabled state changes
            SaveConfiguration();
        }

        private void m_proxyConnections_RemovingItem(object sender, EventArgs<ProxyConnection, bool> e)
        {
            if ((object)e == null)
                return;

            string connectionName;

            if ((object)e.Argument1 == null || string.IsNullOrWhiteSpace(e.Argument1.Name))
                connectionName = "<unnamed proxy connection>";
            else
                connectionName = e.Argument1.Name;

            if (MessageBox.Show(string.Format("Are you sure you want to delete \"{0}\"?", connectionName), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                RemoveProxyConnectionEditorControl(e.Argument1);
            else
                e.Argument2 = false;
        }

        private void toolStripButtonReconnect_Click(object sender, EventArgs e)
        {
            if ((object)m_serviceConnection != null)
                m_serviceConnection.ConnectAsync();
        }

        private void m_serviceConnection_ConnectionState(object sender, EventArgs<bool> e)
        {
            bool connected = e.Argument;

            BeginInvoke((Action)(() => toolStripStatusLabelState.Text = connected ? "Connected to Service" : "Disconnected from Service"));

            if ((object)m_refreshProxyStatusTimer != null)
                m_refreshProxyStatusTimer.Enabled = connected;

            if (!connected)
            {
                // Reset all connection indication bubbles to green
                lock (m_proxyConnections)
                {
                    foreach (ProxyConnection proxyConnection in m_proxyConnections)
                    {
                        ProxyConnectionEditor editorControl = proxyConnection.ProxyConnectionEditor;

                        if ((object)editorControl != null)
                            BeginInvoke((Action)(() => editorControl.ConnectionState = ConnectionState.Disabled));
                    }
                }
            }
        }

        private void m_serviceConnection_StatusMessage(object sender, EventArgs<UpdateType, string> e)
        {
            switch (e.Argument1)
            {
                case UpdateType.Warning:
                    BeginInvoke((Action)(() => toolStripStatusLabelStatus.ForeColor = Color.Yellow));
                    break;
                case UpdateType.Alarm:
                    BeginInvoke((Action)(() => toolStripStatusLabelStatus.ForeColor = Color.Red));
                    break;
                default:
                    BeginInvoke((Action)(() => toolStripStatusLabelStatus.ForeColor = SystemColors.ControlText));
                    break;
            }

            BeginInvoke((Action)(() => toolStripStatusLabelStatus.Text = e.Argument2.Replace("\r\n", "  ")));
            UpdateToolTip(e.Argument2);
        }

        private void m_refreshProxyStatusTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if ((object)m_serviceConnection != null)
                m_serviceConnection.SendCommand("GetStreamProxyStatus");
        }

        private void m_serviceConnection_ServiceResponse(object sender, EventArgs<ServiceResponse, string, bool> e)
        {
            if ((object)e == null)
                return;

            // Handle service responses
            ServiceResponse response = e.Argument1;
            string sourceCommand = e.Argument2;
            bool responseSuccess = e.Argument3;

            if ((object)response == null || string.IsNullOrWhiteSpace(sourceCommand))
                return;

            List<object> attachments = response.Attachments;

            // Handle "GetStreamProxyStatus" response
            if (string.Compare(sourceCommand.Trim(), "GetStreamProxyStatus", true) == 0)
            {
                // A GetStreamProxyStatus will have two attachments: a stream proxy array, item 0, and the original command arguments, item 1
                if (responseSuccess && (object)attachments != null && attachments.Count > 1)
                    ThreadPool.QueueUserWorkItem(UpdateStatusStreamProxy, attachments[0] as StreamProxyStatus[]);
            }
        }

        private void UpdateStatusStreamProxy(object state)
        {
            StreamProxyStatus[] streamProxies = state as StreamProxyStatus[];

            if ((object)streamProxies != null)
            {
                ProxyConnection proxyConnection;
                ProxyConnectionEditor editorControl;

                // Apply updates for each stream proxy status
                foreach (StreamProxyStatus proxyStatus in streamProxies)
                {
                    lock (m_proxyConnections)
                    {
                        // Attempt to find associated proxy connection
                        proxyConnection = m_proxyConnections.FirstOrDefault(connection => connection.ID == proxyStatus.ID);
                    }

                    if ((object)proxyConnection != null)
                    {
                        editorControl = proxyConnection.ProxyConnectionEditor;

                        if ((object)editorControl != null)
                            BeginInvoke((Action<ProxyConnectionEditor, StreamProxyStatus>)ApplyStreamStatusUpdate, editorControl, proxyStatus);
                    }
                }
            }
        }

        private void ApplyStreamStatusUpdate(ProxyConnectionEditor editorControl, StreamProxyStatus proxyStatus)
        {
            editorControl.ConnectionState = proxyStatus.ConnectionState;
            editorControl.ConnectionStatus = proxyStatus.RecentStatusMessages;
        }

        #endregion
    }
}
