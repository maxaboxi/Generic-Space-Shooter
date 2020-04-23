using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using spacerpg.Models;

namespace spacerpg.Views
{
    class GameOverView : BaseView
    {
        private readonly SpriteFont _gameFont;
        private readonly SpriteBatch _spriteBatch;
        private readonly GameOverModel _gameOverModel;

        public GameOverView(ContentManager contentManager, SpriteBatch spriteBatch, GameOverModel gameOverModel) : base(contentManager, spriteBatch)
        {
            _gameFont = contentManager.Load<SpriteFont>("File");
            _spriteBatch = spriteBatch;
            _gameOverModel = gameOverModel;
        }

        public override void Draw()
        {
            
            _spriteBatch.DrawString(_gameFont, "End? No, the journey doesn't end here. Death is just another path. One that we all must take.", _gameOverModel.QuotePosition, Color.White);
            _spriteBatch.DrawString(_gameFont, "Game Over".ToUpper(), _gameOverModel.GameOverPosition, Color.Red);
            _spriteBatch.DrawString(_gameFont, "Press enter to play again or backspace to return to main menu", _gameOverModel.PressButtonTextPosition, Color.Red);
        }
    }
}
