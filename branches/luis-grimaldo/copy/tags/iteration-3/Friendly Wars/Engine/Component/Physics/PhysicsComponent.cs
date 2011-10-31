using System.Windows;
using Friendly_Wars.Engine.Object;

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
		public BoundingBox BoundingBox { get; private set; }

		/// <summary>
		/// Constructor for a new PhysicsComponent.
		/// </summary>
		/// <param name="owner"> The owner of this PhysicsComponent. </param>
		public PhysicsComponent(GameObject owner, Point size, Point offset) : base(owner)
		{
			this.BoundingBox = new BoundingBox(owner, size, offset);
		}

		/// <summary>
		/// Checks whether the owner of this PhysicsComponent is colliding with any other GameObjects.
		/// </summary>
		/// <returns> Determines whether the owner of this PhysicsComponent is colliding with any other GameObjects. </returns>
		public bool IsColliding()
		{
			foreach (GameObject gameObject in World.GameObjects)
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
