using UnityEngine;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class BotPlayer : Player
    {

        public enum Difficulty
        {
            Easy,
            Medium,
            Hard
        }


        private Difficulty _difficulty;

        public BotPlayer(Difficulty difficulty)
        {
            _difficulty = difficulty;
        }

        public BotPlayer(TicTacToeGrid.Sign sign ,Difficulty difficulty)
        {
            Sign = sign;
            _difficulty = difficulty;
        }

        public async override void PlayTurn(IGridState gridState)
        {
            await Task.Delay(1000);
            GameEventsManager.Instance.PlayerTurn(Sign, GetTurnByDifficulty(_difficulty, gridState));
        }

        private TilePosition GetTurnByDifficulty(Difficulty difficulty, IGridState gridState)
        {
            switch (difficulty)
            {
                case Difficulty.Easy:
                    return GetRandomPosition(gridState);
                case Difficulty.Medium:
                    return MiniMaxAlgorithm.GetBestMove(gridState, 1);
                case Difficulty.Hard:
                    return MiniMaxAlgorithm.GetBestMove(gridState, 0);
                default:
                    return GetRandomPosition(gridState);
            }
        }

        private TilePosition GetRandomPosition(IGridState gridState)
        {
            int randomNum = Random.Range(0, gridState.AvailablePositions.Count);
            return gridState.AvailablePositions[randomNum];
        }

    }
}