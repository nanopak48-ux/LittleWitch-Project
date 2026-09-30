using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleWitch_Project.Sources._03_System
{
    public class GameFont
    {
        public BitmapFont MainFont { get; private set; }

        public GameFont(ContentManager content)
        {
            MainFont = content.Load<BitmapFont>("font/fontGenshin");

            //MainFont = new SpriteFont(temp.)

        }

    }
}
