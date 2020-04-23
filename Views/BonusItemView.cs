using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using spacerpg.Enums;
using spacerpg.General;
using spacerpg.Models;

namespace spacerpg.Views
{
    class BonusItemView : BaseView
    {
        private readonly SpriteFont _gameFont;
        private readonly SpriteBatch _spriteBatch;
        private readonly BonusItemListModel _bonusItemListModel;
        private readonly SpriteSheet _bonusItems;
        private readonly SpriteSheet _enemies;

        public BonusItemView(ContentManager contentManager, SpriteBatch spriteBatch, BonusItemListModel bonusItemListModel) : base(contentManager, spriteBatch)
        {
            _gameFont = contentManager.Load<SpriteFont>("File");
            _spriteBatch = spriteBatch;
            var itemTextures = contentManager.Load<Texture2D>("icons");
            _bonusItems = new SpriteSheet(spriteBatch, itemTextures, 32, 32);
            // Get texture for extra life
            var enemyTextures = contentManager.Load<Texture2D>("enemies");
            _enemies = new SpriteSheet(spriteBatch, enemyTextures, 32, 32);
            _bonusItemListModel = bonusItemListModel;
        }

        public override void Draw()
        {
            var yOffset = 0;
            foreach(var item in _bonusItemListModel.BonusItems)
            {
                if (!item.PickedUpByPlayer)
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

                if (item.PickedUpByPlayer && item.ShowText)
                {
                    _spriteBatch.DrawString(_gameFont, item.TextToShowWhenPickedUp.ToUpper(), new Vector2(item.ItemPickedUpTextPosition.X, item.ItemPickedUpTextPosition.Y - yOffset), Color.White);
                    yOffset += 64;
                }

            }
        }
    }
}