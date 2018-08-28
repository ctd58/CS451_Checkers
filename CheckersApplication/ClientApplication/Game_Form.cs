using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using ServerApplication;
using System.Diagnostics;

namespace ClientApplication {
    public partial class Game_Form : Form {
        bool host;
        delegate void StringArgReturningVoidDelegate(string text);
        Client client;
        Task clientTask;
        public bool waitingToSubmit = false;
        public string ipAddress;

        private List<Button> activePieces = new List<Button>();
        private Point[,] boardPositions = new Point[8, 8];

        private PlayerMove playerMove;

        public Game_Form(bool host) {
            InitializeComponent();
            this.host = host;
            FillBoardPositions();
            playerMove = new PlayerMove();
        }

        public Game_Form(bool host, string ip)
        {
            InitializeComponent();
            this.host = host;
            FillBoardPositions();
            playerMove = new PlayerMove();
            ipAddress = ip;
        }

        private void FillBoardPositions() {
            int x = 4;
            int y = 4;
            int step = 72;
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    boardPositions[i, j] = new Point(x, y);
                    x += step;
                }
                y += step;
                x = 4;
            }
        }

        private void ClearPieces() {
            foreach (Button piece in  activePieces) {
                this.BeginInvoke((Action)delegate () {
                    piece.Dispose();
                });
                
            }
            activePieces.Clear();
        }

        private Button CreatePiece(Point pos, CKPoint point, CheckerPieces Ptype) {
            Button piece = new Button();
            piece.TabStop = false;
            piece.FlatStyle = FlatStyle.Flat;
            piece.FlatAppearance.BorderSize = 0;
            piece.Width = 64;
            piece.Height = 64;
            piece.Location = pos;
            piece.Tag = point;
            switch (Ptype){
                case CheckerPieces.Red:
                    piece.Image = Properties.Resources.Red_Piece;
                    piece.Click += new EventHandler(Redbutton_Click);
                    break;
                case CheckerPieces.RedKing:
                    piece.Image = Properties.Resources.Red_Piece_King;
                    piece.Click += new EventHandler(RedKingbutton_Click);
                    break;
                case CheckerPieces.Black:
                    piece.Image = Properties.Resources.Black_Piece;
                    piece.Click += new EventHandler(Blackbutton_Click);
                    break;
                case CheckerPieces.BlackKing:
                    piece.Image = Properties.Resources.Black_Piece_King;
                    piece.Click += new EventHandler(BlackKingbutton_Click);
                    break;
                case CheckerPieces.Empty:
                    //piece.Image = Properties.Resources.Red_Piece;
                    piece.Click += new EventHandler(Emptybutton_Click);
                    break;
                default:
                    break;
            }

            this.BeginInvoke((Action)delegate () { this.Controls.Add(piece); piece.BringToFront(); piece.Parent = Board; piece.BackColor = Color.Transparent; Board.Refresh();});
            return piece;
        }

        private void Exit_Click(object sender, EventArgs e) {
            this.Close();
        }

        public TextBox GetTurnBox() {
            return tbTurn;
        }

        public void SetSubmitMove(bool value)
        {
            waitingToSubmit = value;
        }

        public PlayerMove SubmitPlayerMove()
        {
            waitingToSubmit = false;
            return playerMove;
        }

        public void DisableInputs()
        {
            button1.BeginInvoke((Action)delegate () { button1.Enabled = false; Reset_Move_Button.Enabled = false; });
            playerMove.RestartMove();
        }

        public void EnableInputs()
        {
            button1.BeginInvoke((Action)delegate () { button1.Enabled = true; Reset_Move_Button.Enabled = true; });
            playerMove.RestartMove();
        }

        public void RestartMove()
        {
            this.BeginInvoke((Action)delegate () {
                playerMove.RestartMove();
                UpdateBoard(client.GetBoard());
            });
        }

        public void SetTurnBox(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.tbTurn.InvokeRequired)
            {
                StringArgReturningVoidDelegate d = new StringArgReturningVoidDelegate(SetTurnBox);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                if (text == "")
                    this.tbTurn.Text = "";
                else
                    this.tbTurn.Text += text + "\r\n";
            }
        }

        public void UpdateBoard(GameBoard game) {
            if(activePieces != null)
                ClearPieces();
            CheckerPieces[,] gameBoard = game.GetGameBoard();
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    if(gameBoard[i,j] != CheckerPieces.blank)
                        activePieces.Add(CreatePiece(boardPositions[i, j], new CKPoint(i, j), gameBoard[i, j]));
                }
            }
        }

        private void RunServer() {
            Process.Start(Application.StartupPath.ToString() + @"\ServerApplication.exe");
        }

        private void RunClient() {
            client = new Client(this, ipAddress);
            client.ConnectToServer(); //2. START TRYING TO CONNECT TO SERVER
        }

        private void Game_Form_Shown(object sender, EventArgs e) {
            if (host) {
                Task serverTask = Task.Factory.StartNew(() => RunServer());
                clientTask = Task.Factory.StartNew(() => RunClient());
            } else {
                clientTask = Task.Factory.StartNew(() => RunClient());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (playerMove.GetPlayerMove().Count > 1)
            {
                waitingToSubmit = true;
            }
            //client.ReceiveResponse();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UpdateBoard(new GameBoard());
        }
        
        protected void Redbutton_Click(object sender, EventArgs e)
        {
            if (client.GetPlayerId() == 2)
            {
                return;
            }
            Button button = sender as Button;
            if (playerMove.IsEmpty()) {
                CKPoint point = (CKPoint)button.Tag;
                playerMove.BuildMove(point);
                System.Diagnostics.Debug.WriteLine("Point: " + point.GetRow() + "," + point.GetColumn());

                // Color boarder
                Button piece = (Button)sender;
                piece.FlatAppearance.BorderSize = 1;
                piece.FlatAppearance.BorderColor = Color.White;
            }
            // identify which button was clicked and perform necessary actions
        }
        protected void RedKingbutton_Click(object sender, EventArgs e)
        {
            if (client.GetPlayerId() == 2)
            {
                return;
            }
            Button button = sender as Button;
            if (playerMove.IsEmpty()) {
                CKPoint point = (CKPoint)button.Tag;
                playerMove.BuildMove(point);
                System.Diagnostics.Debug.WriteLine("Point: " + point.GetRow() + "," + point.GetColumn());

                // Color boarder
                Button piece = (Button)sender;
                piece.FlatAppearance.BorderSize = 1;
                piece.FlatAppearance.BorderColor = Color.White;
            }
            // identify which button was clicked and perform necessary actions
        }
        protected void Blackbutton_Click(object sender, EventArgs e)
        {
            if (client.GetPlayerId() == 1)
            {
                return;
            }
            Button button = sender as Button;
            if (playerMove.IsEmpty()) {
                CKPoint point = (CKPoint)button.Tag;
                playerMove.BuildMove(point);
                System.Diagnostics.Debug.WriteLine("Point: " + point.GetRow() + "," + point.GetColumn());

                // Color boarder
                Button piece = (Button)sender;
                piece.FlatAppearance.BorderSize = 1;
                piece.FlatAppearance.BorderColor = Color.White;
            }
            // identify which button was clicked and perform necessary actions
        }
        protected void BlackKingbutton_Click(object sender, EventArgs e)
        {
            if (client.GetPlayerId() == 1)
            {
                return;
            }
            Button button = sender as Button;
            if (playerMove.IsEmpty()) {
                CKPoint point = (CKPoint)button.Tag;
                playerMove.BuildMove(point);
                System.Diagnostics.Debug.WriteLine("Point: " + point.GetRow() + "," + point.GetColumn());

                // Color boarder
                Button piece = (Button)sender;
                piece.FlatAppearance.BorderSize = 1;
                piece.FlatAppearance.BorderColor = Color.White;
            }
            // identify which button was clicked and perform necessary actions
        }
        protected void Emptybutton_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (playerMove.GetSize() >= 1) {
                CKPoint point = (CKPoint)button.Tag;
                playerMove.BuildMove(point);
                System.Diagnostics.Debug.WriteLine("Point: " + point.GetRow() + "," + point.GetColumn());

                // Color boarder
                Button piece = (Button)sender;
                piece.FlatAppearance.BorderSize = 1;
                piece.FlatAppearance.BorderColor = Color.White;
            }
            // identify which button was clicked and perform necessary actions
        }

        private void Reset_Move_Button_Click(object sender, EventArgs e) {
            RestartMove();
        }
    }
}
