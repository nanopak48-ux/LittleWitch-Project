using LittleWitch_Project.Sources._01_State;
using LittleWitch_Project.Sources._03_System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LittleWitch_Project.Sources
{
    public class GameLittleWitch : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GameStateManager State;
        private GameFlowManager GameFlow;
        private GameAdminDebug _adminDebug;

        public GameLittleWitch()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Window.Title = "Little Witch";

            _graphics.PreferredBackBufferHeight = GameConfig.screenHeight;
            _graphics.PreferredBackBufferWidth = GameConfig.screenWidth;

            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            State = new GameStateManager();
            _adminDebug = new GameAdminDebug();
            GameFlow = new GameFlowManager(State,_spriteBatch,Content,_graphics,Window, _adminDebug);

            GameFlow.ChangeFlow(_01_State.GameState.Home);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            State.Update(gameTime);

            KeyboardState keyboardState = Keyboard.GetState();
            State.InputHandler(keyboardState);

            _adminDebug.Update(keyboardState);

            State.AudioHandler(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            State.Draw(gameTime);

            base.Draw(gameTime);
        }

    }
}
