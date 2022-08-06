using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TicTacToe;

public class grid_state_check_result
{

    [Test]
    public void empty_grid_return_false()
    {
        GridState gridState = new GridState(TicTacToeGrid.GridSize);

        Assert.IsFalse(gridState.IsFull || gridState.IsWin(TicTacToeGrid.Sign.X) || gridState.IsWin(TicTacToeGrid.Sign.O));

    }

    [Test]
    public void full_grid_no_win_returns_draw()
    {
        GridState gridState = new GridState(TicTacToeGrid.GridSize);

        gridState.MakeTurn(TicTacToeGrid.Sign.X, new TilePosition(0, 1));
        gridState.MakeTurn(TicTacToeGrid.Sign.O, new TilePosition(0, 2));
        gridState.MakeTurn(TicTacToeGrid.Sign.X, new TilePosition(1, 1));
        gridState.MakeTurn(TicTacToeGrid.Sign.O, new TilePosition(2, 1));
        gridState.MakeTurn(TicTacToeGrid.Sign.X, new TilePosition(1, 0));
        gridState.MakeTurn(TicTacToeGrid.Sign.O, new TilePosition(1, 2));
        gridState.MakeTurn(TicTacToeGrid.Sign.X, new TilePosition(2, 2));
        gridState.MakeTurn(TicTacToeGrid.Sign.O, new TilePosition(0, 0));
        gridState.MakeTurn(TicTacToeGrid.Sign.X, new TilePosition(2, 0));

        Assert.IsTrue(gridState.IsFull && !gridState.IsWin(TicTacToeGrid.Sign.X) && !gridState.IsWin(TicTacToeGrid.Sign.O));

    }

    [Test]
    public void diaognal_o_win_return_true()
    {
        GridState gridState = new GridState(TicTacToeGrid.GridSize);

        gridState.MakeTurn(TicTacToeGrid.Sign.X, new TilePosition(0, 0));
        gridState.MakeTurn(TicTacToeGrid.Sign.O, new TilePosition(1, 1));
        gridState.MakeTurn(TicTacToeGrid.Sign.X, new TilePosition(0, 1));
        gridState.MakeTurn(TicTacToeGrid.Sign.O, new TilePosition(0, 2));
        gridState.MakeTurn(TicTacToeGrid.Sign.X, new TilePosition(1, 0));
        gridState.MakeTurn(TicTacToeGrid.Sign.O, new TilePosition(2, 0));

        Assert.IsTrue(!gridState.IsFull && !gridState.IsWin(TicTacToeGrid.Sign.X) && gridState.IsWin(TicTacToeGrid.Sign.O));

    }

    [Test]
    public void horizontal_x_win_return_true()
    {
        GridState gridState = new GridState(TicTacToeGrid.GridSize);

        gridState.MakeTurn(TicTacToeGrid.Sign.X, new TilePosition(2, 2));
        gridState.MakeTurn(TicTacToeGrid.Sign.O, new TilePosition(0, 0));
        gridState.MakeTurn(TicTacToeGrid.Sign.X, new TilePosition(1, 1));
        gridState.MakeTurn(TicTacToeGrid.Sign.O, new TilePosition(1, 0));
        gridState.MakeTurn(TicTacToeGrid.Sign.X, new TilePosition(2, 0));
        gridState.MakeTurn(TicTacToeGrid.Sign.O, new TilePosition(0, 2));
        gridState.MakeTurn(TicTacToeGrid.Sign.X, new TilePosition(2, 1));

        Assert.IsTrue(!gridState.IsFull && gridState.IsWin(TicTacToeGrid.Sign.X) && !gridState.IsWin(TicTacToeGrid.Sign.O));

    }

    [Test]
    public void vertical_x_win_return_true()
    {
        GridState gridState = new GridState(TicTacToeGrid.GridSize);

        gridState.MakeTurn(TicTacToeGrid.Sign.X, new TilePosition(0, 0));
        gridState.MakeTurn(TicTacToeGrid.Sign.O, new TilePosition(0, 1));
        gridState.MakeTurn(TicTacToeGrid.Sign.X, new TilePosition(1, 2));
        gridState.MakeTurn(TicTacToeGrid.Sign.O, new TilePosition(1, 1));
        gridState.MakeTurn(TicTacToeGrid.Sign.X, new TilePosition(2, 1));
        gridState.MakeTurn(TicTacToeGrid.Sign.O, new TilePosition(2, 0));
        gridState.MakeTurn(TicTacToeGrid.Sign.X, new TilePosition(0, 2));
        gridState.MakeTurn(TicTacToeGrid.Sign.O, new TilePosition(1, 0));
        gridState.MakeTurn(TicTacToeGrid.Sign.X, new TilePosition(2, 2));

        Assert.IsTrue(gridState.IsWin(TicTacToeGrid.Sign.X) && !gridState.IsWin(TicTacToeGrid.Sign.O));

    }



}
