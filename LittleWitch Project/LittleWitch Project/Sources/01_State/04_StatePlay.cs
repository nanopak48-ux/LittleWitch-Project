using LittleWitch_Project.Sources._02_Entity;
using LittleWitch_Project.Sources._03_System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.ViewportAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using LittleWitch_Project.Sources;

namespace LittleWitch_Project.Sources._01_State
{
    public enum GamePlayLevel
    {
        tutorial
    }
    public class StatePlay : IGameState
    {
        private GameFlowManager _gameFlow;
        private GamePlayLevel _currentGamePlay;
        private GameWindow _window;
        private ContentManager _content;
        private SpriteBatch _spriteBatch;
        private GraphicsDeviceManager _graphics;
        private GameAudioManager _audio;
        private GameAdminDebug _adminDebug;
        private GameFont _font;
        private Player _player;

        private GameLevel _level;
        private OrthographicCamera _camera;

        private List<Enemy> _enemy;

        public StatePlay
            (
            GameFlowManager gameFlow,
            GamePlayLevel currentGamePlay,
            GameWindow window,
            ContentManager content,
            SpriteBatch spritebatch,
            GraphicsDeviceManager graphics,
            GameAudioManager audio,
            GameAdminDebug adminDebug,
            GameFont font
            )
        {
            _gameFlow = gameFlow;
            _currentGamePlay = currentGamePlay;
            _window = window;
            _content = content;
            _spriteBatch = spritebatch;
            _graphics = graphics;
            _audio = audio;
            _adminDebug = adminDebug;
            _font = font;

        Initialize();
            LoadContent();
        }

        private void Initialize()
        {
            _player = new Player(_spriteBatch, new Vector2(0, 0), _content);
            _level = new GameLevel(_currentGamePlay,_content, _graphics.GraphicsDevice , _spriteBatch);
            _enemy = new List<Enemy>();

            _enemy.Add(new Enemy(GamePlayLevel.tutorial, _spriteBatch, new Vector2(500, 500), _content));
            _enemy.ElementAt(0).LoadLevelEnemyContent(_content, EnemyName.tutorial);

            ViewportAdapter viewportAdapter = new BoxingViewportAdapter(_window, _graphics.GraphicsDevice, GameConfig.cameraWidth, GameConfig.cameraHeight);
            _camera = new OrthographicCamera(viewportAdapter);
            viewportAdapter.Reset();
        }

        private void LoadContent()
        {

        }
        public Vector2 currentCenter;
        public void Update(GameTime gameTime)
        {

            _player.Update(gameTime);
            _level.Update(gameTime);
            
            foreach (Enemy enemy in _enemy)
            {
                enemy.Update(gameTime);
            }

            Vector2 lookAtPos = _player.Position;
            currentCenter = _camera.Position + _camera.Origin;
            _camera.LookAt(Vector2.Lerp(currentCenter, lookAtPos, 0.1f));
        }
        public void InputHandler(KeyboardState keyboardState)
        {
            _player.InputHandler(keyboardState);
        }
        public void Draw(GameTime gameTime)
        {
            Matrix transformMatrix = _camera.GetViewMatrix();

            _spriteBatch.Begin(transformMatrix: transformMatrix);

            _level.Draw(gameTime, _camera);
            foreach (Enemy enemy in _enemy)
            {
                enemy.Draw();
            }
            _player.Draw();
            _spriteBatch.End();

            _spriteBatch.Begin();
            AdminDebug();
            _spriteBatch.End();
        }
        public void AudioHandler(GameTime gameTime)
        {
            _audio.PlayBGM("bgmTutorial");

            _player.Audio(gameTime);
        }

        private void AdminDebug()
        {
            if (_adminDebug.ShowDebug)
            {
                _spriteBatch.DrawString(_font.MainFont,"CurrentState " + _currentGamePlay.ToString().ToUpper(), _adminDebug.Line(1), Color.Purple, 0f, Vector2.Zero, _adminDebug.DebugSize, SpriteEffects.None, 0);
                _spriteBatch.DrawString(_font.MainFont,"Camera Position " + _camera.Position ,_adminDebug.Line(2),Color.White,0f,Vector2.Zero,_adminDebug.DebugSize, SpriteEffects.None,0);
                _spriteBatch.DrawString(_font.MainFont,"Player Position " + _player.Position ,_adminDebug.Line(3),Color.White,0f,Vector2.Zero,_adminDebug.DebugSize, SpriteEffects.None,0);
            }
            if (_adminDebug.ShowEntityStat)
            {
                _spriteBatch.DrawString(_font.MainFont, "Player Speed" + _player.Speed, new Vector2(_player.Position.X + _player.Size.X,_player.Position.Y), Color.White, 0f, Vector2.Zero, _adminDebug.DebugSize, SpriteEffects.None, 0);
            }
        }

    }
}
