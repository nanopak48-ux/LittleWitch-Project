using LittleWitch_Project.Sources._03_System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleWitch_Project.Sources._01_State
{
    public class StateHome : IGameState
    {
        private GameFlowManager _gameFlow;
        private ContentManager _content;
        private SpriteBatch _spriteBatch;
        private GameAudioManager _audio;

        Texture2D logo;
        private float Opacity = 0f;
        private float OpacityInc = 0.035f;
        private bool IsIntroDisplayed = false;
        private bool IsOpen = true;
        private bool IsClose = false;
        private Stopwatch _stopwatch = new Stopwatch();
        public StateHome
            (
            GameFlowManager gameFlow,
            ContentManager content,
            SpriteBatch spritebatch,
            GameAudioManager audio
            )
        {
            _gameFlow = gameFlow;
            _content = content;
            _spriteBatch = spritebatch;
            _audio = audio;

            LoadContent();
        }

        Texture2D bg;

        private void LoadContent()
        {
            string path = "texture/STATEhome/";
            logo = _content.Load<Texture2D>(path + "logo");
            bg = _content.Load<Texture2D>(path + "home_background");
        }

        public void Update(GameTime gameTime)
        {   
            if (!IsIntroDisplayed) IntroLogo();
        }

        public void InputHandler(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                _gameFlow.ChangeFlow(GameFlow.Tutorial);
            }            
        }

        public void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            
            if (!IsIntroDisplayed) _spriteBatch.Draw(logo, new Vector2(0,0), Color.White * Opacity);

            if (IsIntroDisplayed)
            {
                _spriteBatch.Draw(bg, new Vector2(0, 0), Color.White);
            }

            _spriteBatch.End();
        }

        public void AudioHandler(GameTime gameTime)
        {
            Console.WriteLine();
        }

        private void IntroLogo()
        {
            if (Opacity >= 1f && IsOpen)
            {
                IsOpen = false;
                _stopwatch.Start();
            }

            if(Opacity < 0f && IsClose)
            {
                IsIntroDisplayed = true;
            }

            if (_stopwatch.Elapsed.TotalSeconds >= 5)
            {
                _stopwatch.Stop();
                IsClose = true;
            }

            if (IsOpen) Opacity += OpacityInc;
            if (IsClose) Opacity -= OpacityInc * 0.5f;

        }
    }
}
