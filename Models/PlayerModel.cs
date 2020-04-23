using Microsoft.Xna.Framework;
using spacerpg.Enums;
using spacerpg.General;

namespace spacerpg.Models
{
    class PlayerModel
    {
        public readonly Vector2 StartPosition;
        public int Lives { get; set; }
        public int Level { get; set; }
        public int Stage { get; set; }
        public int Experience { get; set; }
        public int Score { get; set; }
        public int HighScore { get; set; }
        public float HP { get; set; }
        public float HPStartingValue { get; set; }
        public float HPLeft { get; set; }
        public float Damage { get; set; }
        public float DamageStartingValue { get; set; }
        public float Speed { get; set; }
        public float SpeedStartingValue { get; set; }
        public Vector2 Position { get; set; }
        public int Frame { get; set; }
        public float BulletAmount { get; set; }
        public bool Invulnerable { get; set; }
        public float InvulnerableCooldown { get; set; }
        public int SkillPoints { get; set; }
        public int AvailableSkillPoints { get; set; }

        public PlayerModel(int stage, int highScore)
        {
            StartPosition = SetStartingPosition();
            Lives = 3;
            Level = 1;
            Stage = stage;
            Experience = 0;
            HP = 2f;
            HPStartingValue = 2f;
            HPLeft = 2f;
            Damage = 1f;
            DamageStartingValue = 1f;
            Frame = 2;
            Position = SetStartingPosition();
            Speed = 0f;
            SpeedStartingValue = 0f;
            BulletAmount = 1;
            Invulnerable = false;
            SkillPoints = 0;
            AvailableSkillPoints = 0;
            Score = 0;
            HighScore = highScore;
        }

        private Vector2 SetStartingPosition()
        {
            var x = VirtualScreenSize.Width * VirtualScreenSize.ScreenSizeMultiplier / 2 + 31;
            var y = VirtualScreenSize.Height * VirtualScreenSize.ScreenSizeMultiplier - 60;
            return new Vector2(x, y);
        }

        internal void ResetPosition(ResetMode resetMode)
        {
            Position = StartPosition;

            if (resetMode == ResetMode.Death)
            {
                HPLeft = HP;
                Invulnerable = true;
            }

            if (resetMode == ResetMode.Level)
            {
                AddXp(20);
            }
        }

        internal void AddXp(int amount)
        {
            Experience += amount;

            if (Experience > 75 * Level)
            {
                var overFlow = Experience - 75 * Level ;
                Experience = 0;
                LevelUp(overFlow);
            }
        }

        private void LevelUp(int xpOverFlow)
        {
            Level += 1;
            HP += 1f;
            HPLeft = HP;
            Experience += xpOverFlow;
            Damage += 0.5f;
            SkillPoints += 1;
            AvailableSkillPoints += 1;
        }
    }
}
