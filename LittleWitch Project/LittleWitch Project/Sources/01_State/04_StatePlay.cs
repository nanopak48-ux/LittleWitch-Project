using LittleWitch_Project.Sources._02_Entity;
using LittleWitch_Project.Sources._03_System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleWitch_Project.Sources._01_State
{
    public enum GamePlay
    {
        tutorial
    }
    public class StatePlay : IGameState
    {
        private GameFlowManager _gameFlow;
        private GamePlay _currentGamePlay;
        private ContentManager _content;
        private SpriteBatch _spriteBatch;
        private GameAudioManager _audio;

        private Player _player;

        public StatePlay
            (
            GameFlowManager gameFlow,
            GamePlay currentGamePlay,
            ContentManager content,
            SpriteBatch spritebatch,
            GameAudioManager audio
            )
        {
            _gameFlow = gameFlow;
            _currentGamePlay = currentGamePlay;
            _content = content;
            _spriteBatch = spritebatch;
            _audio = audio;

            Initialize();
            LoadContent();
        }

        private void Initialize()
        {
            _player = new Player(_spriteBatch, new Vector2(0, 0), _content);
        }

        private void LoadContent()
        {

        }

        public void Update(GameTime gameTime)
        {
            _player.Update(gameTime);
        }
        public void InputHandler(KeyboardState keyboardState)
        {
            _player.InputHandler(keyboardState);
        }
        public void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _player.Draw();
            _spriteBatch.End();
        }
        public void AudioHandler(GameTime gameTime)
        {
            _audio.PlayBGM("bgmTutorial");

            _player.Audio(gameTime);
        }
    }
}
