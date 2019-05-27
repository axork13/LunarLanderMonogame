using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
            }
            else
            {
                lander.engineOn = false;
            }
            lander.update();
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
