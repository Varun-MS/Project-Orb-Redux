using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SideScroller
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Screen Resolution
        const int GAME_WINDOW_WIDTH = 800;
        const int GAME_WINDOW_HEIGHT = 600;
        
        // Initializations for the Platformer Background
        Texture2D backgroundTexture;
        Rectangle backgroundRectangle;

        //Player Initializations
        Player playerOne;
        Vector2 playerOnePosition = new Vector2(400, 500);

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //Resizes Game Window 
            graphics.PreferredBackBufferWidth = GAME_WINDOW_WIDTH;
            graphics.PreferredBackBufferHeight = GAME_WINDOW_HEIGHT;

            //Keeps Mouse Visible
            this.IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Loads the Texture and Rectangle for the Platformer Background
            backgroundTexture = Content.Load<Texture2D>("bg_castle");
            backgroundRectangle = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            // Loads Player One Data
            playerOne = new Player(Content, playerOnePosition);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // Updates Charactor Position Based on KeyBoard Input
            playerOne.Update(Keyboard.GetState(), gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Draws the Platformer Background
            spriteBatch.Begin();

            spriteBatch.Draw(backgroundTexture, backgroundRectangle, Color.White);
            playerOne.Draw(spriteBatch);
            
            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
