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
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LittleWitch_Project.Sources._02_Entity
{
    public class Player
    {
        public Vector2 Position;
        public Vector2 Velocity = new(5, 5);

        public Vector2 Size = new(200, 300);
        public Rectangle HitBox;

        private int groundLevel = 600;
        private float SpeedUp = 1f;
        public float Speed = 300f;
        public float JumpForce = 8000f;
        public float Gravity = 500f;

        public int Health = 100;

        public enum PlayerState
        {
            Ready,
            Busy
        }
        public enum PlayerActionState
        {
            Idle,
            Walk,
            Interact,
            Jump,
            Fall
        }

        public enum InputState
        {
            W,
            A,
            S,
            D

        }

        private InputState Input { get; set; }
        public PlayerState State { get; private set; }
        public PlayerActionState ActionState { get; private set; }


        private bool isGrounded;
        private SpriteSheet spriteSheet;
        private SpriteBatch spriteBatch;

        private AnimatedSprite playerSprite;

        public Player(SpriteBatch spriteBatch, Vector2 position, ContentManager content)
        {
            this.spriteBatch = spriteBatch;
            Position = position;
            HitBox = new((int)Position.X, (int)Position.Y / 2, (int)Size.X, (int)Size.Y / 2);

            Texture2D texture = content.Load<Texture2D>("texture/STATEplay/sprite_player");
            Texture2DAtlas atlas = Texture2DAtlas.Create("Atlas/playerAtlas", texture, (int)Size.X, (int)Size.Y);
            spriteSheet = new SpriteSheet("SpriteSheet/player", atlas);
            DefineAnimation(content);

            State = PlayerState.Ready;
        }
        float deltaTime;
        public void Update(GameTime gameTime)
        {
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Console.WriteLine(Position);
            UpdateState();
            UpdateAnim(gameTime);
        }

        public void Draw()
        {
            spriteBatch.Draw(playerSprite, Position);
        }
        private void UpdateState()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.A) ||
                keyboardState.IsKeyDown(Keys.D)
                )
            {
                ActionState = PlayerActionState.Walk;
            }else ActionState = PlayerActionState.Idle;


            if (keyboardState.IsKeyDown(Keys.Space) && isGrounded)
            {
                ActionState = PlayerActionState.Jump;
            }

            if (keyboardState.IsKeyDown(Keys.LeftShift))
            {
                SpeedUp = 1.5f;
            }
            else SpeedUp = 1f;


            if (Position.Y >= groundLevel && !isGrounded)
            {
                isGrounded = true;
                ActionState = PlayerActionState.Idle;
            }

                

        }
        public void Audio(GameTime gameTime)
        {

        }
        public void InputHandler(KeyboardState keyboardState)
        {
            UpdateMovement(keyboardState);
        }
        private void UpdateAnim(GameTime gameTime)
        {
            playerSprite.Update(gameTime);
        }
        private void UpdateMovement(KeyboardState keyboardState)
        {
            if (State == PlayerState.Ready)
            {
                if (ActionState == PlayerActionState.Walk)
                {
                    if (keyboardState.IsKeyDown(Keys.A))
                    {
                        Position.X -= Velocity.X * SpeedUp;
                        if (playerSprite.CurrentAnimation != "WalkLeft") playerSprite.SetAnimation("WalkLeft");
                    }
                    if (keyboardState.IsKeyDown(Keys.D))
                    {
                        Position.X += Velocity.X * SpeedUp;
                        if (playerSprite.CurrentAnimation != "WalkRight") playerSprite.SetAnimation("WalkRight");
                    }
                }

                if (ActionState == PlayerActionState.Idle)
                {
                    if (playerSprite.CurrentAnimation != "Idle") playerSprite.SetAnimation("Idle");
                }


                if (ActionState == PlayerActionState.Jump && isGrounded)
                {
                    if (playerSprite.CurrentAnimation != "Jump") playerSprite.SetAnimation("Jump");
                    Position.Y -= MathHelper.Clamp(JumpForce, Position.Y, JumpForce+ Position.Y) * 0.016f;
                    isGrounded = false;
                }

                if (!isGrounded)
                {
                    Velocity.Y += Gravity * deltaTime;
                    Position.Y += Velocity.Y * deltaTime;

                    if (Position.Y >= groundLevel)
                    {
                        Position.Y = groundLevel;
                        Velocity.Y = 0f;
                        isGrounded = true;
                        
                    }
                }
            }
        }

        private void DefineAnimation(ContentManager content)
        {
            string path = Path.Combine(content.RootDirectory, "data/dataPlayer", "player_animation.json");

            string json = File.ReadAllText(path);

            PlayerAnimationData? data = JsonSerializer.Deserialize<PlayerAnimationData>(json);

            if (data == null) return;

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

            playerSprite = new AnimatedSprite(spriteSheet, "Idle");
        }

    }
    public class PlayerAnimationData
    {
        [JsonPropertyName("animations")]
        public List<AnimationData> Animations { get; set; }
    }

    public class AnimationData
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("loop")]
        public bool Loop { get; set; }

        [JsonPropertyName("frameDuration")]
        public float FrameDuration { get; set; }

        [JsonPropertyName("frames")]
        public List<int> Frames { get; set; }
    }

}
