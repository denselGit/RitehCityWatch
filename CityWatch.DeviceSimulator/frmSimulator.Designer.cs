namespace CityWatch.DeviceSimulator
{
    partial class frmSimulator
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
            this.txtServer = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSingleMessagePayload = new System.Windows.Forms.TextBox();
            this.txtSingleMessageTopic = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnTest2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDigitalDeviceUniqueId = new System.Windows.Forms.TextBox();
            this.btnTest1 = new System.Windows.Forms.Button();
            this.btnSendDigitalDeviceState = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.chkState = new System.Windows.Forms.CheckBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.txtDeviceTopic = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(13, 13);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(159, 20);
            this.txtServer.TabIndex = 0;
            this.txtServer.Text = "localhost";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(178, 13);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(100, 20);
            this.txtUsername.TabIndex = 1;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(284, 13);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(100, 20);
            this.txtPassword.TabIndex = 2;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(390, 11);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(99, 23);
            this.btnConnect.TabIndex = 3;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtSingleMessagePayload);
            this.groupBox1.Controls.Add(this.txtSingleMessageTopic);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(13, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(775, 127);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Message";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Payload";
            // 
            // txtSingleMessagePayload
            // 
            this.txtSingleMessagePayload.Location = new System.Drawing.Point(165, 46);
            this.txtSingleMessagePayload.Multiline = true;
            this.txtSingleMessagePayload.Name = "txtSingleMessagePayload";
            this.txtSingleMessagePayload.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSingleMessagePayload.Size = new System.Drawing.Size(604, 75);
            this.txtSingleMessagePayload.TabIndex = 2;
            // 
            // txtSingleMessageTopic
            // 
            this.txtSingleMessageTopic.Location = new System.Drawing.Point(165, 19);
            this.txtSingleMessageTopic.Name = "txtSingleMessageTopic";
            this.txtSingleMessageTopic.Size = new System.Drawing.Size(604, 20);
            this.txtSingleMessageTopic.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Topic";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtDeviceTopic);
            this.groupBox2.Controls.Add(this.btnTest2);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtDigitalDeviceUniqueId);
            this.groupBox2.Controls.Add(this.btnTest1);
            this.groupBox2.Controls.Add(this.btnSendDigitalDeviceState);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.chkState);
            this.groupBox2.Location = new System.Drawing.Point(13, 174);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(775, 95);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Device";
            // 
            // btnTest2
            // 
            this.btnTest2.Location = new System.Drawing.Point(616, 14);
            this.btnTest2.Name = "btnTest2";
            this.btnTest2.Size = new System.Drawing.Size(153, 23);
            this.btnTest2.TabIndex = 7;
            this.btnTest2.Text = "Send 1000, random";
            this.btnTest2.UseVisualStyleBackColor = true;
            this.btnTest2.Click += new System.EventHandler(this.btnTest2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Device unique ID";
            // 
            // txtDigitalDeviceUniqueId
            // 
            this.txtDigitalDeviceUniqueId.Location = new System.Drawing.Point(165, 69);
            this.txtDigitalDeviceUniqueId.Name = "txtDigitalDeviceUniqueId";
            this.txtDigitalDeviceUniqueId.Size = new System.Drawing.Size(100, 20);
            this.txtDigitalDeviceUniqueId.TabIndex = 5;
            // 
            // btnTest1
            // 
            this.btnTest1.Location = new System.Drawing.Point(482, 14);
            this.btnTest1.Name = "btnTest1";
            this.btnTest1.Size = new System.Drawing.Size(128, 23);
            this.btnTest1.TabIndex = 3;
            this.btnTest1.Text = "Send 100, random";
            this.btnTest1.UseVisualStyleBackColor = true;
            this.btnTest1.Click += new System.EventHandler(this.btnTest1_Click);
            // 
            // btnSendDigitalDeviceState
            // 
            this.btnSendDigitalDeviceState.Location = new System.Drawing.Point(377, 14);
            this.btnSendDigitalDeviceState.Name = "btnSendDigitalDeviceState";
            this.btnSendDigitalDeviceState.Size = new System.Drawing.Size(99, 23);
            this.btnSendDigitalDeviceState.TabIndex = 2;
            this.btnSendDigitalDeviceState.Text = "Send";
            this.btnSendDigitalDeviceState.UseVisualStyleBackColor = true;
            this.btnSendDigitalDeviceState.Click += new System.EventHandler(this.btnSendDigitalDeviceState_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Digital device state";
            // 
            // chkState
            // 
            this.chkState.AutoSize = true;
            this.chkState.Location = new System.Drawing.Point(165, 44);
            this.chkState.Name = "chkState";
            this.chkState.Size = new System.Drawing.Size(51, 17);
            this.chkState.TabIndex = 0;
            this.chkState.Text = "State";
            this.chkState.UseVisualStyleBackColor = true;
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(13, 276);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(775, 162);
            this.txtLog.TabIndex = 6;
            // 
            // txtDeviceTopic
            // 
            this.txtDeviceTopic.Location = new System.Drawing.Point(165, 16);
            this.txtDeviceTopic.Name = "txtDeviceTopic";
            this.txtDeviceTopic.Size = new System.Drawing.Size(206, 20);
            this.txtDeviceTopic.TabIndex = 8;
            this.txtDeviceTopic.Text = "/device";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Topic";
            // 
            // frmSimulator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.txtServer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "frmSimulator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Simulator";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSingleMessagePayload;
        private System.Windows.Forms.TextBox txtSingleMessageTopic;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnTest2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDigitalDeviceUniqueId;
        private System.Windows.Forms.Button btnTest1;
        private System.Windows.Forms.Button btnSendDigitalDeviceState;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkState;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDeviceTopic;
    }
}

