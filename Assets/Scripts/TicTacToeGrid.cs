using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TicTacToe
{
    public class TicTacToeGrid
    {
        public List<TilePosition> AvailablePositions { get => GridState.AvailablePositions; }
        public IGridState GridState { get => _gridState; set => _gridState = value; }

        private IGridState _gridState = new GridState(GridSize);
        public static int GridSize => _gridSize;

        private static readonly int _gridSize = 3;

        public enum Sign
        {
            Empty,
            X,
            O
        }

        public enum Result
        {
            Draw,
            PlayerXWin,
            PlayerOWin,
        }


        private IGameMode _gameMode;


        public void InitalizeGame()
        {
            _gameMode = GameSettings.Instance.GameMode;
            GameEventsManager.Instance.OnPlayerTurn += OnPlayerTurn;
            GameEventsManager.Instance.OnRestartGame += StartGame;
            GameEventsManager.Instance.OnUndoTurn += UndoTurn;
            if (_gameMode.AllowUndo)
                GameEventsManager.Instance.DisplayUndoButton();
            GameEventsManager.Instance.OnTurnTimerEnd += TurnTimerEnded;


            _gameMode.InitalizeGame();
        }

        public void StartGame()
        {
            GridState.ResetGrid();
            _gameMode.StartGame(GridState);
        }  

        public void EndGame(Result result)
        {
            _gameMode.EndGame(result);
            GameEventsManager.Instance.DisplayGameOver(result);
        }

        public void OnPlayerTurn(Sign sign, TilePosition pos)
        {
            GridState.MakeTurn(sign, pos);
            if (GridState.IsWin(sign))
            {
                EndGame((Result)sign);
            }
            else if (GridState.IsFull)
            {
                EndGame(Result.Draw);
            }
            else
            {
                _gameMode.OnPlayerTurn(sign, GridState);
            }
        }

        private void TurnTimerEnded()
        {
            Sign oppositeSign = GridState.CurrentPlayer == Sign.X ? Sign.O : Sign.X;
            EndGame((Result)oppositeSign);
        }

        public void UndoTurn()
        {
            if (GridState.UndoTurns(2, out List<TilePosition> positions))
            {
                foreach (var pos in positions)
                {
                    GameEventsManager.Instance.UpdateTile(Sign.Empty, pos);
                }
            }
        }



    }
}