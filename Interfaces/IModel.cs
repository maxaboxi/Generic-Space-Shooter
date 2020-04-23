using Microsoft.Xna.Framework;

namespace spacerpg.Interfaces
{
    interface IModel
    {
        Vector2 Position { get; set; }
        Vector2 TargetPosition { get; set; }
        Rectangle Area { get; set; }
    }
}
