using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TicTacToe
{
    public class TurnController
    {
        private Stack<Tile> _turnHistory;
        private Timer _turnTimer;

        public TurnController()
        {
            _turnHistory = new Stack<Tile>();
        }

        public void MakeTurn(Tile tileChanged)
        {
            _turnHistory.Push(tileChanged);
            _turnTimer.ResetTimer();
        }
        public void UndoTurn()
        {
            Tile lastChangedTile = _turnHistory.Pop();
            lastChangedTile.ResetTile();
        }

        public void ResetHistory()
        {
            _turnHistory.Clear();
        }
    }
}