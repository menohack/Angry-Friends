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
		/// Checks if the owner of this CollisionComponent is colliding with any other GameObjects.
		/// </summary>
		/// <returns> Determines whether this CollisionComponent is colliding with any other gameObjects. </returns>
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

	/// <summary>
	/// A box-representation of the PhysicsComponent's collider.
	/// </summary>
	public class BoundingBox
	{
		/// <summary>
		/// The TransformComponent of the BoundingBox.
		/// </summary>
		private TransformComponent transformComponent { get; private set; }
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
