using Microsoft.Xna.Framework;
using spacerpg.General;

namespace spacerpg.Models
{
    class LevelUpModel
    {
        public bool SkillPointsAssigned { get; set; }
        public int Frame { get; set; }
        public Vector2 QuotePosition { get; set; }
        public Vector2 TitlePosition { get; set; }
        public Vector2 ShipPosition { get; set; }
        public Vector2 HpPosition { get; set; }
        public Vector2 DamagePosition { get; set; }
        public Vector2 SpeedPosition { get; set; }
        public Vector2 AcceptPosition { get; set; }
        public float MoveCooldown { get; set; }
        public int ShipYOffset { get; set; }

        public LevelUpModel()
        {
            Frame = 2;
            ShipPosition = new Vector2(VirtualScreenSize.Width * VirtualScreenSize.ScreenSizeMultiplier / 2 - 70, VirtualScreenSize.Height * VirtualScreenSize.ScreenSizeMultiplier / 2 - 56);
            QuotePosition = new Vector2(VirtualScreenSize.Width * VirtualScreenSize.ScreenSizeMultiplier / 2 - 200, VirtualScreenSize.Height * VirtualScreenSize.ScreenSizeMultiplier / 2 - 164);
            TitlePosition = new Vector2(VirtualScreenSize.Width * VirtualScreenSize.ScreenSizeMultiplier / 2, VirtualScreenSize.Height * VirtualScreenSize.ScreenSizeMultiplier / 2 - 100);
            HpPosition = new Vector2(VirtualScreenSize.Width * VirtualScreenSize.ScreenSizeMultiplier / 2, VirtualScreenSize.Height * VirtualScreenSize.ScreenSizeMultiplier / 2 - 36);
            DamagePosition = new Vector2(VirtualScreenSize.Width * VirtualScreenSize.ScreenSizeMultiplier / 2, VirtualScreenSize.Height * VirtualScreenSize.ScreenSizeMultiplier / 2 + 28);
            SpeedPosition = new Vector2(VirtualScreenSize.Width * VirtualScreenSize.ScreenSizeMultiplier / 2, VirtualScreenSize.Height * VirtualScreenSize.ScreenSizeMultiplier / 2 + 92);
            AcceptPosition = new Vector2(VirtualScreenSize.Width * VirtualScreenSize.ScreenSizeMultiplier / 2, VirtualScreenSize.Height * VirtualScreenSize.ScreenSizeMultiplier / 2 + 156);
            MoveCooldown = 0f;
            ShipYOffset = 20;
        }
    }
}
