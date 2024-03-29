using UnityEngine;

namespace TicTacToe
{
    public class PlayerPhoneUser : Player
    {
        public PlayerPhoneUser(TicTacToeGrid.Sign sign)
        {
            Sign = sign;
        }
        public PlayerPhoneUser()
        {
        }

        public override void PlayTurn(IGridState grid)
        {
            GameEventsManager.Instance.OnTilePress += DoTurn;
            GameEventsManager.Instance.WaitForPlayerPress(true);
        }

        private void DoTurn(TilePosition tilePos)
        {
            GameEventsManager.Instance.OnTilePress -= DoTurn;
            GameEventsManager.Instance.WaitForPlayerPress(false);
            GameEventsManager.Instance.PlayerTurn(Sign, tilePos);
        }
    }
}