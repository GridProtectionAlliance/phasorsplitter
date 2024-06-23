//******************************************************************************************************
//  SplashScreen.designer.cs - Gbtc
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

using System.Windows.Forms;

namespace StreamSplitter
{
    partial class SplashScreen : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreen));
            lblStatus = new Label();
            pnlStatus = new Panel();
            lblTimeRemaining = new Label();
            UpdateTimer = new Timer(components);
            SuspendLayout();
            // 
            // lblStatus
            // 
            lblStatus.BackColor = System.Drawing.Color.Transparent;
            lblStatus.Location = new System.Drawing.Point(139, 118);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new System.Drawing.Size(294, 17);
            lblStatus.TabIndex = 0;
            lblStatus.DoubleClick += SplashScreen_DoubleClick;
            // 
            // pnlStatus
            // 
            pnlStatus.BackColor = System.Drawing.Color.Transparent;
            pnlStatus.Location = new System.Drawing.Point(121, 260);
            pnlStatus.Name = "pnlStatus";
            pnlStatus.Size = new System.Drawing.Size(287, 29);
            pnlStatus.TabIndex = 1;
            pnlStatus.DoubleClick += SplashScreen_DoubleClick;
            // 
            // lblTimeRemaining
            // 
            lblTimeRemaining.BackColor = System.Drawing.Color.Transparent;
            lblTimeRemaining.Location = new System.Drawing.Point(152, 226);
            lblTimeRemaining.Name = "lblTimeRemaining";
            lblTimeRemaining.Size = new System.Drawing.Size(219, 30);
            lblTimeRemaining.TabIndex = 2;
            lblTimeRemaining.Text = "Time remaining";
            lblTimeRemaining.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lblTimeRemaining.DoubleClick += SplashScreen_DoubleClick;
            // 
            // UpdateTimer
            // 
            UpdateTimer.Tick += UpdateTimer_Tick;
            // 
            // SplashScreen
            // 
            AutoScaleBaseSize = new System.Drawing.Size(6, 16);
            BackColor = System.Drawing.Color.Black;
            BackgroundImage = (System.Drawing.Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Zoom;
            ClientSize = new System.Drawing.Size(722, 303);
            Controls.Add(lblTimeRemaining);
            Controls.Add(pnlStatus);
            Controls.Add(lblStatus);
            FormBorderStyle = FormBorderStyle.None;
            Name = "SplashScreen";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SplashScreen";
            DoubleClick += SplashScreen_DoubleClick;
            ResumeLayout(false);
        }
        #endregion


        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblTimeRemaining;
        private System.Windows.Forms.Timer UpdateTimer;
        private System.Windows.Forms.Panel pnlStatus;
    }
}