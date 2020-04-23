using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using spacerpg.General;
using spacerpg.Models;

namespace spacerpg.Views
{
    class LevelUpView : BaseView
    {
        private readonly SpriteFont _gameFont;
        private readonly SpriteBatch _spriteBatch;
        private readonly SpriteSheet _ships;
        private readonly LevelUpModel _levelUpModel;
        private readonly PlayerService _playerService;
        private PlayerModel _playerModel;

        public LevelUpView(ContentManager contentManager, SpriteBatch spriteBatch, LevelUpModel levelUpModel, PlayerService playerService) : base(contentManager, spriteBatch)
        {
            _gameFont = contentManager.Load<SpriteFont>("File");
            _spriteBatch = spriteBatch;
            var shipTextures = contentManager.Load<Texture2D>("shipsall");
            _ships = new SpriteSheet(spriteBatch, shipTextures, 64, 64);
            _levelUpModel = levelUpModel;
            _playerService = playerService;
        }

        public override void Draw()
        {
            _playerModel = _playerService.GetPlayer();
            _spriteBatch.DrawString(_gameFont, "We buy things we don't need, to impress people we don't like.", _levelUpModel.QuotePosition, Color.White);
            _spriteBatch.DrawString(_gameFont, "Available skill points: ".ToUpper() + _playerModel.AvailableSkillPoints.ToString(), _levelUpModel.TitlePosition, Color.Yellow);
            _spriteBatch.DrawString(_gameFont, "HP: ".ToUpper() + _playerModel.HP.ToString(), _levelUpModel.HpPosition, Color.Yellow);
            _spriteBatch.DrawString(_gameFont, "Damage: ".ToUpper() + _playerModel.Damage.ToString(), _levelUpModel.DamagePosition, Color.Yellow);
            _spriteBatch.DrawString(_gameFont, "Speed: ".ToUpper() + (4 + _playerModel.Speed).ToString(), _levelUpModel.SpeedPosition, Color.Yellow);
            _spriteBatch.DrawString(_gameFont, "Accept".ToUpper(), _levelUpModel.AcceptPosition, Color.Yellow);
            _ships.Draw(_levelUpModel.ShipPosition, _levelUpModel.Frame, Color.White);
        }
    }
}
