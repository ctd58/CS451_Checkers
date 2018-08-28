using System;

[Serializable]
public class CKPoint
{
    #region Attributes

    private int row; // Should be 0 to 7 because of an 8 long and wide array
    private int column;
    #endregion

    #region Constructors

    public CKPoint()
	{
        row = 0;
        column = 0;
	}

    public CKPoint(int row, int column) {

        if (row < 0 || row > 7) {
            // Throw error
        }
        else if (column < 0 || column > 7) {
            // Throw error
        }

        this.row = row;
        this.column = column;
    }
    #endregion

    #region Getters and Setters

    public int GetRow() {
        return row;
    }

    public int GetColumn() {
        return column;
    }

    public void SetPoint(int row, int column) {
        if (row < 0 || row > 7) {
            // Throw error
        }
        else if (column < 0 || column > 7) {
            // Throw error
        }

        this.row = row;
        this.column = column;
    }
    #endregion
}
