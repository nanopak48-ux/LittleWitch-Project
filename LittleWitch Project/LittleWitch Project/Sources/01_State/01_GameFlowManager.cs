using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleWitch_Project.Sources._01_State
{
    public enum GameFlow
    {
        Home
    }
    public class GameFlowManager
    {
        public SpriteBatch _spriteBatch;
        private GameStateManager _gameState;
        private ContentManager _content;
        private GraphicsDeviceManager _graphics;
        private GameWindow _window;
        public GameFlow CurrentFlow { get; private set; }

        public GameFlowManager(
            GameStateManager _stateManager,
            SpriteBatch _spritebatch,
            ContentManager _content,
            GraphicsDeviceManager _graphics,
            GameWindow _window
            )
        {
            _gameState = _stateManager;
            _spriteBatch = _spritebatch;
            this._content = _content;
            this._graphics = _graphics;
            this._window = _window;

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
            }
        }
        private void StartHomeState()
        {
            _gameState.StateSetTo(new StateHome(_content,_spriteBatch));
        }
    }
}