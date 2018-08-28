namespace ClientApplication {
    partial class Game_Form {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Game_Form));
            this.tbTurn = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.Board = new System.Windows.Forms.PictureBox();
            this.Reset_Move_Button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Board)).BeginInit();
            this.SuspendLayout();
            // 
            // tbTurn
            // 
            this.tbTurn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(28)))), ((int)(((byte)(33)))));
            this.tbTurn.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbTurn.Font = new System.Drawing.Font("Impact", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTurn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(250)))));
            this.tbTurn.Location = new System.Drawing.Point(12, 38);
            this.tbTurn.Name = "tbTurn";
            this.tbTurn.ReadOnly = true;
            this.tbTurn.Size = new System.Drawing.Size(217, 33);
            this.tbTurn.TabIndex = 6;
            this.tbTurn.Text = "Waiting...";
            this.tbTurn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(28)))), ((int)(((byte)(33)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Impact", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(250)))));
            this.button1.Location = new System.Drawing.Point(58, 118);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(127, 50);
            this.button1.TabIndex = 7;
            this.button1.Text = "Submit";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Board
            // 
            this.Board.Image = ((System.Drawing.Image)(resources.GetObject("Board.Image")));
            this.Board.Location = new System.Drawing.Point(234, 20);
            this.Board.Name = "Board";
            this.Board.Size = new System.Drawing.Size(579, 587);
            this.Board.TabIndex = 5;
            this.Board.TabStop = false;
            // 
            // Reset_Move_Button
            // 
            this.Reset_Move_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(28)))), ((int)(((byte)(33)))));
            this.Reset_Move_Button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Reset_Move_Button.Font = new System.Drawing.Font("Impact", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Reset_Move_Button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(250)))));
            this.Reset_Move_Button.Location = new System.Drawing.Point(58, 174);
            this.Reset_Move_Button.Name = "Reset_Move_Button";
            this.Reset_Move_Button.Size = new System.Drawing.Size(127, 50);
            this.Reset_Move_Button.TabIndex = 8;
            this.Reset_Move_Button.Text = "Reset Move";
            this.Reset_Move_Button.UseVisualStyleBackColor = false;
            this.Reset_Move_Button.Click += new System.EventHandler(this.Reset_Move_Button_Click);
            // 
            // Game_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(28)))), ((int)(((byte)(33)))));
            this.ClientSize = new System.Drawing.Size(823, 611);
            this.Controls.Add(this.Reset_Move_Button);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbTurn);
            this.Controls.Add(this.Board);
            this.Name = "Game_Form";
            this.Text = "King Me - v1.6";
            this.Shown += new System.EventHandler(this.Game_Form_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.Board)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tbTurn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox Board;
        private System.Windows.Forms.Button Reset_Move_Button;
    }
}