using spacerpg.Enums;

namespace spacerpg.Interfaces
{
    /// <summary>
    /// Interface to reset the game when a player has cleared a level or died.
    /// </summary>
    interface IReset
    {
        /// <summary>
        /// Reset the player
        /// </summary>
        /// <param name="resetMode">Reset mode</param>
        void Reset(ResetMode resetMode);
    }
}
