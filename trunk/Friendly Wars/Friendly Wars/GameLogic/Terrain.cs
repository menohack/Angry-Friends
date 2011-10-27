using System;

using Friendly_Wars.Engine.Object;

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
