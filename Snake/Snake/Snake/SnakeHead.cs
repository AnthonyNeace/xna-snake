// Snake By Anthony Neace
// SnakeHead is a child of Sprite
// Handles construction of a SnakeHead object, as well as directional/input logic

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Snake
{
    class SnakeHead : Sprite
    {

        Vector2 currentHeading = new Vector2(0, 1);

        // Sprite is automated. Direction is same as speed
        public override Vector2 direction
        {
            get
            {

                Vector2 inputDirection = currentHeading;

                // If player pressed arrow keys, move the sprite
                // && currentHeading != new Vector2(-1, 0))
                // Go Left
                if ((Keyboard.GetState().IsKeyDown(Keys.Left) && currentHeading != new Vector2(-1, 0)) || this.entityType == 6)
                {
                    inputDirection = new Vector2(-1,0);
                }
                // Go Right
                if ((Keyboard.GetState().IsKeyDown(Keys.Right) && currentHeading != new Vector2(1, 0)) || this.entityType == 7)
                {
                    inputDirection = new Vector2(1, 0);
                }
                // Go Up
                if ((Keyboard.GetState().IsKeyDown(Keys.Up) && currentHeading != new Vector2(0, -1)) || this.entityType == 4)
                {
                    inputDirection = new Vector2(0, -1);
                }
                // Go Down
                if ((Keyboard.GetState().IsKeyDown(Keys.Down) && currentHeading != new Vector2(0, 1)) || this.entityType == 5)
                {
                    inputDirection = new Vector2(0, 1);
                }


                // If player pressed the gamepad thumbstick, move the sprite
                GamePadState gamepadState = GamePad.GetState(PlayerIndex.One);
                if (gamepadState.ThumbSticks.Left.X != 0)
                    inputDirection.X += gamepadState.ThumbSticks.Left.X;
                if (gamepadState.ThumbSticks.Left.Y != 0)
                    inputDirection.Y -= gamepadState.ThumbSticks.Left.Y;

                currentHeading = inputDirection;
                return inputDirection;
            }
        }

        public SnakeHead(Texture2D textureImage, Vector2 position, Point frameSize,
            Vector2 speed, Vector2 scale, Color color, Vector2 board_position, int entityType) :
            base(textureImage, position, frameSize, speed, scale, color, board_position, entityType)
        {

        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            // Move the sprite based on direction
            //position = new Vector2(position.X,position.Y+4);
            //position = direction;

            //System.Console.WriteLine(Game1.spriteManager.snakeArray[0].X);
            //System.Console.WriteLine(Game1.spriteManager.snakeArray[0].Y);

            // THIS UPDATES THE SNAKE BODY ELEMENTS - DO NOT FORGET TO UNCOMMENT
            // for loop here -- update snake body elements
            //for (int i = 1; i < Game1.spriteManager.snakeLength; i++)
            //{
            //    Game1.spriteManager.snakeArray[i] = Game1.spriteManager.snakeArray[i - 1];
            //}

            if(direction == new Vector2(-1,0)){
                //position = new Vector2(Game1.spriteManager.snakeHeadPosition.X - 4, Game1.spriteManager.snakeHeadPosition.Y);
                // update snake head
                Game1.spriteManager.snakeArray[0].X -= 1;
                //position = new Vector2(Game1.spriteManager.snakeHeadPosition.X - 4, Game1.spriteManager.snakeHeadPosition.Y);
                
            }
            if (direction == new Vector2(1, 0))
            {
                //position = new Vector2(Game1.spriteManager.snakeHeadPosition.X + 4, Game1.spriteManager.snakeHeadPosition.Y);
                // update snake head
                Game1.spriteManager.snakeArray[0].X += 1; 
            }
            if (direction == new Vector2(0, -1))
            {
                //position = new Vector2(Game1.spriteManager.snakeHeadPosition.X, Game1.spriteManager.snakeHeadPosition.Y - 4);
                // update snake head
                Game1.spriteManager.snakeArray[0].Y -= 1; 
            }
            if (direction == new Vector2(0, 1))
            {
                //position = new Vector2(Game1.spriteManager.snakeHeadPosition.X, Game1.spriteManager.snakeHeadPosition.Y + 4);
                // update snake head
                Game1.spriteManager.snakeArray[0].Y += 1; 
            }

            position = new Vector2(Game1.spriteManager.snakeArray[0].X * 16, Game1.spriteManager.snakeArray[0].Y * 16);

            // COMMENTED-OUT MOUSE SUPPORT
            //
            //// If player moved the mouse, move the sprite
            //MouseState currMouseState = Mouse.GetState();
            //if (currMouseState.X != prevMouseState.X ||
            //    currMouseState.Y != prevMouseState.Y)
            //{
            //    position = new Vector2(currMouseState.X, currMouseState.Y);
            //}
            //prevMouseState = currMouseState;

            // Collision detection for screen bounds
            // If sprite is off the screen, move it back within the game window
            if (position.X < 0)
            {
                position.X = 0;
                Game1.currentGameState = Game1.GameState.GameOver;
            }
            if (position.Y < 0)
            {
                position.Y = 0;
                Game1.currentGameState = Game1.GameState.GameOver;
            }
            if (position.X > clientBounds.Width - frameSize.X - 310)
            {
                position.X = clientBounds.Width - frameSize.X - 310;
                Game1.currentGameState = Game1.GameState.GameOver;
            }
            if (position.Y > clientBounds.Height - frameSize.Y - 20)
            {
                position.Y = clientBounds.Height - frameSize.Y - 20;
                Game1.currentGameState = Game1.GameState.GameOver;
            }

            // Collision detection for the snake's tail
            // Starting at 1, check against all tail pieces to ensure that they aren't in the same cell as the the head.
            // We start with 1 because the head is in the 0 position, and we don't want to check the head against the head.
            for (int i = 1; i < Game1.spriteManager.snakeLength; i++)
            {
                if (Game1.spriteManager.snakeArray[i] == Game1.spriteManager.snakeArray[0])
                {
                    Game1.currentGameState = Game1.GameState.GameOver;
                }
            }

            //Game.Window.ClientBounds.Width - rMarg - lMarg, Game.Window.ClientBounds.Height - bMarg - tMarg
            //300, 10, 10, 10

            Game1.spriteManager.snakeHeadPosition = position;

            base.Update(gameTime, clientBounds);
        }


    }
}
