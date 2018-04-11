using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Utilities;

namespace MonoEngine
{

    public class Game1 : Game
    {


        #region Singelton
        static Game1 instance;

            public static Game1 Instance
            {
                get { return instance; }
            }
        #endregion


        public Random rand;

        public SpriteBatch spriteBatch;

        GraphicsDeviceManager graphics;


        Color backgroundColor = Color.Black;
        SpriteFont font;


        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            instance = this;
        }


        protected override void Initialize()
        {
            rand = new Random();

            IsMouseVisible = true;

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 800;
            graphics.ApplyChanges();


            base.Initialize();

        }




        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>(@"Fonts\consolas18");

            GameManager.Start();

        }



        protected override void Update(GameTime gameTime)
        {
            Input.Begin();

            GameManager.Update(gameTime);

            Input.End();


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(backgroundColor);

            spriteBatch.Begin();

            GameManager.Draw();

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
