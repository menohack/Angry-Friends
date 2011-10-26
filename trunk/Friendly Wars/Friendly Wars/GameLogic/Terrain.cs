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
using System.Windows.Media.Imaging;

using Friendly_Wars.Engine.Object;
using Friendly_Wars.Engine.Ultilities;

namespace Friendly_Wars.GameLogic
{
    /// <summary>
    /// The Terrain class represents the deformable ground.
    /// </summary>
    public class Terrain : GameObject
    {
        /// <summary>
        /// Constructor for a new instance of Terrain.
        /// </summary>
        /// <param name="image"></param>
        public Terrain(String name) : base(name)
        {
        
        }

        /// <summary>
        /// This function checks if the GameObject is colliding with the terrain.
        /// </summary>
        public void Collide()
        {
           
        }
    }
}
