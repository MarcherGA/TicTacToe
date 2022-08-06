using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TicTacToe
{
    public class GridState : IGridState
    {
        public TicTacToeGrid.Sign CurrentPlayer { get => _currentPlayer;}

        private TicTacToeGrid.Sign[,] _grid;
        private int _gridSize;
        private TicTacToeGrid.Sign _currentPlayer = TicTacToeGrid.Sign.X;

        private TurnController _turnController;
        public GridState(int gridSize)
        {
            _turnController = new TurnController();
            _grid = new TicTacToeGrid.Sign[gridSize, gridSize];
            _gridSize = gridSize;
        }

        public GridState(int gridSize, TicTacToeGrid.Sign[,] grid)
        {
            _turnController = new TurnController();
            _grid = (TicTacToeGrid.Sign[,])grid.Clone();
            _gridSize = gridSize;
        }


        public List<TilePosition> AvailablePositions
        {
            get
            {
                List<TilePosition> availableMoves = new();
                for (int row = 0; row < TicTacToeGrid.GridSize; row++)
                {
                    for (int column = 0; column < TicTacToeGrid.GridSize; column++)
                    {
                        if (_grid[row, column] == TicTacToeGrid.Sign.Empty)
                        {
                            availableMoves.Add(new TilePosition(row, column));
                        }

                    }
                }
                return availableMoves;
            }
        }

        public void MakeTurn(TicTacToeGrid.Sign sign, TilePosition pos)
        {
            _grid[pos.Row, pos.Column] = sign;
            _currentPlayer = sign == TicTacToeGrid.Sign.X ? TicTacToeGrid.Sign.O : TicTacToeGrid.Sign.X;

            _turnController.MakeTurn(pos);
        }

        public bool UndoTurns(int num , out List<TilePosition> positionsChanged)
        {
            positionsChanged = new List<TilePosition>();
            if (AvailablePositions.Count < 8)
            {
                for (int i = 0; i < num; i++)
                {
                    positionsChanged.Add(UndoTurn());
                }
                return true;
            }

            return false;
        }


        public void ResetGrid()
        {
            _turnController.ResetHistory();
            for (int row = 0; row < _gridSize; row++)
            {
                for (int column = 0; column < _gridSize; column++)
                {
                    _grid[row, column] = TicTacToeGrid.Sign.Empty;
                }
            }
        }

        public GridState Clone()
        {
            return new GridState(_gridSize, _grid);
        }

        public bool IsWin(TicTacToeGrid.Sign sign)
        {
            // check rows
            if (_grid[0, 0] == sign && _grid[0, 1] == sign && _grid[0, 2] == sign) { return true; }
            if (_grid[1, 0] == sign && _grid[1, 1] == sign && _grid[1, 2] == sign) { return true; }
            if (_grid[2, 0] == sign && _grid[2, 1] == sign && _grid[2, 2] == sign) { return true; }

            // check columns
            if (_grid[0, 0] == sign && _grid[1, 0] == sign && _grid[2, 0] == sign) { return true; }
            if (_grid[0, 1] == sign && _grid[1, 1] == sign && _grid[2, 1] == sign) { return true; }
            if (_grid[0, 2] == sign && _grid[1, 2] == sign && _grid[2, 2] == sign) { return true; }

            // check diags
            if (_grid[0, 0] == sign && _grid[1, 1] == sign && _grid[2, 2] == sign) { return true; }
            if (_grid[0, 2] == sign && _grid[1, 1] == sign && _grid[2, 0] == sign) { return true; }

            return false;
        }

        public bool IsFull
        {
            get
            {

                foreach (var sign in _grid)
                {
                    if (sign == TicTacToeGrid.Sign.Empty)
                        return false;
                }

                return true;
            }
        }

        private TilePosition UndoTurn()
        {

            TilePosition pos = _turnController.UndoTurn();
            _grid[pos.Row, pos.Column] = TicTacToeGrid.Sign.Empty;
            return pos;
        }
    }
}