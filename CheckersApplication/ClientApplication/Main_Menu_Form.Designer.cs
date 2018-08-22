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
            this.Menu_Title = new System.Windows.Forms.Label();
            this.Menu_Icon = new System.Windows.Forms.PictureBox();
            this.Exit = new System.Windows.Forms.Label();
            this.Menu_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Menu_Icon)).BeginInit();
            this.SuspendLayout();
            // 
            // Menu_Panel
            // 
            this.Menu_Panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(38)))), ((int)(((byte)(50)))));
            this.Menu_Panel.Controls.Add(this.Menu_Title);
            this.Menu_Panel.Controls.Add(this.Menu_Icon);
            this.Menu_Panel.Dock = System.Windows.Forms.DockStyle.Left;
            this.Menu_Panel.Location = new System.Drawing.Point(0, 0);
            this.Menu_Panel.Name = "Menu_Panel";
            this.Menu_Panel.Size = new System.Drawing.Size(784, 821);
            this.Menu_Panel.TabIndex = 0;
            // 
            // Menu_Title
            // 
            this.Menu_Title.AutoSize = true;
            this.Menu_Title.Font = new System.Drawing.Font("Impact", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Menu_Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(250)))));
            this.Menu_Title.Location = new System.Drawing.Point(249, 189);
            this.Menu_Title.Name = "Menu_Title";
            this.Menu_Title.Size = new System.Drawing.Size(285, 80);
            this.Menu_Title.TabIndex = 1;
            this.Menu_Title.Text = "Checkers";
            // 
            // Menu_Icon
            // 
            this.Menu_Icon.Image = ((System.Drawing.Image)(resources.GetObject("Menu_Icon.Image")));
            this.Menu_Icon.Location = new System.Drawing.Point(0, 36);
            this.Menu_Icon.Name = "Menu_Icon";
            this.Menu_Icon.Size = new System.Drawing.Size(784, 150);
            this.Menu_Icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Menu_Icon.TabIndex = 0;
            this.Menu_Icon.TabStop = false;
            // 
            // Exit
            // 
            this.Exit.AutoSize = true;
            this.Exit.Font = new System.Drawing.Font("Corbel", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Exit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(250)))));
            this.Exit.Location = new System.Drawing.Point(917, -6);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(28, 36);
            this.Exit.TabIndex = 2;
            this.Exit.Text = "x";
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // Main_Menu_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(28)))), ((int)(((byte)(33)))));
            this.ClientSize = new System.Drawing.Size(944, 821);
            this.Controls.Add(this.Exit);
            this.Controls.Add(this.Menu_Panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Main_Menu_Form";
            this.Text = "Main_Menu_Form";
            this.Menu_Panel.ResumeLayout(false);
            this.Menu_Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Menu_Icon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel Menu_Panel;
        private System.Windows.Forms.PictureBox Menu_Icon;
        private System.Windows.Forms.Label Menu_Title;
        private System.Windows.Forms.Label Exit;
    }
}

