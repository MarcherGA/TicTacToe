using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using UnityEngine.TestTools;
using TicTacToe;
using UnityEngine.UI;

public class undo_button_test
{
    [Test]
    public void grid_enables_undo_button_with_player_vs_bot_mode()
    {
        GameObject undoButtonObj = GetUndoButtonObj();

        GameSettings.Instance.GameModeName = GameSettings.GameModeNames.vsBot;

        TicTacToeGrid grid = GetTicTacToeGrid();

        Assert.IsTrue(undoButtonObj.activeInHierarchy);
    }

    [Test]
    public void grid_state_undo_is_called()
    {
        var undoButtonObj = GetUndoButtonObj();

        GameSettings.Instance.GameModeName = GameSettings.GameModeNames.vsBot;
        TicTacToeGrid grid = GetTicTacToeGrid();

        grid.GridState = Substitute.For<IGridState>();

        var button = undoButtonObj.GetComponent<Button>();
        button.onClick.Invoke();

        grid.GridState.Received().UndoTurns(2, out List<TilePosition> positions);
    }

    private static TicTacToeGrid GetTicTacToeGrid()
    {
        TicTacToeGrid grid = new TicTacToeGrid();
        grid.InitalizeGame();
        return grid;
    }

    private static GameObject GetUndoButtonObj()
    {
        var undoButtonObj = new GameObject();
        undoButtonObj.AddComponent<UndoButton>();
        return undoButtonObj;
    }

}
