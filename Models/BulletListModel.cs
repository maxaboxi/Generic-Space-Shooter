using System.Collections.Generic;

namespace spacerpg.Models
{
    class BulletListModel
    {
        public List<BulletModel> Bullets { get; } = new List<BulletModel>();

        public BulletListModel(List<BulletModel> bullets)
        {
            Bullets.AddRange(bullets);
        }
    }
}
