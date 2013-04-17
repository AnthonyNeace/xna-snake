// Snake By Anthony Neace
// See Game1 for main game loop
// See SpriteManager for the majority of game logic
// See Sprite for abstract class that all game sprites inherit from.
// See attached text file for audio attribution.

using System;

namespace Snake
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Game1 game = new Game1())
            {
                game.Run();
            }
        }
    }
#endif
}

