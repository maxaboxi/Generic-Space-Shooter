using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using spacerpg.Controllers;
using spacerpg.Enums;
using spacerpg.Factories;
using spacerpg.FSM;
using spacerpg.General;
using spacerpg.Interfaces;
using spacerpg.Models;
using spacerpg.Views;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace spacerpg.States
{
    class GameState : BaseState
    {
        private List<BaseView> _views = new List<BaseView>();
        private List<IController> _controllers = new List<IController>();
        private SpriteBatch _spriteBatch;
        private RenderTarget2D _screen;
        private PlayerModel _playerModel;
        private BulletListModel _bulletListModel;
        private EnemyListModel _enemyListModel;
        private BonusItemListModel _bonusItemListModel;
        private PlayerService _playerService;
        private Song _levelMusic;

        public GameState(StateMachine stateMachine, PlayerService playerService) : base(stateMachine)
        {
            _playerService = playerService;
            _levelMusic = stateMachine.Game.Content.Load<Song>("level");
        }
        public override void Draw()
        {
            StateMachine.Game.GraphicsDevice.SetRenderTarget(_screen);
            StateMachine.Game.GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();

            foreach(var view in _views)
            {
                view.Draw();
            }

            _spriteBatch.End();

            StateMachine.Game.GraphicsDevice.SetRenderTarget(null);

            _spriteBatch.Begin();

            _spriteBatch.Draw(_screen, new Rectangle(0, 0, VirtualScreenSize.Width * VirtualScreenSize.ScreenSizeMultiplier, VirtualScreenSize.Height * VirtualScreenSize.ScreenSizeMultiplier), Color.White);

            _spriteBatch.End();

        }

        public override void Enter(params object[] args)
        {
            _screen = new RenderTarget2D(StateMachine.Game.GraphicsDevice, VirtualScreenSize.Width * VirtualScreenSize.ScreenSizeMultiplier, VirtualScreenSize.Height * VirtualScreenSize.ScreenSizeMultiplier);
            _spriteBatch = new SpriteBatch(StateMachine.Game.GraphicsDevice);

            if (MediaPlayer.State != MediaState.Playing)
            {
                MediaPlayer.Play(_levelMusic);
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Volume = 0.2f;
            }         

            bool resetForNewGame = true;
            if (args.Length > 0 && args[0] is bool)
            {
                resetForNewGame = (bool)args[0];
            }

            if (resetForNewGame)
            {
                int highScore = 0;
                using (var connection = new SQLiteConnection("Data Source=genericspaceshooter.db"))
                {
                    connection.Open();
                    var command = new SQLiteCommand(connection)
                    {
                        CommandText = "SELECT Stage FROM HighScore ORDER BY Stage DESC LIMIT 1"
                    };
                    SQLiteDataReader reader = command.ExecuteReader();

                    while(reader.Read())
                    {
                        highScore = reader.GetInt32(0);
                    }
                    connection.Close();
                }

                _playerModel = new PlayerModel(1, highScore);
            }
            else
            {
                _playerModel = _playerService.GetPlayer();
                _playerModel.Stage += 1;
                _playerModel.Lives = _playerModel.Stage % 4 == 0 ? _playerModel.Lives++ : _playerModel.Lives;
                _playerModel.AddXp(50);
                _playerModel.ResetPosition(ResetMode.Level);
                if (_playerModel.Stage > _playerModel.HighScore)
                {
                    _playerModel.HighScore = _playerModel.Stage;
                }
            }

            _playerService.SetPlayer(_playerModel);

            // Create models
            _bulletListModel = new BulletListModel(new List<BulletModel>());
            _enemyListModel = new EnemyListModel(EnemyFactory.CreateStage(_playerModel.Stage));
            _bonusItemListModel = new BonusItemListModel(BonusItemFactory.CreateBonusItemsForStage(_playerModel.Stage));

            // Create and add controllers
            var playerController = new PlayerController(_playerModel);
            var enemyController = new EnemyController(_enemyListModel, _playerModel, playerController, StateMachine.Game.Content);
            var bulletListController = new BulletController(_bulletListModel, _playerModel, _enemyListModel, playerController, StateMachine.Game.Content);
            var bonusItemController = new BonusItemController(_bonusItemListModel, _playerModel, StateMachine.Game.Content);
            _controllers.Add(playerController);
            _controllers.Add(enemyController);
            _controllers.Add(bulletListController);
            _controllers.Add(bonusItemController);

            // Add views
            _views.Add(new PlayerView(StateMachine.Game.Content, _spriteBatch, _playerModel));
            _views.Add(new BulletView(StateMachine.Game.Content, _spriteBatch, _bulletListModel));
            _views.Add(new EnemyView(StateMachine.Game.Content, _spriteBatch, _enemyListModel));
            _views.Add(new StatsView(StateMachine.Game.Content, _spriteBatch, _playerModel, _enemyListModel));
            _views.Add(new BonusItemView(StateMachine.Game.Content, _spriteBatch, _bonusItemListModel));
        }

        public override void Exit()
        {
            _views.Clear();
            _controllers.Clear();
        }

        public override void Update(GameTime gameTime)
        {
            foreach(var controller in _controllers)
            {
                controller.Update(gameTime);
            }

            var enemiesAlive = _enemyListModel.Enemies.Where(enemy => !enemy.IsDead).ToList();
            if (_playerModel.Lives == 0)
            {
                using (var connection = new SQLiteConnection("Data Source=genericspaceshooter.db"))
                {
                    connection.Open();
                    var command = new SQLiteCommand(connection)
                    {
                        CommandText = @"INSERT INTO HighScore(stage) VALUES(@stage)"
                    };
                    command.Parameters.AddWithValue("@stage", _playerModel.Stage);
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                StateMachine.Change("gameover");
            }
            else if (enemiesAlive.Count == 0 && _playerModel.AvailableSkillPoints >= 1)
            {
                _playerService.SetPlayer(_playerModel);
                StateMachine.Change("levelup");
            }
            else if (enemiesAlive.Count == 0 && _playerModel.AvailableSkillPoints == 0)
            {
                StateMachine.Change("game", false);
            }
        }

    }
}
