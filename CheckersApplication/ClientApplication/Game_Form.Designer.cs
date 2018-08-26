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
            this.Exit = new System.Windows.Forms.Label();
            this.tbConsole = new System.Windows.Forms.TextBox();
            this.tbTurn = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.Board = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Board)).BeginInit();
            this.SuspendLayout();
            // 
            // Exit
            // 
            this.Exit.AutoSize = true;
            this.Exit.Font = new System.Drawing.Font("Corbel", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Exit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(250)))));
            this.Exit.Location = new System.Drawing.Point(917, -6);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(28, 36);
            this.Exit.TabIndex = 3;
            this.Exit.Text = "x";
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // tbConsole
            // 
            this.tbConsole.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(28)))), ((int)(((byte)(33)))));
            this.tbConsole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(250)))));
            this.tbConsole.Location = new System.Drawing.Point(12, 607);
            this.tbConsole.Multiline = true;
            this.tbConsole.Name = "tbConsole";
            this.tbConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbConsole.Size = new System.Drawing.Size(921, 202);
            this.tbConsole.TabIndex = 4;
            // 
            // tbTurn
            // 
            this.tbTurn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(28)))), ((int)(((byte)(33)))));
            this.tbTurn.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbTurn.Font = new System.Drawing.Font("Impact", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTurn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(250)))));
            this.tbTurn.Location = new System.Drawing.Point(71, 33);
            this.tbTurn.Name = "tbTurn";
            this.tbTurn.ReadOnly = true;
            this.tbTurn.Size = new System.Drawing.Size(160, 33);
            this.tbTurn.TabIndex = 6;
            this.tbTurn.Text = "Not Your Turn";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button1.Location = new System.Drawing.Point(107, 121);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Submit";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Board
            // 
            this.Board.Image = global::ClientApplication.Properties.Resources.Checker_Board;
            this.Board.Location = new System.Drawing.Point(318, 20);
            this.Board.Name = "Board";
            this.Board.Size = new System.Drawing.Size(579, 587);
            this.Board.TabIndex = 5;
            this.Board.TabStop = false;
            // 
            // Game_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(28)))), ((int)(((byte)(33)))));
            this.ClientSize = new System.Drawing.Size(944, 821);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbTurn);
            this.Controls.Add(this.tbConsole);
            this.Controls.Add(this.Exit);
            this.Controls.Add(this.Board);
            this.Name = "Game_Form";
            this.Text = "Game_Form";
            this.Shown += new System.EventHandler(this.Game_Form_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.Board)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Exit;
        private System.Windows.Forms.TextBox tbConsole;
        private System.Windows.Forms.TextBox tbTurn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox Board;
    }
}