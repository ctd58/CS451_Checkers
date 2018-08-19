using System;
using System.Collections;
using System.Collections.Generic;

public enum MessageIdentifiers { ReadyUpdate, OnePlayerConnected, TwoPlayersConnected, StartingGame,
    WaitingForOpponent, GameUpdate, RetryGameUpdate, GameOver, PauseRequest, PauseGame };
public enum GameStatus { InProgress, Player1Wins, Player2Wins, Draw };
public enum CheckerPieces { Empty, Red, RedKing, Black, BlackKing };

[Serializable]
public class GameBoard
{
    #region Attributes

    private int currentPlayer = 1; //player 1 and 2
    private DateTime timerExpires = DateTime.Now;

    private CheckerPieces[,] gameBoard = new CheckerPieces[8, 8];
    private GameStatus gameStatus = GameStatus.InProgress;
    #endregion

    #region Constructors

    public GameBoard() {

        MakeBoard();
        PrintBoard(); 
	}
    #endregion

    #region Getters and Setters

    public GameStatus GetGameStatus() {
        return gameStatus;
    }

    public CheckerPieces[,] GetGameBoard() {
        return gameBoard;
    }

    public DateTime GetTimerExpires() {
        return timerExpires;
    }
    public void SetTimerExpires(DateTime timerExpires) {
        this.timerExpires = timerExpires;
    }

    public int GetCurrentPlayer() {
        return currentPlayer;
    }
    public void SetCurrentPlayer(int currentPlayer) {
        this.currentPlayer = currentPlayer;
    }
    #endregion

    #region Methods

    #region Make and Print Board
    private void MakeBoard() {

        // Player 1 is Red, and is placed on the top 3 rows
        // Top left tile [0,0] is white, and will not have a piece placed on it
        for (int i = 0; i < 8; i++) {
            for (int j = 0; j < 8; j++) {

                if (i == 3 || i == 4) { // Middle rows are empty
                    gameBoard[i, j] = CheckerPieces.Empty;
                }
                else if ((i + 1) % 2 == 1) { // Odd rows i.e. 1,3,5,7

                    if ((j + 1) % 2 == 0 && i < 3) { // Green tile that piece can be placed on
                        gameBoard[i, j] = CheckerPieces.Red;
                    }
                    else if ((j + 1) % 2 == 0 && i > 3) {
                        gameBoard[i, j] = CheckerPieces.Black;
                    }
                    else {
                        gameBoard[i, j] = CheckerPieces.Empty;
                    }
                }
                else { // Even rows
                    if ((j + 1) % 2 == 1 && i < 3) { // Green tile that piece can be placed on
                        gameBoard[i, j] = CheckerPieces.Red;
                    }
                    else if ((j + 1) % 2 == 1 && i > 3) {
                        gameBoard[i, j] = CheckerPieces.Black;
                    }
                    else {
                        gameBoard[i, j] = CheckerPieces.Empty;
                    }
                }
            }
        }
    }

    private void PrintBoard() {
        int count = 1;
        foreach (CheckerPieces checkerPiece in gameBoard) {
            if (checkerPiece == CheckerPieces.Red) {
                Console.Write(" " + checkerPiece + "  | ");
            }
            else {
                Console.Write(checkerPiece + " | ");
            }

            if (count % 8 == 0) {
                Console.WriteLine();
            }
            count++;
        }
        Console.WriteLine();
    }
    #endregion

    public bool ApplyMove(PlayerMove move) {

        List<CKPoint> listPoints = move.GetPlayerMove();
        CKPoint startingPoint = listPoints[0]; // A move will always be at least 2 long, with index 0 being the starting point
        CKPoint endingPoint = listPoints[listPoints.Count - 1];
        CheckerPieces piece = gameBoard[startingPoint.GetColumn(), startingPoint.GetRow()]; // Column is x, Row is y
        CheckerPieces[] opponentPieces = new CheckerPieces[2];
        if (piece == CheckerPieces.Red || piece == CheckerPieces.RedKing) {
            opponentPieces[0] = CheckerPieces.Black;
            opponentPieces[1] = CheckerPieces.BlackKing;
        }
        else if (piece == CheckerPieces.Black || piece == CheckerPieces.BlackKing) {
            opponentPieces[0] = CheckerPieces.Red;
            opponentPieces[1] = CheckerPieces.RedKing;
        }

        CKPoint fromPoint = startingPoint;

        foreach (CKPoint point in listPoints) {

            if (point == startingPoint) { // Starting Point, skip
                continue;
            }
            else if (Math.Abs(fromPoint.GetRow() - point.GetRow()) == 0) { // Invalid move
                // Throw error
                return false;
            }
            else if (Math.Abs(fromPoint.GetRow() - point.GetRow()) == 1 && listPoints.Count == 2) { // Not jumping, just move to new point if empty
                if (piece == CheckerPieces.Red) {
                    if (fromPoint.GetRow() - point.GetRow() != -1 || gameBoard[point.GetColumn(), point.GetRow()] != CheckerPieces.Empty) {
                        // Throw error
                        return false;
                    }
                }
                else if (piece == CheckerPieces.Black) {
                    if (fromPoint.GetRow() - point.GetRow() != 1 || gameBoard[point.GetColumn(), point.GetRow()] != CheckerPieces.Empty) {
                        // Throw error
                        return false;
                    }
                }
                else if (piece == CheckerPieces.RedKing || piece == CheckerPieces.BlackKing) {
                    if (Math.Abs(fromPoint.GetRow() - point.GetRow()) != 1 || gameBoard[point.GetColumn(), point.GetRow()] != CheckerPieces.Empty) {
                        // Throw error
                        return false;
                    }
                }

                gameBoard[fromPoint.GetColumn(), fromPoint.GetRow()] = CheckerPieces.Empty;
                gameBoard[point.GetColumn(), point.GetRow()] = piece;
            }
            else if (Math.Abs(fromPoint.GetRow() - point.GetRow()) == 2) { // Jumping to take pieces
                int middleColumn = (fromPoint.GetColumn() - point.GetColumn()) / 2;
                int middleRow = point.GetRow() - 1;
                if (piece == CheckerPieces.Red) {
                    if (fromPoint.GetRow() - point.GetRow() != -2 || gameBoard[point.GetColumn(), point.GetRow()] != CheckerPieces.Empty ||
                    (gameBoard[middleColumn, middleRow] != CheckerPieces.Black && gameBoard[middleColumn, middleRow] != CheckerPieces.BlackKing)) {
                        // Throw error
                        return false;
                    }
                }
                else if (piece == CheckerPieces.Black) {
                    if (fromPoint.GetRow() - point.GetRow() != 2 || gameBoard[point.GetColumn(), point.GetRow()] != CheckerPieces.Empty ||
                    (gameBoard[middleColumn, middleRow] != opponentPieces[0] && gameBoard[middleColumn, middleRow] != opponentPieces[1])) {
                        // Throw error
                        return false;
                    }
                }
                else if (piece == CheckerPieces.RedKing || piece == CheckerPieces.BlackKing) {
                    if (Math.Abs(fromPoint.GetRow() - point.GetRow()) != 2 || gameBoard[point.GetColumn(), point.GetRow()] != CheckerPieces.Empty ||
                    (gameBoard[middleColumn, middleRow] != opponentPieces[0] && gameBoard[middleColumn, middleRow] != opponentPieces[1])) {
                        // Throw error
                        return false;
                    }
                }

                gameBoard[fromPoint.GetColumn(), fromPoint.GetRow()] = CheckerPieces.Empty;
                gameBoard[middleColumn, middleRow] = CheckerPieces.Empty;
                gameBoard[point.GetColumn(), point.GetRow()] = piece;
            }

            fromPoint = point; // Make the point we just moved to the new fromPoint for the next move
        }

        // Check if piece should turn into King
        if(endingPoint.GetRow() == 0 || endingPoint.GetRow() == 7 && (piece != CheckerPieces.RedKing || piece != CheckerPieces.BlackKing)) {
            if (piece == CheckerPieces.Red) {
                gameBoard[endingPoint.GetColumn(), endingPoint.GetRow()] = CheckerPieces.RedKing;
            }
            else if (piece == CheckerPieces.Black) {
                gameBoard[endingPoint.GetColumn(), endingPoint.GetRow()] = CheckerPieces.BlackKing;
            }
        }

        return true;
    }
    #endregion
}
