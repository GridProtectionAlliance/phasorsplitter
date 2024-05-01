namespace StreamSplitter
{
    partial class ConnectTo
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
            this.components = new System.ComponentModel.Container();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.textBoxServerConnection = new System.Windows.Forms.TextBox();
            this.labelTcpConnectionFormat = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // toolTip
            // 
            this.toolTip.AutomaticDelay = 50;
            this.toolTip.AutoPopDelay = 5000;
            this.toolTip.InitialDelay = 50;
            this.toolTip.ReshowDelay = 10;
            this.toolTip.ShowAlways = true;
            // 
            // textBoxServerConnection
            // 
            this.textBoxServerConnection.Location = new System.Drawing.Point(11, 13);
            this.textBoxServerConnection.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.textBoxServerConnection.Name = "textBoxServerConnection";
            this.textBoxServerConnection.Size = new System.Drawing.Size(170, 20);
            this.textBoxServerConnection.TabIndex = 0;
            this.textBoxServerConnection.Text = "localhost:8890";
            this.toolTip.SetToolTip(this.textBoxServerConnection, "Example: 192.168.1.10:8890");
            // 
            // labelTcpConnectionFormat
            // 
            this.labelTcpConnectionFormat.AutoSize = true;
            this.labelTcpConnectionFormat.Location = new System.Drawing.Point(10, 35);
            this.labelTcpConnectionFormat.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTcpConnectionFormat.Name = "labelTcpConnectionFormat";
            this.labelTcpConnectionFormat.Size = new System.Drawing.Size(86, 13);
            this.labelTcpConnectionFormat.TabIndex = 3;
            this.labelTcpConnectionFormat.Text = "Format: host:port";
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(196, 10);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "&OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(196, 39);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // ConnectTo
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(283, 71);
            this.ControlBox = false;
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.labelTcpConnectionFormat);
            this.Controls.Add(this.textBoxServerConnection);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConnectTo";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Connect to Service";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label labelTcpConnectionFormat;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.TextBox textBoxServerConnection;
    }
}