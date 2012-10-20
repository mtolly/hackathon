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
        Texture2D line;
        SpriteFont font;

        bool inZone = false;

        Texture2D question, left_correct, right_correct;
        LinkedList<Texture2D> left_answers;
        LinkedList<Texture2D> right_answers;

        Plate leftPlate = new Plate(210, 25, 1.5, 0.6);
        Plate rightPlate = new Plate(510, 25, 1.5, 0.6);

        int x_actionBox = 225;
        double plateY = 25;
        double plateX = 210;

        

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
            line = this.Content.Load<Texture2D>("Images/line");

            font = this.Content.Load<SpriteFont>("Images/SpriteFont1");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        private void movePlates()
        {
            LinkedListNode<Texture2D> left_first = left_answers.First;
            if (left_first != null)
            {
                left_answers.RemoveFirst();
                left_answers.AddLast(left_first);
            }

            LinkedListNode<Texture2D> right_first = right_answers.First;
            if (right_first != null)
            {
                right_answers.RemoveFirst();
                right_answers.AddLast(right_first);
            }
        }

        private bool newPress(Keys key)
        {
            if (firstFrame)
                return false;
            return lastKeys.IsKeyUp(key) && thisKeys.IsKeyDown(key);
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

            plateY += 1.5;
            plateX -= 0.6;
            if (plateY >= 800)
            {
                plateX = 210;
                plateY = 25;
            }

            if (plateY >= 350)
                inZone = true;
            else
                inZone = false;

            mouse_x = thisMouse.X;
            mouse_y = thisMouse.Y;
            mouse_down = thisMouse.LeftButton == ButtonState.Pressed;

            firstFrame = false;
            base.Update(gameTime);
        }

        private int mod(int x, int m)
        {
            return (x % m + m) % m;
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
            //spriteBatch.Draw(leftTable, new Vector2(0, 0), Color.White);
            //spriteBatch.Draw(rightTable, new Vector2(600, 0), Color.White);
            //spriteBatch.Draw(actionBox, new Vector2(x_actionBox, 50), Color.White);

            spriteBatch.Draw(line, new Vector2(50, 400), Color.White);
            spriteBatch.Draw(line, new Vector2(550, 400), Color.White);

            //spriteBatch.Draw(plate, new Vector2(plateX, plateY), Color.Violet);
            spriteBatch.Draw(plate, new Vector2((int)Math.Ceiling(plateX), (int)Math.Ceiling(plateY)), null, Color.White, 0f, Vector2.Zero, new Vector2((((float)plateY + 170)/(670)), (((float)plateY + 170)/(670))), SpriteEffects.None, 0f);
           
            spriteBatch.Draw(plate, new Vector2((int)Math.Ceiling(plateX), (int)Math.Ceiling(plateY - 100)), null, Color.White, 0f, Vector2.Zero, new Vector2((((float)plateY + 170) / (670)), (((float)plateY + 170) / (670))), SpriteEffects.None, 0f); 
            spriteBatch.Draw(plate, new Vector2((int)Math.Ceiling(plateX), (int)Math.Ceiling(plateY - 200)), null, Color.White, 0f, Vector2.Zero, new Vector2((((float)plateY + 170) / (670)), (((float)plateY + 170) / (670))), SpriteEffects.None, 0f);
            spriteBatch.Draw(plate, new Vector2((int)Math.Ceiling(plateX), (int)Math.Ceiling(plateY - 300)), null, Color.White, 0f, Vector2.Zero, new Vector2((((float)plateY + 170) / (670)), (((float)plateY + 170) / (670))), SpriteEffects.None, 0f);
            
            
            
            
            //spriteBatch.Draw(plate, new Vector2(650, plateY), Color.Thistle);
            spriteBatch.Draw(cursor, new Vector2(mouse_x, mouse_y), mouse_down ? Color.Red : Color.White);

            spriteBatch.DrawString(font, "KONNICHIWA BITCHEZ", new Vector2(50, 50), Color.MintCream);
            spriteBatch.DrawString(font, inZone ? "true" : "false", new Vector2(400, 400), Color.Teal);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
