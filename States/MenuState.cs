using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using spacerpg.Controllers;
using spacerpg.FSM;
using spacerpg.Models;
using spacerpg.Views;

namespace spacerpg.States
{
    class MenuState : BaseState
    {
        private SpriteBatch _spriteBatch;
        private MenuView _view;
        private MenuController _controller;
        private MenuModel _model;
        private Song _menuMusic;
        public MenuState(StateMachine stateMachine) : base(stateMachine)
        {
            _model = new MenuModel();
            _spriteBatch = new SpriteBatch(StateMachine.Game.GraphicsDevice);
            _view = new MenuView(StateMachine.Game.Content, _spriteBatch, _model);
            _controller = new MenuController(StateMachine.Game, _model);
            _menuMusic = StateMachine.Game.Content.Load<Song>("mainmenu");
        }

        public override void Draw()
        {
            _spriteBatch.Begin();

            _view.Draw();

            _spriteBatch.End();
        }

        public override void Enter(params object[] args)
        {
            MediaPlayer.Play(_menuMusic);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.2f;
            _model.NewGame = false;
        }

        public override void Exit()
        {
            MediaPlayer.Stop();
        }

        public override void Update(GameTime gameTime)
        {
            _controller.Update(gameTime);

            if (_model.NewGame)
            {
                StateMachine.Change("game");
            }
        }
    }
}
