using UnityEngine;
using System.Collections.Generic;

namespace TicTacToe
{
    public class TurnController
    {
        private Stack<TilePosition> _turnHistory;
        public TurnController()
        {
            _turnHistory = new Stack<TilePosition>();
        }

        public void MakeTurn(TilePosition tileChanged)
        {
            _turnHistory.Push(tileChanged);
        }
        public TilePosition UndoTurn()
        {
            TilePosition lastChangedTile = _turnHistory.Pop();
            return lastChangedTile;
        }

        public void ResetHistory()
        {
            _turnHistory.Clear();
        }
    }
}