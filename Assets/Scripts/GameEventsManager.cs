using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace TicTacToe
{
    public class GameEventsManager
    {
        #region Singleton
        private static readonly GameEventsManager _instance = new GameEventsManager();
        private GameEventsManager() 
        {
            SceneManager.sceneUnloaded += (Scene scene) =>
            {
                ClearEvents();
            };
        }
        public static GameEventsManager Instance
        {
            get
            {
                return _instance;
            }
        }
        #endregion

        public Action OnRestartGame;
        public void RestartGame() { OnRestartGame?.Invoke(); }

        public Action<bool> OnWaitForPlayerPress;
        public void WaitForPlayerPress(bool isOn) { OnWaitForPlayerPress?.Invoke(isOn); }

        public Action<TilePosition> OnTilePress;
        public void TilePress(TilePosition pos) { OnTilePress?.Invoke(pos); }

        public Action<TicTacToeGrid.Sign, TilePosition> OnUpdateTile;
        public void UpdateTile(TicTacToeGrid.Sign sign,TilePosition pos) { OnUpdateTile?.Invoke(sign, pos); }

        public Action<TicTacToeGrid.Sign, TilePosition> OnPlayerTurn;
        public void PlayerTurn(TicTacToeGrid.Sign sign, TilePosition pos) { OnPlayerTurn?.Invoke(sign, pos); }

        public Action<bool> OnSetActiveTurnTimer;
        public void SetActiveTurnTimer(bool isOn) { OnSetActiveTurnTimer?.Invoke(isOn); }

        public Action OnResetTurnTimer;
        public void ResetTurnTimer() { OnResetTurnTimer?.Invoke(); }

        public Action OnTurnTimerEnd;
        public void TurnTimerEnd() { OnTurnTimerEnd.Invoke(); }

        public Action OnDisplayUndoButton;
        public void DisplayUndoButton() { OnDisplayUndoButton?.Invoke(); }

        public Action OnUndoTurn;
        public void UndoTurn() { OnUndoTurn?.Invoke(); }

        public Action<int> OnDisplayHint;
        public void DisplayHint(int length) { OnDisplayHint?.Invoke(length); }

        public Action<TicTacToeGrid.Result> OnDisplayGameOver;
        public void DisplayGameOver(TicTacToeGrid.Result result) { OnDisplayGameOver?.Invoke(result); }

        public Action OnGameTexturesChanged;
        public void GameTexturesChanged() { OnGameTexturesChanged?.Invoke(); }
        public void ClearEvents() 
        {
            foreach (var field in GetType().GetFields())
            {
                if (field.IsPublic && !field.IsStatic && field.Name.Substring(0,2) == "On")
                {
                    field.SetValue(Instance, null);
                }
            }
        }
    }
}
