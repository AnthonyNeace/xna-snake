// Snake By Anthony Neace
// SnakeBody is a child of Sprite
// Handles construction of a SnakeBody object

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Snake
{
    class SnakeBody : Sprite
    {

        //Vector2 currentHeading2 = new Vector2(0, 1);

        // Sprite is automated. Direction is same as speed
        public override Vector2 direction
        {
            get
            {
                return speed;
            }
        }

        public SnakeBody(Texture2D textureImage, Vector2 position, Point frameSize,
            Vector2 speed, Vector2 scale, Color color, Vector2 board_position, int entityType) :
            base(textureImage, position, frameSize, speed, scale, color, board_position, entityType)
        {

        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            base.Update(gameTime, clientBounds);
        }


    }
}
