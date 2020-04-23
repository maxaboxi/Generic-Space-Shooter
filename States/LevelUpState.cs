using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using spacerpg.Controllers;
using spacerpg.FSM;
using spacerpg.General;
using spacerpg.Models;
using spacerpg.Views;

namespace spacerpg.States
{
    class LevelUpState : BaseState
    {
        private SpriteBatch _spriteBatch;
        private LevelUpView _view;
        private LevelUpController _controller;
        private LevelUpModel _levelUpModel;
        private PlayerService _playerService;

        public LevelUpState(StateMachine stateMachine, PlayerService playerService) : base(stateMachine)
        {
            _playerService = playerService;
            _levelUpModel = new LevelUpModel();
            _spriteBatch = new SpriteBatch(StateMachine.Game.GraphicsDevice);
            _view = new LevelUpView(StateMachine.Game.Content, _spriteBatch, _levelUpModel, _playerService);
            _controller = new LevelUpController(_levelUpModel, _playerService);
        }

        public override void Draw()
        {
            _spriteBatch.Begin();

            _view.Draw();

            _spriteBatch.End();
        }

        public override void Enter(params object[] args)
        {
            _levelUpModel.SkillPointsAssigned = false;
        }

        public override void Exit()
        {
        }

        public override void Update(GameTime gameTime)
        {
            _controller.Update(gameTime);

            if (_levelUpModel.SkillPointsAssigned)
            {
                StateMachine.Change("game", false);
            }
        }


    }
}
