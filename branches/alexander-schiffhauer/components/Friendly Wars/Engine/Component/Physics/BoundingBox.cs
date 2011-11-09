using System;
using System.Windows;
using Friendly_Wars.Engine.Object;

namespace Friendly_Wars.Engine.Component.Physics
{
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
