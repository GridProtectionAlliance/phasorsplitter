//******************************************************************************************************
//  ProxyConnectionEditor.Designer.cs - Gbtc
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
//  09/05/2013 - J. Ritchie Carroll
//       Generated original version of source code.
//
//******************************************************************************************************

namespace StreamSplitter
{
    partial class ProxyConnectionEditor
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components is not null))
            {
                components.Dispose();

                if (m_frameParser is not null)
                {
                    m_frameParser.Dispose();
                    m_frameParser = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProxyConnectionEditor));
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBoxName = new System.Windows.Forms.GroupBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxTcpPublisherListeningPort = new System.Windows.Forms.TextBox();
            this.textBoxUdpListeningPort = new System.Windows.Forms.TextBox();
            this.textBoxIDCode = new System.Windows.Forms.TextBox();
            this.textBoxAlternateTcpConnection = new System.Windows.Forms.TextBox();
            this.textBoxTcpConnection = new System.Windows.Forms.TextBox();
            this.textBoxUdpRebroadcast0 = new System.Windows.Forms.TextBox();
            this.groupBoxSourceConnection = new System.Windows.Forms.GroupBox();
            this.labelIDCode = new System.Windows.Forms.Label();
            this.labelAltermateTcpConnectionFormat = new System.Windows.Forms.Label();
            this.labelAlternateTcpConnection = new System.Windows.Forms.Label();
            this.checkBoxUseAlternateTcpConnection = new System.Windows.Forms.CheckBox();
            this.tabControlSourceConnectionType = new System.Windows.Forms.TabControl();
            this.tabPageTcp = new System.Windows.Forms.TabPage();
            this.textBoxTcpListeningPort = new System.Windows.Forms.TextBox();
            this.labelTcpSourceFormat = new System.Windows.Forms.Label();
            this.labelTcpSourceSettings = new System.Windows.Forms.Label();
            this.checkBoxTcpSourceIsListener = new System.Windows.Forms.CheckBox();
            this.tabPageUdp = new System.Windows.Forms.TabPage();
            this.labelUdpListeningPort = new System.Windows.Forms.Label();
            this.groupBoxProxyDestinations = new System.Windows.Forms.GroupBox();
            this.buttonApply = new System.Windows.Forms.Button();
            this.pictureBoxGreen = new System.Windows.Forms.PictureBox();
            this.pictureBoxRed = new System.Windows.Forms.PictureBox();
            this.pictureBoxYellow = new System.Windows.Forms.PictureBox();
            this.pictureBoxGray = new System.Windows.Forms.PictureBox();
            this.checkBoxEnabled = new System.Windows.Forms.CheckBox();
            this.tabControlProxyDestinationType = new System.Windows.Forms.TabControl();
            this.tabTcpProxyPoint = new System.Windows.Forms.TabPage();
            this.labelTcpProxyFormat = new System.Windows.Forms.Label();
            this.textBoxTcpClientPublisherConnection = new System.Windows.Forms.TextBox();
            this.labelTcpProxySettings = new System.Windows.Forms.Label();
            this.checkBoxTcpProxyIsListener = new System.Windows.Forms.CheckBox();
            this.tabUdpRebroacasts = new System.Windows.Forms.TabPage();
            this.flowLayoutPanelUdpDestinations = new System.Windows.Forms.FlowLayoutPanel();
            this.labelUdpDestinationFormat = new System.Windows.Forms.Label();
            this.buttonAddUdpDestination = new System.Windows.Forms.Button();
            this.buttonRemoveUdpDestination = new System.Windows.Forms.Button();
            this.textBoxConnectionStatus = new System.Windows.Forms.TextBox();
            this.comboBoxProtocol = new System.Windows.Forms.ComboBox();
            this.propertyGridProtocolParameters = new System.Windows.Forms.PropertyGrid();
            this.groupBoxProtocolParameters = new System.Windows.Forms.GroupBox();
            this.textBoxConnectionString = new System.Windows.Forms.TextBox();
            this.panelCenterGroups = new System.Windows.Forms.Panel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipEx = new StreamSplitter.ToolTipEx(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBoxName.SuspendLayout();
            this.groupBoxSourceConnection.SuspendLayout();
            this.tabControlSourceConnectionType.SuspendLayout();
            this.tabPageTcp.SuspendLayout();
            this.tabPageUdp.SuspendLayout();
            this.groupBoxProxyDestinations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxYellow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGray)).BeginInit();
            this.tabControlProxyDestinationType.SuspendLayout();
            this.tabTcpProxyPoint.SuspendLayout();
            this.tabUdpRebroacasts.SuspendLayout();
            this.flowLayoutPanelUdpDestinations.SuspendLayout();
            this.groupBoxProtocolParameters.SuspendLayout();
            this.panelCenterGroups.SuspendLayout();
            this.SuspendLayout();
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // groupBoxName
            // 
            this.groupBoxName.Controls.Add(this.textBoxName);
            this.groupBoxName.Cursor = System.Windows.Forms.Cursors.Default;
            this.groupBoxName.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxName.Location = new System.Drawing.Point(0, 0);
            this.groupBoxName.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.groupBoxName.Name = "groupBoxName";
            this.groupBoxName.Padding = new System.Windows.Forms.Padding(8, 2, 8, 2);
            this.groupBoxName.Size = new System.Drawing.Size(645, 47);
            this.groupBoxName.TabIndex = 0;
            this.groupBoxName.TabStop = false;
            this.groupBoxName.Text = "Connection Name";
            // 
            // textBoxName
            // 
            this.textBoxName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxName.Location = new System.Drawing.Point(8, 17);
            this.textBoxName.Margin = new System.Windows.Forms.Padding(5);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(629, 22);
            this.textBoxName.TabIndex = 0;
            this.textBoxName.TextChanged += new System.EventHandler(this.ControlValueChanged);
            // 
            // textBoxTcpPublisherListeningPort
            // 
            this.textBoxTcpPublisherListeningPort.Location = new System.Drawing.Point(17, 26);
            this.textBoxTcpPublisherListeningPort.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.textBoxTcpPublisherListeningPort.Name = "textBoxTcpPublisherListeningPort";
            this.textBoxTcpPublisherListeningPort.Size = new System.Drawing.Size(66, 22);
            this.textBoxTcpPublisherListeningPort.TabIndex = 1;
            this.textBoxTcpPublisherListeningPort.Text = "4712";
            this.toolTip.SetToolTip(this.textBoxTcpPublisherListeningPort, "Example: 4713");
            this.textBoxTcpPublisherListeningPort.TextChanged += new System.EventHandler(this.ControlValueChanged);
            this.textBoxTcpPublisherListeningPort.Validating += new System.ComponentModel.CancelEventHandler(this.HandleInt16Validation);
            // 
            // textBoxUdpListeningPort
            // 
            this.textBoxUdpListeningPort.Location = new System.Drawing.Point(26, 25);
            this.textBoxUdpListeningPort.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.textBoxUdpListeningPort.Name = "textBoxUdpListeningPort";
            this.textBoxUdpListeningPort.Size = new System.Drawing.Size(66, 22);
            this.textBoxUdpListeningPort.TabIndex = 1;
            this.textBoxUdpListeningPort.Text = "4713";
            this.toolTip.SetToolTip(this.textBoxUdpListeningPort, "Example: 4713");
            this.textBoxUdpListeningPort.TextChanged += new System.EventHandler(this.ControlValueChanged);
            this.textBoxUdpListeningPort.Validating += new System.ComponentModel.CancelEventHandler(this.HandleInt16Validation);
            // 
            // textBoxIDCode
            // 
            this.textBoxIDCode.Location = new System.Drawing.Point(92, 107);
            this.textBoxIDCode.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.textBoxIDCode.Name = "textBoxIDCode";
            this.textBoxIDCode.Size = new System.Drawing.Size(66, 22);
            this.textBoxIDCode.TabIndex = 5;
            this.textBoxIDCode.Text = "1";
            this.toolTip.SetToolTip(this.textBoxIDCode, "Example: 235");
            this.textBoxIDCode.TextChanged += new System.EventHandler(this.ControlValueChanged);
            this.textBoxIDCode.Validating += new System.ComponentModel.CancelEventHandler(this.HandleInt16Validation);
            // 
            // textBoxAlternateTcpConnection
            // 
            this.textBoxAlternateTcpConnection.Enabled = false;
            this.textBoxAlternateTcpConnection.Location = new System.Drawing.Point(18, 167);
            this.textBoxAlternateTcpConnection.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.textBoxAlternateTcpConnection.Name = "textBoxAlternateTcpConnection";
            this.textBoxAlternateTcpConnection.Size = new System.Drawing.Size(170, 22);
            this.textBoxAlternateTcpConnection.TabIndex = 3;
            this.textBoxAlternateTcpConnection.Text = "localhost:4712";
            this.toolTip.SetToolTip(this.textBoxAlternateTcpConnection, "Example: 192.168.1.10:4712");
            this.textBoxAlternateTcpConnection.TextChanged += new System.EventHandler(this.ControlValueChanged);
            this.textBoxAlternateTcpConnection.Validating += new System.ComponentModel.CancelEventHandler(this.HandleHostPortValidation);
            // 
            // textBoxTcpConnection
            // 
            this.textBoxTcpConnection.Location = new System.Drawing.Point(12, 17);
            this.textBoxTcpConnection.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.textBoxTcpConnection.Name = "textBoxTcpConnection";
            this.textBoxTcpConnection.Size = new System.Drawing.Size(170, 22);
            this.textBoxTcpConnection.TabIndex = 2;
            this.textBoxTcpConnection.Text = "localhost:4712";
            this.toolTip.SetToolTip(this.textBoxTcpConnection, "Example: 192.168.1.10:4712");
            this.textBoxTcpConnection.TextChanged += new System.EventHandler(this.ControlValueChanged);
            this.textBoxTcpConnection.Validating += new System.ComponentModel.CancelEventHandler(this.HandleHostPortValidation);
            // 
            // textBoxUdpRebroadcast0
            // 
            this.textBoxUdpRebroadcast0.Location = new System.Drawing.Point(8, 24);
            this.textBoxUdpRebroadcast0.Margin = new System.Windows.Forms.Padding(8, 1, 4, 0);
            this.textBoxUdpRebroadcast0.Name = "textBoxUdpRebroadcast0";
            this.textBoxUdpRebroadcast0.Size = new System.Drawing.Size(160, 22);
            this.textBoxUdpRebroadcast0.TabIndex = 3;
            this.toolTip.SetToolTip(this.textBoxUdpRebroadcast0, "Example: 233.123.123.123:5000");
            this.textBoxUdpRebroadcast0.TextChanged += new System.EventHandler(this.ControlValueChanged);
            this.textBoxUdpRebroadcast0.Validating += new System.ComponentModel.CancelEventHandler(this.HandleHostPortValidation);
            // 
            // groupBoxSourceConnection
            // 
            this.groupBoxSourceConnection.Controls.Add(this.labelIDCode);
            this.groupBoxSourceConnection.Controls.Add(this.textBoxIDCode);
            this.groupBoxSourceConnection.Controls.Add(this.textBoxAlternateTcpConnection);
            this.groupBoxSourceConnection.Controls.Add(this.labelAltermateTcpConnectionFormat);
            this.groupBoxSourceConnection.Controls.Add(this.labelAlternateTcpConnection);
            this.groupBoxSourceConnection.Controls.Add(this.checkBoxUseAlternateTcpConnection);
            this.groupBoxSourceConnection.Controls.Add(this.tabControlSourceConnectionType);
            this.groupBoxSourceConnection.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBoxSourceConnection.Location = new System.Drawing.Point(0, 0);
            this.groupBoxSourceConnection.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.groupBoxSourceConnection.Name = "groupBoxSourceConnection";
            this.groupBoxSourceConnection.Padding = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.groupBoxSourceConnection.Size = new System.Drawing.Size(218, 508);
            this.groupBoxSourceConnection.TabIndex = 0;
            this.groupBoxSourceConnection.TabStop = false;
            this.groupBoxSourceConnection.Text = "Source Connection";
            // 
            // labelIDCode
            // 
            this.labelIDCode.AutoSize = true;
            this.labelIDCode.Location = new System.Drawing.Point(40, 109);
            this.labelIDCode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelIDCode.Name = "labelIDCode";
            this.labelIDCode.Size = new System.Drawing.Size(51, 13);
            this.labelIDCode.TabIndex = 4;
            this.labelIDCode.Text = "ID Code:";
            // 
            // labelAltermateTcpConnectionFormat
            // 
            this.labelAltermateTcpConnectionFormat.AutoSize = true;
            this.labelAltermateTcpConnectionFormat.Enabled = false;
            this.labelAltermateTcpConnectionFormat.Location = new System.Drawing.Point(17, 190);
            this.labelAltermateTcpConnectionFormat.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelAltermateTcpConnectionFormat.Name = "labelAltermateTcpConnectionFormat";
            this.labelAltermateTcpConnectionFormat.Size = new System.Drawing.Size(97, 13);
            this.labelAltermateTcpConnectionFormat.TabIndex = 4;
            this.labelAltermateTcpConnectionFormat.Text = "Format: host:port";
            // 
            // labelAlternateTcpConnection
            // 
            this.labelAlternateTcpConnection.AutoSize = true;
            this.labelAlternateTcpConnection.Enabled = false;
            this.labelAlternateTcpConnection.Location = new System.Drawing.Point(17, 151);
            this.labelAlternateTcpConnection.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelAlternateTcpConnection.Name = "labelAlternateTcpConnection";
            this.labelAlternateTcpConnection.Size = new System.Drawing.Size(68, 13);
            this.labelAlternateTcpConnection.TabIndex = 2;
            this.labelAlternateTcpConnection.Text = "Connect To:";
            // 
            // checkBoxUseAlternateTcpConnection
            // 
            this.checkBoxUseAlternateTcpConnection.AutoSize = true;
            this.checkBoxUseAlternateTcpConnection.Location = new System.Drawing.Point(5, 130);
            this.checkBoxUseAlternateTcpConnection.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.checkBoxUseAlternateTcpConnection.Name = "checkBoxUseAlternateTcpConnection";
            this.checkBoxUseAlternateTcpConnection.Size = new System.Drawing.Size(191, 17);
            this.checkBoxUseAlternateTcpConnection.TabIndex = 1;
            this.checkBoxUseAlternateTcpConnection.Text = "Use alternate command channel";
            this.checkBoxUseAlternateTcpConnection.UseVisualStyleBackColor = true;
            this.checkBoxUseAlternateTcpConnection.CheckedChanged += new System.EventHandler(this.checkBoxUseAlternateTcpConnection_CheckedChanged);
            // 
            // tabControlSourceConnectionType
            // 
            this.tabControlSourceConnectionType.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabControlSourceConnectionType.Controls.Add(this.tabPageTcp);
            this.tabControlSourceConnectionType.Controls.Add(this.tabPageUdp);
            this.tabControlSourceConnectionType.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControlSourceConnectionType.Location = new System.Drawing.Point(2, 19);
            this.tabControlSourceConnectionType.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.tabControlSourceConnectionType.Name = "tabControlSourceConnectionType";
            this.tabControlSourceConnectionType.SelectedIndex = 0;
            this.tabControlSourceConnectionType.Size = new System.Drawing.Size(214, 88);
            this.tabControlSourceConnectionType.TabIndex = 0;
            this.tabControlSourceConnectionType.SelectedIndexChanged += new System.EventHandler(this.ControlValueChanged);
            // 
            // tabPageTcp
            // 
            this.tabPageTcp.Controls.Add(this.textBoxTcpListeningPort);
            this.tabPageTcp.Controls.Add(this.labelTcpSourceFormat);
            this.tabPageTcp.Controls.Add(this.textBoxTcpConnection);
            this.tabPageTcp.Controls.Add(this.labelTcpSourceSettings);
            this.tabPageTcp.Controls.Add(this.checkBoxTcpSourceIsListener);
            this.tabPageTcp.Location = new System.Drawing.Point(4, 25);
            this.tabPageTcp.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.tabPageTcp.Name = "tabPageTcp";
            this.tabPageTcp.Padding = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.tabPageTcp.Size = new System.Drawing.Size(206, 59);
            this.tabPageTcp.TabIndex = 0;
            this.tabPageTcp.Text = " TCP";
            // 
            // textBoxTcpListeningPort
            // 
            this.textBoxTcpListeningPort.Location = new System.Drawing.Point(12, 17);
            this.textBoxTcpListeningPort.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.textBoxTcpListeningPort.Name = "textBoxTcpListeningPort";
            this.textBoxTcpListeningPort.Size = new System.Drawing.Size(66, 22);
            this.textBoxTcpListeningPort.TabIndex = 1;
            this.textBoxTcpListeningPort.Text = "4712";
            this.toolTip.SetToolTip(this.textBoxTcpListeningPort, "Example: 4713");
            this.textBoxTcpListeningPort.Visible = false;
            this.textBoxTcpListeningPort.TextChanged += new System.EventHandler(this.ControlValueChanged);
            this.textBoxTcpListeningPort.Validating += new System.ComponentModel.CancelEventHandler(this.HandleInt16Validation);
            // 
            // labelTcpSourceFormat
            // 
            this.labelTcpSourceFormat.AutoSize = true;
            this.labelTcpSourceFormat.Location = new System.Drawing.Point(11, 40);
            this.labelTcpSourceFormat.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTcpSourceFormat.Name = "labelTcpSourceFormat";
            this.labelTcpSourceFormat.Size = new System.Drawing.Size(97, 13);
            this.labelTcpSourceFormat.TabIndex = 3;
            this.labelTcpSourceFormat.Text = "Format: host:port";
            // 
            // labelTcpSourceSettings
            // 
            this.labelTcpSourceSettings.AutoSize = true;
            this.labelTcpSourceSettings.Location = new System.Drawing.Point(11, 1);
            this.labelTcpSourceSettings.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTcpSourceSettings.Name = "labelTcpSourceSettings";
            this.labelTcpSourceSettings.Size = new System.Drawing.Size(68, 13);
            this.labelTcpSourceSettings.TabIndex = 0;
            this.labelTcpSourceSettings.Text = "Connect To:";
            // 
            // checkBoxTcpSourceIsListener
            // 
            this.checkBoxTcpSourceIsListener.AutoSize = true;
            this.checkBoxTcpSourceIsListener.Location = new System.Drawing.Point(112, 0);
            this.checkBoxTcpSourceIsListener.Name = "checkBoxTcpSourceIsListener";
            this.checkBoxTcpSourceIsListener.Size = new System.Drawing.Size(77, 17);
            this.checkBoxTcpSourceIsListener.TabIndex = 4;
            this.checkBoxTcpSourceIsListener.Text = "Is Listener";
            this.checkBoxTcpSourceIsListener.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxTcpSourceIsListener.UseVisualStyleBackColor = true;
            this.checkBoxTcpSourceIsListener.CheckedChanged += new System.EventHandler(this.checkBoxTcpSourceIsListener_CheckedChanged);
            // 
            // tabPageUdp
            // 
            this.tabPageUdp.Controls.Add(this.textBoxUdpListeningPort);
            this.tabPageUdp.Controls.Add(this.labelUdpListeningPort);
            this.tabPageUdp.Location = new System.Drawing.Point(4, 25);
            this.tabPageUdp.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.tabPageUdp.Name = "tabPageUdp";
            this.tabPageUdp.Padding = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.tabPageUdp.Size = new System.Drawing.Size(206, 59);
            this.tabPageUdp.TabIndex = 1;
            this.tabPageUdp.Text = " UDP";
            this.tabPageUdp.UseVisualStyleBackColor = true;
            // 
            // labelUdpListeningPort
            // 
            this.labelUdpListeningPort.AutoSize = true;
            this.labelUdpListeningPort.Location = new System.Drawing.Point(22, 8);
            this.labelUdpListeningPort.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelUdpListeningPort.Name = "labelUdpListeningPort";
            this.labelUdpListeningPort.Size = new System.Drawing.Size(81, 13);
            this.labelUdpListeningPort.TabIndex = 0;
            this.labelUdpListeningPort.Text = "Listening Port:";
            // 
            // groupBoxProxyDestinations
            // 
            this.groupBoxProxyDestinations.Controls.Add(this.buttonApply);
            this.groupBoxProxyDestinations.Controls.Add(this.pictureBoxGreen);
            this.groupBoxProxyDestinations.Controls.Add(this.pictureBoxRed);
            this.groupBoxProxyDestinations.Controls.Add(this.pictureBoxYellow);
            this.groupBoxProxyDestinations.Controls.Add(this.pictureBoxGray);
            this.groupBoxProxyDestinations.Controls.Add(this.checkBoxEnabled);
            this.groupBoxProxyDestinations.Controls.Add(this.tabControlProxyDestinationType);
            this.groupBoxProxyDestinations.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBoxProxyDestinations.Location = new System.Drawing.Point(427, 0);
            this.groupBoxProxyDestinations.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.groupBoxProxyDestinations.Name = "groupBoxProxyDestinations";
            this.groupBoxProxyDestinations.Padding = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.groupBoxProxyDestinations.Size = new System.Drawing.Size(218, 508);
            this.groupBoxProxyDestinations.TabIndex = 2;
            this.groupBoxProxyDestinations.TabStop = false;
            this.groupBoxProxyDestinations.Text = "Proxy Destination(s)";
            // 
            // buttonApply
            // 
            this.buttonApply.Enabled = false;
            this.buttonApply.Location = new System.Drawing.Point(86, 181);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(75, 23);
            this.buttonApply.TabIndex = 3;
            this.buttonApply.Text = "Apply";
            this.toolTip.SetToolTip(this.buttonApply, "Apply changes for this proxy connection...");
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // pictureBoxGreen
            // 
            this.pictureBoxGreen.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxGreen.Image")));
            this.pictureBoxGreen.Location = new System.Drawing.Point(182, 177);
            this.pictureBoxGreen.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.pictureBoxGreen.Name = "pictureBoxGreen";
            this.pictureBoxGreen.Size = new System.Drawing.Size(30, 31);
            this.pictureBoxGreen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxGreen.TabIndex = 7;
            this.pictureBoxGreen.TabStop = false;
            this.toolTip.SetToolTip(this.pictureBoxGreen, "Connected to source device and receiving data...");
            this.pictureBoxGreen.Visible = false;
            // 
            // pictureBoxRed
            // 
            this.pictureBoxRed.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxRed.Image")));
            this.pictureBoxRed.Location = new System.Drawing.Point(182, 177);
            this.pictureBoxRed.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.pictureBoxRed.Name = "pictureBoxRed";
            this.pictureBoxRed.Size = new System.Drawing.Size(30, 31);
            this.pictureBoxRed.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxRed.TabIndex = 6;
            this.pictureBoxRed.TabStop = false;
            this.toolTip.SetToolTip(this.pictureBoxRed, "Disconnected from source device...");
            this.pictureBoxRed.Visible = false;
            // 
            // pictureBoxYellow
            // 
            this.pictureBoxYellow.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxYellow.Image")));
            this.pictureBoxYellow.Location = new System.Drawing.Point(182, 177);
            this.pictureBoxYellow.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.pictureBoxYellow.Name = "pictureBoxYellow";
            this.pictureBoxYellow.Size = new System.Drawing.Size(30, 31);
            this.pictureBoxYellow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxYellow.TabIndex = 5;
            this.pictureBoxYellow.TabStop = false;
            this.toolTip.SetToolTip(this.pictureBoxYellow, "Connected to source device, waiting for data...");
            this.pictureBoxYellow.Visible = false;
            // 
            // pictureBoxGray
            // 
            this.pictureBoxGray.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxGray.Image")));
            this.pictureBoxGray.Location = new System.Drawing.Point(182, 177);
            this.pictureBoxGray.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.pictureBoxGray.Name = "pictureBoxGray";
            this.pictureBoxGray.Size = new System.Drawing.Size(30, 31);
            this.pictureBoxGray.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxGray.TabIndex = 4;
            this.pictureBoxGray.TabStop = false;
            this.toolTip.SetToolTip(this.pictureBoxGray, "Connection is not enabled.");
            // 
            // checkBoxEnabled
            // 
            this.checkBoxEnabled.AutoSize = true;
            this.checkBoxEnabled.Location = new System.Drawing.Point(7, 183);
            this.checkBoxEnabled.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.checkBoxEnabled.Name = "checkBoxEnabled";
            this.checkBoxEnabled.Size = new System.Drawing.Size(68, 17);
            this.checkBoxEnabled.TabIndex = 2;
            this.checkBoxEnabled.Text = "Enabled";
            this.checkBoxEnabled.UseVisualStyleBackColor = true;
            this.checkBoxEnabled.CheckedChanged += new System.EventHandler(this.checkBoxEnabled_CheckedChanged);
            // 
            // tabControlProxyDestinationType
            // 
            this.tabControlProxyDestinationType.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabControlProxyDestinationType.Controls.Add(this.tabTcpProxyPoint);
            this.tabControlProxyDestinationType.Controls.Add(this.tabUdpRebroacasts);
            this.tabControlProxyDestinationType.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControlProxyDestinationType.Location = new System.Drawing.Point(2, 19);
            this.tabControlProxyDestinationType.Margin = new System.Windows.Forms.Padding(0);
            this.tabControlProxyDestinationType.Name = "tabControlProxyDestinationType";
            this.tabControlProxyDestinationType.SelectedIndex = 0;
            this.tabControlProxyDestinationType.Size = new System.Drawing.Size(214, 154);
            this.tabControlProxyDestinationType.TabIndex = 0;
            this.tabControlProxyDestinationType.SelectedIndexChanged += new System.EventHandler(this.ControlValueChanged);
            // 
            // tabTcpProxyPoint
            // 
            this.tabTcpProxyPoint.Controls.Add(this.labelTcpProxyFormat);
            this.tabTcpProxyPoint.Controls.Add(this.textBoxTcpPublisherListeningPort);
            this.tabTcpProxyPoint.Controls.Add(this.textBoxTcpClientPublisherConnection);
            this.tabTcpProxyPoint.Controls.Add(this.labelTcpProxySettings);
            this.tabTcpProxyPoint.Controls.Add(this.checkBoxTcpProxyIsListener);
            this.tabTcpProxyPoint.Location = new System.Drawing.Point(4, 25);
            this.tabTcpProxyPoint.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.tabTcpProxyPoint.Name = "tabTcpProxyPoint";
            this.tabTcpProxyPoint.Padding = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.tabTcpProxyPoint.Size = new System.Drawing.Size(206, 125);
            this.tabTcpProxyPoint.TabIndex = 0;
            this.tabTcpProxyPoint.Text = "TCP  Proxy Point";
            this.tabTcpProxyPoint.UseVisualStyleBackColor = true;
            // 
            // labelTcpProxyFormat
            // 
            this.labelTcpProxyFormat.AutoSize = true;
            this.labelTcpProxyFormat.Location = new System.Drawing.Point(16, 49);
            this.labelTcpProxyFormat.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTcpProxyFormat.Name = "labelTcpProxyFormat";
            this.labelTcpProxyFormat.Size = new System.Drawing.Size(71, 13);
            this.labelTcpProxyFormat.TabIndex = 3;
            this.labelTcpProxyFormat.Text = "Format: port";
            // 
            // textBoxTcpClientPublisherConnection
            // 
            this.textBoxTcpClientPublisherConnection.Location = new System.Drawing.Point(17, 26);
            this.textBoxTcpClientPublisherConnection.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.textBoxTcpClientPublisherConnection.Name = "textBoxTcpClientPublisherConnection";
            this.textBoxTcpClientPublisherConnection.Size = new System.Drawing.Size(170, 22);
            this.textBoxTcpClientPublisherConnection.TabIndex = 2;
            this.textBoxTcpClientPublisherConnection.Text = "localhost:4712";
            this.toolTip.SetToolTip(this.textBoxTcpClientPublisherConnection, "Example: 192.168.1.10:4712");
            this.textBoxTcpClientPublisherConnection.Visible = false;
            this.textBoxTcpClientPublisherConnection.TextChanged += new System.EventHandler(this.ControlValueChanged);
            this.textBoxTcpClientPublisherConnection.Validating += new System.ComponentModel.CancelEventHandler(this.HandleHostPortValidation);
            // 
            // labelTcpProxySettings
            // 
            this.labelTcpProxySettings.AutoSize = true;
            this.labelTcpProxySettings.Location = new System.Drawing.Point(15, 10);
            this.labelTcpProxySettings.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTcpProxySettings.Name = "labelTcpProxySettings";
            this.labelTcpProxySettings.Size = new System.Drawing.Size(81, 13);
            this.labelTcpProxySettings.TabIndex = 0;
            this.labelTcpProxySettings.Text = "Listening Port:";
            // 
            // checkBoxTcpProxyIsListener
            // 
            this.checkBoxTcpProxyIsListener.AutoSize = true;
            this.checkBoxTcpProxyIsListener.Checked = true;
            this.checkBoxTcpProxyIsListener.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxTcpProxyIsListener.Location = new System.Drawing.Point(116, 9);
            this.checkBoxTcpProxyIsListener.Name = "checkBoxTcpProxyIsListener";
            this.checkBoxTcpProxyIsListener.Size = new System.Drawing.Size(77, 17);
            this.checkBoxTcpProxyIsListener.TabIndex = 4;
            this.checkBoxTcpProxyIsListener.Text = "Is Listener";
            this.checkBoxTcpProxyIsListener.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxTcpProxyIsListener.UseVisualStyleBackColor = true;
            this.checkBoxTcpProxyIsListener.CheckedChanged += new System.EventHandler(this.checkBoxTcpProxyIsListener_CheckedChanged);
            // 
            // tabUdpRebroacasts
            // 
            this.tabUdpRebroacasts.Controls.Add(this.flowLayoutPanelUdpDestinations);
            this.tabUdpRebroacasts.Location = new System.Drawing.Point(4, 25);
            this.tabUdpRebroacasts.Margin = new System.Windows.Forms.Padding(0);
            this.tabUdpRebroacasts.Name = "tabUdpRebroacasts";
            this.tabUdpRebroacasts.Size = new System.Drawing.Size(206, 125);
            this.tabUdpRebroacasts.TabIndex = 1;
            this.tabUdpRebroacasts.Text = "UDP Rebroadcasts";
            this.tabUdpRebroacasts.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanelUdpDestinations
            // 
            this.flowLayoutPanelUdpDestinations.AutoScroll = true;
            this.flowLayoutPanelUdpDestinations.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanelUdpDestinations.Controls.Add(this.labelUdpDestinationFormat);
            this.flowLayoutPanelUdpDestinations.Controls.Add(this.buttonAddUdpDestination);
            this.flowLayoutPanelUdpDestinations.Controls.Add(this.buttonRemoveUdpDestination);
            this.flowLayoutPanelUdpDestinations.Controls.Add(this.textBoxUdpRebroadcast0);
            this.flowLayoutPanelUdpDestinations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelUdpDestinations.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanelUdpDestinations.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanelUdpDestinations.Name = "flowLayoutPanelUdpDestinations";
            this.flowLayoutPanelUdpDestinations.Size = new System.Drawing.Size(206, 125);
            this.flowLayoutPanelUdpDestinations.TabIndex = 0;
            // 
            // labelUdpDestinationFormat
            // 
            this.labelUdpDestinationFormat.AutoSize = true;
            this.labelUdpDestinationFormat.Location = new System.Drawing.Point(10, 4);
            this.labelUdpDestinationFormat.Margin = new System.Windows.Forms.Padding(10, 4, 2, 1);
            this.labelUdpDestinationFormat.Name = "labelUdpDestinationFormat";
            this.labelUdpDestinationFormat.Size = new System.Drawing.Size(97, 13);
            this.labelUdpDestinationFormat.TabIndex = 2;
            this.labelUdpDestinationFormat.Text = "Format: host:port";
            // 
            // buttonAddUdpDestination
            // 
            this.buttonAddUdpDestination.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddUdpDestination.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.buttonAddUdpDestination.Location = new System.Drawing.Point(116, 0);
            this.buttonAddUdpDestination.Margin = new System.Windows.Forms.Padding(7, 0, 2, 1);
            this.buttonAddUdpDestination.Name = "buttonAddUdpDestination";
            this.buttonAddUdpDestination.Size = new System.Drawing.Size(24, 22);
            this.buttonAddUdpDestination.TabIndex = 0;
            this.buttonAddUdpDestination.Text = "+";
            this.buttonAddUdpDestination.UseVisualStyleBackColor = true;
            this.buttonAddUdpDestination.Click += new System.EventHandler(this.buttonAddUdpDestination_Click);
            // 
            // buttonRemoveUdpDestination
            // 
            this.buttonRemoveUdpDestination.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemoveUdpDestination.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRemoveUdpDestination.Location = new System.Drawing.Point(144, 0);
            this.buttonRemoveUdpDestination.Margin = new System.Windows.Forms.Padding(2, 0, 2, 1);
            this.buttonRemoveUdpDestination.Name = "buttonRemoveUdpDestination";
            this.buttonRemoveUdpDestination.Size = new System.Drawing.Size(24, 22);
            this.buttonRemoveUdpDestination.TabIndex = 1;
            this.buttonRemoveUdpDestination.Text = "-";
            this.buttonRemoveUdpDestination.UseVisualStyleBackColor = true;
            this.buttonRemoveUdpDestination.Click += new System.EventHandler(this.buttonRemoveUdpDestination_Click);
            // 
            // textBoxConnectionStatus
            // 
            this.textBoxConnectionStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxConnectionStatus.BackColor = System.Drawing.Color.Black;
            this.textBoxConnectionStatus.CausesValidation = false;
            this.textBoxConnectionStatus.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxConnectionStatus.ForeColor = System.Drawing.Color.White;
            this.textBoxConnectionStatus.Location = new System.Drawing.Point(0, 331);
            this.textBoxConnectionStatus.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.textBoxConnectionStatus.Multiline = true;
            this.textBoxConnectionStatus.Name = "textBoxConnectionStatus";
            this.textBoxConnectionStatus.ReadOnly = true;
            this.textBoxConnectionStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxConnectionStatus.Size = new System.Drawing.Size(645, 224);
            this.textBoxConnectionStatus.TabIndex = 1;
            this.textBoxConnectionStatus.Text = "Status Text";
            // 
            // comboBoxProtocol
            // 
            this.comboBoxProtocol.FormattingEnabled = true;
            this.comboBoxProtocol.Location = new System.Drawing.Point(6, 19);
            this.comboBoxProtocol.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxProtocol.Name = "comboBoxProtocol";
            this.comboBoxProtocol.Size = new System.Drawing.Size(192, 21);
            this.comboBoxProtocol.TabIndex = 2;
            this.comboBoxProtocol.SelectedIndexChanged += new System.EventHandler(this.comboBoxProtocol_SelectedIndexChanged);
            // 
            // propertyGridProtocolParameters
            // 
            this.propertyGridProtocolParameters.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.propertyGridProtocolParameters.Location = new System.Drawing.Point(6, 44);
            this.propertyGridProtocolParameters.Margin = new System.Windows.Forms.Padding(4);
            this.propertyGridProtocolParameters.Name = "propertyGridProtocolParameters";
            this.propertyGridProtocolParameters.Size = new System.Drawing.Size(192, 161);
            this.propertyGridProtocolParameters.TabIndex = 3;
            this.propertyGridProtocolParameters.ToolbarVisible = false;
            // 
            // groupBoxProtocolParameters
            // 
            this.groupBoxProtocolParameters.Controls.Add(this.propertyGridProtocolParameters);
            this.groupBoxProtocolParameters.Controls.Add(this.comboBoxProtocol);
            this.groupBoxProtocolParameters.Location = new System.Drawing.Point(219, 0);
            this.groupBoxProtocolParameters.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.groupBoxProtocolParameters.Name = "groupBoxProtocolParameters";
            this.groupBoxProtocolParameters.Padding = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.groupBoxProtocolParameters.Size = new System.Drawing.Size(205, 258);
            this.groupBoxProtocolParameters.TabIndex = 1;
            this.groupBoxProtocolParameters.TabStop = false;
            this.groupBoxProtocolParameters.Text = "Protocol Parameters";
            // 
            // textBoxConnectionString
            // 
            this.textBoxConnectionString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxConnectionString.Location = new System.Drawing.Point(0, 260);
            this.textBoxConnectionString.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.textBoxConnectionString.Multiline = true;
            this.textBoxConnectionString.Name = "textBoxConnectionString";
            this.textBoxConnectionString.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxConnectionString.Size = new System.Drawing.Size(645, 71);
            this.textBoxConnectionString.TabIndex = 2;
            this.textBoxConnectionString.TextChanged += new System.EventHandler(this.textBoxConnectionString_TextChanged);
            // 
            // panelCenterGroups
            // 
            this.panelCenterGroups.Controls.Add(this.groupBoxSourceConnection);
            this.panelCenterGroups.Controls.Add(this.groupBoxProxyDestinations);
            this.panelCenterGroups.Controls.Add(this.groupBoxProtocolParameters);
            this.panelCenterGroups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCenterGroups.Location = new System.Drawing.Point(0, 47);
            this.panelCenterGroups.Name = "panelCenterGroups";
            this.panelCenterGroups.Size = new System.Drawing.Size(645, 508);
            this.panelCenterGroups.TabIndex = 1;
            // 
            // toolTip
            // 
            this.toolTip.AutomaticDelay = 50;
            this.toolTip.AutoPopDelay = 5000;
            this.toolTip.InitialDelay = 50;
            this.toolTip.ReshowDelay = 10;
            this.toolTip.ShowAlways = true;
            // 
            // toolTipEx
            // 
            this.toolTipEx.AutomaticDelay = 10;
            this.toolTipEx.AutoPopDelay = 5000;
            this.toolTipEx.BackColor = System.Drawing.SystemColors.Window;
            this.toolTipEx.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolTipEx.ForeColor = System.Drawing.SystemColors.WindowText;
            this.toolTipEx.InitialDelay = 10;
            this.toolTipEx.OwnerDraw = true;
            this.toolTipEx.ReshowDelay = 2;
            this.toolTipEx.ShowAlways = true;
            // 
            // ProxyConnectionEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxConnectionString);
            this.Controls.Add(this.textBoxConnectionStatus);
            this.Controls.Add(this.panelCenterGroups);
            this.Controls.Add(this.groupBoxName);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.Name = "ProxyConnectionEditor";
            this.Size = new System.Drawing.Size(645, 555);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBoxName.ResumeLayout(false);
            this.groupBoxName.PerformLayout();
            this.groupBoxSourceConnection.ResumeLayout(false);
            this.groupBoxSourceConnection.PerformLayout();
            this.tabControlSourceConnectionType.ResumeLayout(false);
            this.tabPageTcp.ResumeLayout(false);
            this.tabPageTcp.PerformLayout();
            this.tabPageUdp.ResumeLayout(false);
            this.tabPageUdp.PerformLayout();
            this.groupBoxProxyDestinations.ResumeLayout(false);
            this.groupBoxProxyDestinations.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxYellow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGray)).EndInit();
            this.tabControlProxyDestinationType.ResumeLayout(false);
            this.tabTcpProxyPoint.ResumeLayout(false);
            this.tabTcpProxyPoint.PerformLayout();
            this.tabUdpRebroacasts.ResumeLayout(false);
            this.flowLayoutPanelUdpDestinations.ResumeLayout(false);
            this.flowLayoutPanelUdpDestinations.PerformLayout();
            this.groupBoxProtocolParameters.ResumeLayout(false);
            this.panelCenterGroups.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.GroupBox groupBoxName;
        private System.Windows.Forms.GroupBox groupBoxProxyDestinations;
        private System.Windows.Forms.TextBox textBoxConnectionStatus;
        private System.Windows.Forms.TabControl tabControlProxyDestinationType;
        private System.Windows.Forms.TabPage tabTcpProxyPoint;
        private System.Windows.Forms.Label labelTcpProxySettings;
        private System.Windows.Forms.TextBox textBoxTcpPublisherListeningPort;
        private System.Windows.Forms.TabPage tabUdpRebroacasts;
        private System.Windows.Forms.GroupBox groupBoxSourceConnection;
        private System.Windows.Forms.TabControl tabControlSourceConnectionType;
        private System.Windows.Forms.TabPage tabPageTcp;
        private System.Windows.Forms.TabPage tabPageUdp;
        private System.Windows.Forms.Label labelAltermateTcpConnectionFormat;
        private System.Windows.Forms.Label labelAlternateTcpConnection;
        private System.Windows.Forms.TextBox textBoxAlternateTcpConnection;
        private System.Windows.Forms.CheckBox checkBoxUseAlternateTcpConnection;
        private System.Windows.Forms.Label labelTcpSourceFormat;
        private System.Windows.Forms.TextBox textBoxTcpConnection;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelUdpDestinations;
        private System.Windows.Forms.Label labelUdpDestinationFormat;
        private System.Windows.Forms.Button buttonAddUdpDestination;
        private System.Windows.Forms.Button buttonRemoveUdpDestination;
        private System.Windows.Forms.TextBox textBoxUdpRebroadcast0;
        private System.Windows.Forms.Label labelUdpListeningPort;
        private System.Windows.Forms.TextBox textBoxUdpListeningPort;
        private System.Windows.Forms.ComboBox comboBoxProtocol;
        private System.Windows.Forms.GroupBox groupBoxProtocolParameters;
        private System.Windows.Forms.Label labelIDCode;
        private System.Windows.Forms.TextBox textBoxIDCode;
        private System.Windows.Forms.PropertyGrid propertyGridProtocolParameters;
        private StreamSplitter.ToolTipEx toolTipEx;
        private System.Windows.Forms.PictureBox pictureBoxGray;
        private System.Windows.Forms.PictureBox pictureBoxGreen;
        private System.Windows.Forms.PictureBox pictureBoxRed;
        private System.Windows.Forms.PictureBox pictureBoxYellow;
        private System.Windows.Forms.TextBox textBoxConnectionString;
        private System.Windows.Forms.Panel panelCenterGroups;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.ToolTip toolTip;

        /// <summary>
        /// Button control used to apply changes to service.
        /// </summary>
        public System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.CheckBox checkBoxTcpSourceIsListener;
        private System.Windows.Forms.TextBox textBoxTcpClientPublisherConnection;
        private System.Windows.Forms.TextBox textBoxTcpListeningPort;
        private System.Windows.Forms.Label labelTcpSourceSettings;
        private System.Windows.Forms.CheckBox checkBoxTcpProxyIsListener;
        private System.Windows.Forms.Label labelTcpProxyFormat;
        internal System.Windows.Forms.CheckBox checkBoxEnabled;
    }
}
