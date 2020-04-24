
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using spacerpg.Enums;
using spacerpg.General;
using spacerpg.Interfaces;
using spacerpg.Models;
using System;
using System.Linq;

namespace spacerpg.Controllers
{
    class BulletController : IController
    {
        private readonly float BulletDistance = 6;
        private readonly float EnemyBulletDistance = 3;
        private readonly BulletListModel _bulletListModel;
        private readonly PlayerModel _playerModel;
        private readonly EnemyListModel _enemyListModel;
        private readonly IReset _reset;
        private float _shootCooldownTime = 0f;
        private readonly SoundEffect _gunSound;
        private readonly SoundEffect _enemyDies;
        private readonly SoundEffect _playerDies;


        public BulletController(BulletListModel bulletListModel, PlayerModel playerModel, EnemyListModel enemyListModel, IReset reset, ContentManager contentManager)
        {
            _bulletListModel = bulletListModel;
            _playerModel = playerModel;
            _enemyListModel = enemyListModel;
            _reset = reset;
            _gunSound = contentManager.Load<SoundEffect>("gun");
            _enemyDies = contentManager.Load<SoundEffect>("enemydies");
            _playerDies = contentManager.Load<SoundEffect>("playerdies");
        }

        /// <summary>
        /// Handles bullet movement and collision
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            PlayerShoots(gameTime);
            EnemyShoots(gameTime);
            MoveBullets();
        }

        /// <summary>
        /// Player shoots when space is pressed and shot is not on cooldown
        /// </summary>
        /// <param name="gameTime">Gametime used for shooting cooldown</param>
        private void PlayerShoots(GameTime gameTime)
        {
            var state = Keyboard.GetState();
            var pressedKeys = state.GetPressedKeys();
            _shootCooldownTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (pressedKeys.Contains(Keys.Space) && _shootCooldownTime >= 200)
            {
                _gunSound.Play(0.1f, 1f, 0f);
                GeneratePlayerBullets();
                _shootCooldownTime = 0f;
            }
        }

        /// <summary>
        /// Generate bullets for the enemies
        /// </summary>
        /// <param name="gameTime">Gametime used for shooting cooldown</param>
        private void EnemyShoots(GameTime gameTime)
        {
            var enemiesAlive = _enemyListModel.Enemies.Where(e => !e.IsDead).ToList();
            foreach (var enemy in _enemyListModel.Enemies)
            {
                enemy.ShootCooldown += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (_playerModel.Stage <= 20)
                {
                    ShootUpOrDown(enemy);
                }
                else if (_playerModel.Stage > 20 && _playerModel.Stage <= 40)
                {
                    ShootUpAndDown(enemy);
                }
                else if (_playerModel.Stage > 40 && enemiesAlive.Count <= 7)
                {
                    ShootEveryDirection(enemy);
                }
                else
                {
                    ShootUpAndDown(enemy);
                }

            }
        }

        private void ShootUpOrDown(EnemyModel enemy)
        {
            float y = 0;
            // Shoot up
            if (_playerModel.Position.Y <= enemy.Position.Y)
            {
                y -= EnemyBulletDistance;
            }

            // Shoot down
            if (_playerModel.Position.Y >= enemy.Position.Y)
            {
                y += EnemyBulletDistance;
            }

            if (!enemy.IsDead && !enemy.IsBoss && enemy.ShootCooldown >= 2000)
            {

                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(enemy.Position.X + 25, enemy.Position.Y), 0, true, true, false, Color.Yellow, enemy.Damage, new Vector2(0, y)));
                enemy.ShootCooldown = 0f;
            }

            if (!enemy.IsDead && enemy.IsBoss && enemy.ShootCooldown >= 1000)
            {
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(enemy.Position.X + 25, enemy.Position.Y), 0, true, true, true, Color.Yellow, enemy.Damage, new Vector2(0, y * 2)));
                enemy.ShootCooldown = 0f;
            }
        }

        private void ShootUpAndDown(EnemyModel enemy)
        {
            if (!enemy.IsDead && !enemy.IsBoss && enemy.ShootCooldown >= 1000)
            {
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(enemy.Position.X + 25, enemy.Position.Y), 0, true, true, false, Color.Yellow, enemy.Damage, new Vector2(0, -EnemyBulletDistance)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(enemy.Position.X + 25, enemy.Position.Y), 0, true, true, false, Color.Yellow, enemy.Damage, new Vector2(0, +EnemyBulletDistance)));
                enemy.ShootCooldown = 0f;
            }

            if (!enemy.IsDead && enemy.IsBoss && enemy.ShootCooldown >= 500)
            {
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(enemy.Position.X + 25, enemy.Position.Y), 0, true, true, true, Color.Yellow, enemy.Damage, new Vector2(0, -EnemyBulletDistance * 2)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(enemy.Position.X + 25, enemy.Position.Y), 0, true, true, true, Color.Yellow, enemy.Damage, new Vector2(0, +EnemyBulletDistance * 2)));
                enemy.ShootCooldown = 0f;
            }
        }

        private void ShootEveryDirection(EnemyModel enemy)
        {
            if (!enemy.IsDead && !enemy.IsBoss && enemy.ShootCooldown >= 1500)
            {
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(enemy.Position.X + 25, enemy.Position.Y), 0, true, true, false, Color.Yellow, enemy.Damage, new Vector2(+EnemyBulletDistance, 0)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(enemy.Position.X + 25, enemy.Position.Y), 0, true, true, false, Color.Yellow, enemy.Damage, new Vector2(-EnemyBulletDistance, 0)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(enemy.Position.X + 25, enemy.Position.Y), 0, true, true, false, Color.Yellow, enemy.Damage, new Vector2(0, -EnemyBulletDistance)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(enemy.Position.X + 25, enemy.Position.Y), 0, true, true, false, Color.Yellow, enemy.Damage, new Vector2(0, +EnemyBulletDistance)));
                enemy.ShootCooldown = 0f;
            }

            if (!enemy.IsDead && enemy.IsBoss && enemy.ShootCooldown >= 1000)
            {
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(enemy.Position.X + 25, enemy.Position.Y), 0, true, true, true, Color.Yellow, enemy.Damage, new Vector2(+EnemyBulletDistance, 0)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(enemy.Position.X + 25, enemy.Position.Y), 0, true, true, true, Color.Yellow, enemy.Damage, new Vector2(-EnemyBulletDistance, 0)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(enemy.Position.X + 25, enemy.Position.Y), 0, true, true, true, Color.Yellow, enemy.Damage, new Vector2(0, -EnemyBulletDistance * 2)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(enemy.Position.X + 25, enemy.Position.Y), 0, true, true, true, Color.Yellow, enemy.Damage, new Vector2(0, +EnemyBulletDistance * 2)));
                enemy.ShootCooldown = 0f;
            }
        }

        /// <summary>
        /// Move bullets to the direction given for the bullets
        /// </summary>
        private void MoveBullets()
        {
            foreach (var bullet in _bulletListModel.Bullets)
            {
                bullet.Position += bullet.Direction;
                var bulletArea = new Rectangle((int)bullet.Position.X, (int)bullet.Position.Y, 8, 8);

                if (!bullet.ShotByEnemy && bullet.Visible)
                {
                    foreach (var enemy in _enemyListModel.Enemies)
                    {
                        
                        if (enemy.Area.Intersects(bulletArea) && !enemy.IsDead)
                        {
                            bullet.Visible = false;
                            enemy.HPLeft -= bullet.BulletDamage;
                            if (enemy.HPLeft <= 0)
                            {
                                _enemyDies.Play(0.3f, 1f, 0f);
                                enemy.IsDead = true;
                                _playerModel.AddXp(enemy.IsBoss ? 50 : (1 + _playerModel.Level));
                            }
                        }

                    }
                }

                if (bullet.ShotByEnemy && bullet.Visible)
                {
                    var playerArea = new Rectangle((int)_playerModel.Position.X + 4, (int)_playerModel.Position.Y + 4, 56, 56);

                    if (bulletArea.Intersects(playerArea) && !_playerModel.Invulnerable)
                    {
                        bullet.Visible = false;
                        _playerModel.HPLeft -= bullet.BulletDamage;

                        if (_playerModel.HPLeft <= 0)
                        {
                            _playerDies.Play(0.3f, 1f, 0f);
                            _reset.Reset(ResetMode.Death);
                        }
                    }
                }

                if (bullet.Position.Y < 0 
                    || bullet.Position.Y >= VirtualScreenSize.Height * VirtualScreenSize.ScreenSizeMultiplier 
                    || bullet.Position.X < 0 
                    || bullet.Position.X >= VirtualScreenSize.Width * VirtualScreenSize.ScreenSizeMultiplier)
                {
                    bullet.Visible = false;
                }
            }
        }

        /// <summary>
        /// Generate bullets with correct direction based
        /// on the amount of bullets the player has.
        /// </summary>
        private void GeneratePlayerBullets()
        {
            if (_playerModel.BulletAmount == 1)
            {
                // Up
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 30, _playerModel.Position.Y), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(0, -BulletDistance + _playerModel.Speed)));

            }
            if (_playerModel.BulletAmount == 2)
            {
                // Up & down
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 30, _playerModel.Position.Y), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(0, -BulletDistance + _playerModel.Speed)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 30, _playerModel.Position.Y + 50), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(0, +BulletDistance + _playerModel.Speed)));
            }

            if (_playerModel.BulletAmount == 3)
            {
                // Up, down and left
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 30, _playerModel.Position.Y), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(0, -BulletDistance + _playerModel.Speed)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 30, _playerModel.Position.Y + 50), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(0, +BulletDistance + _playerModel.Speed)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X, _playerModel.Position.Y + 30), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(-BulletDistance + _playerModel.Speed, 0)));
            }

            if (_playerModel.BulletAmount == 4)
            {
                // Up, down, left and right
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 30, _playerModel.Position.Y), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(0, -BulletDistance + _playerModel.Speed)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 30, _playerModel.Position.Y + 50), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(0, +BulletDistance + _playerModel.Speed)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X, _playerModel.Position.Y + 30), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(-BulletDistance + _playerModel.Speed, 0)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 50, _playerModel.Position.Y + 30), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(+BulletDistance + _playerModel.Speed, 0)));
            }

            if (_playerModel.BulletAmount == 5)
            {
                // Two up, down, left and right
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 2, _playerModel.Position.Y), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(0, -BulletDistance + _playerModel.Speed)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 55, _playerModel.Position.Y), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(0, -BulletDistance + _playerModel.Speed)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 30, _playerModel.Position.Y + 50), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(0, +BulletDistance + _playerModel.Speed)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X, _playerModel.Position.Y + 30), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(-BulletDistance + _playerModel.Speed, 0)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 50, _playerModel.Position.Y + 30), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(+BulletDistance + _playerModel.Speed, 0)));
            }

            if (_playerModel.BulletAmount == 6)
            {
                // Two up, two down, left and right
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 2, _playerModel.Position.Y), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(0, -BulletDistance + _playerModel.Speed)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 55, _playerModel.Position.Y), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(0, -BulletDistance + _playerModel.Speed)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 2, _playerModel.Position.Y + 50), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(0, +BulletDistance + _playerModel.Speed)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 55, _playerModel.Position.Y + 50), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(0, +BulletDistance + _playerModel.Speed)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X, _playerModel.Position.Y + 30), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(-BulletDistance + _playerModel.Speed, 0)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 50, _playerModel.Position.Y + 30), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(+BulletDistance + _playerModel.Speed, 0)));
            }

            if (_playerModel.BulletAmount == 7)
            {
                // Two up, two down, two left and right
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 2, _playerModel.Position.Y), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(0, -BulletDistance + _playerModel.Speed)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 55, _playerModel.Position.Y), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(0, -BulletDistance + _playerModel.Speed)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 2, _playerModel.Position.Y + 50), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(0, +BulletDistance + _playerModel.Speed)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 55, _playerModel.Position.Y + 50), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(0, +BulletDistance + _playerModel.Speed)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X, _playerModel.Position.Y + 2), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(-BulletDistance + _playerModel.Speed, 0)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X, _playerModel.Position.Y + 55), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(-BulletDistance + _playerModel.Speed, 0)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 50, _playerModel.Position.Y + 30), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(+BulletDistance + _playerModel.Speed, 0)));
            }

            if (_playerModel.BulletAmount == 8)
            {
                // Two up, two down, two left and two right
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 2, _playerModel.Position.Y), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(0, -BulletDistance + _playerModel.Speed)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 55, _playerModel.Position.Y), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(0, -BulletDistance + _playerModel.Speed)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 2, _playerModel.Position.Y + 50), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(0, +BulletDistance + _playerModel.Speed)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 55, _playerModel.Position.Y + 50), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(0, +BulletDistance + _playerModel.Speed)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X, _playerModel.Position.Y + 2), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(-BulletDistance + _playerModel.Speed, 0)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X, _playerModel.Position.Y + 55), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(-BulletDistance + _playerModel.Speed, 0)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 50, _playerModel.Position.Y + 2), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(+BulletDistance + _playerModel.Speed, 0)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 50, _playerModel.Position.Y + 55), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(+BulletDistance + _playerModel.Speed, 0)));
            }

            if (_playerModel.BulletAmount == 9)
            {
                // Three up, two down, two left and two right
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 2, _playerModel.Position.Y), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(0, -BulletDistance + _playerModel.Speed)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 30, _playerModel.Position.Y), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(0, -BulletDistance + _playerModel.Speed)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 55, _playerModel.Position.Y), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(0, -BulletDistance + _playerModel.Speed)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 2, _playerModel.Position.Y + 50), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(0, +BulletDistance + _playerModel.Speed)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 55, _playerModel.Position.Y + 50), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(0, +BulletDistance + _playerModel.Speed)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X, _playerModel.Position.Y + 2), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(-BulletDistance + _playerModel.Speed, 0)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X, _playerModel.Position.Y + 55), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(-BulletDistance + _playerModel.Speed, 0)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 50, _playerModel.Position.Y + 2), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(+BulletDistance + _playerModel.Speed, 0)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 50, _playerModel.Position.Y + 55), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(+BulletDistance + _playerModel.Speed, 0)));
            }

            if (_playerModel.BulletAmount == 10)
            {
                // Three up, three down, two left and two right
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 2, _playerModel.Position.Y), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(0, -BulletDistance + _playerModel.Speed)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 30, _playerModel.Position.Y), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(0, -BulletDistance + _playerModel.Speed)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 55, _playerModel.Position.Y), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(0, -BulletDistance + _playerModel.Speed)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 2, _playerModel.Position.Y + 50), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(0, +BulletDistance + _playerModel.Speed)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 30, _playerModel.Position.Y + 50), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(0, +BulletDistance + _playerModel.Speed)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 55, _playerModel.Position.Y + 50), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(0, +BulletDistance + _playerModel.Speed)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X, _playerModel.Position.Y + 2), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(-BulletDistance + _playerModel.Speed, 0)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X, _playerModel.Position.Y + 55), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(-BulletDistance + _playerModel.Speed, 0)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 50, _playerModel.Position.Y + 2), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(+BulletDistance + _playerModel.Speed, 0)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 50, _playerModel.Position.Y + 55), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(+BulletDistance + _playerModel.Speed, 0)));
            }

            if (_playerModel.BulletAmount == 11)
            {
                // Three up, three down, three left and two right
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 2, _playerModel.Position.Y), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(0, -BulletDistance + _playerModel.Speed)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 30, _playerModel.Position.Y), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(0, -BulletDistance + _playerModel.Speed)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 55, _playerModel.Position.Y), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(0, -BulletDistance + _playerModel.Speed)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 2, _playerModel.Position.Y + 50), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(0, +BulletDistance + _playerModel.Speed)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 30, _playerModel.Position.Y + 50), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(0, +BulletDistance + _playerModel.Speed)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 55, _playerModel.Position.Y + 50), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(0, +BulletDistance + _playerModel.Speed)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X, _playerModel.Position.Y + 2), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(-BulletDistance + _playerModel.Speed, 0)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X, _playerModel.Position.Y + 30), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(-BulletDistance + _playerModel.Speed, 0)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X, _playerModel.Position.Y + 55), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(-BulletDistance + _playerModel.Speed, 0)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 50, _playerModel.Position.Y + 2), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(+BulletDistance + _playerModel.Speed, 0)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 50, _playerModel.Position.Y + 55), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(+BulletDistance + _playerModel.Speed, 0)));
            }

            if (_playerModel.BulletAmount >= 12)
            {
                // Three up, three down, three left and three right
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 2, _playerModel.Position.Y), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(0, -BulletDistance + _playerModel.Speed)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 30, _playerModel.Position.Y), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(0, -BulletDistance + _playerModel.Speed)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 55, _playerModel.Position.Y), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(0, -BulletDistance + _playerModel.Speed)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 2, _playerModel.Position.Y + 50), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(0, +BulletDistance + _playerModel.Speed)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 30, _playerModel.Position.Y + 50), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(0, +BulletDistance + _playerModel.Speed)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 55, _playerModel.Position.Y + 50), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(0, +BulletDistance + _playerModel.Speed)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X, _playerModel.Position.Y + 2), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(-BulletDistance + _playerModel.Speed, 0)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X, _playerModel.Position.Y + 30), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(-BulletDistance + _playerModel.Speed, 0)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X, _playerModel.Position.Y + 55), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(-BulletDistance + _playerModel.Speed, 0)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 50, _playerModel.Position.Y + 2), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(+BulletDistance + _playerModel.Speed, 0)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 50, _playerModel.Position.Y + 30), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(+BulletDistance + _playerModel.Speed, 0)));
                _bulletListModel.Bullets.Add(new BulletModel(new Vector2(_playerModel.Position.X + 50, _playerModel.Position.Y + 55), 0, true, false, false, Color.Red, _playerModel.Damage, new Vector2(+BulletDistance + _playerModel.Speed, 0)));
            }

        }
    }
}
