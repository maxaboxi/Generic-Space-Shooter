using Microsoft.Xna.Framework;
using spacerpg.Enums;
using spacerpg.General;
using spacerpg.Interfaces;

namespace spacerpg.Models
{
    class BonusItem : IModel
    {
        public Rectangle Area { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 TargetPosition { get; set; }
        public Vector2 ItemPickedUpTextPosition { get; set; }
        public int Frame { get; set; }
        public BonusItemType Type { get; set; }
        public float Amount { get; set; }
        public bool PickedUpByPlayer { get; set; }
        public string TextToShowWhenPickedUp { get; set; }
        public bool ShowText { get; set; }
        public float ShowTextCooldown { get; set; }

        public BonusItem(Vector2 position, int frame, BonusItemType type, float amount, string textToShowWhenPickedUp)
        {
            Area = new Rectangle((int)position.X, (int)position.Y, 8, 8);
            ItemPickedUpTextPosition = new Vector2(0, VirtualScreenSize.Height * VirtualScreenSize.ScreenSizeMultiplier - 50);
            Position = position;
            TargetPosition = position;
            Frame = frame;
            Type = type;
            Amount = amount;
            PickedUpByPlayer = false;
            ShowText = false;
            TextToShowWhenPickedUp = textToShowWhenPickedUp;
            ShowTextCooldown = 0f;
        }
    }
}
