// Snake By Anthony Neace
// Fruit is a child of Sprite
// Handles construction of a fruit object, generation and random location placement.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Snake
{
    class Fruit : Sprite
    {

        static Random rand = new Random();

        //Vector2 currentHeading2 = new Vector2(0, 1);

        // Sprite is automated. Direction is same as speed
        public override Vector2 direction
        {
            get { return speed; }
        }

        public Fruit(Texture2D textureImage, Vector2 position, Point frameSize,
            Vector2 speed, Vector2 scale, Color color, Vector2 board_position, int entityType) :
            base(textureImage, position, frameSize, speed, scale, color, board_position, entityType)
        {

        }

        //Function: placeFruit()
        //Randomly Generate Fruit Location
        public static Vector2 placeFruit()
        {
            Vector2 fruitCoordinates = new Vector2(27,27);
            Boolean collisionCheck = false;
            // Run a while loop to check for collision against the snake; resets position if they collide.
            while (!collisionCheck)
            {

                fruitCoordinates = new Vector2((rand.Next(28))*16, (rand.Next(28))*16);
                for (int i = 0; i < Game1.spriteManager.snakeLength; i++)
                {
                    System.Console.WriteLine(i);
                    if ((fruitCoordinates.X == (Game1.spriteManager.snakeArray[i].X) * 16) && (fruitCoordinates.Y == (Game1.spriteManager.snakeArray[i].Y) * 16))
                    {
                        // continue iterating through while loop
                    }
                    else
                    {
                        //passed the collision check; space is available
                        collisionCheck = true;
                    }
                }
                


            }
            //System.Console.WriteLine(fruitCoordinates.X);
            //System.Console.WriteLine(fruitCoordinates.Y);
            return fruitCoordinates;
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {


            base.Update(gameTime, clientBounds);
        }


    }
}