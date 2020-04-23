using Microsoft.Xna.Framework;

namespace spacerpg.Models
{
    class BulletModel
    {
        public Rectangle Area { get; set; }
        public Vector2 Position { get; set; }
        public int Frame { get; set; }
        public bool Visible { get; set; }
        public bool ShotByEnemy { get; set; }
        public bool ShotByBoss { get; set; }
        public Color BulletColor { get; set; }
        public float BulletDamage { get; set; }
        public Vector2 Direction { get; set; }

        public BulletModel(Vector2 position, int frame, bool visible, bool shotByEnemy, bool shotByBoss, Color bulletColor, float damage, Vector2 direction)
        {
            Area = new Rectangle((int)position.X, (int)position.Y, 8, 8);
            Position = position;
            Frame = frame;
            Visible = visible;
            ShotByEnemy = shotByEnemy;
            ShotByBoss = shotByBoss;
            BulletColor = bulletColor;
            BulletDamage = damage;
            Direction = direction;
        }
    }
}
