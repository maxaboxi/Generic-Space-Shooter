using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using spacerpg.Controllers;
using spacerpg.FSM;
using spacerpg.Models;
using spacerpg.Views;

namespace spacerpg.States
{
    class GameOverState : BaseState
    {
        private SpriteBatch _spriteBatch;
        private GameOverView _view;
        private GameOverController _controller;
        private GameOverModel _model;
        private Song _music;

        public GameOverState(StateMachine stateMachine) : base(stateMachine)
        {
            _spriteBatch = new SpriteBatch(StateMachine.Game.GraphicsDevice);
            _model = new GameOverModel();
            _view = new GameOverView(StateMachine.Game.Content, _spriteBatch, _model);
            _controller = new GameOverController(_model, stateMachine);
            _music = StateMachine.Game.Content.Load<Song>("mainmenu");
        }

        public override void Draw()
        {
            _spriteBatch.Begin();

            _view.Draw();

            _spriteBatch.End();
            
        }

        public override void Enter(params object[] args)
        {
            MediaPlayer.Play(_music);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.2f;
            _model.PlayAgain = false;
        }

        public override void Exit()
        {
            MediaPlayer.Stop();
        }

        public override void Update(GameTime gameTime)
        {
            _controller.Update(gameTime);

            if (_model.PlayAgain)
            {
                StateMachine.Change("game");
            }
        }
    }
}
