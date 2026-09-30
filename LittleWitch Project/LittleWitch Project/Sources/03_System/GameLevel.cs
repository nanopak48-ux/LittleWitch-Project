using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Tilemaps;
using MonoGame.Extended.Tilemaps.Rendering;
using MonoGame.Extended.ViewportAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using LittleWitch_Project.Sources._01_State;

namespace LittleWitch_Project.Sources._03_System
{
    public class GameLevel
    {
        private GamePlayLevel _currentFlow;
        private GraphicsDevice _graphic;
        private SpriteBatch _spriteBatch;
        private Tilemap _tilemap;
        private TilemapSpriteBatchRenderer _renderer;
        public GameLevel
            (
            GamePlayLevel gameFlow,
            ContentManager content,
            GraphicsDevice graphic,
            SpriteBatch spriteBatch
            )
        {
            _currentFlow = gameFlow;
            _graphic = graphic;
            _spriteBatch = spriteBatch;

            LoadContent(content);
        }

        public void LoadContent(ContentManager content)
        {
            _tilemap = content.Load<Tilemap>("level/tile/level_" + _currentFlow.ToString());

            _renderer = new TilemapSpriteBatchRenderer();

            _renderer.LoadTilemap(_tilemap);

            _spriteBatch = new SpriteBatch(_graphic);
        }

        public void Update(GameTime gameTime)
        {
            _renderer.Update(gameTime);
        }

        public void Draw(GameTime gameTime, OrthographicCamera _camera)
        {
            _graphic.Clear(Color.Black);

            _renderer.DrawLayers(_spriteBatch, _camera, "Background");

        }

    }
}


