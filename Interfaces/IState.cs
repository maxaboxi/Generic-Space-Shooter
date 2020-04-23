using Microsoft.Xna.Framework;

namespace spacerpg.Interfaces
{
    interface IState
    {
        void Update(GameTime gameTime);

        void Draw();

        void Enter(params object[] args);

        void Exit();
    }
}
