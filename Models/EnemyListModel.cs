using System.Collections.Generic;

namespace spacerpg.Models
{
    class EnemyListModel
    {
        public List<EnemyModel> Enemies { get; } = new List<EnemyModel>();

        public EnemyListModel(IEnumerable<EnemyModel> enemies)
        {
            Enemies.AddRange(enemies);
        }
    }
}
