using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using spacerpg.FSM;
using spacerpg.Interfaces;
using spacerpg.Models;
using System.Linq;

namespace spacerpg.Controllers
{
    class GameOverController : IController
    {
        private readonly GameOverModel _gameOverModel;
        private readonly StateMachine _stateMachine;

        public GameOverController(GameOverModel gameOverModel, StateMachine stateMachine)
        {
            _gameOverModel = gameOverModel;
            _stateMachine = stateMachine;
        }

        /// <summary>
        /// Starts a new game if player presses enter
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            var state = Keyboard.GetState();
            var pressedKeys = state.GetPressedKeys();

            if (pressedKeys.Contains(Keys.Enter)) 
            {
                _gameOverModel.PlayAgain = true;
            }

            if (pressedKeys.Contains(Keys.Back))
            {
                _stateMachine.Change("menu");
            }
        }
    }
}
