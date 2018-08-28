using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApplication {
    public class ClientCheckersGame {
        #region Attributes

        private int PlayerID;
        private PlayerMove currentPlayerMove;
        private const float TURNTIME = 10.0f;

        private GameBoard gameBoard;

        private Game_Form gameForm;
        #endregion

        #region Constructors

        public ClientCheckersGame(Game_Form game) {
            gameForm = game;
            gameBoard = new GameBoard();
        }
        #endregion

        #region Getters and Setters

        public int GetPlayerID()
        {
            return PlayerID;
        }

        public void SetPlayerID(int id)
        {            
            PlayerID = id;
        }

        public void UpdateBoard(GameBoard board)
        {
            gameBoard = board;
            gameBoard.PrintBoard();
        }

        public PlayerMove GetCurrentPlayerMove() {
            return currentPlayerMove;
        }
        public void SetCurrentPlayerMove(PlayerMove currentPlayerMove) {
            this.currentPlayerMove = currentPlayerMove;
        }

        public int GetCurrentPlayer() {
            return gameBoard.GetCurrentPlayer();
        }
        public void SetCurrentPlayer(int currentPlayer) {
            gameBoard.SetCurrentPlayer(currentPlayer);
        }

        public float GetTurnTime() {
            return TURNTIME;
        }

        public GameStatus GetGameStatus() {
            return gameBoard.CheckForWin();
        }

        public GameBoard GetGameBoard() {
            return gameBoard;
        }

        public DateTime GetTimerExpires() {
            return gameBoard.GetTimerExpires();
        }
        public void SetTimerExpires(DateTime timerExpires) {
            gameBoard.SetTimerExpires(timerExpires);
        }
        #endregion

        #region Methods

        public bool IsMyTurn()
        {
            if(gameBoard.GetCurrentPlayer() == PlayerID)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ApplyMove() {

            try {
                gameBoard.ApplyMove(currentPlayerMove);
            }
            catch (InvalidOperationException e) {
                Console.WriteLine("You cheater!");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.ToString());
                return false;
            }
            return true;
        }

        public void SwitchTurns() {

            currentPlayerMove.RestartMove();
            int newCurrentPlayer = (gameBoard.GetCurrentPlayer() == 1) ? 2 : 1;
            gameBoard.SetCurrentPlayer(newCurrentPlayer);
        }
        #endregion
    }
}
