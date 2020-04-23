using Microsoft.Xna.Framework;
using spacerpg.General;

namespace spacerpg.Models
{
    class GameOverModel
    {
        public bool PlayAgain { get; set; }
        public Vector2 QuotePosition { get; set; }
        public Vector2 GameOverPosition { get; set; }
        public Vector2 PressButtonTextPosition { get; set; }

        public GameOverModel()
        {
            QuotePosition = new Vector2(VirtualScreenSize.Width * VirtualScreenSize.ScreenSizeMultiplier / 2 - 460, VirtualScreenSize.Height * VirtualScreenSize.ScreenSizeMultiplier / 2 - 240);
            GameOverPosition = new Vector2(VirtualScreenSize.Width * VirtualScreenSize.ScreenSizeMultiplier / 2, VirtualScreenSize.Height * VirtualScreenSize.ScreenSizeMultiplier / 2 - 100);
            PressButtonTextPosition = new Vector2(VirtualScreenSize.Width * VirtualScreenSize.ScreenSizeMultiplier / 2 - 290, VirtualScreenSize.Height * VirtualScreenSize.ScreenSizeMultiplier / 2 - 60);
        }
    }
}
