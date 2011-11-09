using System.Windows;
using Friendly_Wars.Engine.Object;
using System.Collections.Generic;
using Friendly_Wars.Engine.Utilities;

namespace Friendly_Wars.Engine.Component
{
	/// <summary>
	/// CollisionComponent handles the collision with other GameObjects.
	/// </summary>
	public class CollisionComponent
	{
		/// <summary>
		/// The BoundingBox that encapsulates this GameObject.
		/// </summary>
		private BoundingBox boundingBox;
		/// <summary>
		/// Constructor for a new CollisionComponent.
		/// </summary>
		/// <param name="boundingBox">This CollisionComponent's BoundingBox. </param>
		public CollisionComponent(BoundingBox boundingBox)
		{
			this.boundingBox = boundingBox;
		}

		/// <summary>
		/// Determines if there is any collision from a linear path from the currentPosition to the desiredPosition.
		/// </summary>
		/// <param name="currentPosition">The current position.</param>
		/// <param name="desiredPosition">The position that is desired.</param>
		/// <returns>Returns desiredPosition if there is no collision.
		/// If there is collision, it returns the closest modified position, such that there is no collision.</returns>
		public Point CheckCollision(Point currentPosition, Point desiredPosition)
		{
			return new Point();
		}
	}

	/// <summary>
	/// A box-representation of the PhysicsComponent's collider.
	/// </summary>
	public class BoundingBox
	{
		/// <summary>
		/// The TransformComponent of the BoundingBox.
		/// </summary>
		private TransformComponent transformComponent;
		/// <summary>
		/// Constructor for a new BoundingBox.
		/// </summary>
		public BoundingBox(TransformComponent transformComponent)
		{
			this.transformComponent = transformComponent;
		}

		/// <summary>
		/// Checks to see if this BoundingBox is colliding with a given GameObject.
		/// </summary>
		/// <param name="gameObject"> The GameObject in question that might be colliding with this BoundingBox. </param>
		/// <returns>Determines if this BoundingBox is colliding with the given GameObject.</returns>
		public bool IsCollidingWith(GameObject gameObject)
		{
			return true;
		}
	}
}
