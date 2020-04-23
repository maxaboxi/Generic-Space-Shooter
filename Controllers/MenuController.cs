using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using spacerpg.Interfaces;
using spacerpg.Models;
using System.Linq;

namespace spacerpg.Controllers
{
    class MenuController : IController
    {
        private readonly MenuModel _model;
        private readonly Game _game;

        public MenuController(Game game, MenuModel model)
        {
            _model = model;
            _game = game;
        }

        /// <summary>
        /// Main menu movement controller
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            var state = Keyboard.GetState();
            var pressedKeys = state.GetPressedKeys();
            _model.MoveCooldown += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (_model.MoveCooldown >= 200)
            {
                if (pressedKeys.Contains(Keys.Up) && _model.ShipPosition.Y <= _model.ExitGamePosition.Y - _model.ShipYOffset && _model.ShipPosition.Y >= _model.NewGamePosition.Y)
                {
                    _model.ShipPosition += new Vector2(0, -64);
                    _model.MoveCooldown = 0f;
                }

                if (pressedKeys.Contains(Keys.Down) && _model.ShipPosition.Y >= _model.NewGamePosition.Y -_model.ShipYOffset && _model.ShipPosition.Y < _model.ExitGamePosition.Y -_model.ShipYOffset)
                {
                    _model.ShipPosition += new Vector2(0, 64);
                    _model.MoveCooldown = 0f;
                }


                if (pressedKeys.Contains(Keys.Enter))
                {

                    if (_model.ShipPosition.Y == _model.NewGamePosition.Y - _model.ShipYOffset)
                    {
                        _model.NewGame = true;
                    }

                    if (_model.ShipPosition.Y == _model.InfoPosition.Y - _model.ShipYOffset)
                    {
                        _model.ShowInfo = !_model.ShowInfo;
                    }

                    if (_model.ShipPosition.Y == _model.ExitGamePosition.Y - _model.ShipYOffset)
                    {
                        _game.Exit();
                    }

                    _model.MoveCooldown = 0f;
                }
            }
        }
    }
}
