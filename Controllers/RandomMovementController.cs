
using Microsoft.Xna.Framework;
using spacerpg.General;
using spacerpg.Interfaces;

namespace spacerpg.Controllers
{
    class RandomMovementController
    {
        private readonly int XOffset = 23;
        private readonly int YOffset = 40;

        /// <summary>
        /// Move items randomly around the screen
        /// </summary>
        /// <param name="model"></param>
        /// <param name="distance"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void Move(IModel model, float distance, int width, int height)
        {
            // Up & left
            if (model.Position.Y > 0 
                && model.Position.Y > model.TargetPosition.Y 
                && model.Position.X > 0 
                && model.Position.X > model.TargetPosition.X)
            {
                model.Position += new Vector2(-distance, -distance);
            }
            // Up & right
            else if (model.Position.Y > 0 
                && model.Position.Y > model.TargetPosition.Y 
                && model.Position.Y > 0 
                && model.Position.X < VirtualScreenSize.Width * VirtualScreenSize.ScreenSizeMultiplier - XOffset 
                && model.Position.X < model.TargetPosition.X)
            {
                model.Position += new Vector2(distance, -distance);
            }
            // Down & left
            else if (model.Position.Y < VirtualScreenSize.Height * VirtualScreenSize.ScreenSizeMultiplier - YOffset 
                && model.Position.Y < model.TargetPosition.Y 
                && model.Position.X > 0 
                && model.Position.X > model.TargetPosition.X)
            {
                model.Position += new Vector2(-distance, distance);
            }
            // Down & right
            else if (model.Position.Y < VirtualScreenSize.Height * VirtualScreenSize.ScreenSizeMultiplier - YOffset 
                && model.Position.Y < model.TargetPosition.Y 
                && model.Position.X < VirtualScreenSize.Width * VirtualScreenSize.ScreenSizeMultiplier - XOffset 
                && model.Position.X < model.TargetPosition.X)
            {
                model.Position += new Vector2(distance, distance);
            }
            // Up
            else if (model.Position.Y > 0 
                && model.Position.Y > model.TargetPosition.Y)
            {
                model.Position += new Vector2(0, -distance);
            }
            // Down
            else if (model.Position.Y < VirtualScreenSize.Height * VirtualScreenSize.ScreenSizeMultiplier - YOffset 
                && model.Position.Y < model.TargetPosition.Y)
            {
                model.Position += new Vector2(0, distance);
            }
            // Left
            else if (model.Position.X > 0 && model.Position.X > model.TargetPosition.X)
            {
                model.Position += new Vector2(-distance, 0);
            }
            // Right
            else if (model.Position.X < VirtualScreenSize.Width * VirtualScreenSize.ScreenSizeMultiplier - XOffset 
                && model.Position.X < model.TargetPosition.X)
            {
                model.Position += new Vector2(distance, 0);
            }

            model.Area = new Rectangle((int)model.Position.X + 4, (int)model.Position.Y + 4, width, height);
        }
    }
}
