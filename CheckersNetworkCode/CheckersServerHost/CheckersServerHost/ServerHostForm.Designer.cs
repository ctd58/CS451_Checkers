namespace CheckersServerHost {
    partial class ServerHostForm {
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
            this.StartHostButton = new System.Windows.Forms.Button();
            this.IPLabel = new System.Windows.Forms.Label();
            this.IPTextBox = new System.Windows.Forms.TextBox();
            this.PortTextBox = new System.Windows.Forms.TextBox();
            this.PortLabel = new System.Windows.Forms.Label();
            this.EndHostButton = new System.Windows.Forms.Button();
            this.ConsoleTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // StartHostButton
            // 
            this.StartHostButton.Location = new System.Drawing.Point(359, 36);
            this.StartHostButton.Name = "StartHostButton";
            this.StartHostButton.Size = new System.Drawing.Size(116, 33);
            this.StartHostButton.TabIndex = 0;
            this.StartHostButton.Text = "Create Game";
            this.StartHostButton.UseVisualStyleBackColor = true;
            this.StartHostButton.Click += new System.EventHandler(this.HostButton_Click);
            // 
            // IPLabel
            // 
            this.IPLabel.AutoSize = true;
            this.IPLabel.Location = new System.Drawing.Point(49, 44);
            this.IPLabel.Name = "IPLabel";
            this.IPLabel.Size = new System.Drawing.Size(61, 17);
            this.IPLabel.TabIndex = 1;
            this.IPLabel.Text = "Host IP: ";
            // 
            // IPTextBox
            // 
            this.IPTextBox.Location = new System.Drawing.Point(116, 41);
            this.IPTextBox.Name = "IPTextBox";
            this.IPTextBox.Size = new System.Drawing.Size(100, 22);
            this.IPTextBox.TabIndex = 2;
            this.IPTextBox.Text = "127.0.0.1";
            // 
            // PortTextBox
            // 
            this.PortTextBox.Location = new System.Drawing.Point(283, 41);
            this.PortTextBox.Name = "PortTextBox";
            this.PortTextBox.Size = new System.Drawing.Size(43, 22);
            this.PortTextBox.TabIndex = 4;
            this.PortTextBox.Text = "8910";
            // 
            // PortLabel
            // 
            this.PortLabel.AutoSize = true;
            this.PortLabel.Location = new System.Drawing.Point(235, 44);
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.Size = new System.Drawing.Size(42, 17);
            this.PortLabel.TabIndex = 3;
            this.PortLabel.Text = "Port: ";
            // 
            // EndHostButton
            // 
            this.EndHostButton.Location = new System.Drawing.Point(496, 36);
            this.EndHostButton.Name = "EndHostButton";
            this.EndHostButton.Size = new System.Drawing.Size(116, 33);
            this.EndHostButton.TabIndex = 5;
            this.EndHostButton.Text = "End Game";
            this.EndHostButton.UseVisualStyleBackColor = true;
            this.EndHostButton.Click += new System.EventHandler(this.EndHostButton_Click);
            // 
            // ConsoleTextBox
            // 
            this.ConsoleTextBox.Location = new System.Drawing.Point(52, 75);
            this.ConsoleTextBox.Multiline = true;
            this.ConsoleTextBox.Name = "ConsoleTextBox";
            this.ConsoleTextBox.Size = new System.Drawing.Size(560, 206);
            this.ConsoleTextBox.TabIndex = 6;
            // 
            // ServerHostForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 373);
            this.Controls.Add(this.ConsoleTextBox);
            this.Controls.Add(this.EndHostButton);
            this.Controls.Add(this.PortTextBox);
            this.Controls.Add(this.PortLabel);
            this.Controls.Add(this.IPTextBox);
            this.Controls.Add(this.IPLabel);
            this.Controls.Add(this.StartHostButton);
            this.Name = "ServerHostForm";
            this.Text = "Test Host Create Server";
            this.Load += new System.EventHandler(this.TestHostForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StartHostButton;
        private System.Windows.Forms.Label IPLabel;
        private System.Windows.Forms.TextBox IPTextBox;
        private System.Windows.Forms.TextBox PortTextBox;
        private System.Windows.Forms.Label PortLabel;
        private System.Windows.Forms.Button EndHostButton;
        private System.Windows.Forms.TextBox ConsoleTextBox;
    }
}

