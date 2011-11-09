using System;

using Friendly_Wars.Engine.Object;
using Friendly_Wars.Engine.Component;
using System.Windows;
using Friendly_Wars.Engine.Component.Graphic;
using System.Windows.Controls;

namespace Friendly_Wars.GameLogic
{
    /// <summary>
    /// The Terrain class represents the deformable ground with a bitmap (Image). Completely transparent pixel values represent empty space.
    /// </summary>
    public class Terrain : GameObject
    {

		//We should accelerate collision detection with a quadtree. The bounding
		//box is only used to check if the object is outside the screen.
		//public QuadTree quadTree;

        /// <summary>
        /// Constructor for a new instance of Terrain with a deformable image.
        /// </summary>
        /// <param name="image">The bitmap image that represents the terrain.</param>
		/// <param name="name">The name of the terrain.</param>
        public Terrain(Image image, String name) : base(name)
        {
			Frame map = new Frame(image, new Point(0.0, 0.0));

			Animation animation = new Animation(map, "Level 1");

			//Set the RenderComponent
			AddAnimation(animation);

			//Set the TransformComponent
			TransformComponent.Position = new Point(0.0, 0.0);
			TransformComponent.Size = new Point(RenderComponent.CanvasWidth, RenderComponent.CanvasHeight);

			//Sound disabled for now.
			AudioComponent.Disable();

			Play("Level 1");
        }

        /// <summary>
        /// This function checks if the GameObject is colliding with the terrain.
        /// </summary>
        public void Collide()
        {
           
        }
    }
}
