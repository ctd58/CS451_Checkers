using System;


enum MessageIdentifiers { OnePlayerConnected, TwoPlayersConnected, StartingGame,
    WaitingForOpponent, GameUpdate, RetryGameUpdate, GameOver, PauseRequest, PauseGame };
public enum GameStatus { InProgress, Player1Wins, Player2Wins, Draw };
public enum CheckerPieces { Empty, Red, RedKing, Black, BlackKing };

[Serializable]
public class GameBoard
{

    #region Attributes

    private int currentPlayer = 1;
    private DateTime timerExpires = DateTime.Now;

    private CheckerPieces[,] gameBoard = new CheckerPieces[8, 8];
    private GameStatus gameStatus = GameStatus.InProgress;
    #endregion

    #region Constructors

    public GameBoard() {

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

    #region Getters and Setters
    public GameStatus GetGameStatus() {
        return gameStatus;
    }
    public CheckerPieces[,] GetGameBoard() {
        return gameBoard;
    }

    #endregion
}
