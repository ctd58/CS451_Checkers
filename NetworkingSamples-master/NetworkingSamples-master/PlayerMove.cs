﻿using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class PlayerMove
{
    #region Attributes

    private List<CKPoint> move = new List<CKPoint>();
    private int player = 1;
    #endregion

    #region Constructors

    public PlayerMove()
	{

	}
    #endregion

    #region Getters and Setters

    public List<CKPoint> GetPlayerMove() {
        return move;
    }
    #endregion

    #region Methods

    void BuildMove(CKPoint point) {
        //RestartMove();

        move.Add(point);
    }

    void RestartMove() {
        move.Clear();
    }
    #endregion
}
