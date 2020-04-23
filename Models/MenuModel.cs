using Microsoft.Xna.Framework;
using spacerpg.General;

namespace spacerpg.Models
{
    class MenuModel
    {
        public bool NewGame { get; set; }
        public int Frame { get; set; }
        public Vector2 ShipPosition { get; set; }
        public Vector2 NewGamePosition { get; set; }
        public Vector2 InfoPosition { get; set; }
        public Vector2 ExitGamePosition { get; set; }
        public float MoveCooldown { get; set; }
        public bool ShowInfo { get; set; }
        public int ShipYOffset { get; set; }
        public Vector2 InfoXOffset { get; set; }
        public Vector2 InfoYOffset { get; set; }
        public int BonusItemYOffset { get; set; }
        public int BonusItemXOffset { get; set; }

        public MenuModel()
        {
            Frame = 2;
            ShipPosition = new Vector2(VirtualScreenSize.Width * VirtualScreenSize.ScreenSizeMultiplier / 2 - 70, VirtualScreenSize.Height * VirtualScreenSize.ScreenSizeMultiplier / 2 - 120);
            NewGamePosition = new Vector2(VirtualScreenSize.Width * VirtualScreenSize.ScreenSizeMultiplier / 2, VirtualScreenSize.Height * VirtualScreenSize.ScreenSizeMultiplier / 2 - 100);
            InfoPosition = new Vector2(VirtualScreenSize.Width * VirtualScreenSize.ScreenSizeMultiplier / 2, VirtualScreenSize.Height * VirtualScreenSize.ScreenSizeMultiplier / 2 - 36);
            ExitGamePosition = new Vector2(VirtualScreenSize.Width * VirtualScreenSize.ScreenSizeMultiplier / 2, VirtualScreenSize.Height * VirtualScreenSize.ScreenSizeMultiplier / 2 + 28);
            MoveCooldown = 0f;
            ShowInfo = false;
            ShipYOffset = 20;
            InfoXOffset = new Vector2(500, 0);
            InfoYOffset = new Vector2(0, 30);
            BonusItemXOffset = 50;
            BonusItemYOffset = 270;
        }
    }
}
