using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using spacerpg.General;
using spacerpg.Interfaces;
using spacerpg.Models;
using System.Linq;

namespace spacerpg.Controllers
{
    class LevelUpController : IController
    {
        private readonly LevelUpModel _levelUpModel;
        private readonly PlayerService _playerService;
        private PlayerModel _playerModel;

        public LevelUpController(LevelUpModel levelUpModel, PlayerService playerService)
        {
            _levelUpModel = levelUpModel;
            _playerService = playerService;
            
        }

        /// <summary>
        /// Control the position of the ship which player can move up and down
        /// Left and right are used to adjust stats
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            _playerModel = _playerService.GetPlayer();
            var state = Keyboard.GetState();
            var pressedKeys = state.GetPressedKeys();
            _levelUpModel.MoveCooldown += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
             if (_levelUpModel.MoveCooldown >= 200)
            {
                if (pressedKeys.Contains(Keys.Up) && _levelUpModel.ShipPosition.Y <= _levelUpModel.AcceptPosition.Y - _levelUpModel.ShipYOffset && _levelUpModel.ShipPosition.Y >= _levelUpModel.HpPosition.Y)
                {
                    _levelUpModel.ShipPosition += new Vector2(0, -64);
                    _levelUpModel.MoveCooldown = 0f;
                } 
                else if (pressedKeys.Contains(Keys.Down) && _levelUpModel.ShipPosition.Y >= _levelUpModel.HpPosition.Y - _levelUpModel.ShipYOffset && _levelUpModel.ShipPosition.Y < _levelUpModel.AcceptPosition.Y - _levelUpModel.ShipYOffset)
                {
                    _levelUpModel.ShipPosition += new Vector2(0, 64);
                    _levelUpModel.MoveCooldown = 0f;
                }
                else if (pressedKeys.Contains(Keys.Left))
                {
                    RemoveSkillPoint();
                }
                else if (pressedKeys.Contains(Keys.Right) && _playerModel.AvailableSkillPoints > 0)
                {
                    AddSkillPoint();
                }
                else if (pressedKeys.Contains(Keys.Enter) && _levelUpModel.ShipPosition.Y == _levelUpModel.AcceptPosition.Y - _levelUpModel.ShipYOffset)
                {
                    _levelUpModel.SkillPointsAssigned = true;
                }
            }
        }

        /// <summary>
        /// Add stats based on the position of the ship
        /// Removes skill point after stats adjusted
        /// </summary>
        private void AddSkillPoint()
        {
            if (_levelUpModel.ShipPosition.Y == _levelUpModel.HpPosition.Y - _levelUpModel.ShipYOffset )
            {
                _playerModel.HP += 1f;
                _playerModel.AvailableSkillPoints--;
            }

            if (_levelUpModel.ShipPosition.Y == _levelUpModel.DamagePosition.Y - _levelUpModel.ShipYOffset && _playerModel.AvailableSkillPoints > 0)
            {
                _playerModel.Damage += 0.5f;
                _playerModel.AvailableSkillPoints--;
            }

            if (_levelUpModel.ShipPosition.Y == _levelUpModel.SpeedPosition.Y - _levelUpModel.ShipYOffset && _playerModel.AvailableSkillPoints > 0)
            {
                _playerModel.Speed += 0.1f;
                _playerModel.AvailableSkillPoints--;
            }
            _playerService.SetPlayer(_playerModel);
            _levelUpModel.MoveCooldown = 0f;

        }

        /// <summary>
        /// Remove stats based on the position of the ship
        /// Add a skill point after stats adjusted
        /// </summary>
        private void RemoveSkillPoint()
        {
            if (_levelUpModel.ShipPosition.Y == _levelUpModel.HpPosition.Y - _levelUpModel.ShipYOffset && _playerModel.HP > _playerModel.HPStartingValue)
            {
                _playerModel.HP -= 1f;
                _playerModel.AvailableSkillPoints++;
            }

            if (_levelUpModel.ShipPosition.Y == _levelUpModel.DamagePosition.Y - _levelUpModel.ShipYOffset && _playerModel.Damage > _playerModel.DamageStartingValue)
            {
                _playerModel.Damage -= 0.5f;
                _playerModel.AvailableSkillPoints++;
            }

            if (_levelUpModel.ShipPosition.Y == _levelUpModel.SpeedPosition.Y - _levelUpModel.ShipYOffset && _playerModel.Speed > _playerModel.SpeedStartingValue)
            {
                _playerModel.Speed -= 0.1f;
                _playerModel.AvailableSkillPoints++;
            }
            _playerService.SetPlayer(_playerModel);
            _levelUpModel.MoveCooldown = 0f;
        }
    }
}
