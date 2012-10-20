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
        List<Texture2D> left_answers;
        List<Texture2D> right_answers;

        Plate[] AllPlates = { new Plate(220, 30, -0.5, 1.5, 30, 220, null), 
                              new Plate(265, -120, -0.5, 1.5, 30, 220, null), 
                              new Plate(315, -270, -0.5, 1.5, 30, 220, null), 
                              new Plate(370, -420, -0.5, 1.5, 30, 220, null), 

                              new Plate(540, 30, 0.2, 1.5, 30, 540, null), 
                              new Plate(520, -120, 0.2, 1.5, 30, 540, null), 
                              new Plate(500, -270, 0.2, 1.5, 30, 540, null), 
                              new Plate(480, -420, 0.2, 1.5, 30, 540, null) };

        int x_actionBox = 225;

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
            sushiSensei = this.Content.Load<Texture2D>("Images/chinaman");
            leftTable = this.Content.Load<Texture2D>("Images/leftTable");
            rightTable = this.Content.Load<Texture2D>("Images/rightTable");
            actionBox = this.Content.Load<Texture2D>("Images/actionBox");
            background = this.Content.Load<Texture2D>("Images/background");
            cursor = this.Content.Load<Texture2D>("Images/cursor");
            plate = this.Content.Load<Texture2D>("Images/plate");
            line = this.Content.Load<Texture2D>("Images/line");
            q10a = this.Content.Load<Texture2D>("Japanese/q10a");

            font = this.Content.Load<SpriteFont>("Images/SpriteFont1");
            for (int i = 0; i < AllPlates.Length; i++)
            {
                AllPlates[i].plateContents = q10a;
            }
        }

        private Texture2D q10a;

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        private bool newPress(Keys key)
        {
            if (firstFrame)
                return false;
            return lastKeys.IsKeyUp(key) && thisKeys.IsKeyDown(key);
        }

        private List<Question> allQuestions;
        private List<Question> questionsToAsk;

        // Loads a new question from the questionsToAsk onto the plates.
        private void loadPlates()
        {
            if (questionsToAsk.Count == 0)
                return;
            Question asking = questionsToAsk[questionsToAsk.Count - 1];
            questionsToAsk.RemoveAt(questionsToAsk.Count - 1);

            question = asking.question;

            // Take the correct answer and the three duds, and assign them randomly to left plates
            left_answers = new List<Texture2D>();
            left_answers.Add(asking.left_answer);
            foreach (Texture2D tex in asking.left_duds)
                left_answers.Add(tex);
            this.shuffle<Texture2D>(left_answers);
            for (int i = 0; i < 4; i++)
                AllPlates[i].plateContents = left_answers[i];

            // Take four random right answers (from all questions) and assign randomly to right plates
            right_answers = new List<Texture2D>();
            List<int> indexes = new List<int>();
            for (int i = 0; i < allQuestions.Count; i++)
                indexes.Add(i);
            this.shuffle<int>(indexes);
            for (int i = 0; i < 4; i++)
                AllPlates[i + 4].plateContents = allQuestions[indexes[i]].right_answer;
        }

        private void shuffle<T>(IList<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
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

            // for each plate, adjust it
            for (int i = 0; i < AllPlates.Length; i++)
            {
                Plate plate = AllPlates[i];
                plate.x_value += plate.x_speed;
                plate.y_value += plate.y_speed;
                if (plate.y_value >= 650)
                {
                    plate.x_value = plate.x_reset;
                    //plate.y_value = plate.y_orgin;
                    plate.y_value = plate.y_reset;
                }

                plate.in_zone = 450 <= plate.y_value && plate.y_value <= 600;
            }

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
            drawCenter(sushiSensei, new Vector2(430, 250), Color.White);

            spriteBatch.Draw(line, new Vector2(50, 400), Color.White);
            spriteBatch.Draw(line, new Vector2(550, 400), Color.White);

            for (int i = 0; i < AllPlates.Length; i++)
            {
                Color plateColor = Color.White;
                if (AllPlates[i].in_zone)
                {
                    plateColor = Color.Yellow;
                }
                spriteBatch.Draw(plate, new Vector2((int)Math.Ceiling(AllPlates[i].x_value), (int)Math.Ceiling(AllPlates[i].y_value - 100)), null, plateColor, 0f, Vector2.Zero, new Vector2((((float)AllPlates[i].y_value + 170) / (670)), (((float)AllPlates[i].y_value + 170) / (670))), SpriteEffects.None, 0f);
                spriteBatch.Draw(AllPlates[i].plateContents, new Vector2((int)Math.Ceiling(AllPlates[i].x_value), (int)Math.Ceiling(AllPlates[i].y_value - 100)), null, plateColor, 0f, Vector2.Zero, new Vector2((((float)AllPlates[i].y_value + 170) / (670)), (((float)AllPlates[i].y_value + 170) / (670))), SpriteEffects.None, 0f); 
            }

            spriteBatch.Draw(cursor, new Vector2(mouse_x, mouse_y), mouse_down ? Color.Red : Color.White);

            //spriteBatch.DrawString(font, "KONNICHIWA BITCHEZ", new Vector2(50, 50), Color.MintCream);
            spriteBatch.DrawString(font, inZone ? "true" : "false", new Vector2(400, 400), Color.Teal);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
