namespace robot
{
    partial class BillingSynchronizer
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
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnStartStopProcess = new System.Windows.Forms.Button();
            this.lblUsersQuantity = new System.Windows.Forms.Label();
            this.txtUsersQuantity = new System.Windows.Forms.Label();
            this.txtLastUserUpdate = new System.Windows.Forms.Label();
            this.lblLastUserUpdate = new System.Windows.Forms.Label();
            this.webBrowserNetFlix = new System.Windows.Forms.WebBrowser();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabBrowser = new System.Windows.Forms.TabPage();
            this.tabLog = new System.Windows.Forms.TabPage();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.tabControl.SuspendLayout();
            this.tabBrowser.SuspendLayout();
            this.tabLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStartStopProcess
            // 
            this.btnStartStopProcess.Location = new System.Drawing.Point(1031, 12);
            this.btnStartStopProcess.Name = "btnStartStopProcess";
            this.btnStartStopProcess.Size = new System.Drawing.Size(75, 23);
            this.btnStartStopProcess.TabIndex = 0;
            this.btnStartStopProcess.Text = "Start";
            this.btnStartStopProcess.UseVisualStyleBackColor = true;
            // 
            // lblUsersQuantity
            // 
            this.lblUsersQuantity.AutoSize = true;
            this.lblUsersQuantity.Location = new System.Drawing.Point(45, 12);
            this.lblUsersQuantity.Name = "lblUsersQuantity";
            this.lblUsersQuantity.Size = new System.Drawing.Size(37, 13);
            this.lblUsersQuantity.TabIndex = 2;
            this.lblUsersQuantity.Text = "Users:";
            // 
            // txtUsersQuantity
            // 
            this.txtUsersQuantity.AutoSize = true;
            this.txtUsersQuantity.Location = new System.Drawing.Point(81, 13);
            this.txtUsersQuantity.Name = "txtUsersQuantity";
            this.txtUsersQuantity.Size = new System.Drawing.Size(13, 13);
            this.txtUsersQuantity.TabIndex = 3;
            this.txtUsersQuantity.Text = "0";
            // 
            // txtLastUserUpdate
            // 
            this.txtLastUserUpdate.AutoSize = true;
            this.txtLastUserUpdate.Location = new System.Drawing.Point(81, 30);
            this.txtLastUserUpdate.Name = "txtLastUserUpdate";
            this.txtLastUserUpdate.Size = new System.Drawing.Size(0, 13);
            this.txtLastUserUpdate.TabIndex = 5;
            // 
            // lblLastUserUpdate
            // 
            this.lblLastUserUpdate.AutoSize = true;
            this.lblLastUserUpdate.Location = new System.Drawing.Point(16, 29);
            this.lblLastUserUpdate.Name = "lblLastUserUpdate";
            this.lblLastUserUpdate.Size = new System.Drawing.Size(66, 13);
            this.lblLastUserUpdate.TabIndex = 4;
            this.lblLastUserUpdate.Text = "Last update:";
            // 
            // webBrowserNetFlix
            // 
            this.webBrowserNetFlix.Location = new System.Drawing.Point(6, 5);
            this.webBrowserNetFlix.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserNetFlix.Name = "webBrowserNetFlix";
            this.webBrowserNetFlix.Size = new System.Drawing.Size(1071, 505);
            this.webBrowserNetFlix.TabIndex = 6;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabBrowser);
            this.tabControl.Controls.Add(this.tabLog);
            this.tabControl.Location = new System.Drawing.Point(19, 59);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1091, 539);
            this.tabControl.TabIndex = 7;
            // 
            // tabBrowser
            // 
            this.tabBrowser.Controls.Add(this.webBrowserNetFlix);
            this.tabBrowser.Location = new System.Drawing.Point(4, 22);
            this.tabBrowser.Name = "tabBrowser";
            this.tabBrowser.Padding = new System.Windows.Forms.Padding(3);
            this.tabBrowser.Size = new System.Drawing.Size(1083, 513);
            this.tabBrowser.TabIndex = 0;
            this.tabBrowser.Text = "Browser Netflix";
            this.tabBrowser.UseVisualStyleBackColor = true;
            // 
            // tabLog
            // 
            this.tabLog.Controls.Add(this.txtLog);
            this.tabLog.Location = new System.Drawing.Point(4, 22);
            this.tabLog.Name = "tabLog";
            this.tabLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabLog.Size = new System.Drawing.Size(1083, 513);
            this.tabLog.TabIndex = 1;
            this.tabLog.Text = "Log";
            this.tabLog.UseVisualStyleBackColor = true;
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(4, 3);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(1074, 510);
            this.txtLog.TabIndex = 2;
            // 
            // BillingSynchronizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1122, 610);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.txtLastUserUpdate);
            this.Controls.Add(this.lblLastUserUpdate);
            this.Controls.Add(this.txtUsersQuantity);
            this.Controls.Add(this.lblUsersQuantity);
            this.Controls.Add(this.btnStartStopProcess);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(603, 405);
            this.Name = "BillingSynchronizer";
            this.Text = "BillingSynchronizer";
            this.tabControl.ResumeLayout(false);
            this.tabBrowser.ResumeLayout(false);
            this.tabLog.ResumeLayout(false);
            this.tabLog.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStartStopProcess;
        private System.Windows.Forms.Label lblUsersQuantity;
        private System.Windows.Forms.Label txtUsersQuantity;
        private System.Windows.Forms.Label txtLastUserUpdate;
        private System.Windows.Forms.Label lblLastUserUpdate;
        private System.Windows.Forms.WebBrowser webBrowserNetFlix;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabBrowser;
        private System.Windows.Forms.TabPage tabLog;
        private System.Windows.Forms.TextBox txtLog;
    }
}