using Microsoft.Xna.Framework;
using spacerpg.Interfaces;
using System.Collections.Generic;

namespace spacerpg.FSM
{
    class StateMachine
    {
        private readonly Dictionary<string, IState> _states = new Dictionary<string, IState>();
        private IState _currentState;

        public Game Game { get; }

        public StateMachine(Game game)
        {
            Game = game;
        }

        public void Add(string stateName, IState state)
        {
            _states[stateName] = state;
        }

        /// <summary>
        /// Change to a given state
        /// </summary>
        /// <param name="stateName">Name of the state to be changed to</param>
        /// <param name="args">The arguments for the state change</param>
        public void Change(string stateName, params object[] args)
        {
            if (!_states.ContainsKey(stateName))
            {
                throw new KeyNotFoundException($"{stateName} is not a valid state.");
            }

            if (_currentState != null)
            {
                _currentState.Exit();
            }


            _currentState = _states[stateName];
            _currentState.Enter(args);
        }

        public void Draw()
        {
            if (_currentState != null)
            {
                _currentState.Draw();
            }
        }

        public void Update(GameTime gameTime)
        {
            if (_currentState != null)
            {
                _currentState.Update(gameTime);
            }
        }
    }
}
