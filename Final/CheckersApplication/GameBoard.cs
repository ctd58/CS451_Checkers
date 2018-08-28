using System;
using System.Collections;
using System.Collections.Generic;


public enum MessageIdentifiers { ReadyUpdate, OnePlayerConnected, TwoPlayersConnected, StartingGame,
    WaitingForOpponent, GameUpdate, RetryGameUpdate, GameOver, PauseRequest, PauseGame };
public enum GameStatus { InProgress, Player1Wins, Player2Wins, Draw };
public enum CheckerPieces { blank, Empty, Red, RedKing, Black, BlackKing };

[Serializable]
public class GameBoard
{
    #region Attributes

    private int currentPlayer = 1; //player 1 and 2, 1 is always red, 2 is always black
    private DateTime timerExpires = DateTime.Now;

    private CheckerPieces[,] gameBoard = new CheckerPieces[8, 8];
    private GameStatus gameStatus = GameStatus.InProgress;
    #endregion

    #region Constructors

    public GameBoard() {

        MakeBoard();
        //PrintBoard(); 
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
        if (currentPlayer == 1 || currentPlayer == 2) {
            this.currentPlayer = currentPlayer;
        }
        else {
            throw new InvalidOperationException("currentPlayer must be set to either 1 or 2");
        }
    }
    #endregion

    #region Methods

    #region Make and Print Board
    private void MakeBoard() {

        // Player 1 is Red, and is placed on the top 3 rows
        // Top left tile [0,0] is white, and will not have a piece placed on it
        for (int i = 0; i < 8; i++) {
            for (int j = 0; j < 8; j++) {
                 // Middle rows are empty
                if(i == 3)
                {
                    gameBoard[i, j] = CheckerPieces.Empty;
                }
                else if(i == 4)
                {
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
                if(i % 2 == 0 && j % 2 == 0)
                {
                    gameBoard[i, j] = CheckerPieces.blank;
                }
                if(i % 2 != 0 && j % 2 != 0)
                {
                    gameBoard[i, j] = CheckerPieces.blank;
                }
            }

        }
    }

    public void PrintBoard() {
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

    public GameStatus CheckForWin() {

        int redCount = 0;
        int blackCount = 0;

        foreach (CheckerPieces piece in gameBoard) {
            if (piece == CheckerPieces.Red || piece == CheckerPieces.RedKing) {
                redCount++;
            }
            else if (piece == CheckerPieces.Black || piece == CheckerPieces.BlackKing) {
                blackCount++;
            }
        }

        if (redCount == 0) {
            return GameStatus.Player2Wins;
        }
        else if (blackCount == 0) {
            return GameStatus.Player1Wins;
        }

        // Need to check if no valid moves are left for a team

        // Need to check for draw

        return GameStatus.InProgress;
    }

    public bool ApplyMove(PlayerMove move) {

        List<CKPoint> listPoints = move.GetPlayerMove();
        if(listPoints.Count < 2)
        {
            return false;
        }
        CKPoint startingPoint = listPoints[0]; // A move will always be at least 2 long, with index 0 being the starting point
        CKPoint endingPoint = listPoints[listPoints.Count - 1];
        CheckerPieces piece = gameBoard[startingPoint.GetRow(), startingPoint.GetColumn()]; // Column is x, Row is y
        Console.WriteLine(startingPoint.GetRow() + " " + startingPoint.GetColumn());
        Console.WriteLine(endingPoint.GetRow() + " " + endingPoint.GetColumn());
        Console.WriteLine(piece);
        if(piece == CheckerPieces.Empty)
        {
            return false;
        }
        Console.WriteLine("test");
        CheckerPieces[] opponentPieces = new CheckerPieces[2];
        if (piece == CheckerPieces.Red || piece == CheckerPieces.RedKing && currentPlayer == 1) {
            opponentPieces[0] = CheckerPieces.Black;
            opponentPieces[1] = CheckerPieces.BlackKing;
        }
        else if (piece == CheckerPieces.Black || piece == CheckerPieces.BlackKing && currentPlayer == 2) {
            opponentPieces[0] = CheckerPieces.Red;
            opponentPieces[1] = CheckerPieces.RedKing;
        }
        else{
            return false; // you moved the wrong peice
        }

        CKPoint fromPoint = startingPoint;

        foreach (CKPoint point in listPoints) {

            if (point == startingPoint) { // Starting Point, skip
                continue;
            }
            else if (Math.Abs(fromPoint.GetRow() - point.GetRow()) == 0) { // Invalid move
                // Throw error
                throw new InvalidOperationException("Move not changing rows (y direction)");
                //return false;
            }
            else if (Math.Abs(fromPoint.GetColumn() - point.GetColumn()) == 0) { // Invalid move
                // Throw error
                throw new InvalidOperationException("Move not changing columns (x direction)");
                //return false;
            }
            else if (Math.Abs(fromPoint.GetRow() - point.GetRow()) == 1 && listPoints.Count == 2) { // Not jumping, just move to new point if empty
                if (piece == CheckerPieces.Red) {
                    if (fromPoint.GetRow() - point.GetRow() != -1 || gameBoard[point.GetRow(), point.GetColumn()] != CheckerPieces.Empty) {
                        // Throw error
                        throw new InvalidOperationException("Red piece single move invalid");
                        //return false;
                    }
                }
                else if (piece == CheckerPieces.Black) {
                    if (fromPoint.GetRow() - point.GetRow() != 1 || gameBoard[point.GetRow(), point.GetColumn()] != CheckerPieces.Empty) {
                        // Throw error
                        throw new InvalidOperationException("Black piece single move invalid");
                        //return false;
                    }
                }
                else if (piece == CheckerPieces.RedKing || piece == CheckerPieces.BlackKing) {
                    if (Math.Abs(fromPoint.GetRow() - point.GetRow()) != 1 || gameBoard[point.GetRow(), point.GetColumn()] != CheckerPieces.Empty) {
                        // Throw error
                        throw new InvalidOperationException("King piece single move invalid");
                        //return false;
                    }
                }

                gameBoard[fromPoint.GetRow(), fromPoint.GetColumn()] = CheckerPieces.Empty;
                gameBoard[point.GetRow(), point.GetColumn()] = piece;
            }
            else if (Math.Abs(fromPoint.GetRow() - point.GetRow()) == 2) { // Jumping to take pieces
                int middleColumn = Math.Abs(fromPoint.GetColumn() + point.GetColumn()) / 2;
                int middleRow = Math.Abs(fromPoint.GetRow() + point.GetRow()) / 2;
                if (piece == CheckerPieces.Red) {
                    if (fromPoint.GetRow() - point.GetRow() != -2 || gameBoard[point.GetRow(), point.GetColumn()] != CheckerPieces.Empty ||
                    (gameBoard[middleRow, middleColumn] != CheckerPieces.Black && gameBoard[middleRow, middleColumn] != CheckerPieces.BlackKing)) {
                        // Throw error
                        throw new InvalidOperationException("Red piece single jump invalid");
                        //return false;
                    }
                }
                else if (piece == CheckerPieces.Black) {
                    if (fromPoint.GetRow() - point.GetRow() != 2 || gameBoard[point.GetRow(), point.GetColumn()] != CheckerPieces.Empty ||
                    (gameBoard[middleRow, middleColumn] != opponentPieces[0] && gameBoard[middleRow, middleColumn] != opponentPieces[1])) {
                        // Throw error
                        Console.WriteLine(fromPoint.GetRow() + " " + point.GetRow());
                        Console.WriteLine(gameBoard[point.GetRow(), point.GetColumn()]);
                        Console.WriteLine(gameBoard[middleRow, middleColumn]);
                        Console.WriteLine(middleRow);
                        Console.WriteLine(middleColumn);
                        throw new InvalidOperationException("Black piece single jump invalid");
                        //return false;
                    }
                }
                else if (piece == CheckerPieces.RedKing || piece == CheckerPieces.BlackKing) {
                    if (Math.Abs(fromPoint.GetRow() - point.GetRow()) != 2 || gameBoard[point.GetRow(), point.GetColumn()] != CheckerPieces.Empty ||
                    (gameBoard[middleRow, middleColumn] != opponentPieces[0] && gameBoard[middleRow, middleColumn] != opponentPieces[1])) {
                        // Throw error
                        throw new InvalidOperationException("King piece single jump invalid");
                        //return false;
                    }
                }

                gameBoard[fromPoint.GetRow(), fromPoint.GetColumn()] = CheckerPieces.Empty;
                gameBoard[middleRow, middleColumn] = CheckerPieces.Empty;
                gameBoard[point.GetRow(), point.GetColumn()] = piece;
            }
            else {
                return false;
            }

            fromPoint = point; // Make the point we just moved to the new fromPoint for the next move
        }

        // Check if piece should turn into King
        if(endingPoint.GetRow() == 0 || endingPoint.GetRow() == 7 && (piece != CheckerPieces.RedKing || piece != CheckerPieces.BlackKing)) {
            if (piece == CheckerPieces.Red) {
                gameBoard[endingPoint.GetRow(), endingPoint.GetColumn()] = CheckerPieces.RedKing;
            }
            else if (piece == CheckerPieces.Black) {
                gameBoard[endingPoint.GetRow(), endingPoint.GetColumn()] = CheckerPieces.BlackKing;
            }
        }

        return true;
    }

    CheckerPieces SetPieceFromPoint(CheckerPieces piece, CKPoint point) {
        return gameBoard[point.GetRow(), point.GetColumn()];
    }
    #endregion
}
