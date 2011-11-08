using System.Windows;
using Friendly_Wars.Engine.Object;
using System.Collections.Generic;
using Friendly_Wars.Engine.Utilities;

namespace Friendly_Wars.Engine.Component.Physics
{
	/// <summary>
	/// PhysicsComponent handles the collision with other gameObjects.
	/// </summary>
	public class PhysicsComponent : BaseComponent
	{
		/// <summary>
		/// The BoundingBox that encapsulates this GameObject.
		/// </summary>
		public BoundingBox BoundingBox { get; private set; }

		/// <summary>
		/// Constructor for a new PhysicsComponent.
		/// </summary>
		/// <param name="owner"> The owner of this PhysicsComponent. </param>
		/// <param name="size">The size of the BoundingBox.</param>
		/// <param name="offset">The offset of the BoundingBox.</param>
		public PhysicsComponent(GameObject owner, Point size, Point offset) : base(owner)
		{
			this.BoundingBox = new BoundingBox(owner, size, offset);
		}

		/// <summary>
		/// Checks if the owner of this PhysicsComponent is colliding with any other gameObjects.
		/// </summary>
		/// <returns> Determines whether the owner of this PhysicsComponent is colliding with any other gameObjects. </returns>
		public bool IsColliding()
		{
			foreach (GameObject gameObject in World.Instance.GetGameObjects())
			{
				if (BoundingBox.IsCollidingWith(gameObject))
				{
					return true;
				}
			}
			return false;
		}
	}
}
