using System.Windows;
using Friendly_Wars.Engine.Object;
using System.Collections.Generic;
using Friendly_Wars.Engine.Utilities;

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
		private BoundingBox boundingBox;

		/// <summary>
		/// Constructor for a new PhysicsComponent.
		/// </summary>
		/// <param name="owner"> The owner of this PhysicsComponent. </param>
		/// <param name="boundingBox">This PhysicsComponent's BoundingBox. </param>
		public PhysicsComponent(GameObject owner, BoundingBox boundingBox) : base(owner)
		{
			this.boundingBox = boundingBox;
		}

		/// <summary>
		/// Checks if the owner of this physicsComponent is colliding with any other gameObjects.
		/// </summary>
		/// <returns> Determines whether the owner of this physicsComponent is colliding with any other gameObjects. </returns>
		public bool IsColliding()
		{
			foreach (GameObject gameObject in World.Instance.GetGameObjects())
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
