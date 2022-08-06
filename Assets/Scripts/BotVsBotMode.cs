using UnityEngine;
namespace TicTacToe
{
    public class BotVsBotMode : IGameMode
    {
        public Player Player1 => _botPlayer1;

        public Player Player2 => _botPlayer2;

        public bool AllowUndo => false;

        private BotPlayer _botPlayer1;
        private BotPlayer _botPlayer2;

        public void InitalizeGame()
        {
            GameEventsManager.Instance.SetActiveTurnTimer(false);
            _botPlayer1 = new BotPlayer(TicTacToeGrid.Sign.X, GameSettings.Instance.Difficulty);
            _botPlayer2 = new BotPlayer(TicTacToeGrid.Sign.O, GameSettings.Instance.Difficulty);
        }


        public void StartGame(IGridState grid)
        {
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

    }
}