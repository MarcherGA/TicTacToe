
namespace TicTacToe
{
    public readonly struct TilePosition
    {
        public readonly int Row;
        public readonly int Column;


        public static readonly TilePosition EmptyPosition = new TilePosition(-1, -1);

        public TilePosition(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public bool Equals(TilePosition pos)
        {
            return pos.Row == Row && pos.Column == Column;
        }

    }

}