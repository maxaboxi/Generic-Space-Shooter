using spacerpg.Models;

namespace spacerpg.General
{

    class PlayerService
    {
        private PlayerModel _playerModel;

        public PlayerService() {}

        /// <summary>
        /// Update the player model
        /// </summary>
        /// <param name="playerModel"></param>
        public void SetPlayer(PlayerModel playerModel)
        {
            _playerModel = playerModel;
        }

        /// <summary>
        /// Get the player model
        /// </summary>
        /// <returns></returns>
        public PlayerModel GetPlayer()
        {
            return _playerModel;
        }
    }
}
