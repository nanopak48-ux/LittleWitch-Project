using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LittleWitch_Project.Sources._03_System
{
    public class GameAdminDebug
    {
        private int lineSpace = 20;
        public float DebugSize = 0.5f;
        public bool ShowDebug { get; private set; }
        public bool ShowEntityStat { get; private set; }
        public bool ShowMonsterStatus { get; private set; }

        public GameAdminDebug()
        {
            previousKeyboardState = new KeyboardState();
        }
        private KeyboardState previousKeyboardState;
        public void Update(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.F1) && previousKeyboardState.IsKeyUp(Keys.F1))
                ShowDebug = !ShowDebug;

            if (keyboardState.IsKeyDown(Keys.F2)&& previousKeyboardState.IsKeyUp(Keys.F2))
                ShowEntityStat = !ShowEntityStat;

            if (keyboardState.IsKeyDown(Keys.F3)&& previousKeyboardState.IsKeyUp(Keys.F3))
                ShowMonsterStatus = !ShowMonsterStatus;

            previousKeyboardState = keyboardState;
        }

        public Vector2 Line(int line)
        {
            float PosX = 20;
            float PosY = ((line * lineSpace) + DebugSize);

            Vector2 DebugAt = new Vector2(PosX,PosY);

            return DebugAt;
        }
    }
}
