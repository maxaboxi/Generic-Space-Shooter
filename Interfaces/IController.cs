using Microsoft.Xna.Framework;

namespace spacerpg.Interfaces
{
    /// <summary>
    /// MVC Controller interface.
    /// </summary>
    interface IController
    {
        /// <summary>
        /// Update the controller.
        /// </summary>
        /// <param name="gameTime">Game time</param>
        void Update(GameTime gameTime);
    }
}
