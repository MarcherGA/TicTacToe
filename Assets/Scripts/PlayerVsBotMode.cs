using UnityEngine;
namespace TicTacToe
{
    public class PlayerVsBotMode : IGameMode
    {
        public Player Player1 => _userPlayer;

        public Player Player2 => _botPlayer;

        public bool AllowUndo => true;

        private PlayerPhoneUser _userPlayer;
        private BotPlayer _botPlayer;

        public void InitalizeGame()
        {
            _userPlayer = new PlayerPhoneUser();
            _botPlayer = new BotPlayer(GameSettings.Instance.Difficulty);
        }


        public void StartGame(IGridState grid)
        {
            GeneratePlayersSigns();
            PlayTurn(TicTacToeGrid.Sign.X, grid);
        }

        public void EndGame(TicTacToeGrid.Result result)
        {
            GameEventsManager.Instance.SetActiveTurnTimer(false);
        }

        public void OnPlayerTurn(TicTacToeGrid.Sign sign, IGridState grid)
        {
            PlayTurn(GetOppositeSign(sign), grid);
        }

        private void PlayTurn(TicTacToeGrid.Sign sign, IGridState grid) 
        {
            var player = GetPlayer(sign);
            if (player == _userPlayer)
            {
                GameEventsManager.Instance.SetActiveTurnTimer(true);
                GameEventsManager.Instance.ResetTurnTimer();
            }
            else
            {
                GameEventsManager.Instance.SetActiveTurnTimer(false);
            }
            player.PlayTurn(grid);
        }

        private Player GetPlayer(TicTacToeGrid.Sign sign)
        {
            return Player1.Sign == sign ? Player1 : Player2;
        }

        private static TicTacToeGrid.Sign GetOppositeSign(TicTacToeGrid.Sign sign)
        {
            return sign == TicTacToeGrid.Sign.X ? TicTacToeGrid.Sign.O : TicTacToeGrid.Sign.X;
        }

        private void GeneratePlayersSigns()
        {
            TicTacToeGrid.Sign sign = Random.Range(0, 2) == 0 ? TicTacToeGrid.Sign.X : TicTacToeGrid.Sign.O;
            Player1.Sign = sign;
            Player2.Sign = GetOppositeSign(sign);
        }

    }
}