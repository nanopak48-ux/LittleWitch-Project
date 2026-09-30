using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LittleWitch_Project.Sources._03_System;
using LittleWitch_Project.Sources._02_Entity;

namespace LittleWitch_Project.Sources._01_State
{
    public enum GameState
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
        private GameAdminDebug _adminDebug;
        private GameFont _font;
        private GamePlayLevel currentgameplay;
        public GameState CurrentState { get; private set; }

        public GameFlowManager(
            GameStateManager stateManager,
            SpriteBatch spritebatch,
            ContentManager content,
            GraphicsDeviceManager graphics,
            GameWindow window,
            GameAdminDebug adminDebug
            )
        {
            _gameState = stateManager;
            _spriteBatch = spritebatch;
            _content = content;
            _graphics = graphics;
            _window = window;
            _adminDebug = adminDebug;

            _font = new GameFont(_content);
            _audio = new GameAudioManager(_content);
            CurrentState = GameState.Home;
        }

        public void SkipState()
        {
            ChangeFlow((CurrentState + 1));
        }

        public void ChangeFlow(GameState nextFlow)
        {
            CurrentState = nextFlow;

            switch (nextFlow)
            {
                case GameState.Home:
                    StartHomeState();
                    break;
                case GameState.Tutorial:
                   
                    StartPlayState(GamePlayLevel.tutorial);
                    break;
            }
        }
        private void StartHomeState()
        {
            _gameState.StateSetTo(new StateHome(this,_content,_spriteBatch, _audio, _adminDebug,_font));
        }
        private void StartPlayState(GamePlayLevel Level)
        {
            _gameState.StateSetTo(new StatePlay(this,Level, _window, _content, _spriteBatch, _graphics, _audio, _adminDebug,_font));
        }
    }
}