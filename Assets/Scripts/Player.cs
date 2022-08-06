using System;
namespace TicTacToe
{
    public abstract class Player
    {
        public TicTacToeGrid.Sign Sign { get => _sign; set => _sign = value; }
        private TicTacToeGrid.Sign _sign;

        public abstract void PlayTurn(IGridState grid);
    }
}