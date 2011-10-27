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
using System.Collections;

namespace Friendly_Wars.Engine.Component.Physics
{
	/// <summary>
	/// PhysicsComponent handles the collision with other GameObjects.
	/// </summary>
	public class PhysicsComponent : BaseComponent
	{
		/// <summary>
		/// The BoundingBox that encapsulates this GameObject.
		/// </summary>
		public BoundingBox boundingBox { get; private set; }

		/// <summary>
		/// Constructor for a new PhysicsComponent.
		/// </summary>
		/// <param name="owner"> The owner of this PhysicsComponent. </param>
		public PhysicsComponent(GameObject owner, Point size, Point offset) : base(owner)
		{
			boundingBox = new BoundingBox(owner, size, offset);
		}

		/// <summary>
		/// Checks whether the owner of this PhysicsComponent is colliding with any other GameObjects.
		/// </summary>
		/// <returns> Determines whether the owner of this PhysicsComponent is colliding with any other GameObjects. </returns>
		public bool IsColliding()
		{
			foreach (GameObject gameObject in World.gameObjects)
			{
				if (boundingBox.IsCollidingWith(gameObject))
				{
					return true;
				}
			}
			return false;
		}
	}
}
