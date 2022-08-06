using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TicTacToe;
using NSubstitute;
using UnityEngine.UI;
using System.Reflection;

public class hint_button_test
{

    [Test]
    public void hint_button_click_trigger_hint_on_grid()
    {
        var hintButtonObj = new GameObject();
        var hintButton = hintButtonObj.AddComponent<HintButton>();

        var gameGrid = (new GameObject()).AddComponent<GameGrid>();

        var tilePrefab = (new GameObject()).AddComponent<Tile>();
        tilePrefab.gameObject.AddComponent<Image>();

        gameGrid.TilePrefab = tilePrefab;
        gameGrid.CreateTiles();

        var mockHintActivator = Substitute.For<HintActivator>(gameGrid.Grid.GridState, gameGrid.TilesGrid);
        gameGrid.HintActivator = mockHintActivator;

        var button = hintButton.GetComponent<Button>();
        button.onClick.Invoke();

        mockHintActivator.Received().ActivateHint(hintButton.HintLength);
    }

    [Test]
    public void hint_activator_check_empty()
    {
        var gridState = new GridState(TicTacToeGrid.GridSize);
        var tilesGrid = SetupTilesGrid();
        var hintActivator = new HintActivator(gridState, tilesGrid);

        hintActivator.ActivateHint(2);

        TilePosition pos = GetTileFlickerPos(tilesGrid);

        Assert.IsTrue(!pos.Equals(TilePosition.EmptyPosition));
    }

    [Test]
    public void hint_activator_check_two_tiles_full()
    {
        var gridState = new GridState(TicTacToeGrid.GridSize);
        var tilesGrid = SetupTilesGrid();
        var hintActivator = new HintActivator(gridState, tilesGrid);

        gridState.MakeTurn(TicTacToeGrid.Sign.X, new TilePosition(0, 1));
        gridState.MakeTurn(TicTacToeGrid.Sign.O, new TilePosition(1, 1));

        hintActivator.ActivateHint(3);

        TilePosition pos = GetTileFlickerPos(tilesGrid);

        Assert.IsTrue(gridState.AvailablePositions.Exists((TilePosition emptyPos) => pos.Equals(emptyPos))); // Checks if the flcikering tile is empty tile
    }

    [Test]
    public void hint_activator_check_five_tiles_full()
    {
        var gridState = new GridState(TicTacToeGrid.GridSize);
        var tilesGrid = SetupTilesGrid();
        var hintActivator = new HintActivator(gridState, tilesGrid);

        gridState.MakeTurn(TicTacToeGrid.Sign.X, new TilePosition(0, 1));
        gridState.MakeTurn(TicTacToeGrid.Sign.O, new TilePosition(1, 1));
        gridState.MakeTurn(TicTacToeGrid.Sign.X, new TilePosition(0, 2));
        gridState.MakeTurn(TicTacToeGrid.Sign.O, new TilePosition(0, 0));
        gridState.MakeTurn(TicTacToeGrid.Sign.X, new TilePosition(2, 2));

        hintActivator.ActivateHint(3);

        TilePosition pos = GetTileFlickerPos(tilesGrid);

        Assert.IsTrue(gridState.AvailablePositions.Exists((TilePosition emptyPos) => pos.Equals(emptyPos))); // Checks if the flcikering tile is empty tile
    }




    private static Tile[,] SetupTilesGrid()
    {
        var tilesGrid = new Tile[TicTacToeGrid.GridSize, TicTacToeGrid.GridSize];

        var tilePrefab = (new GameObject()).AddComponent<Tile>();
        tilePrefab.gameObject.AddComponent<Image>();


        for (int row = 0; row < TicTacToeGrid.GridSize; row++)
        {
            for (int column = 0; column < TicTacToeGrid.GridSize; column++)
            {
                tilesGrid[row, column] = (new GameObject()).AddComponent<Tile>();
                tilesGrid[row, column].GetComponent<Button>().image = tilesGrid[row, column].gameObject.AddComponent<Image>();
            }
        }

        return tilesGrid;
    }

    private static TilePosition GetTileFlickerPos(Tile[,] tilesGrid) //returns position of the only tile that is flickering
    {                                                               // otherwise returns empty position

        int count = 0;
        TilePosition pos = TilePosition.EmptyPosition;

        for (int row = 0; row < TicTacToeGrid.GridSize; row++)
        {
            for (int column = 0; column < TicTacToeGrid.GridSize; column++)
            {
                if (tilesGrid[row, column].isFlicker) //Check if tile is flickering after the press
                {
                    if (count == 0) //make sure only one tile flicker
                    {
                        pos = new TilePosition(row, column);
                        count++;
                    }
                    else
                    {
                        pos = TilePosition.EmptyPosition;
                        break;
                    }
                }
            }
        }

        return pos;
    }

}
