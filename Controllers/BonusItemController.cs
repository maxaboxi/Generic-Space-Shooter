using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using spacerpg.Enums;
using spacerpg.General;
using spacerpg.Interfaces;
using spacerpg.Models;
using System;

namespace spacerpg.Controllers
{
    class BonusItemController : RandomMovementController, IController
    {
        private readonly float _distance = 4;
        private readonly BonusItemListModel _bonusItemListModel;
        private readonly PlayerModel _playerModel;
        private readonly SoundEffect _soundEffect;

        public BonusItemController(BonusItemListModel bonusItemListModel, PlayerModel playerModel, ContentManager contentManager)
        {
            _bonusItemListModel = bonusItemListModel;
            _playerModel = playerModel;
            _soundEffect = contentManager.Load<SoundEffect>("powerup");
        }
        public void Update(GameTime gameTime)
        {
            MoveItems(gameTime);
        }

        /// <summary>
        /// Moves generated bonus items.
        /// </summary>
        /// <param name="gameTime"></param>
        private void MoveItems(GameTime gameTime)
        {
            var playerArea = new Rectangle((int)_playerModel.Position.X + 4, (int)_playerModel.Position.Y + 4, 56, 56);
            foreach (var item in _bonusItemListModel.BonusItems)
            {
                if (item.Area.Intersects(playerArea) && !item.PickedUpByPlayer)
                {
                    switch (item.Type)
                    {
                        case BonusItemType.XP:
                            _playerModel.AddXp((int)item.Amount * _playerModel.Level);
                            item.PickedUpByPlayer = true;
                            item.ShowText = true;
                            break;
                        case BonusItemType.HP:
                            _playerModel.HP += item.Amount;
                            item.PickedUpByPlayer = true;
                            item.ShowText = true;
                            break;
                        case BonusItemType.Damage:
                            _playerModel.Damage += item.Amount;
                            item.PickedUpByPlayer = true;
                            item.ShowText = true;
                            break;
                        case BonusItemType.ExtraLive:
                            if (_playerModel.Lives < 5)
                            {
                                _playerModel.Lives += (int)item.Amount;
                            }
                            else
                            {
                                _playerModel.HP += 0.1f;
                                _playerModel.HPLeft += 0.1f;
                                _playerModel.HPStartingValue += 0.1f;
                            }
                            item.PickedUpByPlayer = true;
                            item.ShowText = true;
                            break;
                        case BonusItemType.Speed:
                            _playerModel.Speed += item.Amount;
                            item.PickedUpByPlayer = true;
                            item.ShowText = true;
                            break;
                        case BonusItemType.Bullet:
                            if (_playerModel.BulletAmount < 12)
                            {
                                _playerModel.BulletAmount += item.Amount;
                            } 
                            else
                            {
                                _playerModel.Damage += 0.1f;
                                _playerModel.DamageStartingValue += 0.1f;
                            }
                            item.PickedUpByPlayer = true;
                            item.ShowText = true;
                            break;
                    }
                    _soundEffect.Play(0.3f, 1f, 0f);
                }

                // Check if item is either at the target position or close to it
                // Randomize new target position if true
                var targetArea = new Rectangle((int)item.TargetPosition.X, (int)item.TargetPosition.Y, 8, 8);

                if (item.Area.Intersects(targetArea) && !item.PickedUpByPlayer)
                {
                    Random random = new Random();
                    var x = random.Next(VirtualScreenSize.Width * VirtualScreenSize.ScreenSizeMultiplier - 100);
                    var y = random.Next(VirtualScreenSize.Height * VirtualScreenSize.ScreenSizeMultiplier);
                    item.TargetPosition = new Vector2(x, y);
                }
                else
                {
                    Move(item, _distance + _playerModel.Speed, 30, 30);
                }
            }

            HideItemTexts(gameTime);
        }

        /// <summary>
        /// Hides the item pick up text after 2 seconds
        /// </summary>
        /// <param name="gameTime"></param>
        private void HideItemTexts(GameTime gameTime)
        {
            foreach(var item in _bonusItemListModel.BonusItems)
            {
                if (item.ShowText && item.ShowTextCooldown > 4000)
                {
                    item.ShowText = false;
                }

                if (item.ShowText)
                {
                    item.ShowTextCooldown += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                }
            }
        }
    }
}