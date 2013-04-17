// Snake By Anthony Neace
// SpriteManager is based on the textbook example class by the same name.
// SpriteManager handles game logic and drawing of all sprites in the program
// Game states are primarily handled here.

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



namespace Snake
{

    public class SpriteManager : Microsoft.Xna.Framework.DrawableGameComponent
    {

        // SpriteBatch for drawing
        SpriteBatch spriteBatch;

        // Game Handling for the Snake Head
        SnakeHead snakeHeadUp;
        SnakeHead snakeHeadDown;
        SnakeHead snakeHeadLeft;
        SnakeHead snakeHeadRight;
        SnakeHead snakeHeadActive;
        // The initial snake body elements
        SnakeBody initialSnakeBody;
        SnakeBody initialSnakeTail;
        //Fruit: Collected by the snake for points
        Fruit currentFruit;
        Fruit apple;
        Fruit cherry;
        Fruit orange;
        //gameArray is a 2D representation of the board... primarily used for fruit handling
        public int[,] gameArray;
        //snakeArray stores the full length of the snake as coordinates
        public Vector2[] snakeArray;
        //snakeBody stores all sprites related to the body (not the head) of the snake
        SnakeBody[] snakeBody = new SnakeBody[300];
        //snakeCopyCurrentArray is used when moving the snake from one grid location to the next
        //It copies the array data to a temporary location so that it isn't overwritten.
        Vector2[] snakeCopyCurrentArray = new Vector2[300];
        //Keeps track of snake length for loop logic and scoring.
        public int snakeLength = 3;
        public Vector2 snakeHeadPosition = new Vector2(0, 32);
        public int score = 0;

        //Random Class Variable
        static Random rand = new Random();

        //SpriteList for generic sprites
        List<Sprite> spriteList = new List<Sprite>();

        //SpriteManager Constructor
        public SpriteManager(Game game)
            : base(game)
        {  }

        // Function: Initialize
        // Building the initial gameboard and snake is handled here.
        public override void Initialize()
        {
            gameArray = new int[30,30];
            snakeArray = new Vector2[300];
            // Initial Snake Position in Game Array
            gameArray[0, 0] = 9;
            gameArray[0, 1] = 8;
            gameArray[0, 2] = 5;
            // Initial Snake Length (3 head, 2 body, 1 tail, 0 void)
            // 1st param: row 
            // 2nd param: col
            snakeArray[0] = new Vector2(0, 2);
            snakeArray[1] = new Vector2(0, 1);
            snakeArray[2] = new Vector2(0, 0);
            base.Initialize();
        }

        //Function: LoadContent
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            //Get the actual sprites related to the snake and fruit
            //Set appropriate game logic objects to appropriate snake body parts
            //Note: The "tail" is never formally kept track of, it was just a convienent
            //name for the second body part of the initial snake.
            snakeAssets();
            snakeHeadActive = snakeHeadDown;
            snakeBody[0] = initialSnakeBody;
            snakeBody[1] = initialSnakeTail;
            chooseFruit();
            base.LoadContent();
        }

        // Function: Choose Fruit
        // Selects Fruit Type and loads position
        public void chooseFruit()
        {
            // Reload and randomize fruit positions
            fruitAssets();
            // Load initial fruit and helpful variables
            int initialFruit = 1;
            int itX, itY;
            //  Select Fruit:
            //  0 = apple
            //  1 = cherry
            //  2 = orange
            initialFruit = rand.Next(3);
            // Translate sprite/screen position to grid position to be handled
            // for purposes of game logic
            switch (initialFruit)
            {
                case (0):
                    currentFruit = apple;
                    itX = ((int)currentFruit.position.X) / 16;
                    itY = ((int)currentFruit.position.Y) / 16;
                    gameArray[itX, itY] = 1;
                    break;
                case (1):
                    currentFruit = cherry;
                    itX = ((int)currentFruit.position.X) / 16;
                    itY = ((int)currentFruit.position.Y) / 16;
                    gameArray[itX, itY] = 2;
                    break;
                case (2):
                    currentFruit = orange;
                    itX = ((int)currentFruit.position.X) / 16;
                    itY = ((int)currentFruit.position.Y) / 16;
                    gameArray[itX, itY] = 3;
                    break;
            }
        }

        // Load fruit assets
        public void fruitAssets()
        {
            apple = new Fruit(Game.Content.Load<Texture2D>(@"images\apple"), Fruit.placeFruit(), new Point(16, 16), new Vector2(4f, 4f), new Vector2(1, 1), Color.CornflowerBlue, new Vector2(0, 0), 0);
            cherry = new Fruit(Game.Content.Load<Texture2D>(@"images\cherry"), Fruit.placeFruit(), new Point(16, 16), new Vector2(4f, 4f), new Vector2(1, 1), Color.CornflowerBlue, new Vector2(0, 0), 1);
            orange = new Fruit(Game.Content.Load<Texture2D>(@"images\orange"), Fruit.placeFruit(), new Point(16, 16), new Vector2(4f, 4f), new Vector2(1, 1), Color.CornflowerBlue, new Vector2(0, 0), 2);
        }

        // Load snake head assets
        public void snakeAssets()
        {
            snakeHeadUp = new SnakeHead(Game.Content.Load<Texture2D>(@"images\snakehead-up"),
                snakeHeadPosition, new Point(16, 16), new Vector2(4f, 4f), new Vector2(1, 1), Color.CornflowerBlue, new Vector2(0, 0), 4);

            snakeHeadDown = new SnakeHead(Game.Content.Load<Texture2D>(@"images\snakehead-down"),
                snakeHeadPosition, new Point(16, 16), new Vector2(4f, 4f), new Vector2(1, 1), Color.CornflowerBlue, new Vector2(0, 0), 5);

            snakeHeadLeft = new SnakeHead(Game.Content.Load<Texture2D>(@"images\snakehead-left"),
                snakeHeadPosition, new Point(16, 16), new Vector2(4f, 4f), new Vector2(1, 1), Color.CornflowerBlue, new Vector2(0, 0), 6);

            snakeHeadRight = new SnakeHead(Game.Content.Load<Texture2D>(@"images\snakehead-right"),
                snakeHeadPosition, new Point(16, 16), new Vector2(4f, 4f), new Vector2(1, 1), Color.CornflowerBlue, new Vector2(0, 0), 7);

            initialSnakeBody = new SnakeBody(Game.Content.Load<Texture2D>(@"images\snakebody"),
                new Vector2(0,16), new Point(16, 16), new Vector2(4f, 4f), new Vector2(1, 1), Color.CornflowerBlue, new Vector2(0, 0), 8);

            initialSnakeTail = new SnakeBody(Game.Content.Load<Texture2D>(@"images\snakebody"),
                new Vector2(0,0), new Point(16, 16), new Vector2(4f, 4f), new Vector2(1, 1), Color.CornflowerBlue, new Vector2(0, 0), 8);
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left) && snakeHeadActive.entityType != 7 && snakeHeadActive.textureImage != Game.Content.Load<Texture2D>(@"images\snakehead-right"))
            {
                snakeHeadActive = snakeHeadLeft;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right) && snakeHeadActive.entityType != 6 && snakeHeadActive.textureImage != Game.Content.Load<Texture2D>(@"images\snakehead-left"))
            {
                snakeHeadActive = snakeHeadRight;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && snakeHeadActive.entityType != 5 && snakeHeadActive.textureImage != Game.Content.Load<Texture2D>(@"images\snakehead-down"))
            {
                snakeHeadActive = snakeHeadUp;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down) && snakeHeadActive.entityType != 4 && snakeHeadActive.textureImage != Game.Content.Load<Texture2D>(@"images\snakehead-up"))
            {
                snakeHeadActive = snakeHeadDown;
            }
            snakeHeadActive.Update(gameTime, Game.Window.ClientBounds);

            // Update Snake Body
            for (int i = 0; i < snakeLength - 1; i++)
            {
                snakeBody[i].Update(gameTime, Game.Window.ClientBounds);
                snakeBody[i].position = new Vector2(Game1.spriteManager.snakeArray[i + 1].X * 16.0f, Game1.spriteManager.snakeArray[i + 1].Y * 16.0f);
            }

            // Copy the present location of the snake into a temporary location for reading
            for (int i = 0; i < snakeLength; i++)
            {
                snakeCopyCurrentArray[i] = snakeArray[i];
            }

            // Overwrite the current snake location with the new snake location
            for (int i = 0; i < snakeLength-1; i++)
            {
                snakeArray[i + 1] = snakeCopyCurrentArray[i];
            }

            //------
            //The commented code below is a step-by-step example of how this process
            //updates the snake, using only the initial snake pieces.
            //snakeCopyCurrentArray[0] = snakeArray[0];
            //snakeCopyCurrentArray[1] = snakeArray[1];
            //snakeCopyCurrentArray[2] = snakeArray[2];

            //snakeArray[1] = snakeCopyCurrentArray[0];
            //snakeArray[2] = snakeCopyCurrentArray[1];
            //------
            // Update Fruit
            currentFruit.Update(gameTime, Game.Window.ClientBounds);

            // Check for fruit collision
            // Update Score, Update Fruit Location
            if (new Vector2((snakeHeadActive.position.X) / 16,(snakeHeadActive.position.Y) / 16) == (new Vector2((int)(currentFruit.position.X) / 16, (int)(currentFruit.position.Y) / 16)))
            {
                Game1.audioEating.Play();
                System.Console.WriteLine("Fruit collected!");
                //Update Score
                switch (currentFruit.entityType)
                {
                    case(0):
                        score += 100;
                        break;
                    case(1):
                        score += 250;
                        break;
                    case(2):
                        score += 500;
                        break;
                }


                // Add Snake Bit
                snakeLength++;
                snakeArray[snakeLength - 1] = snakeArray[snakeLength - 2];
                snakeBody[snakeLength - 2] = new SnakeBody(Game.Content.Load<Texture2D>(@"images\snakebody"),
                       new Vector2(snakeArray[snakeLength - 1].X * 16, snakeArray[snakeLength - 1].Y * 16), new Point(16, 16), new Vector2(4f, 4f), new Vector2(1, 1), Color.CornflowerBlue, new Vector2(0, 0), 8);

                // Replace fruit location with snake's head
                if(snakeHeadActive.textureImage == Game.Content.Load<Texture2D>(@"images\snakehead-right")){
                    snakeHeadActive = new SnakeHead(Game.Content.Load<Texture2D>(@"images\snakehead-right"),
                        new Vector2(snakeHeadPosition.X, snakeHeadPosition.Y), 
                        new Point(16, 16), new Vector2(4f, 4f), new Vector2(1, 1), Color.CornflowerBlue, new Vector2(0, 0), 7);                
                }
                if(snakeHeadActive.textureImage == Game.Content.Load<Texture2D>(@"images\snakehead-left")){
                    snakeHeadActive = new SnakeHead(Game.Content.Load<Texture2D>(@"images\snakehead-left"),
                        new Vector2(snakeHeadPosition.X, snakeHeadPosition.Y), 
                        new Point(16, 16), new Vector2(4f, 4f), new Vector2(1, 1), Color.CornflowerBlue, new Vector2(0, 0), 6);                  
                }
                if(snakeHeadActive.textureImage == Game.Content.Load<Texture2D>(@"images\snakehead-up")){
                    snakeHeadActive = new SnakeHead(Game.Content.Load<Texture2D>(@"images\snakehead-up"),
                        new Vector2(snakeHeadPosition.X, snakeHeadPosition.Y), 
                        new Point(16, 16), new Vector2(4f, 4f), new Vector2(1, 1), Color.CornflowerBlue, new Vector2(0, 0), 4);                  
                }
                if(snakeHeadActive.textureImage == Game.Content.Load<Texture2D>(@"images\snakehead-down")){
                    snakeHeadActive = new SnakeHead(Game.Content.Load<Texture2D>(@"images\snakehead-down"),
                        new Vector2(snakeHeadPosition.X, snakeHeadPosition.Y), 
                        new Point(16, 16), new Vector2(4f, 4f), new Vector2(1, 1), Color.CornflowerBlue, new Vector2(0, 0), 5);                  
                }
                // Replace fruit location with snake's current head
                snakeArray[0] = (new Vector2((int)(currentFruit.position.X) / 16, (int)(currentFruit.position.Y) / 16));
                // Regenerate Fruit
                chooseFruit();
            }   
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            //Draw Snake Head
            snakeHeadActive.Draw(gameTime, spriteBatch);
            //Draw Snake's Torso
            for (int j = 0; j < snakeLength - 1; j++)
            {
                snakeBody[j].Draw(gameTime, spriteBatch);
            }
            //draw fruit
            currentFruit.Draw(gameTime, spriteBatch);
            spriteBatch.End();
          
            base.Draw(gameTime);
        }
    }
}
