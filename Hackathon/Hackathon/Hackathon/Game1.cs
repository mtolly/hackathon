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

namespace Hackathon
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D sushiSensei;
        Texture2D leftTable;
        Texture2D rightTable;
        Texture2D actionBox;
        Texture2D background;
        Texture2D cursor;
        Texture2D plate;
        SpriteFont font;

        int left_selected = 0;
        int right_selected = 0;
        string[] left_words = {};
        Texture2D[] right_pics = {};
        string question = "";

        int x_actionBox = 225;
        int plateY = -25;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
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

            // TODO: use this.Content to load your game content here
            sushiSensei = this.Content.Load<Texture2D>("Images/fumanchu");
            leftTable = this.Content.Load<Texture2D>("Images/leftTable");
            rightTable = this.Content.Load<Texture2D>("Images/rightTable");
            actionBox = this.Content.Load<Texture2D>("Images/actionBox");
            background = this.Content.Load<Texture2D>("Images/background");
            cursor = this.Content.Load<Texture2D>("Images/cursor");
            plate = this.Content.Load<Texture2D>("Images/plate");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        private bool newPress(Keys key) {
            if (firstFrame)
                return false;
            return lastKeys.IsKeyUp(key) && lastKeys.IsKeyDown(key);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            lastKeys = thisKeys;
            thisKeys = Keyboard.GetState();
            thisMouse = Mouse.GetState();

            if (thisKeys.IsKeyDown(Keys.Right))
                x_actionBox += 1;
            if (thisKeys.IsKeyDown(Keys.Left))
                x_actionBox -= 1;

            plateY += 5;
            if (plateY >= 800)
                plateY = -25;

            mouse_x = thisMouse.X;
            mouse_y = thisMouse.Y;
            mouse_down = thisMouse.LeftButton == ButtonState.Pressed;

            if (newPress(Keys.Up))
                right_selected = (right_selected - 1) % 4;
            if (newPress(Keys.Down))
                right_selected = (right_selected + 1) % 4;
            if (newPress(Keys.A))
                left_selected = (left_selected - 1) % 4;
            if (newPress(Keys.Z))
                left_selected = (left_selected + 1) % 4;

            firstFrame = false;
            base.Update(gameTime);
        }

        bool firstFrame = true;
        MouseState thisMouse;
        KeyboardState lastKeys;
        KeyboardState thisKeys;

        int mouse_x, mouse_y;
        bool mouse_down;

        private void drawCenter(Texture2D tex, Vector2 centerAt, Color color)
        {
            Vector2 centerPoint = new Vector2(tex.Width / 2, tex.Height / 2);
            spriteBatch.Draw(tex, Vector2.Add(centerAt, Vector2.Negate(centerPoint)), color);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(background, Vector2.Zero, Color.Wheat);
            drawCenter(sushiSensei, new Vector2(400, 450), Color.White);
            spriteBatch.Draw(leftTable, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(rightTable, new Vector2(600, 0), Color.White);
            spriteBatch.Draw(actionBox, new Vector2(x_actionBox, 50), Color.White);
            spriteBatch.Draw(plate, new Vector2(25, plateY), Color.Violet);
            spriteBatch.Draw(plate, new Vector2(725, plateY), Color.Thistle);
            spriteBatch.Draw(cursor, new Vector2(mouse_x, mouse_y), mouse_down ? Color.Red : Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
