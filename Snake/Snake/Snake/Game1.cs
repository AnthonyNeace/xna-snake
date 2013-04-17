// Snake By Anthony Neace
// See attached txt for audio attribution.
// Game1 handles content loading and initializes objects used throughout the game
// Game states are primarily handled here.

// See "SpriteManager.cs" for the bulk of the game logic being handled.

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
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static SpriteManager spriteManager;
        SpriteFont startFont;
        ResizeAndGrid myGameSpace;
        int updatecounter = 0;
        public static SoundEffect audioEating;
        SoundEffect audioBackground;
        public static SoundEffect gameOver;
        SoundEffectInstance backgroundInstance;

        //Game State Handler
        public enum GameState { Start, InGame, GameOver };
        public static GameState currentGameState = GameState.Start;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            this.IsMouseVisible = true;
            Window.AllowUserResizing = true;
            Window.Title = "Anthony Neace's Snake Arcade";

            myGameSpace = new ResizeAndGrid(this, 30, 30, 300, 20, 10, 22);

            Components.Add(myGameSpace);
            myGameSpace.Enabled = false;
            myGameSpace.Visible = false;

            spriteManager = new SpriteManager(this);
            Components.Add(spriteManager);
            spriteManager.Enabled = false;
            spriteManager.Visible = false;

            base.Initialize();
            
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            startFont = Content.Load<SpriteFont>(@"fonts\Start");

            //Load in Audio and set properties
            audioEating = Content.Load<SoundEffect>(@"Audio\eating");
            gameOver = Content.Load<SoundEffect>(@"Audio\gameOver");
            audioBackground = Content.Load<SoundEffect>(@"Audio\backgroundTrack");
            backgroundInstance = audioBackground.CreateInstance();
            backgroundInstance.Volume = 0.35f;
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            // Update Counter to keep snake from moving too fast.
            if (updatecounter == 6)
            {
                // Game State Manager
                // Actions performed depend on game state
                switch (currentGameState)
                {
                    case GameState.Start:
                        if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                        {
                            currentGameState = GameState.InGame;
                            // Turn on Game Components related to the InGame state
                            spriteManager.Enabled = true;
                            spriteManager.Visible = true;
                            myGameSpace.Enabled = true;
                            myGameSpace.Visible = true;
                        }
                        break;
                    case GameState.InGame:
                        if (backgroundInstance.State == SoundState.Stopped)
                        {
                            backgroundInstance.Play();
                        }
                        break;
                    case GameState.GameOver:
                        // If they are on, turn off Game Components related to the InGame state
                        if (spriteManager.Enabled)
                        {
                            spriteManager.Enabled = false;
                            spriteManager.Visible = false;
                            myGameSpace.Enabled = false;
                            myGameSpace.Visible = false;
                            gameOver.Play();
                        }
                        // Press Escape to Exit Program
                        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                            this.Exit();
                        if (backgroundInstance.State == SoundState.Playing)
                        {
                            backgroundInstance.Stop();
                        }
                        break;
                }

                // Allows the game to exit with a gamepad
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                    this.Exit();
                updatecounter = 0;
                base.Update(gameTime);
            }
            updatecounter++;
        }

        protected override void Draw(GameTime gameTime)
        {
            switch (currentGameState)
            {
                case GameState.Start:
                    GraphicsDevice.Clear(Color.Black);
                    spriteBatch.Begin();
                    spriteBatch.DrawString(startFont, "S N A K E", new Vector2(10, 10), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
                    spriteBatch.DrawString(startFont, "Press Enter to Begin", new Vector2(10, 10), Color.White, 0, new Vector2(0,-40), 1, SpriteEffects.None, 1);
                    spriteBatch.DrawString(startFont, "Scoring", new Vector2(10, 10), Color.White, 0, new Vector2(0, -80), 1, SpriteEffects.None, 1);
                    spriteBatch.DrawString(startFont, "-------", new Vector2(10, 10), Color.White, 0, new Vector2(0, -100), 1, SpriteEffects.None, 1);
                    spriteBatch.DrawString(startFont, "Apple  =  100 Points", new Vector2(10, 10), Color.White, 0, new Vector2(0, -120), 1, SpriteEffects.None, 1);
                    spriteBatch.DrawString(startFont, "Cherry =  250 Points", new Vector2(10, 10), Color.White, 0, new Vector2(0, -140), 1, SpriteEffects.None, 1);
                    spriteBatch.DrawString(startFont, "Orange =  500 Points", new Vector2(10, 10), Color.White, 0, new Vector2(0, -160), 1, SpriteEffects.None, 1);
                    spriteBatch.End();
                    break;

                case GameState.InGame:
                    GraphicsDevice.Clear(Color.CornflowerBlue);
                    // set the viewport to window bounds because the GameComponent changed it
                    GraphicsDevice.Viewport = new Viewport(new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height));

                    spriteBatch.Begin();//begin spritebatch
                    spriteBatch.DrawString(startFont, "Score: "+spriteManager.score, new Vector2(2, 2), Color.White);// draw text 
                    spriteBatch.DrawString(startFont, "Fruit: " + (spriteManager.snakeLength - 3), new Vector2(2, 20), Color.White);
                    spriteBatch.End();//end spritebatch

                    break;

                case GameState.GameOver:
                    GraphicsDevice.Clear(Color.Crimson);
                    spriteBatch.Begin();
                    spriteBatch.DrawString(startFont, "Game over!", new Vector2(10, 10), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
                    spriteBatch.DrawString(startFont, "Don't hit walls, or yourself!", new Vector2(10, 10), Color.White, 0, new Vector2(0,-20), 1, SpriteEffects.None, 1);
                    spriteBatch.DrawString(startFont, "Press Escape to exit the program.", new Vector2(10, 10), Color.White, 0, new Vector2(0, -40), 1, SpriteEffects.None, 1);
                    spriteBatch.DrawString(startFont, "Final Score: " + spriteManager.score, new Vector2(10, 10), Color.White, 0, new Vector2(0, -80), 1, SpriteEffects.None, 1);
                    spriteBatch.DrawString(startFont, "Final Fruit: " + (spriteManager.snakeLength - 3), new Vector2(10, 10), Color.White, 0, new Vector2(0, -100), 1, SpriteEffects.None, 1);
                    spriteBatch.End();
                    break;

            }

            base.Draw(gameTime);
        }
    }
}
