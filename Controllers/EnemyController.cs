using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using spacerpg.Enums;
using spacerpg.General;
using spacerpg.Interfaces;
using spacerpg.Models;
using System;
using System.Linq;

namespace spacerpg.Controllers
{
    class EnemyController : RandomMovementController, IController
    {
        private readonly float _distance = 2;
        private readonly int XOffset = 23;
        private readonly int YOffset = 40;

        private readonly EnemyListModel _enemyListModel;
        private readonly PlayerModel _playerModel;
        private readonly IReset _reset;
        private readonly SoundEffect _playerDies;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="enemyListModel">Enemy list model</param>
        /// <param name="playerModel">Player model</param>
        /// <param name="reset">Controller implementing IReset interface</param>
        public EnemyController(EnemyListModel enemyListModel, PlayerModel playerModel, IReset reset, ContentManager contentManager)
        {
            _enemyListModel = enemyListModel;
            _playerModel = playerModel;
            _reset = reset;
            _playerDies = contentManager.Load<SoundEffect>("playerdies");
        }

        /// <summary>
        /// Move the enemies randomly and kill player on collision.
        /// </summary>
        /// <param name="gameTime">Game time</param>
        public void Update(GameTime gameTime)
        {
            var enemiesAlive = _enemyListModel.Enemies.Where(enemy => !enemy.IsDead).ToList();
            if (enemiesAlive.Count > 0)
            {
                var playerArea = new Rectangle((int)_playerModel.Position.X + 4, (int)_playerModel.Position.Y + 4, 56, 56);
                foreach (var enemy in _enemyListModel.Enemies)
                {
                    if (enemy.Area.Intersects(playerArea) && !enemy.IsDead && !_playerModel.Invulnerable)
                    {
                        _playerDies.Play(0.3f, 1f, 0f);
                        _reset.Reset(ResetMode.Death);
                    }

                    // Check if enemy area intersects the target position area
                    // Randomize new target position if true
                    var targetArea = new Rectangle((int)enemy.TargetPosition.X, (int)enemy.TargetPosition.Y, 8, 8);

                    if (enemy.Area.Intersects(targetArea))
                    {
                        Random random = new Random();
                        var x = random.Next(VirtualScreenSize.Width * VirtualScreenSize.ScreenSizeMultiplier - XOffset);
                        var y = random.Next(VirtualScreenSize.Height * VirtualScreenSize.ScreenSizeMultiplier - YOffset);
                        enemy.TargetPosition = new Vector2(x, y);
                    }
                    else
                    {
                        var speed = 0f;
                        if (_playerModel.Stage > 20)
                        {
                            speed = 1f;
                        } else if (_playerModel.Stage > 30)
                        {
                            speed = 3f;
                        } else if (_playerModel.Stage > 50)
                        {
                            speed = 5f;
                        }

                        var width = enemy.IsBoss ? 60 : 26;
                        var height = enemy.IsBoss ? 55 : 26;
                        Move(enemy, _distance + speed, width, height);
                    }
                }
            }
        }
    }
}
