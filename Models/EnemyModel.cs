using Microsoft.Xna.Framework;
using spacerpg.Interfaces;

namespace spacerpg.Models
{
    class EnemyModel : IModel
    {
        public Rectangle Area { get; set; }
        public Vector2 Position { get; set; }
        public int Frame { get; set; }
        public Vector2 TargetPosition { get; set; }
        public bool IsDead { get; set; }
        public float ShootCooldown { get; set; }
        public float Damage { get; set; }
        public float HP { get; set; }
        public float HPLeft { get; set; }
        public bool IsBoss { get; set; }

        public EnemyModel(int frame, Vector2 position, Vector2 targetPosition, float hP, float damage, int areaX, int areaY, bool isBoss)
        {
            Area = new Rectangle((int)position.X + 1, (int)position.Y + 1, areaX, areaY);
            Frame = frame;
            Position = position;
            TargetPosition = targetPosition;
            IsDead = false;
            ShootCooldown = 0f;
            Damage = damage;
            HP = hP;
            HPLeft = hP;
            IsBoss = isBoss;
        }
    }
}
