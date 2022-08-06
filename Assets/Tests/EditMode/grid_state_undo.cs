using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using UnityEngine.TestTools;
using TicTacToe;
using UnityEngine.UI;

public class grid_state_undo
{
    [Test]
    public void undo_empty_grid_returns_false()
    {

        GridState gridState = new GridState(TicTacToeGrid.GridSize);

        bool gridWasChanged =  gridState.UndoTurns(2, out List<TilePosition> positions);

        Assert.IsFalse(gridWasChanged || positions.Count > 0);
    }

    [Test]
    public void undo_one_tile_grid_returns_false()
    {

        GridState gridState = new GridState(TicTacToeGrid.GridSize);

        gridState.MakeTurn(TicTacToeGrid.Sign.X, new TilePosition(1, 1));

        bool gridWasChanged = gridState.UndoTurns(2, out List<TilePosition> positions);

        Assert.IsFalse(gridWasChanged || positions.Count > 0);
    }

    [Test]
    public void undo_four_tiles_grid_returns_true_and_correct_tiles()
    {

        GridState gridState = new GridState(TicTacToeGrid.GridSize);

        gridState.MakeTurn(TicTacToeGrid.Sign.X, new TilePosition(1, 1));
        gridState.MakeTurn(TicTacToeGrid.Sign.O, new TilePosition(1, 2));
        gridState.MakeTurn(TicTacToeGrid.Sign.X, new TilePosition(0, 0));
        gridState.MakeTurn(TicTacToeGrid.Sign.O, new TilePosition(2, 2));

        List<TilePosition> positionsChanged = new List<TilePosition>();
        positionsChanged.Add(new TilePosition(0, 0));
        positionsChanged.Add(new TilePosition(2, 2));

        bool gridWasChanged = gridState.UndoTurns(2, out List<TilePosition> positions);

        Assert.IsTrue(gridWasChanged && PositionsMatch(positionsChanged, positions));
    }

    [Test]
    public void undo_seven_tiles_grid_returns_true_and_correct_tiles()
    {

        GridState gridState = new GridState(TicTacToeGrid.GridSize);

        gridState.MakeTurn(TicTacToeGrid.Sign.X, new TilePosition(0, 0));
        gridState.MakeTurn(TicTacToeGrid.Sign.O, new TilePosition(0, 1));
        gridState.MakeTurn(TicTacToeGrid.Sign.X, new TilePosition(1, 1));
        gridState.MakeTurn(TicTacToeGrid.Sign.O, new TilePosition(2, 2));
        gridState.MakeTurn(TicTacToeGrid.Sign.X, new TilePosition(2, 1));
        gridState.MakeTurn(TicTacToeGrid.Sign.O, new TilePosition(1, 2));
        gridState.MakeTurn(TicTacToeGrid.Sign.X, new TilePosition(0, 2));

        List<TilePosition> positionsChanged = new List<TilePosition>();
        positionsChanged.Add(new TilePosition(1, 2));
        positionsChanged.Add(new TilePosition(0, 2));

        bool gridWasChanged = gridState.UndoTurns(2, out List<TilePosition> positions);

        Assert.IsTrue(gridWasChanged && PositionsMatch(positionsChanged, positions));
    }

    private static bool PositionsMatch(List<TilePosition> list1, List<TilePosition> list2)
    {
        if (list1.Count != list2.Count)
            return false;
        
        foreach(var position in list1)
        {
            if (!list2.Exists((pos) => pos.Equals(position)))
                return false;
        }

        return true;
    }

}
