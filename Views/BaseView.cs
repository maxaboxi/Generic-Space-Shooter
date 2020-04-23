using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace spacerpg.Views
{
    abstract class BaseView
    {
        public ContentManager ContentManager { get; }
        public SpriteBatch SpriteBatch { get; }
        public BaseView(ContentManager contentManager, SpriteBatch spriteBatch)
        {
            ContentManager = contentManager;
            SpriteBatch = spriteBatch;
        }

        public abstract void Draw();
    }
}
