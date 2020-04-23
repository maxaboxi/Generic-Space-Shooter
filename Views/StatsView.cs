
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using spacerpg.General;
using spacerpg.Models;
using System;
using System.Linq;

namespace spacerpg.Views
{
    class StatsView : BaseView
    {
        private SpriteSheet _ships;
        private SpriteFont _gameFont;
        private PlayerModel _playerModel;
        private SpriteBatch _spriteBatch;
        private EnemyListModel _enemyListModel;

        public StatsView(ContentManager contentManager, SpriteBatch spriteBatch, PlayerModel playerModel, EnemyListModel enemyListModel) : base(contentManager, spriteBatch)
        {
            _gameFont = contentManager.Load<SpriteFont>("File");
            var shipTextures = contentManager.Load<Texture2D>("shipsall");
            _ships = new SpriteSheet(spriteBatch, shipTextures, 64, 64);
            _playerModel = playerModel;
            _spriteBatch = spriteBatch;
            _enemyListModel = enemyListModel;
        }

        public override void Draw()
        {
            // Draw lives
            for (int i = 0; i < _playerModel.Lives; i++)
            {
                _ships.Draw(new Vector2(i * 64, 0), _playerModel.Frame, Color.White);
            }

            var boss = _enemyListModel.Enemies.First(e => e.IsBoss);
            var enemy = _enemyListModel.Enemies.First(e => !e.IsBoss);
            _spriteBatch.DrawString(_gameFont, "Stage: " + _playerModel.Stage.ToString(), new Vector2(0, 64), Color.White);
            _spriteBatch.DrawString(_gameFont, "Level: " + _playerModel.Level.ToString(), new Vector2(0, 94), Color.White);
            _spriteBatch.DrawString(_gameFont, "XP: " + _playerModel.Experience.ToString(), new Vector2(0, 124), Color.White);
            _spriteBatch.DrawString(_gameFont, "HP: " + Math.Round(_playerModel.HPLeft, 2).ToString() + " / " + _playerModel.HP.ToString(), new Vector2(0, 154), Color.White);
            _spriteBatch.DrawString(_gameFont, "Damage: " + Math.Round(_playerModel.Damage, 2).ToString(), new Vector2(0, 184), Color.White);
            _spriteBatch.DrawString(_gameFont, "Speed: " + (4 + _playerModel.Speed).ToString(), new Vector2(0, 214), Color.White);

            _spriteBatch.DrawString(_gameFont, "Enemy HP: " + enemy.HP.ToString(), new Vector2(0, 274), Color.Red);
            _spriteBatch.DrawString(_gameFont, "Enemy damage: " + enemy.Damage.ToString(), new Vector2(0, 304), Color.Red);
            if (boss.HP > 0)
            {
                var bossHpLeft = boss.HPLeft >= 0 ? boss.HPLeft.ToString() : "0";
                _spriteBatch.DrawString(_gameFont, "Boss HP: " + bossHpLeft + " / " + boss.HP.ToString(), new Vector2(0, 334), Color.Red);
                _spriteBatch.DrawString(_gameFont, "Boss damage: " + boss.Damage.ToString(), new Vector2(0, 364), Color.Red);
            }

            _spriteBatch.DrawString(_gameFont, "Highest stage reached: " + (_playerModel.HighScore).ToString(), new Vector2(0, 450), Color.White);
        }
    }
}
