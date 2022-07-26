
namespace TicTacToe
{
    public readonly struct TilePosition
    {
        public readonly int Row;
        public readonly int Column;

        public TilePosition(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}