using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using spacerpg.General;
using spacerpg.Models;

namespace spacerpg.Views
{
    class PlayerView : BaseView
    {
        private readonly SpriteSheet _ships;
        private readonly PlayerModel _playerModel;

        // Uncomment for debugging
        //private readonly SpriteBatch _spriteBatch;
        //private readonly SpriteFont _gameFont;

        public PlayerView(ContentManager contentManager, SpriteBatch spriteBatch, PlayerModel playerModel) : base(contentManager, spriteBatch)
        {
            var shipTextures = contentManager.Load<Texture2D>("shipsall");
            _ships = new SpriteSheet(spriteBatch, shipTextures, 64, 64);
            _playerModel = playerModel;

            // Uncomment for debugging
            //_spriteBatch = spriteBatch;
            //_gameFont = contentManager.Load<SpriteFont>("File");
        }

        public override void Draw()
        {
            _ships.Draw(_playerModel.Position, _playerModel.Frame, Color.White);

            // Uncomment for debugging
            //_spriteBatch.DrawString(_gameFont, _playerModel.Position.ToString().Replace("{", "").Replace("}", ""), new Vector2(1650, 4), Color.Yellow);
        }
    }
}
