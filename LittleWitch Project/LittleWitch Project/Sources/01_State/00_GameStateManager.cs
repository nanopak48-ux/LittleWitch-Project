using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleWitch_Project.Sources._01_State
{
    public interface IGameState
    {
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);
        void InputHandler(KeyboardState keyboardState);
        void AudioHandler(GameTime gameTime);
    }
    public class GameStateManager : IGameState
    {
        private Stack<IGameState> _stateStack = new Stack<IGameState>();

        public IGameState CurrentState
        {
            get
            {
                return _stateStack.Peek();
            }
        }
        public void StatePush(IGameState newState)
        {
            _stateStack.Push(newState);
        }

        public void StateReturn()
        {
            _stateStack.Pop();
        }

        public void StateSetTo(IGameState newState)
        {
            _stateStack.Clear();
            _stateStack.Push(newState);
        }

        public void Update(GameTime gameTime)
        {
            CurrentState?.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            CurrentState?.Draw(gameTime);
        }

        public void InputHandler(KeyboardState keyboardState)
        {
            CurrentState?.InputHandler(keyboardState);
        }

        public void AudioHandler(GameTime gameTime)
        {
            CurrentState?.AudioHandler(gameTime);
        }
    }
}
