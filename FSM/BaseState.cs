using Microsoft.Xna.Framework;
using spacerpg.Interfaces;

namespace spacerpg.FSM
{
    abstract class BaseState : IState
    {
        public abstract void Update(GameTime gameTime);

        public abstract void Draw();

        /// <summary>
        /// Enter the state.
        /// </summary>
        /// <param name="args">The arguments to pass to the state</param>
        public abstract void Enter(params object[] args);

        public abstract void Exit();

        public StateMachine StateMachine { get; }

        public BaseState(StateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }
    }
}
