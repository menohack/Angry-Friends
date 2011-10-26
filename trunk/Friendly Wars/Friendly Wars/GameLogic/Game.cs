using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using Friendly_Wars.Engine.Object;

namespace Friendly_Wars.GameLogic
{
    /// <summary>
    /// This class represents the Game. All game logic stems from here.
    /// </summary>
    public class Game
    {
        /// <summary>
        /// The world of the game.
        /// </summary>
        private World world;
        /// <summary>
        /// The terrain of the game.
        /// </summary>
        private Terrain terrain;

        /// <summary>
        /// The constructor for the Game.
        /// </summary>
        /// <param name="image"> The image used for the terrain. </param>
        public Game(Image image)
        {
            world = new World(World.WORLD_NAME);

           // terrain = new Terrain(image, "terrain");
        }
    }
}
