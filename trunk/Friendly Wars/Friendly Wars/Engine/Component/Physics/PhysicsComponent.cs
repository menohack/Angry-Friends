using System.Windows;
using Friendly_Wars.Engine.Object;
using System.Collections.Generic;
using Friendly_Wars.Engine.Utilities;

namespace Friendly_Wars.Engine.Component.Physics
{
	/// <summary>
	/// PhysicsComponent handles the collision with other GameObjects.
	/// </summary>
	public class PhysicsComponent : BaseComponent, IUpdateable
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
		/// Checks whether the owner of this PhysicsComponent is colliding with any other GameObjects.
		/// </summary>
		/// <returns> Determines whether the owner of this PhysicsComponent is colliding with any other GameObjects. </returns>
		public bool IsColliding(IList<GameObject> gameObjects)
		{
			foreach (GameObject gameObject in gameObjects)
			{
				if (BoundingBox.IsCollidingWith(gameObject))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Updates the PhysicsComponent.
		/// </summary>
		/// <param name="deltaTime">The time since the last update.</param>
		public void Update(double deltaTime)
		{
			
		}
	}
}
