namespace CheckersPlayerClient {
    partial class PlayerClientForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.ConsoleTextBox = new System.Windows.Forms.TextBox();
            this.SendMessageButton = new System.Windows.Forms.Button();
            this.PortTextBox = new System.Windows.Forms.TextBox();
            this.PortLabel = new System.Windows.Forms.Label();
            this.IPTextBox = new System.Windows.Forms.TextBox();
            this.IPLabel = new System.Windows.Forms.Label();
            this.ConnectClientButton = new System.Windows.Forms.Button();
            this.MessageTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ConsoleTextBox
            // 
            this.ConsoleTextBox.Location = new System.Drawing.Point(35, 65);
            this.ConsoleTextBox.Multiline = true;
            this.ConsoleTextBox.Name = "ConsoleTextBox";
            this.ConsoleTextBox.Size = new System.Drawing.Size(560, 206);
            this.ConsoleTextBox.TabIndex = 13;
            // 
            // SendMessageButton
            // 
            this.SendMessageButton.Location = new System.Drawing.Point(479, 374);
            this.SendMessageButton.Name = "SendMessageButton";
            this.SendMessageButton.Size = new System.Drawing.Size(116, 33);
            this.SendMessageButton.TabIndex = 12;
            this.SendMessageButton.Text = "Send Message";
            this.SendMessageButton.UseVisualStyleBackColor = true;
            this.SendMessageButton.Click += new System.EventHandler(this.SendMessageButton_Click);
            // 
            // PortTextBox
            // 
            this.PortTextBox.Location = new System.Drawing.Point(266, 31);
            this.PortTextBox.Name = "PortTextBox";
            this.PortTextBox.Size = new System.Drawing.Size(43, 22);
            this.PortTextBox.TabIndex = 11;
            this.PortTextBox.Text = "8910";
            // 
            // PortLabel
            // 
            this.PortLabel.AutoSize = true;
            this.PortLabel.Location = new System.Drawing.Point(218, 34);
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.Size = new System.Drawing.Size(42, 17);
            this.PortLabel.TabIndex = 10;
            this.PortLabel.Text = "Port: ";
            // 
            // IPTextBox
            // 
            this.IPTextBox.Location = new System.Drawing.Point(99, 31);
            this.IPTextBox.Name = "IPTextBox";
            this.IPTextBox.Size = new System.Drawing.Size(100, 22);
            this.IPTextBox.TabIndex = 9;
            this.IPTextBox.Text = "127.0.0.1";
            // 
            // IPLabel
            // 
            this.IPLabel.AutoSize = true;
            this.IPLabel.Location = new System.Drawing.Point(32, 34);
            this.IPLabel.Name = "IPLabel";
            this.IPLabel.Size = new System.Drawing.Size(61, 17);
            this.IPLabel.TabIndex = 8;
            this.IPLabel.Text = "Host IP: ";
            // 
            // ConnectClientButton
            // 
            this.ConnectClientButton.Location = new System.Drawing.Point(342, 26);
            this.ConnectClientButton.Name = "ConnectClientButton";
            this.ConnectClientButton.Size = new System.Drawing.Size(116, 33);
            this.ConnectClientButton.TabIndex = 7;
            this.ConnectClientButton.Text = "Join Game";
            this.ConnectClientButton.UseVisualStyleBackColor = true;
            this.ConnectClientButton.Click += new System.EventHandler(this.ConnectClientButton_Click);
            // 
            // MessageTextBox
            // 
            this.MessageTextBox.Location = new System.Drawing.Point(35, 309);
            this.MessageTextBox.Multiline = true;
            this.MessageTextBox.Name = "MessageTextBox";
            this.MessageTextBox.Size = new System.Drawing.Size(560, 59);
            this.MessageTextBox.TabIndex = 14;
            // 
            // PlayerClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 446);
            this.Controls.Add(this.MessageTextBox);
            this.Controls.Add(this.ConsoleTextBox);
            this.Controls.Add(this.SendMessageButton);
            this.Controls.Add(this.PortTextBox);
            this.Controls.Add(this.PortLabel);
            this.Controls.Add(this.IPTextBox);
            this.Controls.Add(this.IPLabel);
            this.Controls.Add(this.ConnectClientButton);
            this.Name = "PlayerClientForm";
            this.Text = "Test Player Client Connection to Server";
            this.Load += new System.EventHandler(this.PlayerClientForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ConsoleTextBox;
        private System.Windows.Forms.Button SendMessageButton;
        private System.Windows.Forms.TextBox PortTextBox;
        private System.Windows.Forms.Label PortLabel;
        private System.Windows.Forms.TextBox IPTextBox;
        private System.Windows.Forms.Label IPLabel;
        private System.Windows.Forms.Button ConnectClientButton;
        private System.Windows.Forms.TextBox MessageTextBox;
    }
}

