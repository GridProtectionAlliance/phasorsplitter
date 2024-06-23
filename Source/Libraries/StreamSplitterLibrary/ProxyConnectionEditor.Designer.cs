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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProxyConnectionEditor));
            errorProvider = new System.Windows.Forms.ErrorProvider(components);
            groupBoxName = new System.Windows.Forms.GroupBox();
            textBoxName = new System.Windows.Forms.TextBox();
            textBoxTcpPublisherListeningPort = new System.Windows.Forms.TextBox();
            textBoxUdpListeningPort = new System.Windows.Forms.TextBox();
            textBoxIDCode = new System.Windows.Forms.TextBox();
            textBoxAlternateTcpConnection = new System.Windows.Forms.TextBox();
            textBoxTcpConnection = new System.Windows.Forms.TextBox();
            textBoxUdpRebroadcast0 = new System.Windows.Forms.TextBox();
            groupBoxSourceConnection = new System.Windows.Forms.GroupBox();
            labelIDCode = new System.Windows.Forms.Label();
            labelAltermateTcpConnectionFormat = new System.Windows.Forms.Label();
            labelAlternateTcpConnection = new System.Windows.Forms.Label();
            checkBoxUseAlternateTcpConnection = new System.Windows.Forms.CheckBox();
            tabControlSourceConnectionType = new System.Windows.Forms.TabControl();
            tabPageTcp = new System.Windows.Forms.TabPage();
            textBoxTcpListeningPort = new System.Windows.Forms.TextBox();
            labelTcpSourceFormat = new System.Windows.Forms.Label();
            labelTcpSourceSettings = new System.Windows.Forms.Label();
            checkBoxTcpSourceIsListener = new System.Windows.Forms.CheckBox();
            tabPageUdp = new System.Windows.Forms.TabPage();
            labelUdpListeningPort = new System.Windows.Forms.Label();
            groupBoxProxyDestinations = new System.Windows.Forms.GroupBox();
            buttonApply = new System.Windows.Forms.Button();
            pictureBoxGreen = new System.Windows.Forms.PictureBox();
            pictureBoxRed = new System.Windows.Forms.PictureBox();
            pictureBoxYellow = new System.Windows.Forms.PictureBox();
            pictureBoxGray = new System.Windows.Forms.PictureBox();
            checkBoxEnabled = new System.Windows.Forms.CheckBox();
            tabControlProxyDestinationType = new System.Windows.Forms.TabControl();
            tabTcpProxyPoint = new System.Windows.Forms.TabPage();
            labelTcpProxyFormat = new System.Windows.Forms.Label();
            textBoxTcpClientPublisherConnection = new System.Windows.Forms.TextBox();
            labelTcpProxySettings = new System.Windows.Forms.Label();
            checkBoxTcpProxyIsListener = new System.Windows.Forms.CheckBox();
            tabUdpRebroacasts = new System.Windows.Forms.TabPage();
            flowLayoutPanelUdpDestinations = new System.Windows.Forms.FlowLayoutPanel();
            labelUdpDestinationFormat = new System.Windows.Forms.Label();
            buttonAddUdpDestination = new System.Windows.Forms.Button();
            buttonRemoveUdpDestination = new System.Windows.Forms.Button();
            textBoxConnectionStatus = new System.Windows.Forms.TextBox();
            comboBoxProtocol = new System.Windows.Forms.ComboBox();
            propertyGridProtocolParameters = new System.Windows.Forms.PropertyGrid();
            groupBoxProtocolParameters = new System.Windows.Forms.GroupBox();
            textBoxConnectionString = new System.Windows.Forms.TextBox();
            panelCenterGroups = new System.Windows.Forms.Panel();
            toolTip = new System.Windows.Forms.ToolTip(components);
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            groupBoxName.SuspendLayout();
            groupBoxSourceConnection.SuspendLayout();
            tabControlSourceConnectionType.SuspendLayout();
            tabPageTcp.SuspendLayout();
            tabPageUdp.SuspendLayout();
            groupBoxProxyDestinations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxGreen).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxRed).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxYellow).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxGray).BeginInit();
            tabControlProxyDestinationType.SuspendLayout();
            tabTcpProxyPoint.SuspendLayout();
            tabUdpRebroacasts.SuspendLayout();
            flowLayoutPanelUdpDestinations.SuspendLayout();
            groupBoxProtocolParameters.SuspendLayout();
            panelCenterGroups.SuspendLayout();
            SuspendLayout();
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // groupBoxName
            // 
            groupBoxName.Controls.Add(textBoxName);
            groupBoxName.Dock = System.Windows.Forms.DockStyle.Top;
            groupBoxName.Location = new System.Drawing.Point(0, 0);
            groupBoxName.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            groupBoxName.Name = "groupBoxName";
            groupBoxName.Padding = new System.Windows.Forms.Padding(8, 2, 8, 2);
            groupBoxName.Size = new System.Drawing.Size(645, 47);
            groupBoxName.TabIndex = 0;
            groupBoxName.TabStop = false;
            groupBoxName.Text = "Connection Name";
            // 
            // textBoxName
            // 
            textBoxName.Dock = System.Windows.Forms.DockStyle.Fill;
            textBoxName.Location = new System.Drawing.Point(8, 17);
            textBoxName.Margin = new System.Windows.Forms.Padding(5);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new System.Drawing.Size(629, 22);
            textBoxName.TabIndex = 0;
            textBoxName.TextChanged += ControlValueChanged;
            // 
            // textBoxTcpPublisherListeningPort
            // 
            textBoxTcpPublisherListeningPort.Location = new System.Drawing.Point(17, 26);
            textBoxTcpPublisherListeningPort.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            textBoxTcpPublisherListeningPort.Name = "textBoxTcpPublisherListeningPort";
            textBoxTcpPublisherListeningPort.Size = new System.Drawing.Size(66, 22);
            textBoxTcpPublisherListeningPort.TabIndex = 1;
            textBoxTcpPublisherListeningPort.Text = "4712";
            toolTip.SetToolTip(textBoxTcpPublisherListeningPort, "Example: 4713");
            textBoxTcpPublisherListeningPort.TextChanged += ControlValueChanged;
            textBoxTcpPublisherListeningPort.Validating += HandleInt16Validation;
            // 
            // textBoxUdpListeningPort
            // 
            textBoxUdpListeningPort.Location = new System.Drawing.Point(26, 25);
            textBoxUdpListeningPort.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            textBoxUdpListeningPort.Name = "textBoxUdpListeningPort";
            textBoxUdpListeningPort.Size = new System.Drawing.Size(66, 22);
            textBoxUdpListeningPort.TabIndex = 1;
            textBoxUdpListeningPort.Text = "4713";
            toolTip.SetToolTip(textBoxUdpListeningPort, "Example: 4713");
            textBoxUdpListeningPort.TextChanged += ControlValueChanged;
            textBoxUdpListeningPort.Validating += HandleInt16Validation;
            // 
            // textBoxIDCode
            // 
            textBoxIDCode.Location = new System.Drawing.Point(92, 107);
            textBoxIDCode.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            textBoxIDCode.Name = "textBoxIDCode";
            textBoxIDCode.Size = new System.Drawing.Size(66, 22);
            textBoxIDCode.TabIndex = 5;
            textBoxIDCode.Text = "1";
            toolTip.SetToolTip(textBoxIDCode, "Example: 235");
            textBoxIDCode.TextChanged += ControlValueChanged;
            textBoxIDCode.Validating += HandleInt16Validation;
            // 
            // textBoxAlternateTcpConnection
            // 
            textBoxAlternateTcpConnection.Enabled = false;
            textBoxAlternateTcpConnection.Location = new System.Drawing.Point(18, 167);
            textBoxAlternateTcpConnection.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            textBoxAlternateTcpConnection.Name = "textBoxAlternateTcpConnection";
            textBoxAlternateTcpConnection.Size = new System.Drawing.Size(170, 22);
            textBoxAlternateTcpConnection.TabIndex = 3;
            textBoxAlternateTcpConnection.Text = "localhost:4712";
            toolTip.SetToolTip(textBoxAlternateTcpConnection, "Example: 192.168.1.10:4712");
            textBoxAlternateTcpConnection.TextChanged += ControlValueChanged;
            textBoxAlternateTcpConnection.Validating += HandleHostPortValidation;
            // 
            // textBoxTcpConnection
            // 
            textBoxTcpConnection.Location = new System.Drawing.Point(12, 17);
            textBoxTcpConnection.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            textBoxTcpConnection.Name = "textBoxTcpConnection";
            textBoxTcpConnection.Size = new System.Drawing.Size(170, 22);
            textBoxTcpConnection.TabIndex = 2;
            textBoxTcpConnection.Text = "localhost:4712";
            toolTip.SetToolTip(textBoxTcpConnection, "Example: 192.168.1.10:4712");
            textBoxTcpConnection.TextChanged += ControlValueChanged;
            textBoxTcpConnection.Validating += HandleHostPortValidation;
            // 
            // textBoxUdpRebroadcast0
            // 
            textBoxUdpRebroadcast0.Location = new System.Drawing.Point(8, 24);
            textBoxUdpRebroadcast0.Margin = new System.Windows.Forms.Padding(8, 1, 4, 0);
            textBoxUdpRebroadcast0.Name = "textBoxUdpRebroadcast0";
            textBoxUdpRebroadcast0.Size = new System.Drawing.Size(160, 22);
            textBoxUdpRebroadcast0.TabIndex = 3;
            toolTip.SetToolTip(textBoxUdpRebroadcast0, "Example: 233.123.123.123:5000");
            textBoxUdpRebroadcast0.TextChanged += ControlValueChanged;
            textBoxUdpRebroadcast0.Validating += HandleHostPortValidation;
            // 
            // groupBoxSourceConnection
            // 
            groupBoxSourceConnection.Controls.Add(labelIDCode);
            groupBoxSourceConnection.Controls.Add(textBoxIDCode);
            groupBoxSourceConnection.Controls.Add(textBoxAlternateTcpConnection);
            groupBoxSourceConnection.Controls.Add(labelAltermateTcpConnectionFormat);
            groupBoxSourceConnection.Controls.Add(labelAlternateTcpConnection);
            groupBoxSourceConnection.Controls.Add(checkBoxUseAlternateTcpConnection);
            groupBoxSourceConnection.Controls.Add(tabControlSourceConnectionType);
            groupBoxSourceConnection.Dock = System.Windows.Forms.DockStyle.Left;
            groupBoxSourceConnection.Location = new System.Drawing.Point(0, 0);
            groupBoxSourceConnection.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            groupBoxSourceConnection.Name = "groupBoxSourceConnection";
            groupBoxSourceConnection.Padding = new System.Windows.Forms.Padding(2, 4, 2, 4);
            groupBoxSourceConnection.Size = new System.Drawing.Size(218, 398);
            groupBoxSourceConnection.TabIndex = 0;
            groupBoxSourceConnection.TabStop = false;
            groupBoxSourceConnection.Text = "Source Connection";
            // 
            // labelIDCode
            // 
            labelIDCode.AutoSize = true;
            labelIDCode.Location = new System.Drawing.Point(40, 109);
            labelIDCode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            labelIDCode.Name = "labelIDCode";
            labelIDCode.Size = new System.Drawing.Size(51, 13);
            labelIDCode.TabIndex = 4;
            labelIDCode.Text = "ID Code:";
            // 
            // labelAltermateTcpConnectionFormat
            // 
            labelAltermateTcpConnectionFormat.AutoSize = true;
            labelAltermateTcpConnectionFormat.Enabled = false;
            labelAltermateTcpConnectionFormat.Location = new System.Drawing.Point(17, 190);
            labelAltermateTcpConnectionFormat.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            labelAltermateTcpConnectionFormat.Name = "labelAltermateTcpConnectionFormat";
            labelAltermateTcpConnectionFormat.Size = new System.Drawing.Size(97, 13);
            labelAltermateTcpConnectionFormat.TabIndex = 4;
            labelAltermateTcpConnectionFormat.Text = "Format: host:port";
            // 
            // labelAlternateTcpConnection
            // 
            labelAlternateTcpConnection.AutoSize = true;
            labelAlternateTcpConnection.Enabled = false;
            labelAlternateTcpConnection.Location = new System.Drawing.Point(17, 151);
            labelAlternateTcpConnection.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            labelAlternateTcpConnection.Name = "labelAlternateTcpConnection";
            labelAlternateTcpConnection.Size = new System.Drawing.Size(68, 13);
            labelAlternateTcpConnection.TabIndex = 2;
            labelAlternateTcpConnection.Text = "Connect To:";
            // 
            // checkBoxUseAlternateTcpConnection
            // 
            checkBoxUseAlternateTcpConnection.AutoSize = true;
            checkBoxUseAlternateTcpConnection.Location = new System.Drawing.Point(5, 130);
            checkBoxUseAlternateTcpConnection.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            checkBoxUseAlternateTcpConnection.Name = "checkBoxUseAlternateTcpConnection";
            checkBoxUseAlternateTcpConnection.Size = new System.Drawing.Size(191, 17);
            checkBoxUseAlternateTcpConnection.TabIndex = 1;
            checkBoxUseAlternateTcpConnection.Text = "Use alternate command channel";
            checkBoxUseAlternateTcpConnection.UseVisualStyleBackColor = true;
            checkBoxUseAlternateTcpConnection.CheckedChanged += checkBoxUseAlternateTcpConnection_CheckedChanged;
            // 
            // tabControlSourceConnectionType
            // 
            tabControlSourceConnectionType.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            tabControlSourceConnectionType.Controls.Add(tabPageTcp);
            tabControlSourceConnectionType.Controls.Add(tabPageUdp);
            tabControlSourceConnectionType.Dock = System.Windows.Forms.DockStyle.Top;
            tabControlSourceConnectionType.Location = new System.Drawing.Point(2, 19);
            tabControlSourceConnectionType.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            tabControlSourceConnectionType.Name = "tabControlSourceConnectionType";
            tabControlSourceConnectionType.SelectedIndex = 0;
            tabControlSourceConnectionType.Size = new System.Drawing.Size(214, 88);
            tabControlSourceConnectionType.TabIndex = 0;
            tabControlSourceConnectionType.SelectedIndexChanged += ControlValueChanged;
            // 
            // tabPageTcp
            // 
            tabPageTcp.Controls.Add(textBoxTcpListeningPort);
            tabPageTcp.Controls.Add(labelTcpSourceFormat);
            tabPageTcp.Controls.Add(textBoxTcpConnection);
            tabPageTcp.Controls.Add(labelTcpSourceSettings);
            tabPageTcp.Controls.Add(checkBoxTcpSourceIsListener);
            tabPageTcp.Location = new System.Drawing.Point(4, 25);
            tabPageTcp.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            tabPageTcp.Name = "tabPageTcp";
            tabPageTcp.Padding = new System.Windows.Forms.Padding(2, 4, 2, 4);
            tabPageTcp.Size = new System.Drawing.Size(206, 59);
            tabPageTcp.TabIndex = 0;
            tabPageTcp.Text = " TCP";
            // 
            // textBoxTcpListeningPort
            // 
            textBoxTcpListeningPort.Location = new System.Drawing.Point(12, 17);
            textBoxTcpListeningPort.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            textBoxTcpListeningPort.Name = "textBoxTcpListeningPort";
            textBoxTcpListeningPort.Size = new System.Drawing.Size(66, 22);
            textBoxTcpListeningPort.TabIndex = 1;
            textBoxTcpListeningPort.Text = "4712";
            toolTip.SetToolTip(textBoxTcpListeningPort, "Example: 4713");
            textBoxTcpListeningPort.Visible = false;
            textBoxTcpListeningPort.TextChanged += ControlValueChanged;
            textBoxTcpListeningPort.Validating += HandleInt16Validation;
            // 
            // labelTcpSourceFormat
            // 
            labelTcpSourceFormat.AutoSize = true;
            labelTcpSourceFormat.Location = new System.Drawing.Point(11, 40);
            labelTcpSourceFormat.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            labelTcpSourceFormat.Name = "labelTcpSourceFormat";
            labelTcpSourceFormat.Size = new System.Drawing.Size(97, 13);
            labelTcpSourceFormat.TabIndex = 3;
            labelTcpSourceFormat.Text = "Format: host:port";
            // 
            // labelTcpSourceSettings
            // 
            labelTcpSourceSettings.AutoSize = true;
            labelTcpSourceSettings.Location = new System.Drawing.Point(11, 1);
            labelTcpSourceSettings.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            labelTcpSourceSettings.Name = "labelTcpSourceSettings";
            labelTcpSourceSettings.Size = new System.Drawing.Size(68, 13);
            labelTcpSourceSettings.TabIndex = 0;
            labelTcpSourceSettings.Text = "Connect To:";
            // 
            // checkBoxTcpSourceIsListener
            // 
            checkBoxTcpSourceIsListener.AutoSize = true;
            checkBoxTcpSourceIsListener.Location = new System.Drawing.Point(112, 0);
            checkBoxTcpSourceIsListener.Name = "checkBoxTcpSourceIsListener";
            checkBoxTcpSourceIsListener.Size = new System.Drawing.Size(77, 17);
            checkBoxTcpSourceIsListener.TabIndex = 4;
            checkBoxTcpSourceIsListener.Text = "Is Listener";
            checkBoxTcpSourceIsListener.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            checkBoxTcpSourceIsListener.UseVisualStyleBackColor = true;
            checkBoxTcpSourceIsListener.CheckedChanged += checkBoxTcpSourceIsListener_CheckedChanged;
            // 
            // tabPageUdp
            // 
            tabPageUdp.Controls.Add(textBoxUdpListeningPort);
            tabPageUdp.Controls.Add(labelUdpListeningPort);
            tabPageUdp.Location = new System.Drawing.Point(4, 27);
            tabPageUdp.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            tabPageUdp.Name = "tabPageUdp";
            tabPageUdp.Padding = new System.Windows.Forms.Padding(2, 4, 2, 4);
            tabPageUdp.Size = new System.Drawing.Size(206, 57);
            tabPageUdp.TabIndex = 1;
            tabPageUdp.Text = " UDP";
            tabPageUdp.UseVisualStyleBackColor = true;
            // 
            // labelUdpListeningPort
            // 
            labelUdpListeningPort.AutoSize = true;
            labelUdpListeningPort.Location = new System.Drawing.Point(22, 8);
            labelUdpListeningPort.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            labelUdpListeningPort.Name = "labelUdpListeningPort";
            labelUdpListeningPort.Size = new System.Drawing.Size(81, 13);
            labelUdpListeningPort.TabIndex = 0;
            labelUdpListeningPort.Text = "Listening Port:";
            // 
            // groupBoxProxyDestinations
            // 
            groupBoxProxyDestinations.Controls.Add(buttonApply);
            groupBoxProxyDestinations.Controls.Add(pictureBoxGreen);
            groupBoxProxyDestinations.Controls.Add(pictureBoxRed);
            groupBoxProxyDestinations.Controls.Add(pictureBoxYellow);
            groupBoxProxyDestinations.Controls.Add(pictureBoxGray);
            groupBoxProxyDestinations.Controls.Add(checkBoxEnabled);
            groupBoxProxyDestinations.Controls.Add(tabControlProxyDestinationType);
            groupBoxProxyDestinations.Dock = System.Windows.Forms.DockStyle.Right;
            groupBoxProxyDestinations.Location = new System.Drawing.Point(427, 0);
            groupBoxProxyDestinations.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            groupBoxProxyDestinations.Name = "groupBoxProxyDestinations";
            groupBoxProxyDestinations.Padding = new System.Windows.Forms.Padding(2, 4, 2, 4);
            groupBoxProxyDestinations.Size = new System.Drawing.Size(218, 398);
            groupBoxProxyDestinations.TabIndex = 2;
            groupBoxProxyDestinations.TabStop = false;
            groupBoxProxyDestinations.Text = "Proxy Destination(s)";
            // 
            // buttonApply
            // 
            buttonApply.Enabled = false;
            buttonApply.Location = new System.Drawing.Point(86, 181);
            buttonApply.Name = "buttonApply";
            buttonApply.Size = new System.Drawing.Size(75, 23);
            buttonApply.TabIndex = 3;
            buttonApply.Text = "Apply";
            toolTip.SetToolTip(buttonApply, "Apply changes for this proxy connection...");
            buttonApply.UseVisualStyleBackColor = true;
            buttonApply.Click += buttonApply_Click;
            // 
            // pictureBoxGreen
            // 
            pictureBoxGreen.Image = (System.Drawing.Image)resources.GetObject("pictureBoxGreen.Image");
            pictureBoxGreen.Location = new System.Drawing.Point(182, 177);
            pictureBoxGreen.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            pictureBoxGreen.Name = "pictureBoxGreen";
            pictureBoxGreen.Size = new System.Drawing.Size(30, 31);
            pictureBoxGreen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBoxGreen.TabIndex = 7;
            pictureBoxGreen.TabStop = false;
            toolTip.SetToolTip(pictureBoxGreen, "Connected to source device and receiving data...");
            pictureBoxGreen.Visible = false;
            // 
            // pictureBoxRed
            // 
            pictureBoxRed.Image = (System.Drawing.Image)resources.GetObject("pictureBoxRed.Image");
            pictureBoxRed.Location = new System.Drawing.Point(182, 177);
            pictureBoxRed.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            pictureBoxRed.Name = "pictureBoxRed";
            pictureBoxRed.Size = new System.Drawing.Size(30, 31);
            pictureBoxRed.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBoxRed.TabIndex = 6;
            pictureBoxRed.TabStop = false;
            toolTip.SetToolTip(pictureBoxRed, "Disconnected from source device...");
            pictureBoxRed.Visible = false;
            // 
            // pictureBoxYellow
            // 
            pictureBoxYellow.Image = (System.Drawing.Image)resources.GetObject("pictureBoxYellow.Image");
            pictureBoxYellow.Location = new System.Drawing.Point(182, 177);
            pictureBoxYellow.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            pictureBoxYellow.Name = "pictureBoxYellow";
            pictureBoxYellow.Size = new System.Drawing.Size(30, 31);
            pictureBoxYellow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBoxYellow.TabIndex = 5;
            pictureBoxYellow.TabStop = false;
            toolTip.SetToolTip(pictureBoxYellow, "Connected to source device, waiting for data...");
            pictureBoxYellow.Visible = false;
            // 
            // pictureBoxGray
            // 
            pictureBoxGray.Image = (System.Drawing.Image)resources.GetObject("pictureBoxGray.Image");
            pictureBoxGray.Location = new System.Drawing.Point(182, 177);
            pictureBoxGray.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            pictureBoxGray.Name = "pictureBoxGray";
            pictureBoxGray.Size = new System.Drawing.Size(30, 31);
            pictureBoxGray.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBoxGray.TabIndex = 4;
            pictureBoxGray.TabStop = false;
            toolTip.SetToolTip(pictureBoxGray, "Connection is not enabled.");
            // 
            // checkBoxEnabled
            // 
            checkBoxEnabled.AutoSize = true;
            checkBoxEnabled.Location = new System.Drawing.Point(7, 183);
            checkBoxEnabled.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            checkBoxEnabled.Name = "checkBoxEnabled";
            checkBoxEnabled.Size = new System.Drawing.Size(68, 17);
            checkBoxEnabled.TabIndex = 2;
            checkBoxEnabled.Text = "Enabled";
            checkBoxEnabled.UseVisualStyleBackColor = true;
            checkBoxEnabled.CheckedChanged += checkBoxEnabled_CheckedChanged;
            // 
            // tabControlProxyDestinationType
            // 
            tabControlProxyDestinationType.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            tabControlProxyDestinationType.Controls.Add(tabTcpProxyPoint);
            tabControlProxyDestinationType.Controls.Add(tabUdpRebroacasts);
            tabControlProxyDestinationType.Dock = System.Windows.Forms.DockStyle.Top;
            tabControlProxyDestinationType.Location = new System.Drawing.Point(2, 19);
            tabControlProxyDestinationType.Margin = new System.Windows.Forms.Padding(0);
            tabControlProxyDestinationType.Name = "tabControlProxyDestinationType";
            tabControlProxyDestinationType.SelectedIndex = 0;
            tabControlProxyDestinationType.Size = new System.Drawing.Size(214, 154);
            tabControlProxyDestinationType.TabIndex = 0;
            tabControlProxyDestinationType.SelectedIndexChanged += ControlValueChanged;
            // 
            // tabTcpProxyPoint
            // 
            tabTcpProxyPoint.Controls.Add(labelTcpProxyFormat);
            tabTcpProxyPoint.Controls.Add(textBoxTcpPublisherListeningPort);
            tabTcpProxyPoint.Controls.Add(textBoxTcpClientPublisherConnection);
            tabTcpProxyPoint.Controls.Add(labelTcpProxySettings);
            tabTcpProxyPoint.Controls.Add(checkBoxTcpProxyIsListener);
            tabTcpProxyPoint.Location = new System.Drawing.Point(4, 25);
            tabTcpProxyPoint.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            tabTcpProxyPoint.Name = "tabTcpProxyPoint";
            tabTcpProxyPoint.Padding = new System.Windows.Forms.Padding(2, 4, 2, 4);
            tabTcpProxyPoint.Size = new System.Drawing.Size(206, 125);
            tabTcpProxyPoint.TabIndex = 0;
            tabTcpProxyPoint.Text = "TCP  Proxy Point";
            tabTcpProxyPoint.UseVisualStyleBackColor = true;
            // 
            // labelTcpProxyFormat
            // 
            labelTcpProxyFormat.AutoSize = true;
            labelTcpProxyFormat.Location = new System.Drawing.Point(16, 49);
            labelTcpProxyFormat.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            labelTcpProxyFormat.Name = "labelTcpProxyFormat";
            labelTcpProxyFormat.Size = new System.Drawing.Size(71, 13);
            labelTcpProxyFormat.TabIndex = 3;
            labelTcpProxyFormat.Text = "Format: port";
            // 
            // textBoxTcpClientPublisherConnection
            // 
            textBoxTcpClientPublisherConnection.Location = new System.Drawing.Point(17, 26);
            textBoxTcpClientPublisherConnection.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            textBoxTcpClientPublisherConnection.Name = "textBoxTcpClientPublisherConnection";
            textBoxTcpClientPublisherConnection.Size = new System.Drawing.Size(170, 22);
            textBoxTcpClientPublisherConnection.TabIndex = 2;
            textBoxTcpClientPublisherConnection.Text = "localhost:4712";
            toolTip.SetToolTip(textBoxTcpClientPublisherConnection, "Example: 192.168.1.10:4712");
            textBoxTcpClientPublisherConnection.Visible = false;
            textBoxTcpClientPublisherConnection.TextChanged += ControlValueChanged;
            textBoxTcpClientPublisherConnection.Validating += HandleHostPortValidation;
            // 
            // labelTcpProxySettings
            // 
            labelTcpProxySettings.AutoSize = true;
            labelTcpProxySettings.Location = new System.Drawing.Point(15, 10);
            labelTcpProxySettings.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            labelTcpProxySettings.Name = "labelTcpProxySettings";
            labelTcpProxySettings.Size = new System.Drawing.Size(81, 13);
            labelTcpProxySettings.TabIndex = 0;
            labelTcpProxySettings.Text = "Listening Port:";
            // 
            // checkBoxTcpProxyIsListener
            // 
            checkBoxTcpProxyIsListener.AutoSize = true;
            checkBoxTcpProxyIsListener.Checked = true;
            checkBoxTcpProxyIsListener.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxTcpProxyIsListener.Location = new System.Drawing.Point(116, 9);
            checkBoxTcpProxyIsListener.Name = "checkBoxTcpProxyIsListener";
            checkBoxTcpProxyIsListener.Size = new System.Drawing.Size(77, 17);
            checkBoxTcpProxyIsListener.TabIndex = 4;
            checkBoxTcpProxyIsListener.Text = "Is Listener";
            checkBoxTcpProxyIsListener.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            checkBoxTcpProxyIsListener.UseVisualStyleBackColor = true;
            checkBoxTcpProxyIsListener.CheckedChanged += checkBoxTcpProxyIsListener_CheckedChanged;
            // 
            // tabUdpRebroacasts
            // 
            tabUdpRebroacasts.Controls.Add(flowLayoutPanelUdpDestinations);
            tabUdpRebroacasts.Location = new System.Drawing.Point(4, 27);
            tabUdpRebroacasts.Margin = new System.Windows.Forms.Padding(0);
            tabUdpRebroacasts.Name = "tabUdpRebroacasts";
            tabUdpRebroacasts.Size = new System.Drawing.Size(206, 123);
            tabUdpRebroacasts.TabIndex = 1;
            tabUdpRebroacasts.Text = "UDP Rebroadcasts";
            tabUdpRebroacasts.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanelUdpDestinations
            // 
            flowLayoutPanelUdpDestinations.AutoScroll = true;
            flowLayoutPanelUdpDestinations.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            flowLayoutPanelUdpDestinations.Controls.Add(labelUdpDestinationFormat);
            flowLayoutPanelUdpDestinations.Controls.Add(buttonAddUdpDestination);
            flowLayoutPanelUdpDestinations.Controls.Add(buttonRemoveUdpDestination);
            flowLayoutPanelUdpDestinations.Controls.Add(textBoxUdpRebroadcast0);
            flowLayoutPanelUdpDestinations.Dock = System.Windows.Forms.DockStyle.Fill;
            flowLayoutPanelUdpDestinations.Location = new System.Drawing.Point(0, 0);
            flowLayoutPanelUdpDestinations.Margin = new System.Windows.Forms.Padding(0);
            flowLayoutPanelUdpDestinations.Name = "flowLayoutPanelUdpDestinations";
            flowLayoutPanelUdpDestinations.Size = new System.Drawing.Size(206, 123);
            flowLayoutPanelUdpDestinations.TabIndex = 0;
            // 
            // labelUdpDestinationFormat
            // 
            labelUdpDestinationFormat.AutoSize = true;
            labelUdpDestinationFormat.Location = new System.Drawing.Point(10, 4);
            labelUdpDestinationFormat.Margin = new System.Windows.Forms.Padding(10, 4, 2, 1);
            labelUdpDestinationFormat.Name = "labelUdpDestinationFormat";
            labelUdpDestinationFormat.Size = new System.Drawing.Size(97, 13);
            labelUdpDestinationFormat.TabIndex = 2;
            labelUdpDestinationFormat.Text = "Format: host:port";
            // 
            // buttonAddUdpDestination
            // 
            buttonAddUdpDestination.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            buttonAddUdpDestination.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            buttonAddUdpDestination.Location = new System.Drawing.Point(116, 0);
            buttonAddUdpDestination.Margin = new System.Windows.Forms.Padding(7, 0, 2, 1);
            buttonAddUdpDestination.Name = "buttonAddUdpDestination";
            buttonAddUdpDestination.Size = new System.Drawing.Size(24, 22);
            buttonAddUdpDestination.TabIndex = 0;
            buttonAddUdpDestination.Text = "+";
            buttonAddUdpDestination.UseVisualStyleBackColor = true;
            buttonAddUdpDestination.Click += buttonAddUdpDestination_Click;
            // 
            // buttonRemoveUdpDestination
            // 
            buttonRemoveUdpDestination.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            buttonRemoveUdpDestination.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            buttonRemoveUdpDestination.Location = new System.Drawing.Point(144, 0);
            buttonRemoveUdpDestination.Margin = new System.Windows.Forms.Padding(2, 0, 2, 1);
            buttonRemoveUdpDestination.Name = "buttonRemoveUdpDestination";
            buttonRemoveUdpDestination.Size = new System.Drawing.Size(24, 22);
            buttonRemoveUdpDestination.TabIndex = 1;
            buttonRemoveUdpDestination.Text = "-";
            buttonRemoveUdpDestination.UseVisualStyleBackColor = true;
            buttonRemoveUdpDestination.Click += buttonRemoveUdpDestination_Click;
            // 
            // textBoxConnectionStatus
            // 
            textBoxConnectionStatus.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBoxConnectionStatus.BackColor = System.Drawing.Color.Black;
            textBoxConnectionStatus.CausesValidation = false;
            textBoxConnectionStatus.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            textBoxConnectionStatus.ForeColor = System.Drawing.Color.White;
            textBoxConnectionStatus.Location = new System.Drawing.Point(0, 331);
            textBoxConnectionStatus.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            textBoxConnectionStatus.Multiline = true;
            textBoxConnectionStatus.Name = "textBoxConnectionStatus";
            textBoxConnectionStatus.ReadOnly = true;
            textBoxConnectionStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            textBoxConnectionStatus.Size = new System.Drawing.Size(645, 114);
            textBoxConnectionStatus.TabIndex = 1;
            textBoxConnectionStatus.Text = "Status Text";
            // 
            // comboBoxProtocol
            // 
            comboBoxProtocol.FormattingEnabled = true;
            comboBoxProtocol.Location = new System.Drawing.Point(6, 19);
            comboBoxProtocol.Margin = new System.Windows.Forms.Padding(4);
            comboBoxProtocol.Name = "comboBoxProtocol";
            comboBoxProtocol.Size = new System.Drawing.Size(192, 21);
            comboBoxProtocol.TabIndex = 2;
            comboBoxProtocol.SelectedIndexChanged += comboBoxProtocol_SelectedIndexChanged;
            // 
            // propertyGridProtocolParameters
            // 
            propertyGridProtocolParameters.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            propertyGridProtocolParameters.Location = new System.Drawing.Point(6, 44);
            propertyGridProtocolParameters.Margin = new System.Windows.Forms.Padding(4);
            propertyGridProtocolParameters.Name = "propertyGridProtocolParameters";
            propertyGridProtocolParameters.Size = new System.Drawing.Size(192, 161);
            propertyGridProtocolParameters.TabIndex = 3;
            propertyGridProtocolParameters.ToolbarVisible = false;
            // 
            // groupBoxProtocolParameters
            // 
            groupBoxProtocolParameters.Controls.Add(propertyGridProtocolParameters);
            groupBoxProtocolParameters.Controls.Add(comboBoxProtocol);
            groupBoxProtocolParameters.Location = new System.Drawing.Point(219, 0);
            groupBoxProtocolParameters.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            groupBoxProtocolParameters.Name = "groupBoxProtocolParameters";
            groupBoxProtocolParameters.Padding = new System.Windows.Forms.Padding(2, 4, 2, 4);
            groupBoxProtocolParameters.Size = new System.Drawing.Size(205, 258);
            groupBoxProtocolParameters.TabIndex = 1;
            groupBoxProtocolParameters.TabStop = false;
            groupBoxProtocolParameters.Text = "Protocol Parameters";
            // 
            // textBoxConnectionString
            // 
            textBoxConnectionString.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBoxConnectionString.Location = new System.Drawing.Point(0, 260);
            textBoxConnectionString.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            textBoxConnectionString.Multiline = true;
            textBoxConnectionString.Name = "textBoxConnectionString";
            textBoxConnectionString.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            textBoxConnectionString.Size = new System.Drawing.Size(645, 71);
            textBoxConnectionString.TabIndex = 2;
            textBoxConnectionString.TextChanged += textBoxConnectionString_TextChanged;
            // 
            // panelCenterGroups
            // 
            panelCenterGroups.Controls.Add(groupBoxSourceConnection);
            panelCenterGroups.Controls.Add(groupBoxProxyDestinations);
            panelCenterGroups.Controls.Add(groupBoxProtocolParameters);
            panelCenterGroups.Dock = System.Windows.Forms.DockStyle.Fill;
            panelCenterGroups.Location = new System.Drawing.Point(0, 47);
            panelCenterGroups.Name = "panelCenterGroups";
            panelCenterGroups.Size = new System.Drawing.Size(645, 398);
            panelCenterGroups.TabIndex = 1;
            // 
            // toolTip
            // 
            toolTip.AutomaticDelay = 50;
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 50;
            toolTip.ReshowDelay = 10;
            toolTip.ShowAlways = true;
            // 
            // ProxyConnectionEditor
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(textBoxConnectionString);
            Controls.Add(textBoxConnectionStatus);
            Controls.Add(panelCenterGroups);
            Controls.Add(groupBoxName);
            DoubleBuffered = true;
            Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            MinimumSize = new System.Drawing.Size(645, 445);
            Name = "ProxyConnectionEditor";
            Size = new System.Drawing.Size(645, 445);
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            groupBoxName.ResumeLayout(false);
            groupBoxName.PerformLayout();
            groupBoxSourceConnection.ResumeLayout(false);
            groupBoxSourceConnection.PerformLayout();
            tabControlSourceConnectionType.ResumeLayout(false);
            tabPageTcp.ResumeLayout(false);
            tabPageTcp.PerformLayout();
            tabPageUdp.ResumeLayout(false);
            tabPageUdp.PerformLayout();
            groupBoxProxyDestinations.ResumeLayout(false);
            groupBoxProxyDestinations.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxGreen).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxRed).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxYellow).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxGray).EndInit();
            tabControlProxyDestinationType.ResumeLayout(false);
            tabTcpProxyPoint.ResumeLayout(false);
            tabTcpProxyPoint.PerformLayout();
            tabUdpRebroacasts.ResumeLayout(false);
            flowLayoutPanelUdpDestinations.ResumeLayout(false);
            flowLayoutPanelUdpDestinations.PerformLayout();
            groupBoxProtocolParameters.ResumeLayout(false);
            panelCenterGroups.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
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
