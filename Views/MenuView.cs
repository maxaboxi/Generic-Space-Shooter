using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using spacerpg.Enums;
using spacerpg.General;
using spacerpg.Models;
using System.Collections.Generic;

namespace spacerpg.Views
{
    class MenuView : BaseView
    {
        private readonly SpriteFont _gameFont;
        private readonly SpriteBatch _spriteBatch;
        private readonly SpriteSheet _ships;
        private readonly MenuModel _model;
        private readonly SpriteSheet _bonusItems;
        private readonly SpriteSheet _enemies;
        private readonly List<BonusItem> _items;


        public MenuView(ContentManager contentManager, SpriteBatch spriteBatch, MenuModel model) : base(contentManager, spriteBatch)
        {
            _gameFont = contentManager.Load<SpriteFont>("File");
            _spriteBatch = spriteBatch;
            var shipTextures = contentManager.Load<Texture2D>("shipsall");
            var itemTextures = contentManager.Load<Texture2D>("icons");
            var enemyTextures = contentManager.Load<Texture2D>("enemies");
            _ships = new SpriteSheet(spriteBatch, shipTextures, 64, 64);
            _bonusItems = new SpriteSheet(spriteBatch, itemTextures, 32, 32);
            _enemies = new SpriteSheet(spriteBatch, enemyTextures, 32, 32);
            _model = model;
            _items = new List<BonusItem>
            {
                new BonusItem(new Vector2(_model.NewGamePosition.X + _model.BonusItemXOffset + 13, _model.NewGamePosition.Y + _model.BonusItemYOffset), 8, BonusItemType.ExtraLive, 1, ""),
                new BonusItem(new Vector2(_model.NewGamePosition.X + _model.BonusItemXOffset * 3 + 15, _model.NewGamePosition.Y + _model.BonusItemYOffset), 121, BonusItemType.XP, 20, ""),
                new BonusItem(new Vector2(_model.NewGamePosition.X + _model.BonusItemXOffset * 5 + 10, _model.NewGamePosition.Y + _model.BonusItemYOffset), 108, BonusItemType.HP, 1, ""),
                new BonusItem(new Vector2(_model.NewGamePosition.X + _model.BonusItemXOffset * 8 + 15, _model.NewGamePosition.Y + _model.BonusItemYOffset), 56, BonusItemType.Damage, 1, ""),
                new BonusItem(new Vector2(_model.NewGamePosition.X + _model.BonusItemXOffset * 11 - 2, _model.NewGamePosition.Y + _model.BonusItemYOffset), 139, BonusItemType.Speed, 0.2f, ""),
                new BonusItem(new Vector2(_model.NewGamePosition.X + _model.BonusItemXOffset * 15, _model.NewGamePosition.Y + _model.BonusItemYOffset), 87, BonusItemType.Bullet, 1, "")
            };
        }

        public override void Draw()
        {
            if (!_model.ShowInfo)
            {
                _spriteBatch.DrawString(_gameFont, "New Game".ToUpper(), _model.NewGamePosition, Color.Yellow);
                _spriteBatch.DrawString(_gameFont, "Info".ToUpper(), _model.InfoPosition, Color.Yellow);
                _spriteBatch.DrawString(_gameFont, "Exit".ToUpper(), _model.ExitGamePosition, Color.Yellow);
                _ships.Draw(_model.ShipPosition, _model.Frame, Color.White);
            }
            else
            {
                _spriteBatch.DrawString(_gameFont, "New Game".ToUpper(), _model.NewGamePosition - _model.InfoXOffset, Color.Yellow);
                _spriteBatch.DrawString(_gameFont, "Hide info".ToUpper(), _model.InfoPosition - _model.InfoXOffset, Color.Yellow);
                _spriteBatch.DrawString(_gameFont, "Exit".ToUpper(), _model.ExitGamePosition - _model.InfoXOffset, Color.Yellow);
                _ships.Draw(_model.ShipPosition - _model.InfoXOffset, _model.Frame, Color.White);

                _spriteBatch.DrawString(_gameFont, "Use arrow keys to move and space to shoot.", _model.NewGamePosition, Color.White);
                _spriteBatch.DrawString(_gameFont, "Kill all the enemies to advance to the next level.", _model.NewGamePosition + _model.InfoYOffset, Color.White);
                _spriteBatch.DrawString(_gameFont, "Enemies will get stronger each level.", _model.NewGamePosition + _model.InfoYOffset * 2, Color.White);
                _spriteBatch.DrawString(_gameFont, "Killing enemies will grant you XP.", _model.NewGamePosition + _model.InfoYOffset * 3, Color.White);
                _spriteBatch.DrawString(_gameFont, "Kill enough to get a level up and to become more stronger.", _model.NewGamePosition + _model.InfoYOffset * 4, Color.White);
                _spriteBatch.DrawString(_gameFont, "Level up and adjust your stats the way you want.", _model.NewGamePosition + _model.InfoYOffset * 5, Color.White);
                _spriteBatch.DrawString(_gameFont, "Sometimes there will be bonus items floating around.", _model.NewGamePosition + _model.InfoYOffset * 6, Color.White);
                _spriteBatch.DrawString(_gameFont, "Loot 'em or lose 'em!", _model.NewGamePosition + _model.InfoYOffset * 7, Color.White);
                _spriteBatch.DrawString(_gameFont, "Bonus items:", _model.NewGamePosition + _model.InfoYOffset * 8, Color.White);
                _spriteBatch.DrawString(_gameFont, "1UP: ", _model.NewGamePosition + _model.InfoYOffset * 9, Color.White);
                _spriteBatch.DrawString(_gameFont, "XP: ", new Vector2(_model.NewGamePosition.X + _model.BonusItemXOffset * 2 + 10, _model.NewGamePosition.Y) + _model.InfoYOffset * 9, Color.White);
                _spriteBatch.DrawString(_gameFont, "HP: ", new Vector2(_model.NewGamePosition.X + _model.BonusItemXOffset * 4 + 10, _model.NewGamePosition.Y) + _model.InfoYOffset * 9, Color.White);
                _spriteBatch.DrawString(_gameFont, "Damage: ", new Vector2(_model.NewGamePosition.X + _model.BonusItemXOffset * 6, _model.NewGamePosition.Y) + _model.InfoYOffset * 9, Color.White);
                _spriteBatch.DrawString(_gameFont, "Speed: ", new Vector2(_model.NewGamePosition.X + _model.BonusItemXOffset * 9 + 8, _model.NewGamePosition.Y) + _model.InfoYOffset * 9, Color.White);
                _spriteBatch.DrawString(_gameFont, "More bullets: ", new Vector2(_model.NewGamePosition.X + _model.BonusItemXOffset * 12 - 15, _model.NewGamePosition.Y) + _model.InfoYOffset * 9, Color.White);
                foreach (var item in _items)
                {
                    if (item.Type != BonusItemType.ExtraLive)
                    {
                        _bonusItems.Draw(item.Position, item.Frame, Color.White);
                    }

                    if (item.Type == BonusItemType.ExtraLive)
                    {
                        _enemies.Draw(item.Position, item.Frame, Color.White);
                    }
                }
            }

        }
    }
}
