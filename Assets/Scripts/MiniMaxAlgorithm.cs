using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class MiniMaxAlgorithm
    {

        //check depth = 0 means no limit
        public static TilePosition GetBestMove(IGridState state, int checkDepth)
        {
            var sign = state.CurrentPlayer;
            var clonedGrid = state.Clone();
            var availableMoves = state.AvailablePositions;

            var scores = new Dictionary<TilePosition, int>();
            var topScore = int.MinValue;
            // go through all available moves and get a minmax score for each. Then chose the best move.
            foreach (var move in availableMoves)
            {
                var score = GetMinMaxScoreForMovesRecursive(sign, move, state.Clone(), 1, checkDepth);
                scores[move] = score;

                if (score > topScore)
                {
                    topScore = score;
                }
            }

            // This isn't needed but to keep things interesting, if there are multiple spots with top scores, switch things
            // up by randomly choosing one rather than just the first each time.  
            var topMoves = scores
                .Where(x => x.Value == topScore)
                .Select(x => x.Key)
                .ToArray();

            if (topMoves.Length == 1)
            {
                return topMoves[0];
            }

            var rndIndex = new Random().Next(0, topMoves.Length - 1);
            return topMoves[rndIndex];
        }

        private static int GetMinMaxScoreForMovesRecursive(TicTacToeGrid.Sign sign, TilePosition move, GridState state, int depth, int checkDepth)
        {
            if (depth > checkDepth && checkDepth != 0)
                return DrawScore();

            // make the move on the cloned board
            state.MakeTurn(sign, move);

            // check if the game is over. If it is, return the score
            if(state.IsWin(TicTacToeGrid.Sign.X))
            {
                return WinOrLoseScore(sign, TicTacToeGrid.Sign.X, depth);
            }
            else if(state.IsWin(TicTacToeGrid.Sign.O))
            {
                return WinOrLoseScore(sign, TicTacToeGrid.Sign.O , depth);
            }
            else if(state.IsFull)
            {
                return DrawScore();
            }

            // The game is not done yet. Now we'll look at all the next available spots and check all the possible scores.
            // We'll continue to do so recursively until we get a result for each position

            depth++;
            var nextAvailableMoves = state.AvailablePositions;
            var scores = new int[nextAvailableMoves.Count];

            for (var i = 0; i < nextAvailableMoves.Count; i++)
            {
                var score = GetMinMaxScoreForMovesRecursive(sign, nextAvailableMoves[i], state.Clone(), depth, checkDepth);
                scores[i] = score;
            }

            // here we check if it's the maximizing player (minimax bot) turn. If so, pick the best score.
            // if it's not (i.e. the opponent's turn), assume that they will choose the best possible option (lowest score)
            var isMyTurn = state.CurrentPlayer == sign;
            return isMyTurn ? scores.Max() : scores.Min();
        }

        // Scoring:
        //  - Draw is meh... 0 points
        //  - A Loss is bad: -10. But a loss much later in the game is better than an immediate loss, so we add depth
        //  - A Win is great: +10. An immediate win trumps a later win. So we subtract the depth from the win score
        private static int DrawScore() => 0;
        private static int LoseScore(int depth) => -100 + depth;
        private static int WinScore(int depth) => 100 - depth;

        private static int WinOrLoseScore(TicTacToeGrid.Sign mySign, TicTacToeGrid.Sign winningSign, int depth)
        {
            if (depth == 1 && mySign != winningSign)
                return WinScore(depth + 1);

            return mySign == winningSign
                ? WinScore(depth)
                : LoseScore(depth);
        }
    }
}