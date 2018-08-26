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
            this.Exit = new System.Windows.Forms.Label();
            this.tbConsole = new System.Windows.Forms.TextBox();
            this.Board = new System.Windows.Forms.PictureBox();
            this.tbTurn = new System.Windows.Forms.TextBox();
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
            // Board
            // 
            this.Board.Image = ((System.Drawing.Image)(resources.GetObject("Board.Image")));
            this.Board.Location = new System.Drawing.Point(332, 20);
            this.Board.Name = "Board";
            this.Board.Size = new System.Drawing.Size(579, 587);
            this.Board.TabIndex = 5;
            this.Board.TabStop = false;
            // 
            // tbTurn
            // 
            this.tbTurn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(28)))), ((int)(((byte)(33)))));
            this.tbTurn.Font = new System.Drawing.Font("Impact", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTurn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(250)))));
            this.tbTurn.Location = new System.Drawing.Point(71, 33);
            this.tbTurn.Name = "tbTurn";
            this.tbTurn.Size = new System.Drawing.Size(160, 40);
            this.tbTurn.TabIndex = 6;
            this.tbTurn.Text = "Not Your Turn";
            // 
            // Game_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(28)))), ((int)(((byte)(33)))));
            this.ClientSize = new System.Drawing.Size(944, 821);
            this.Controls.Add(this.tbTurn);
            this.Controls.Add(this.Board);
            this.Controls.Add(this.tbConsole);
            this.Controls.Add(this.Exit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
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
        private System.Windows.Forms.PictureBox Board;
        private System.Windows.Forms.TextBox tbTurn;
    }
}