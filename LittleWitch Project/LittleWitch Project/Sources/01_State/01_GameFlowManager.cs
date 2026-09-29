using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LittleWitch_Project.Sources._03_System;

namespace LittleWitch_Project.Sources._01_State
{
    public enum GameFlow
    {
        Home,
        Tutorial
    }
    public class GameFlowManager
    {
        public SpriteBatch _spriteBatch;
        private GameStateManager _gameState;
        private ContentManager _content;
        private GraphicsDeviceManager _graphics;
        private GameWindow _window;
        private GameAudioManager _audio;
        private GamePlay currentgameplay;
        public GameFlow CurrentFlow { get; private set; }

        public GameFlowManager(
            GameStateManager stateManager,
            SpriteBatch spritebatch,
            ContentManager content,
            GraphicsDeviceManager graphics,
            GameWindow window
            )
        {
            _gameState = stateManager;
            _spriteBatch = spritebatch;
            _content = content;
            _graphics = graphics;
            _window = window;

            _audio = new GameAudioManager(_content);
            CurrentFlow = GameFlow.Home;
        }

        public void SkipState()
        {
            ChangeFlow((CurrentFlow + 1));
        }

        public void ChangeFlow(GameFlow nextFlow)
        {
            CurrentFlow = nextFlow;

            switch (nextFlow)
            {
                case GameFlow.Home:
                    StartHomeState();
                    break;
                case GameFlow.Tutorial:
                    StartPlayState(GamePlay.tutorial);
                    break;
            }
        }
        private void StartHomeState()
        {
            _gameState.StateSetTo(new StateHome(this,_content,_spriteBatch, _audio));
        }
        private void StartPlayState(GamePlay Level)
        {
            _gameState.StateSetTo(new StatePlay(this,Level,_content, _spriteBatch, _audio));
        }
    }
}