
namespace TicTacToe
{
    public interface IGameMode
    {
        public Player Player1  { get; }
        public Player Player2 { get; }

        public bool AllowUndo { get; }

        public void InitalizeGame();
        public void StartGame(IGridState grid);
        public void EndGame(TicTacToeGrid.Result result);
        public void OnPlayerTurn(TicTacToeGrid.Sign sign, IGridState grid);
    }
}