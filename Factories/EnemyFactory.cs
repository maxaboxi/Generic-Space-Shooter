using Microsoft.Xna.Framework;
using spacerpg.General;
using spacerpg.Models;
using System;
using System.Collections.Generic;

namespace spacerpg.Factories
{
    static class EnemyFactory
    {
        private static int XOffset = 70;
        private static int YOffset = 200;

        /// <summary>
        /// Create enemies for the stage
        /// </summary>
        /// <param name="stageNumber">Stage number (+ 3) is the amount of enemies for each stage</param>
        /// <returns>Created enemies for the stage</returns>
        public static IEnumerable<EnemyModel> CreateStage(int stageNumber)
        {
            List<int> possibleEnemyFrames = new List<int>
            {
                20,21,22,32,33,34,48,49,50,60,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,88,89,90,92,93,94,96,97,98,112,113,114,115,116,117,118
            };

            List<EnemyModel> models = new List<EnemyModel>();

            Random random = new Random();

            int i = 1;
            int extraEnemiesToAdd;
            if (stageNumber <= 10)
            {
                extraEnemiesToAdd = 5;
            } else if (stageNumber > 10)
            {
                extraEnemiesToAdd = 10;
            } else if (stageNumber > 20)
            {
                extraEnemiesToAdd = 15;
            } 
            else
            {
                extraEnemiesToAdd = stageNumber;
            }

            while (i <= stageNumber + extraEnemiesToAdd)
            {
                var frame = random.Next(possibleEnemyFrames.Count);
                models.Add(
                    new EnemyModel(
                        possibleEnemyFrames[frame], 
                        new Vector2(random.Next(VirtualScreenSize.Width * VirtualScreenSize.ScreenSizeMultiplier - XOffset), random.Next(VirtualScreenSize.Height * VirtualScreenSize.ScreenSizeMultiplier - YOffset)),
                        new Vector2(random.Next(VirtualScreenSize.Width * VirtualScreenSize.ScreenSizeMultiplier - XOffset), random.Next(VirtualScreenSize.Height * VirtualScreenSize.ScreenSizeMultiplier - YOffset)),
                        stageNumber * 1.5f,
                        stageNumber * 0.5f,
                        30,
                        30,
                        false)
                    );
                i++;
            }

            models.Add(
                new EnemyModel(
                    0, 
                    new Vector2(random.Next(VirtualScreenSize.Width * VirtualScreenSize.ScreenSizeMultiplier - XOffset), random.Next(VirtualScreenSize.Height * VirtualScreenSize.ScreenSizeMultiplier - YOffset)),
                    new Vector2(random.Next(VirtualScreenSize.Width * VirtualScreenSize.ScreenSizeMultiplier - XOffset), random.Next(VirtualScreenSize.Height * VirtualScreenSize.ScreenSizeMultiplier - YOffset)),
                    stageNumber * 5.0f,
                    stageNumber * 0.65f,
                    60,
                    55,
                    true)
                );

            return models;
        }
    }
}
