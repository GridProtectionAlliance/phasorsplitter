//******************************************************************************************************
//  StreamSplitterManager.Designer.cs - Gbtc
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
    partial class StreamSplitterManager
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            if ((object)m_serviceConnection != null)
            {
                m_serviceConnection.StatusMessage -= m_serviceConnection_StatusMessage;
                m_serviceConnection.ServiceResponse -= m_serviceConnection_ServiceResponse;
                m_serviceConnection.ConnectionState -= m_serviceConnection_ConnectionState;
                m_serviceConnection.Dispose();
                m_serviceConnection = null;
            }

            if ((object)m_refreshProxyStatusTimer != null)
            {
                m_refreshProxyStatusTimer.Elapsed -= m_refreshProxyStatusTimer_Elapsed;
                m_refreshProxyStatusTimer.Dispose();
                m_refreshProxyStatusTimer = null;
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StreamSplitterManager));
            this.bindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonNewConfig = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonLoadConfig = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSaveConfig = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSaveConfigAs = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonDownloadConfig = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonUploadConfig = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonConnectTo = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRestartService = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonGPA = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelState = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.flowLayoutPanelProxyConnections = new System.Windows.Forms.FlowLayoutPanel();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.toolTipEx = new StreamSplitter.ToolTipEx(this.components);
            this.toolTipNewHelp = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator)).BeginInit();
            this.bindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // bindingNavigator
            // 
            this.bindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.bindingNavigator.BindingSource = this.bindingSource;
            this.bindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.bindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.bindingNavigator.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.bindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonNewConfig,
            this.toolStripButtonLoadConfig,
            this.toolStripButtonSaveConfig,
            this.toolStripButtonSaveConfigAs,
            this.toolStripSeparator1,
            this.toolStripButtonDownloadConfig,
            this.toolStripButtonUploadConfig,
            this.toolStripSeparator2,
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem,
            this.toolStripSeparator3,
            this.toolStripButtonConnectTo,
            this.toolStripButtonRestartService,
            this.toolStripSeparator4,
            this.toolStripButtonGPA});
            this.bindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.bindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bindingNavigator.Name = "bindingNavigator";
            this.bindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.bindingNavigator.Size = new System.Drawing.Size(761, 25);
            this.bindingNavigator.Stretch = true;
            this.bindingNavigator.TabIndex = 0;
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.AutoToolTip = false;
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "&Add New Proxy Connection";
            this.bindingNavigatorAddNewItem.ToolTipText = "Add New Proxy Connection";
            // 
            // bindingSource
            // 
            this.bindingSource.AddingNew += new System.ComponentModel.AddingNewEventHandler(this.bindingSource_AddingNew);
            this.bindingSource.PositionChanged += new System.EventHandler(this.bindingSource_PositionChanged);
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total Number of Proxy Connections";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.AutoToolTip = false;
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "D&elete Selected Proxy Connection";
            this.bindingNavigatorDeleteItem.ToolTipText = "Delete Selected Proxy Connection";
            this.bindingNavigatorDeleteItem.MouseEnter += new System.EventHandler(this.bindingNavigatorDeleteItem_MouseEnter);
            this.bindingNavigatorDeleteItem.MouseLeave += new System.EventHandler(this.bindingNavigatorDeleteItem_MouseLeave);
            // 
            // toolStripButtonNewConfig
            // 
            this.toolStripButtonNewConfig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonNewConfig.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonNewConfig.Image")));
            this.toolStripButtonNewConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNewConfig.Name = "toolStripButtonNewConfig";
            this.toolStripButtonNewConfig.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonNewConfig.Text = "Create &New Local Configuration";
            this.toolStripButtonNewConfig.ToolTipText = "Create New Local Configuration";
            this.toolStripButtonNewConfig.Click += new System.EventHandler(this.toolStripButtonNewConfig_Click);
            // 
            // toolStripButtonLoadConfig
            // 
            this.toolStripButtonLoadConfig.AutoToolTip = false;
            this.toolStripButtonLoadConfig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonLoadConfig.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonLoadConfig.Image")));
            this.toolStripButtonLoadConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLoadConfig.Name = "toolStripButtonLoadConfig";
            this.toolStripButtonLoadConfig.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonLoadConfig.Text = "&Open Local Configuration";
            this.toolStripButtonLoadConfig.ToolTipText = "Open Local Configuration";
            this.toolStripButtonLoadConfig.Click += new System.EventHandler(this.toolStripButtonLoadConfig_Click);
            // 
            // toolStripButtonSaveConfig
            // 
            this.toolStripButtonSaveConfig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSaveConfig.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSaveConfig.Image")));
            this.toolStripButtonSaveConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSaveConfig.Name = "toolStripButtonSaveConfig";
            this.toolStripButtonSaveConfig.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonSaveConfig.Text = "&Save Local Configuration";
            this.toolStripButtonSaveConfig.ToolTipText = "Save Local Configuration";
            this.toolStripButtonSaveConfig.Click += new System.EventHandler(this.toolStripButtonSaveConfig_Click);
            // 
            // toolStripButtonSaveConfigAs
            // 
            this.toolStripButtonSaveConfigAs.AutoToolTip = false;
            this.toolStripButtonSaveConfigAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSaveConfigAs.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSaveConfigAs.Image")));
            this.toolStripButtonSaveConfigAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSaveConfigAs.Name = "toolStripButtonSaveConfigAs";
            this.toolStripButtonSaveConfigAs.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonSaveConfigAs.Text = "Sa&ve Local Configuration As";
            this.toolStripButtonSaveConfigAs.ToolTipText = "Save Local Configuration As";
            this.toolStripButtonSaveConfigAs.Click += new System.EventHandler(this.toolStripButtonSaveConfigAs_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonDownloadConfig
            // 
            this.toolStripButtonDownloadConfig.AutoToolTip = false;
            this.toolStripButtonDownloadConfig.Enabled = false;
            this.toolStripButtonDownloadConfig.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDownloadConfig.Image")));
            this.toolStripButtonDownloadConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDownloadConfig.Name = "toolStripButtonDownloadConfig";
            this.toolStripButtonDownloadConfig.Size = new System.Drawing.Size(81, 22);
            this.toolStripButtonDownloadConfig.Text = "&Download";
            this.toolStripButtonDownloadConfig.ToolTipText = "Download Current Service Configuration";
            this.toolStripButtonDownloadConfig.Click += new System.EventHandler(this.toolStripButtonDownloadConfig_Click);
            // 
            // toolStripButtonUploadConfig
            // 
            this.toolStripButtonUploadConfig.AutoToolTip = false;
            this.toolStripButtonUploadConfig.Enabled = false;
            this.toolStripButtonUploadConfig.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonUploadConfig.Image")));
            this.toolStripButtonUploadConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonUploadConfig.Name = "toolStripButtonUploadConfig";
            this.toolStripButtonUploadConfig.Size = new System.Drawing.Size(65, 22);
            this.toolStripButtonUploadConfig.Text = "&Upload";
            this.toolStripButtonUploadConfig.ToolTipText = "Upload Local Configuration to Service";
            this.toolStripButtonUploadConfig.Click += new System.EventHandler(this.toolStripButtonUploadConfig_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.AutoToolTip = false;
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move to &First Proxy Connection";
            this.bindingNavigatorMoveFirstItem.ToolTipText = "Move to First Proxy Connection";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.AutoToolTip = false;
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Move to &Previous Proxy Connection";
            this.bindingNavigatorMovePreviousItem.ToolTipText = "Move to Previous Proxy Connection";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(30, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current Proxy Connection";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.AutoToolTip = false;
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Move to Ne&xt Proxy Connection";
            this.bindingNavigatorMoveNextItem.ToolTipText = "Move to Next Proxy Connection";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.AutoToolTip = false;
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Move to &Last Proxy Connection";
            this.bindingNavigatorMoveLastItem.ToolTipText = "Move to Last Proxy Connection";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonConnectTo
            // 
            this.toolStripButtonConnectTo.AutoToolTip = false;
            this.toolStripButtonConnectTo.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonConnectTo.Image")));
            this.toolStripButtonConnectTo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonConnectTo.Name = "toolStripButtonConnectTo";
            this.toolStripButtonConnectTo.Size = new System.Drawing.Size(89, 22);
            this.toolStripButtonConnectTo.Text = "&Connect To";
            this.toolStripButtonConnectTo.ToolTipText = "Connect to Service";
            this.toolStripButtonConnectTo.Click += new System.EventHandler(this.toolStripButtonConnectTo_Click);
            // 
            // toolStripButtonRestartService
            // 
            this.toolStripButtonRestartService.AutoToolTip = false;
            this.toolStripButtonRestartService.Enabled = false;
            this.toolStripButtonRestartService.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRestartService.Image")));
            this.toolStripButtonRestartService.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRestartService.Name = "toolStripButtonRestartService";
            this.toolStripButtonRestartService.Size = new System.Drawing.Size(63, 22);
            this.toolStripButtonRestartService.Text = "&Restart";
            this.toolStripButtonRestartService.ToolTipText = "Restart Service";
            this.toolStripButtonRestartService.Click += new System.EventHandler(this.toolStripButtonRestartService_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonGPA
            // 
            this.toolStripButtonGPA.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonGPA.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonGPA.Image")));
            this.toolStripButtonGPA.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonGPA.Name = "toolStripButtonGPA";
            this.toolStripButtonGPA.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonGPA.Text = "Grid Protection Alliance";
            this.toolStripButtonGPA.Click += new System.EventHandler(this.toolStripButtonGPA_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelStatus,
            this.toolStripStatusLabelState,
            this.toolStripStatusLabelVersion});
            this.statusStrip.Location = new System.Drawing.Point(0, 404);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(761, 24);
            this.statusStrip.TabIndex = 2;
            this.toolTipEx.SetToolTip(this.statusStrip, "...");
            // 
            // toolStripStatusLabelStatus
            // 
            this.toolStripStatusLabelStatus.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabelStatus.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabelStatus.Margin = new System.Windows.Forms.Padding(0, 3, 5, 2);
            this.toolStripStatusLabelStatus.Name = "toolStripStatusLabelStatus";
            this.toolStripStatusLabelStatus.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripStatusLabelStatus.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.toolStripStatusLabelStatus.Size = new System.Drawing.Size(545, 19);
            this.toolStripStatusLabelStatus.Spring = true;
            this.toolStripStatusLabelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabelState
            // 
            this.toolStripStatusLabelState.AutoSize = false;
            this.toolStripStatusLabelState.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabelState.Image = ((System.Drawing.Image)(resources.GetObject("toolStripStatusLabelState.Image")));
            this.toolStripStatusLabelState.Name = "toolStripStatusLabelState";
            this.toolStripStatusLabelState.Size = new System.Drawing.Size(165, 19);
            this.toolStripStatusLabelState.Text = "Disconnected from Service";
            // 
            // toolStripStatusLabelVersion
            // 
            this.toolStripStatusLabelVersion.Name = "toolStripStatusLabelVersion";
            this.toolStripStatusLabelVersion.Size = new System.Drawing.Size(31, 19);
            this.toolStripStatusLabelVersion.Text = "0.0.0";
            // 
            // flowLayoutPanelProxyConnections
            // 
            this.flowLayoutPanelProxyConnections.AutoScroll = true;
            this.flowLayoutPanelProxyConnections.AutoSize = true;
            this.flowLayoutPanelProxyConnections.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanelProxyConnections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelProxyConnections.Location = new System.Drawing.Point(0, 25);
            this.flowLayoutPanelProxyConnections.Name = "flowLayoutPanelProxyConnections";
            this.flowLayoutPanelProxyConnections.Size = new System.Drawing.Size(761, 379);
            this.flowLayoutPanelProxyConnections.TabIndex = 3;
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "s3config";
            this.openFileDialog.Filter = "Configuration Files (*.s3config)|*.s3config|All files (*.*)|*.*";
            this.openFileDialog.Title = "Load Configuration";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "s3config";
            this.saveFileDialog.Filter = "Configuration Files (*.s3config)|*.s3config|All files (*.*)|*.*";
            this.saveFileDialog.Title = "Save Configuration";
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "ServiceConnected.png");
            this.imageList.Images.SetKeyName(1, "ServiceDisconnected.png");
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
            // toolTipNewHelp
            // 
            this.toolTipNewHelp.AutomaticDelay = 1;
            this.toolTipNewHelp.IsBalloon = true;
            this.toolTipNewHelp.ShowAlways = true;
            // 
            // StreamSplitterManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 428);
            this.Controls.Add(this.flowLayoutPanelProxyConnections);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.bindingNavigator);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "StreamSplitterManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "Synchrophasor Stream Splitter";
            this.Text = "Synchrophasor Stream Splitter";
            this.Activated += new System.EventHandler(this.StreamSplitterManager_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StreamSplitterManager_FormClosing);
            this.Load += new System.EventHandler(this.StreamSplitterManager_Load);
            this.Move += new System.EventHandler(this.StreamSplitterManager_Activated);
            this.Resize += new System.EventHandler(this.StreamSplitterManager_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator)).EndInit();
            this.bindingNavigator.ResumeLayout(false);
            this.bindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingNavigator bindingNavigator;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.BindingSource bindingSource;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelProxyConnections;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonConnectTo;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelState;
        private StreamSplitter.ToolTipEx toolTipEx;
        private System.Windows.Forms.ToolStripButton toolStripButtonRestartService;
        private System.Windows.Forms.ToolStripButton toolStripButtonDownloadConfig;
        private System.Windows.Forms.ToolStripButton toolStripButtonUploadConfig;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButtonSaveConfig;
        private System.Windows.Forms.ToolStripButton toolStripButtonNewConfig;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripButton toolStripButtonSaveConfigAs;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolStripButtonGPA;
        private System.Windows.Forms.ToolStripButton toolStripButtonLoadConfig;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelVersion;
        private System.Windows.Forms.ToolTip toolTipNewHelp;
    }
}

