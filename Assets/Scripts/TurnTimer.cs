using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TicTacToe
{
    public class TurnTimer : Timer
    {
        private void OnEnable()
        {
            TimerLength = GameSettings.Instance.TurnLength;
            OnTimerEnd += GameEventsManager.Instance.TurnTimerEnd;
            GameEventsManager.Instance.OnResetTurnTimer += ResetTimer;
            GameEventsManager.Instance.OnSetActiveTurnTimer += gameObject.SetActive;
        }

        private void OnDisable()
        {
            GameEventsManager.Instance.OnResetTurnTimer -= ResetTimer;
            Stop();
        }
    }
}