using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using spacerpg.General;
using spacerpg.Models;

namespace spacerpg.Views
{
    class EnemyView : BaseView
    {
        private readonly EnemyListModel _enemyList;
        private readonly SpriteSheet _enemies;
        private readonly SpriteSheet _boss;

        // Uncomment to debug enemy positioning
        //private readonly SpriteFont _gameFont;
        //private readonly SpriteBatch _spriteBatch;

        public EnemyView(ContentManager contentManager, SpriteBatch spriteBatch, EnemyListModel enemyListModel) : base(contentManager, spriteBatch)
        {
            // Uncomment to debug enemy positioning
            //_spriteBatch = spriteBatch;
            //_gameFont = contentManager.Load<SpriteFont>("File");

            var enemyTextures = contentManager.Load<Texture2D>("enemies");
            var bossTexture = contentManager.Load<Texture2D>("boss");
            _enemies = new SpriteSheet(spriteBatch, enemyTextures, 32, 32);
            _boss = new SpriteSheet(spriteBatch, bossTexture, 64, 59);
            _enemyList = enemyListModel;

        }

        public override void Draw()
        {
            foreach(var enemy in _enemyList.Enemies)
            {
                if (!enemy.IsDead && !enemy.IsBoss)
                {
                    _enemies.Draw(enemy.Position, enemy.Frame, Color.White);

                    // Uncomment to debug enemy positioning & area
                    //_spriteBatch.DrawString(_gameFont, enemy.Position.ToString(), new Vector2(enemy.Position.X, enemy.Position.Y + 32), Color.Yellow);
                    //_spriteBatch.DrawString(_gameFont, enemy.TargetPosition.ToString(), new Vector2(enemy.Position.X, enemy.Position.Y + 64), Color.Yellow);
                    //_spriteBatch.DrawString(_gameFont, enemy.Area.ToString(), new Vector2(enemy.Position.X, enemy.Position.Y + 96), Color.Yellow);
                }

                if (!enemy.IsDead && enemy.IsBoss)
                {
                    _boss.Draw(enemy.Position, enemy.Frame, Color.White);
                }

            }
        }
    }
}
