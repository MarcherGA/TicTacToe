using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TicTacToe
{
    public class LocalMultiplayerMode : IGameMode
    {
        public Player Player1 { get => _player1; }

        public Player Player2 { get => _player2; }

        public bool AllowUndo { get => false; }

        private PlayerPhoneUser _player1;
        private PlayerPhoneUser _player2;



        public void InitalizeGame()
        { 

            _player1 = new PlayerPhoneUser(TicTacToeGrid.Sign.X);
            _player2 = new PlayerPhoneUser(TicTacToeGrid.Sign.O);
        }

        public void StartGame()
        {

            GameEventsManager.Instance.SetActiveTurnTimer(true);
            GameEventsManager.Instance.ResetTurnTimer();
            Player1.PlayTurn();
        }

        public void EndGame(TicTacToeGrid.Result result)
        {
            GameEventsManager.Instance.SetActiveTurnTimer(false);
        }

        public void OnPlayerTurn(TicTacToeGrid.Sign sign)
        {
            GameEventsManager.Instance.ResetTurnTimer();
            GetPlayer(sign == TicTacToeGrid.Sign.X ? TicTacToeGrid.Sign.O : TicTacToeGrid.Sign.X).PlayTurn();
        }

        private Player GetPlayer(TicTacToeGrid.Sign sign)
        {
            return Player1.Sign == sign ? Player1 : Player2;
        }

    }
}