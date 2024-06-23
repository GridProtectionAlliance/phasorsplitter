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
            if (disposing && (components is not null))
            {
                components.Dispose();
            }

            if (m_serviceConnection is not null)
            {
                m_serviceConnection.StatusMessage -= m_serviceConnection_StatusMessage;
                m_serviceConnection.ServiceResponse -= m_serviceConnection_ServiceResponse;
                m_serviceConnection.ConnectionState -= m_serviceConnection_ConnectionState;
                m_serviceConnection.Dispose();
                m_serviceConnection = null;
            }

            if (m_refreshProxyStatusTimer is not null)
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StreamSplitterManager));
            bindingNavigator = new System.Windows.Forms.BindingNavigator(components);
            bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            bindingSource = new System.Windows.Forms.BindingSource(components);
            bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            toolStripButtonNewConfig = new System.Windows.Forms.ToolStripButton();
            toolStripButtonLoadConfig = new System.Windows.Forms.ToolStripButton();
            toolStripButtonSaveConfig = new System.Windows.Forms.ToolStripButton();
            toolStripButtonSaveConfigAs = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            toolStripButtonDownloadConfig = new System.Windows.Forms.ToolStripButton();
            toolStripButtonUploadConfig = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            toolStripButtonConnectTo = new System.Windows.Forms.ToolStripButton();
            toolStripButtonRestartService = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            toolStripButtonGPA = new System.Windows.Forms.ToolStripButton();
            statusStrip = new System.Windows.Forms.StatusStrip();
            toolStripStatusLabelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripStatusLabelState = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripStatusLabelVersion = new System.Windows.Forms.ToolStripStatusLabel();
            openFileDialog = new System.Windows.Forms.OpenFileDialog();
            saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            imageList = new System.Windows.Forms.ImageList(components);
            toolTipNewHelp = new System.Windows.Forms.ToolTip(components);
            panel = new System.Windows.Forms.Panel();
            toolStrip = new System.Windows.Forms.ToolStrip();
            toolStripLabelSearch = new System.Windows.Forms.ToolStripLabel();
            toolStripTextBoxSearch = new System.Windows.Forms.ToolStripTextBox();
            toolStripButtonSearch = new System.Windows.Forms.ToolStripButton();
            toolStripButtonClearSearch = new System.Windows.Forms.ToolStripButton();
            dataGridView = new System.Windows.Forms.DataGridView();
            proxyConnectionEditor = new ProxyConnectionEditor();
            toolTipEx = new ToolTipEx(components);
            ((System.ComponentModel.ISupportInitialize)bindingNavigator).BeginInit();
            bindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingSource).BeginInit();
            statusStrip.SuspendLayout();
            panel.SuspendLayout();
            toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // bindingNavigator
            // 
            bindingNavigator.AddNewItem = bindingNavigatorAddNewItem;
            bindingNavigator.BindingSource = bindingSource;
            bindingNavigator.CountItem = bindingNavigatorCountItem;
            bindingNavigator.DeleteItem = bindingNavigatorDeleteItem;
            bindingNavigator.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            bindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripButtonNewConfig, toolStripButtonLoadConfig, toolStripButtonSaveConfig, toolStripButtonSaveConfigAs, toolStripSeparator1, toolStripButtonDownloadConfig, toolStripButtonUploadConfig, toolStripSeparator2, bindingNavigatorMoveFirstItem, bindingNavigatorMovePreviousItem, bindingNavigatorSeparator, bindingNavigatorPositionItem, bindingNavigatorCountItem, bindingNavigatorSeparator1, bindingNavigatorMoveNextItem, bindingNavigatorMoveLastItem, bindingNavigatorSeparator2, bindingNavigatorAddNewItem, bindingNavigatorDeleteItem, toolStripSeparator3, toolStripButtonConnectTo, toolStripButtonRestartService, toolStripSeparator4, toolStripButtonGPA });
            bindingNavigator.Location = new System.Drawing.Point(0, 0);
            bindingNavigator.MoveFirstItem = bindingNavigatorMoveFirstItem;
            bindingNavigator.MoveLastItem = bindingNavigatorMoveLastItem;
            bindingNavigator.MoveNextItem = bindingNavigatorMoveNextItem;
            bindingNavigator.MovePreviousItem = bindingNavigatorMovePreviousItem;
            bindingNavigator.Name = "bindingNavigator";
            bindingNavigator.PositionItem = bindingNavigatorPositionItem;
            bindingNavigator.Size = new System.Drawing.Size(1352, 25);
            bindingNavigator.Stretch = true;
            bindingNavigator.TabIndex = 0;
            // 
            // bindingNavigatorAddNewItem
            // 
            bindingNavigatorAddNewItem.AutoToolTip = false;
            bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            bindingNavigatorAddNewItem.Image = (System.Drawing.Image)resources.GetObject("bindingNavigatorAddNewItem.Image");
            bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            bindingNavigatorAddNewItem.Text = "&Add New Proxy Connection";
            bindingNavigatorAddNewItem.ToolTipText = "Add New Proxy Connection";
            bindingNavigatorAddNewItem.MouseDown += bindingNavigatorAddNewItem_MouseDown;
            // 
            // bindingSource
            // 
            bindingSource.AddingNew += bindingSource_AddingNew;
            bindingSource.PositionChanged += bindingSource_PositionChanged;
            // 
            // bindingNavigatorCountItem
            // 
            bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 22);
            bindingNavigatorCountItem.Text = "of {0}";
            bindingNavigatorCountItem.ToolTipText = "Total Number of Proxy Connections";
            // 
            // bindingNavigatorDeleteItem
            // 
            bindingNavigatorDeleteItem.AutoToolTip = false;
            bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            bindingNavigatorDeleteItem.Image = (System.Drawing.Image)resources.GetObject("bindingNavigatorDeleteItem.Image");
            bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            bindingNavigatorDeleteItem.Text = "D&elete Selected Proxy Connection";
            bindingNavigatorDeleteItem.ToolTipText = "Delete Selected Proxy Connection";
            bindingNavigatorDeleteItem.MouseDown += bindingNavigatorDeleteItem_MouseDown;
            // 
            // toolStripButtonNewConfig
            // 
            toolStripButtonNewConfig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButtonNewConfig.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonNewConfig.Image");
            toolStripButtonNewConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButtonNewConfig.Name = "toolStripButtonNewConfig";
            toolStripButtonNewConfig.Size = new System.Drawing.Size(23, 22);
            toolStripButtonNewConfig.Text = "Create &New Local Configuration";
            toolStripButtonNewConfig.ToolTipText = "Create New Local Configuration";
            toolStripButtonNewConfig.Click += toolStripButtonNewConfig_Click;
            // 
            // toolStripButtonLoadConfig
            // 
            toolStripButtonLoadConfig.AutoToolTip = false;
            toolStripButtonLoadConfig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButtonLoadConfig.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonLoadConfig.Image");
            toolStripButtonLoadConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButtonLoadConfig.Name = "toolStripButtonLoadConfig";
            toolStripButtonLoadConfig.Size = new System.Drawing.Size(23, 22);
            toolStripButtonLoadConfig.Text = "&Open Local Configuration";
            toolStripButtonLoadConfig.ToolTipText = "Open Local Configuration";
            toolStripButtonLoadConfig.Click += toolStripButtonLoadConfig_Click;
            // 
            // toolStripButtonSaveConfig
            // 
            toolStripButtonSaveConfig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButtonSaveConfig.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonSaveConfig.Image");
            toolStripButtonSaveConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButtonSaveConfig.Name = "toolStripButtonSaveConfig";
            toolStripButtonSaveConfig.Size = new System.Drawing.Size(23, 22);
            toolStripButtonSaveConfig.Text = "&Save Local Configuration";
            toolStripButtonSaveConfig.ToolTipText = "Save Local Configuration";
            toolStripButtonSaveConfig.Click += toolStripButtonSaveConfig_Click;
            // 
            // toolStripButtonSaveConfigAs
            // 
            toolStripButtonSaveConfigAs.AutoToolTip = false;
            toolStripButtonSaveConfigAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButtonSaveConfigAs.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonSaveConfigAs.Image");
            toolStripButtonSaveConfigAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButtonSaveConfigAs.Name = "toolStripButtonSaveConfigAs";
            toolStripButtonSaveConfigAs.Size = new System.Drawing.Size(23, 22);
            toolStripButtonSaveConfigAs.Text = "Sa&ve Local Configuration As";
            toolStripButtonSaveConfigAs.ToolTipText = "Save Local Configuration As";
            toolStripButtonSaveConfigAs.Click += toolStripButtonSaveConfigAs_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonDownloadConfig
            // 
            toolStripButtonDownloadConfig.AutoToolTip = false;
            toolStripButtonDownloadConfig.Enabled = false;
            toolStripButtonDownloadConfig.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonDownloadConfig.Image");
            toolStripButtonDownloadConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButtonDownloadConfig.Name = "toolStripButtonDownloadConfig";
            toolStripButtonDownloadConfig.Size = new System.Drawing.Size(81, 22);
            toolStripButtonDownloadConfig.Text = "&Download";
            toolStripButtonDownloadConfig.ToolTipText = "Download Current Service Configuration";
            toolStripButtonDownloadConfig.Click += toolStripButtonDownloadConfig_Click;
            // 
            // toolStripButtonUploadConfig
            // 
            toolStripButtonUploadConfig.AutoToolTip = false;
            toolStripButtonUploadConfig.Enabled = false;
            toolStripButtonUploadConfig.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonUploadConfig.Image");
            toolStripButtonUploadConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButtonUploadConfig.Name = "toolStripButtonUploadConfig";
            toolStripButtonUploadConfig.Size = new System.Drawing.Size(65, 22);
            toolStripButtonUploadConfig.Text = "&Upload";
            toolStripButtonUploadConfig.ToolTipText = "Upload Local Configuration to Service";
            toolStripButtonUploadConfig.Click += toolStripButtonUploadConfig_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveFirstItem
            // 
            bindingNavigatorMoveFirstItem.AutoToolTip = false;
            bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            bindingNavigatorMoveFirstItem.Image = (System.Drawing.Image)resources.GetObject("bindingNavigatorMoveFirstItem.Image");
            bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            bindingNavigatorMoveFirstItem.Text = "Move to &First Proxy Connection";
            bindingNavigatorMoveFirstItem.ToolTipText = "Move to First Proxy Connection";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            bindingNavigatorMovePreviousItem.AutoToolTip = false;
            bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            bindingNavigatorMovePreviousItem.Image = (System.Drawing.Image)resources.GetObject("bindingNavigatorMovePreviousItem.Image");
            bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            bindingNavigatorMovePreviousItem.Text = "Move to &Previous Proxy Connection";
            bindingNavigatorMovePreviousItem.ToolTipText = "Move to Previous Proxy Connection";
            // 
            // bindingNavigatorSeparator
            // 
            bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            bindingNavigatorPositionItem.AccessibleName = "Position";
            bindingNavigatorPositionItem.AutoSize = false;
            bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            bindingNavigatorPositionItem.Size = new System.Drawing.Size(34, 23);
            bindingNavigatorPositionItem.Text = "0";
            bindingNavigatorPositionItem.ToolTipText = "Current Proxy Connection";
            // 
            // bindingNavigatorSeparator1
            // 
            bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            bindingNavigatorMoveNextItem.AutoToolTip = false;
            bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            bindingNavigatorMoveNextItem.Image = (System.Drawing.Image)resources.GetObject("bindingNavigatorMoveNextItem.Image");
            bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            bindingNavigatorMoveNextItem.Text = "Move to Ne&xt Proxy Connection";
            bindingNavigatorMoveNextItem.ToolTipText = "Move to Next Proxy Connection";
            // 
            // bindingNavigatorMoveLastItem
            // 
            bindingNavigatorMoveLastItem.AutoToolTip = false;
            bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            bindingNavigatorMoveLastItem.Image = (System.Drawing.Image)resources.GetObject("bindingNavigatorMoveLastItem.Image");
            bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            bindingNavigatorMoveLastItem.Text = "Move to &Last Proxy Connection";
            bindingNavigatorMoveLastItem.ToolTipText = "Move to Last Proxy Connection";
            // 
            // bindingNavigatorSeparator2
            // 
            bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonConnectTo
            // 
            toolStripButtonConnectTo.AutoToolTip = false;
            toolStripButtonConnectTo.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonConnectTo.Image");
            toolStripButtonConnectTo.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButtonConnectTo.Name = "toolStripButtonConnectTo";
            toolStripButtonConnectTo.Size = new System.Drawing.Size(87, 22);
            toolStripButtonConnectTo.Text = "&Connect To";
            toolStripButtonConnectTo.ToolTipText = "Connect to Service";
            toolStripButtonConnectTo.Click += toolStripButtonConnectTo_Click;
            // 
            // toolStripButtonRestartService
            // 
            toolStripButtonRestartService.AutoToolTip = false;
            toolStripButtonRestartService.Enabled = false;
            toolStripButtonRestartService.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonRestartService.Image");
            toolStripButtonRestartService.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButtonRestartService.Name = "toolStripButtonRestartService";
            toolStripButtonRestartService.Size = new System.Drawing.Size(63, 22);
            toolStripButtonRestartService.Text = "&Restart";
            toolStripButtonRestartService.ToolTipText = "Restart Service";
            toolStripButtonRestartService.Click += toolStripButtonRestartService_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonGPA
            // 
            toolStripButtonGPA.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButtonGPA.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonGPA.Image");
            toolStripButtonGPA.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButtonGPA.Name = "toolStripButtonGPA";
            toolStripButtonGPA.Size = new System.Drawing.Size(23, 22);
            toolStripButtonGPA.Text = "Grid Protection Alliance";
            toolStripButtonGPA.Click += toolStripButtonGPA_Click;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripStatusLabelStatus, toolStripStatusLabelState, toolStripStatusLabelVersion });
            statusStrip.Location = new System.Drawing.Point(0, 750);
            statusStrip.Name = "statusStrip";
            statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            statusStrip.Size = new System.Drawing.Size(1352, 24);
            statusStrip.TabIndex = 2;
            // 
            // toolStripStatusLabelStatus
            // 
            toolStripStatusLabelStatus.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            toolStripStatusLabelStatus.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            toolStripStatusLabelStatus.Margin = new System.Windows.Forms.Padding(0, 3, 5, 2);
            toolStripStatusLabelStatus.Name = "toolStripStatusLabelStatus";
            toolStripStatusLabelStatus.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            toolStripStatusLabelStatus.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            toolStripStatusLabelStatus.Size = new System.Drawing.Size(1134, 19);
            toolStripStatusLabelStatus.Spring = true;
            toolStripStatusLabelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabelState
            // 
            toolStripStatusLabelState.AutoSize = false;
            toolStripStatusLabelState.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            toolStripStatusLabelState.Image = (System.Drawing.Image)resources.GetObject("toolStripStatusLabelState.Image");
            toolStripStatusLabelState.Name = "toolStripStatusLabelState";
            toolStripStatusLabelState.Size = new System.Drawing.Size(165, 19);
            toolStripStatusLabelState.Text = "Disconnected from Service";
            // 
            // toolStripStatusLabelVersion
            // 
            toolStripStatusLabelVersion.Name = "toolStripStatusLabelVersion";
            toolStripStatusLabelVersion.Size = new System.Drawing.Size(31, 19);
            toolStripStatusLabelVersion.Text = "0.0.0";
            // 
            // openFileDialog
            // 
            openFileDialog.DefaultExt = "s3config";
            openFileDialog.Filter = "Configuration Files (*.s3config)|*.s3config|All files (*.*)|*.*";
            openFileDialog.Title = "Load Configuration";
            // 
            // saveFileDialog
            // 
            saveFileDialog.DefaultExt = "s3config";
            saveFileDialog.Filter = "Configuration Files (*.s3config)|*.s3config|All files (*.*)|*.*";
            saveFileDialog.Title = "Save Configuration";
            // 
            // imageList
            // 
            imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            imageList.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList.ImageStream");
            imageList.TransparentColor = System.Drawing.Color.Transparent;
            imageList.Images.SetKeyName(0, "ServiceConnected.png");
            imageList.Images.SetKeyName(1, "ServiceDisconnected.png");
            // 
            // toolTipNewHelp
            // 
            toolTipNewHelp.AutomaticDelay = 1;
            toolTipNewHelp.IsBalloon = true;
            toolTipNewHelp.ShowAlways = true;
            // 
            // panel
            // 
            panel.Controls.Add(toolStrip);
            panel.Dock = System.Windows.Forms.DockStyle.Top;
            panel.Location = new System.Drawing.Point(0, 25);
            panel.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            panel.Name = "panel";
            panel.Size = new System.Drawing.Size(1352, 50);
            panel.TabIndex = 0;
            // 
            // toolStrip
            // 
            toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            toolStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripLabelSearch, toolStripTextBoxSearch, toolStripButtonSearch, toolStripButtonClearSearch });
            toolStrip.Location = new System.Drawing.Point(0, 0);
            toolStrip.Name = "toolStrip";
            toolStrip.Padding = new System.Windows.Forms.Padding(0, 10, 4, 0);
            toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            toolStrip.Size = new System.Drawing.Size(1352, 42);
            toolStrip.TabIndex = 0;
            toolStrip.TabStop = true;
            // 
            // toolStripLabelSearch
            // 
            toolStripLabelSearch.Margin = new System.Windows.Forms.Padding(6, 2, 0, 3);
            toolStripLabelSearch.Name = "toolStripLabelSearch";
            toolStripLabelSearch.Size = new System.Drawing.Size(45, 27);
            toolStripLabelSearch.Text = "Search:";
            // 
            // toolStripTextBoxSearch
            // 
            toolStripTextBoxSearch.Name = "toolStripTextBoxSearch";
            toolStripTextBoxSearch.Size = new System.Drawing.Size(233, 32);
            toolStripTextBoxSearch.KeyDown += toolStripTextBoxSearch_KeyDown;
            // 
            // toolStripButtonSearch
            // 
            toolStripButtonSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButtonSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButtonSearch.Margin = new System.Windows.Forms.Padding(10, 1, 0, 2);
            toolStripButtonSearch.Name = "toolStripButtonSearch";
            toolStripButtonSearch.Size = new System.Drawing.Size(26, 29);
            toolStripButtonSearch.Text = "&Go";
            toolStripButtonSearch.Click += toolStripButtonSearch_Click;
            // 
            // toolStripButtonClearSearch
            // 
            toolStripButtonClearSearch.AutoSize = false;
            toolStripButtonClearSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButtonClearSearch.Name = "toolStripButtonClearSearch";
            toolStripButtonClearSearch.Size = new System.Drawing.Size(60, 29);
            toolStripButtonClearSearch.Text = "Clear";
            toolStripButtonClearSearch.Click += toolStripButtonClearSearch_Click;
            // 
            // dataGridView
            // 
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dataGridView.AutoGenerateColumns = false;
            dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.DataSource = bindingSource;
            dataGridView.Location = new System.Drawing.Point(4, 78);
            dataGridView.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            dataGridView.MultiSelect = false;
            dataGridView.Name = "dataGridView";
            dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dataGridView.Size = new System.Drawing.Size(697, 667);
            dataGridView.TabIndex = 7;
            dataGridView.CellContentClick += dataGridView_CellContentClick;
            dataGridView.CellFormatting += dataGridView_CellFormatting;
            dataGridView.ColumnHeaderMouseClick += dataGridView_ColumnHeaderMouseClick;
            // 
            // proxyConnectionEditor
            // 
            proxyConnectionEditor.ConnectionParameters = null;
            proxyConnectionEditor.ConnectionState = ConnectionState.Disabled;
            proxyConnectionEditor.ConnectionString = resources.GetString("proxyConnectionEditor.ConnectionString");
            proxyConnectionEditor.Dock = System.Windows.Forms.DockStyle.Right;
            proxyConnectionEditor.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            proxyConnectionEditor.ID = new System.Guid("00000000-0000-0000-0000-000000000000");
            proxyConnectionEditor.Location = new System.Drawing.Point(707, 75);
            proxyConnectionEditor.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            proxyConnectionEditor.MinimumSize = new System.Drawing.Size(645, 445);
            proxyConnectionEditor.Name = "proxyConnectionEditor";
            proxyConnectionEditor.ProxyConnection = null;
            proxyConnectionEditor.SelectionFocus = true;
            proxyConnectionEditor.Size = new System.Drawing.Size(645, 675);
            proxyConnectionEditor.TabIndex = 8;
            // 
            // toolTipEx
            // 
            toolTipEx.AutomaticDelay = 10;
            toolTipEx.AutoPopDelay = 5000;
            toolTipEx.BackColor = System.Drawing.SystemColors.Window;
            toolTipEx.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            toolTipEx.ForeColor = System.Drawing.SystemColors.WindowText;
            toolTipEx.InitialDelay = 10;
            toolTipEx.OwnerDraw = true;
            toolTipEx.ReshowDelay = 2;
            toolTipEx.ShowAlways = true;
            // 
            // StreamSplitterManager
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1352, 774);
            Controls.Add(proxyConnectionEditor);
            Controls.Add(dataGridView);
            Controls.Add(statusStrip);
            Controls.Add(panel);
            Controls.Add(bindingNavigator);
            DoubleBuffered = true;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MinimumSize = new System.Drawing.Size(1158, 531);
            Name = "StreamSplitterManager";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Tag = "Synchrophasor Stream Splitter";
            Text = "Synchrophasor Stream Splitter";
            Activated += StreamSplitterManager_Activated;
            FormClosing += StreamSplitterManager_FormClosing;
            Load += StreamSplitterManager_Load;
            Move += StreamSplitterManager_Activated;
            Resize += StreamSplitterManager_Resize;
            ((System.ComponentModel.ISupportInitialize)bindingNavigator).EndInit();
            bindingNavigator.ResumeLayout(false);
            bindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bindingSource).EndInit();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            panel.ResumeLayout(false);
            panel.PerformLayout();
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
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
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripLabel toolStripLabelSearch;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxSearch;
        private System.Windows.Forms.ToolStripButton toolStripButtonClearSearch;
        private System.Windows.Forms.ToolStripButton toolStripButtonSearch;
        private System.Windows.Forms.DataGridView dataGridView;
        private ProxyConnectionEditor proxyConnectionEditor;
    }
}

