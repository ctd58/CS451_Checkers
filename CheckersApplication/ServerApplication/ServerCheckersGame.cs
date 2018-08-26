using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApplication {
    public class ServerCheckersGame {
        #region Attributes

        private PlayerMove currentPlayerMove;
        private const float TURNTIME = 10.0f;

        private GameBoard gameBoard;
        #endregion

        #region Constructors

        public ServerCheckersGame() {
            gameBoard = new GameBoard();
            SetCurrentPlayer(1);
        }
        #endregion

        #region Getters and Setters

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

        public bool ApplyMove() {

            try {
                return gameBoard.ApplyMove(currentPlayerMove);
            }
            catch (InvalidOperationException e) {
                Console.WriteLine("You cheater!");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        public void SwitchTurns() {

            try
            {
                currentPlayerMove.RestartMove();
                currentPlayerMove.SwitchPlayer();
            }
            catch(Exception e) { Console.WriteLine("Empty CurrentPlayerMove"); }
            int newCurrentPlayer = (gameBoard.GetCurrentPlayer() == 1) ? 2 : 1;
            gameBoard.SetCurrentPlayer(newCurrentPlayer);
        }
        #endregion
    }
}
