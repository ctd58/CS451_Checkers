using System;


public enum CheckerPieces { Red, RedKing, Black, BlackKing };

[Serializable]
public class GameBoard
{

    private CheckerPieces[,] gameboard;

	public GameBoard()
	{


	}
}
