using LittleWitch_Project.Sources;
using LittleWitch_Project.Sources._01_State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Timers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LittleWitch_Project.Sources._02_Entity
{
    public enum EnemyName
    {
        tutorial
    }
    public class Enemy
    {
        public Vector2 Position;
        public Vector2 Velocity = new(5, 5);
        public Vector2 Size = new(200, 300);
        public Rectangle HitBox { get; private set; }

        private float SpeedUp = 1f;
        public float Speed = 300f;

        public int Health = 100;
        private bool isGrounded;

        private GamePlayLevel _currentLevel;
        private SpriteSheet spriteSheet;
        private SpriteBatch _spriteBatch;
        private ContentManager _content;
        private AnimatedSprite _enemySprite;

        public Enemy(GamePlayLevel level,SpriteBatch spriteBatch, Vector2 position, ContentManager content)
        {
            _currentLevel = level;
            _content = content;
            _spriteBatch = spriteBatch;
            Position = position;
            HitBox = new((int)Position.X, (int)Position.Y / 2, (int)Size.X, (int)Size.Y / 2);
        }
        public void Update(GameTime gameTime)
        {
            _enemySprite.Update(gameTime);

            UpdateAnim(gameTime);
        }

        private void UpdateAnim(GameTime gameTime)
        {
        }

        public void Draw()
        {
            _spriteBatch.Draw(_enemySprite, Position);
        }
        public void LoadLevelEnemyContent(ContentManager content, EnemyName enemyName)
        {
            string path = Path.Combine(content.RootDirectory, "data/dataEnemy","enemy_" + enemyName.ToString() + "_animation.json");

            string json = File.ReadAllText(path);

            EnemyAnimationData? data = JsonSerializer.Deserialize<EnemyAnimationData>(json);

            if (data == null) return;

            Texture2D texture = _content.Load<Texture2D>("texture/STATEplay/sprite_enemy_" + data.Name);
            Texture2DAtlas atlas = Texture2DAtlas.Create("Atlas/enemyAtlas/" + data.Name, texture, data.width, data.height);
            spriteSheet = new SpriteSheet("SpriteSheet/enemy/" + data.Name, atlas);

            foreach (AnimationData animation in data.Animations)
            {
                spriteSheet.DefineAnimation(
                    animation.Name,
                    builder =>
                    {
                        builder.IsLooping(animation.Loop);

                        TimeSpan frameDuration =
                            TimeSpan.FromSeconds(animation.FrameDuration);

                        foreach (int frame in animation.Frames)
                        {
                            builder.AddFrame(
                                frame,
                                frameDuration);
                        }
                    });
            }

            _enemySprite = new AnimatedSprite(spriteSheet, "Idle");
        }


    }
    public class EnemyAnimationData
    {
        [JsonPropertyName("enemy")]
        public string Name { get; set; }

        [JsonPropertyName("width")]
        public int width { get; set; }

        [JsonPropertyName("height")]
        public int height { get; set; }

        [JsonPropertyName("animations")]
        public List<AnimationData> Animations { get; set; }

    }

}

