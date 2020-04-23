using System.Collections;
using System.Collections.Generic;

namespace spacerpg.Models
{
    class BonusItemListModel
    {
        public List<BonusItem> BonusItems { get; } = new List<BonusItem>();

        public BonusItemListModel(IEnumerable<BonusItem> bonusItems)
        {
            BonusItems.AddRange(bonusItems);
        }
    }
}
