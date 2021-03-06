﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace LunarLander
{
    public class Lander
    {
        public Vector2 position { get; set; } = Vector2.Zero;
        public Vector2 velocity { get; set; } = Vector2.Zero;
        public float angle { get; set; } = 270;
        public bool engineOn { get; set; } = false;
        public float speed { get; set; } = 0.02f;
        private float speedMax = 2f;
        public Texture2D img { get; set; }
        public Texture2D imgEngine { get; set; }

        public void update()
        {
            velocity += new Vector2(0, 0.005f);

            if(Math.Abs(velocity.X) >= speedMax)
            {
                velocity = new Vector2((velocity.X < 0 ? -speedMax : speedMax), velocity.Y);
            }

            if (Math.Abs(velocity.Y) >= speedMax)
            {
                velocity = new Vector2(velocity.X, (velocity.Y < 0 ? -speedMax : speedMax));
            }

            position += velocity;
        }
    }

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Lander lander;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            lander = new Lander();
            lander.img = Content.Load<Texture2D>("ship");
            lander.imgEngine = Content.Load<Texture2D>("engine");

            lander.position = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);

        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                lander.angle += 2;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                lander.angle -= 2;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                lander.engineOn = true;

                float angleRad = MathHelper.ToRadians(lander.angle);
                float forceX = (float)Math.Cos(angleRad) * lander.speed;
                float forceY = (float)Math.Sin(angleRad) * lander.speed;
                lander.velocity += new Vector2(forceX, forceY);
            }
            else
            {
                lander.engineOn = false;
            }
            lander.update();

            if (lander.position.X < 0)
            {
                lander.position = new Vector2(GraphicsDevice.Viewport.Width, lander.position.Y);
                //lander.position = new Vector2(0, lander.position.Y);
                //lander.velocity = new Vector2(-lander.velocity.X, lander.velocity.Y);
            }

            if (lander.position.X > GraphicsDevice.Viewport.Width)
            {
                lander.position = new Vector2(GraphicsDevice.Viewport.Width, lander.position.Y);
                lander.velocity = new Vector2(-lander.velocity.X, lander.velocity.Y);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            Vector2 originImg = new Vector2(lander.img.Width / 2, lander.img.Height / 2);
            spriteBatch.Draw(lander.img, lander.position, null, null, originImg, MathHelper.ToRadians(lander.angle), null, Color.White, SpriteEffects.None, 0);

            if (lander.engineOn)
            {
                Vector2 originImgEngine = new Vector2(lander.imgEngine.Width / 2, lander.imgEngine.Height / 2);
                spriteBatch.Draw(lander.imgEngine, lander.position, null, null, originImgEngine, MathHelper.ToRadians(lander.angle), null, Color.White, SpriteEffects.None, 0);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
