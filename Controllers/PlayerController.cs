using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using spacerpg.Enums;
using spacerpg.General;
using spacerpg.Interfaces;
using spacerpg.Models;
using System.Linq;

namespace spacerpg.Controllers
{
    /// <summary>
    /// Player controller. Handles player input and updates the player model.
    /// </summary>
    class PlayerController : IController, IReset
    {
        private readonly float _distance = 4;
        private readonly PlayerModel _playerModel;
        private readonly int _playerOffset = 60;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="playerModel">Player model</param>
        public PlayerController(PlayerModel playerModel)
        {
            _playerModel = playerModel;
        }

        /// <summary>
        /// Update the player. Takes input from the keyboard and updates the position of the player.
        /// </summary>
        /// <param name="gameTime">Game time</param>
        public void Update(GameTime gameTime)
        {
            if (_playerModel.Invulnerable)
            {
                _playerModel.InvulnerableCooldown += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (_playerModel.InvulnerableCooldown >= 1000)
                {
                    _playerModel.Invulnerable = false;
                    _playerModel.InvulnerableCooldown = 0f;
                }
            }

            var speed = _distance + _playerModel.Speed;
            var state = Keyboard.GetState();
            var pressedKeys = state.GetPressedKeys();

            if (pressedKeys.Contains(Keys.Up) && pressedKeys.Contains(Keys.Left) && _playerModel.Position.Y > 0 && _playerModel.Position.X > 0)
            {
                _playerModel.Position += new Vector2(-speed, -speed);
            }
            else if (pressedKeys.Contains(Keys.Up) 
                && pressedKeys.Contains(Keys.Right) 
                && _playerModel.Position.Y > 0 
                && _playerModel.Position.X < VirtualScreenSize.Width * VirtualScreenSize.ScreenSizeMultiplier - _playerOffset)
            {
                _playerModel.Position += new Vector2(speed, -speed);
            }
            else if (pressedKeys.Contains(Keys.Down) 
                && pressedKeys.Contains(Keys.Left) 
                && _playerModel.Position.Y < VirtualScreenSize.Height * VirtualScreenSize.ScreenSizeMultiplier - _playerOffset 
                && _playerModel.Position.X > 0)
            {
                _playerModel.Position += new Vector2(-speed, speed);
            }
            else if (pressedKeys.Contains(Keys.Down) 
                && pressedKeys.Contains(Keys.Right) 
                && _playerModel.Position.Y < VirtualScreenSize.Height * VirtualScreenSize.ScreenSizeMultiplier - _playerOffset 
                && _playerModel.Position.X < VirtualScreenSize.Width * VirtualScreenSize.ScreenSizeMultiplier - _playerOffset)
            {
                _playerModel.Position += new Vector2(speed, speed);
            }
            else if (pressedKeys.Contains(Keys.Up) && _playerModel.Position.Y > 0)
            {
                _playerModel.Position += new Vector2(0, -speed);
            } 
            else if (pressedKeys.Contains(Keys.Down) && _playerModel.Position.Y < VirtualScreenSize.Height * VirtualScreenSize.ScreenSizeMultiplier - _playerOffset)
            {
                _playerModel.Position += new Vector2(0, speed);
            }
            else if (pressedKeys.Contains(Keys.Left) && _playerModel.Position.X > 0)
            {
                _playerModel.Position += new Vector2(-speed, 0);
            }
            else if (pressedKeys.Contains(Keys.Right) && _playerModel.Position.X < VirtualScreenSize.Width * VirtualScreenSize.ScreenSizeMultiplier - _playerOffset)
            {
                _playerModel.Position += new Vector2(speed, 0);
            }

        }

        /// <summary>
        /// Reset the player.
        /// </summary>
        /// <param name="resetMode">Reset mode</param>
        public void Reset(ResetMode resetMode)
        {
            if (resetMode == ResetMode.Death)
            {
                _playerModel.Lives--;
            }

            _playerModel.ResetPosition(resetMode);
        }
    }
}
