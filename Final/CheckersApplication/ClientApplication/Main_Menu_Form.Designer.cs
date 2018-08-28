namespace ClientApplication 
{
    partial class Main_Menu_Form {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main_Menu_Form));
            this.Menu_Panel = new System.Windows.Forms.Panel();
            this.Join_Button = new System.Windows.Forms.Button();
            this.Host_Button = new System.Windows.Forms.Button();
            this.Menu_Title = new System.Windows.Forms.Label();
            this.Menu_Icon = new System.Windows.Forms.PictureBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.ipBox = new System.Windows.Forms.TextBox();
            this.Menu_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Menu_Icon)).BeginInit();
            this.SuspendLayout();
            // 
            // Menu_Panel
            // 
            this.Menu_Panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(38)))), ((int)(((byte)(50)))));
            this.Menu_Panel.Controls.Add(this.ipBox);
            this.Menu_Panel.Controls.Add(this.Join_Button);
            this.Menu_Panel.Controls.Add(this.Host_Button);
            this.Menu_Panel.Controls.Add(this.Menu_Title);
            this.Menu_Panel.Controls.Add(this.Menu_Icon);
            this.Menu_Panel.Dock = System.Windows.Forms.DockStyle.Left;
            this.Menu_Panel.Location = new System.Drawing.Point(0, 0);
            this.Menu_Panel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Menu_Panel.Name = "Menu_Panel";
            this.Menu_Panel.Size = new System.Drawing.Size(1020, 752);
            this.Menu_Panel.TabIndex = 0;
            // 
            // Join_Button
            // 
            this.Join_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(28)))), ((int)(((byte)(33)))));
            this.Join_Button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Join_Button.Font = new System.Drawing.Font("Impact", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Join_Button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(250)))));
            this.Join_Button.Location = new System.Drawing.Point(385, 476);
            this.Join_Button.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Join_Button.Name = "Join_Button";
            this.Join_Button.Size = new System.Drawing.Size(240, 79);
            this.Join_Button.TabIndex = 3;
            this.Join_Button.Text = "Join";
            this.Join_Button.UseVisualStyleBackColor = false;
            this.Join_Button.Click += new System.EventHandler(this.Join_Button_Click);
            // 
            // Host_Button
            // 
            this.Host_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(28)))), ((int)(((byte)(33)))));
            this.Host_Button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Host_Button.Font = new System.Drawing.Font("Impact", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Host_Button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(250)))));
            this.Host_Button.Location = new System.Drawing.Point(385, 374);
            this.Host_Button.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Host_Button.Name = "Host_Button";
            this.Host_Button.Size = new System.Drawing.Size(240, 79);
            this.Host_Button.TabIndex = 2;
            this.Host_Button.Text = "Host";
            this.Host_Button.UseVisualStyleBackColor = false;
            this.Host_Button.Click += new System.EventHandler(this.Host_Button_Click);
            // 
            // Menu_Title
            // 
            this.Menu_Title.AutoSize = true;
            this.Menu_Title.Font = new System.Drawing.Font("Impact", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Menu_Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(250)))));
            this.Menu_Title.Location = new System.Drawing.Point(347, 233);
            this.Menu_Title.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Menu_Title.Name = "Menu_Title";
            this.Menu_Title.Size = new System.Drawing.Size(302, 98);
            this.Menu_Title.TabIndex = 1;
            this.Menu_Title.Text = "King Me";
            // 
            // Menu_Icon
            // 
            this.Menu_Icon.Image = ((System.Drawing.Image)(resources.GetObject("Menu_Icon.Image")));
            this.Menu_Icon.Location = new System.Drawing.Point(0, 44);
            this.Menu_Icon.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Menu_Icon.Name = "Menu_Icon";
            this.Menu_Icon.Size = new System.Drawing.Size(1020, 185);
            this.Menu_Icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Menu_Icon.TabIndex = 0;
            this.Menu_Icon.TabStop = false;
            // 
            // ipBox
            // 
            this.ipBox.Font = new System.Drawing.Font("Impact", 12F);
            this.ipBox.Location = new System.Drawing.Point(385, 586);
            this.ipBox.Name = "ipBox";
            this.ipBox.Size = new System.Drawing.Size(240, 32);
            this.ipBox.TabIndex = 4;
            this.ipBox.Text = "10.250.123.157";
            // 
            // Main_Menu_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(28)))), ((int)(((byte)(33)))));
            this.ClientSize = new System.Drawing.Size(1097, 752);
            this.Controls.Add(this.Menu_Panel);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Main_Menu_Form";
            this.Tag = "";
            this.Text = "King Me - v1.6";
            this.Menu_Panel.ResumeLayout(false);
            this.Menu_Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Menu_Icon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Menu_Panel;
        private System.Windows.Forms.PictureBox Menu_Icon;
        private System.Windows.Forms.Label Menu_Title;
        private System.Windows.Forms.Button Host_Button;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button Join_Button;
        private System.Windows.Forms.TextBox ipBox;
    }
}

