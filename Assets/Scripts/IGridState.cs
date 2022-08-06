using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TicTacToe
{
    public interface IGridState
    {

        public void MakeTurn(TicTacToeGrid.Sign sign, TilePosition pos);
        public bool UndoTurns(int num, out List<TilePosition> positionsChanged);

        public List<TilePosition> AvailablePositions { get; }
        public bool IsWin(TicTacToeGrid.Sign sign);
        public bool IsFull { get; }

        public TicTacToeGrid.Sign CurrentPlayer { get; }

        public void ResetGrid();
        public GridState Clone();


    }
}