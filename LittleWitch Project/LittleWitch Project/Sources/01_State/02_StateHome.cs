using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace LittleWitch_Project.Sources._01_State
{
    public class StateHome : IGameState
    {
        private ContentManager _content;
        private SpriteBatch _spriteBatch;

        Texture2D logo;
        private float Opacity = 0f;
        private float OpacityInc = 0.035f;
        private bool IsIntroDisplayed = false;
        private bool IsOpen = true;
        private bool IsClose = false;
        private Stopwatch _stopwatch = new Stopwatch();
        public StateHome
            (
            ContentManager content,
            SpriteBatch spritebatch
            )
        {
            _content = content;
            _spriteBatch = spritebatch;

            LoadContent();
        }

        private void LoadContent()
        {
            logo = _content.Load<Texture2D>("texture/STATEhome/logo");
        }

        public void Update(GameTime gameTime)
        {   
            if (!IsIntroDisplayed) IntroLogo();
        }

        public void InputHandler(GameTime gameTime)
        {
            Console.WriteLine();
        }

        public void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            if (!IsIntroDisplayed) _spriteBatch.Draw(logo, new Vector2(0,0), Color.White * Opacity);

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
