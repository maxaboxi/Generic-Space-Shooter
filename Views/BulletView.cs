using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using spacerpg.General;
using spacerpg.Models;

namespace spacerpg.Views
{
    class BulletView : BaseView
    {
        private readonly SpriteSheet _bullet;
        private readonly BulletListModel _bulletListModel;

        public BulletView(ContentManager contentManager, SpriteBatch spriteBatch, BulletListModel bulletListModel) : base(contentManager, spriteBatch)
        {
            var bulletTexture = contentManager.Load<Texture2D>("bullet01");
            _bullet = new SpriteSheet(spriteBatch, bulletTexture, 8, 8);
            _bulletListModel = bulletListModel;
        }

        public override void Draw()
        {
            foreach(var bullet in _bulletListModel.Bullets)
            {
                if (bullet.Visible)
                {
                    _bullet.Draw(bullet.Position, 0, bullet.BulletColor);
                }
            }
        }
    }
}
