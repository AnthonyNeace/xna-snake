// Snake By Anthony Neace
// ResizeAndGrid is repurposed from the Grid example class.
// Grid has been replaced with a rectangle to represent the game field
// Resizing logic has not been altered from the example

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
    class ResizeAndGrid : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public int xC, yC, lMarg, rMarg, tMarg, bMarg;
        Rectangle gridrect;

        VertexPositionColor[] vlines, hlines;
        BasicEffect be;

        public bool isVisible { get; set; }

        public ResizeAndGrid(Game game, int xCells, int yCells, int leftMargin, int rightMargin, int topMargin, int bottomMargin)
            : base(game)
        {

            xC = xCells;
            yC = yCells;

            isVisible = true; // visible by default

            lMarg = leftMargin; rMarg = rightMargin;
            tMarg = topMargin; bMarg = bottomMargin;

            createGridLines();
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            //event handler for window size 
            Game.Window.ClientSizeChanged += new EventHandler<EventArgs>(windowSizeChanged);


            base.Initialize();
            be = new BasicEffect(GraphicsDevice);
        }

        void windowSizeChanged(object sender, EventArgs e)
        {
            createGridLines();
        }


        private void createGridLines()
        {

            gridrect = new Rectangle(lMarg, tMarg, Game.Window.ClientBounds.Width - rMarg - lMarg, Game.Window.ClientBounds.Height - bMarg - tMarg);

            float cellWidth = (float)(Game.Window.ClientBounds.Width - 1) / (float)xC; //compute cell width and height
            float cellHeight = (float)(Game.Window.ClientBounds.Height - 1) / (float)yC;

            vlines = new VertexPositionColor[(xC + 1) * 2];//create vertices array for vertical lines
            hlines = new VertexPositionColor[(yC + 1) * 2];//create vertices array for horizontal lines

            for (int i = 0; i <= xC; i++) // number of columns + 1 (this is done via <= in lieu of <)
            {
                if ((i == 0) || (i == xC))
                {
                    vlines[i * 2].Position = new Vector3((float)i * cellWidth, 0, -1.0f);
                    vlines[i * 2].Color = Color.Yellow;

                    vlines[i * 2 + 1].Position = new Vector3((float)i * cellWidth, Game.Window.ClientBounds.Height, -1.0f);
                    vlines[i * 2 + 1].Color = Color.Yellow;
                }
            }

            for (int i = 0; i <= yC; i++) // number of rows + 1 (this is done via <= in lieu of <)
            {
                if ((i == 0) || (i == yC))
                {
                    hlines[i * 2].Position = new Vector3(0, (float)i * cellHeight, -1.0f);
                    hlines[i * 2].Color = Color.Yellow;
                    hlines[i * 2 + 1].Position = new Vector3(Game.Window.ClientBounds.Width, (float)i * cellHeight, -1.0f);
                    hlines[i * 2 + 1].Color = Color.Yellow;
                }
            }
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            if (isVisible)
            {
                //create effect object
                
                //create orthographic projection matrix (basically discards Z value of a vertex)
                Matrix projection = Matrix.CreateOrthographicOffCenter(0.0f, Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height, 0.0f, 0.1f, 10.0f);
                //set effect properties
                be.Projection = projection;
                be.View = Matrix.Identity;
                be.World = Matrix.Identity;
                //be.VertexColorEnabled = true;
                //change viewport to fit desired grid 
                GraphicsDevice.Viewport = new Viewport(gridrect);

                //set vertex/pixel shaders from the effect
                be.Techniques[0].Passes[0].Apply();

                //draw the lines
                GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineList, vlines, 0, xC + 1);
                GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineList, hlines, 0, yC + 1);
            }
            base.Draw(gameTime);
        }
    }
}
