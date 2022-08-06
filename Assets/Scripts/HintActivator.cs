using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TicTacToe
{
    public class HintActivator
    {
        private IGridState _grid;
        private Tile[,] _tilesGrid;
        public HintActivator(IGridState grid, Tile[,] tilesGrid)
        {
            _grid = grid;
            _tilesGrid = tilesGrid;
        }

        public void ActivateHint(int length)
        {
            TilePosition hint = MiniMaxAlgorithm.GetBestMove(_grid, 0);
            _tilesGrid[hint.Row, hint.Column].Flicker(length);
        }

    }
}