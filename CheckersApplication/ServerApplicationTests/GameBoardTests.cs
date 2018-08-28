using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests {
    [TestClass()]
    public class GameBoardTests {

        private GameBoard game = new GameBoard();

        [TestMethod()]
        public void GameBoardTest() {
            GameBoard gameTest = new GameBoard();
            Assert.AreNotEqual(gameTest, null);
        }

        [TestMethod()]
        public void GetGameBoardTest() {
            Assert.AreNotEqual(game.GetGameBoard(), null);
        }

        [TestMethod()]
        public void GetCurrentPlayerTest() {
            Assert.AreEqual(game.GetCurrentPlayer(), 1);
        }

        [TestMethod()]
        public void SetCurrentPlayerTest() {
            game.SetCurrentPlayer(2);
            Assert.AreEqual(game.GetCurrentPlayer(), 2);
        }

        [TestMethod()]
        public void CheckForWinTest() {
            Assert.AreEqual(game.CheckForWin(), GameStatus.InProgress);
        }

        [TestMethod()]
        public void ApplyMoveTest() {
            CKPoint p1 = new CKPoint(2,1);
            CKPoint p2 = new CKPoint(3, 2);
            PlayerMove move = new PlayerMove();
            move.BuildMove(p1);
            move.BuildMove(p2);
            Assert.AreEqual(game.ApplyMove(move), true);
        }
    }
}