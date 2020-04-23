using Microsoft.Xna.Framework;
using spacerpg.Enums;
using spacerpg.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace spacerpg.Factories
{
    static class BonusItemFactory
    {
        /// <summary>
        /// Generate bonus items for the stage. One chance per stage.
        /// I.e. if player is at stage 3, the player has 3 chances of getting bonus items
        /// </summary>
        /// <param name="stageNumber">Number of the stage player is at</param>
        /// <returns>Created bonus items for the stage</returns>
        public static IEnumerable<BonusItem> CreateBonusItemsForStage(int stageNumber)
        {
            List<BonusItem> bonusItems = new List<BonusItem>();
            var random = new Random();
            int i = 0;
            while (i < stageNumber && bonusItems.Count < 3)
            {
                var number = random.Next(100);
                if (number <= 12)
                {
                    if (number <= 2 && !bonusItems.Any(item => item.Type == BonusItemType.ExtraLive))
                    {
                        bonusItems.Add(new BonusItem(new Vector2(random.Next(1880), random.Next(700)), 8, BonusItemType.ExtraLive, 1, "We all end up dead, it's just a question of how and why."));
                    }

                    if (number > 2 && number <= 4 && !bonusItems.Any(item => item.Type == BonusItemType.Damage))
                    {
                        bonusItems.Add(new BonusItem(new Vector2(random.Next(1880), random.Next(700)), 56, BonusItemType.Damage, 0.5f, "We go through with this, nobody gets hurt right?"));
                    }

                    if (number > 4 && number <= 6)
                    {
                        bonusItems.Add(new BonusItem(new Vector2(random.Next(1880), random.Next(700)), 121, BonusItemType.XP, 20, "Great men are not born great, they grow great."));
                    }

                    if (number > 6 && number <= 8 && !bonusItems.Any(item => item.Type == BonusItemType.HP))
                    {
                        bonusItems.Add(new BonusItem(new Vector2(random.Next(1880), random.Next(700)), 108, BonusItemType.HP, 1f, "All we have to decide is what to do with the time that is given to us."));
                    }

                    if (number > 8 && number <= 10 && !bonusItems.Any(item => item.Type == BonusItemType.Speed))
                    {
                        bonusItems.Add(new BonusItem(new Vector2(random.Next(1880), random.Next(700)), 139, BonusItemType.Speed, 0.1f, "If everything seems under control, you're not going fast enough."));
                    }

                    if (number > 10 && !bonusItems.Any(item => item.Type == BonusItemType.Bullet))
                    {
                        bonusItems.Add(new BonusItem(new Vector2(random.Next(1880), random.Next(700)), 87, BonusItemType.Bullet, 1, "They're fake bullets, so why do I feel like Im bleeding out?"));
                    }
                }

                i++;
            }

            return bonusItems;
        }
    }
}
