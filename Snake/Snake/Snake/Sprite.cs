// Snake By Anthony Neace
// Sprite is based on the textbook example class by the same name.
// Sprite is an abstract class -- SnakeHead, SnakeBody, and Fruit inherit from here.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake
{
    abstract class Sprite
    {

        // Class members
        public Texture2D textureImage;
        protected Point frameSize;
        Point currentFrame;
        Point sheetSize;
        int timeSinceLastFrame = 0;
        int millisecondsPerFrame = 0;
        Vector2 scale = new Vector2(1,1);
        const int defaultMillisecondsPerFrame = 16;
        Color color = Color.CornflowerBlue;
        protected Vector2 speed;
        public Vector2 position;
        public int entityType;

        //Grid Entity Items (Snake, Fruit, Fruit) Constructor
        public Sprite(Texture2D textureImage, Vector2 window_position, Point frameSize,
            Vector2 speed, Vector2 scale, Color color, Vector2 board_position, int entityType)        
        {
            this.textureImage = textureImage;
            this.position = window_position;
            this.frameSize = frameSize;
            this.currentFrame = new Point(0,0);
            sheetSize = new Point(1,1);
            this.speed = speed;
            this.entityType = entityType;
        }


        // Abstract definition of direction property
        public abstract Vector2 direction
        {
            get;
        }

        public virtual void Update(GameTime gameTime, Rectangle clientBounds)
        {

            // Update frame if time to do so based on framerate
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                // Increment to next frame
                timeSinceLastFrame = 0;
                ++currentFrame.X;
                if (currentFrame.X >= sheetSize.X)
                {
                    currentFrame.X = 0;
                    ++currentFrame.Y;
                    if (currentFrame.Y >= sheetSize.Y)
                        currentFrame.Y = 0;
                }
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureImage,
                position,
                new Rectangle(currentFrame.X * frameSize.X,
                    currentFrame.Y * frameSize.Y,
                    frameSize.X, frameSize.Y),
                color, 0, Vector2.Zero,
                scale, SpriteEffects.None, 0);
        }
    }
}
