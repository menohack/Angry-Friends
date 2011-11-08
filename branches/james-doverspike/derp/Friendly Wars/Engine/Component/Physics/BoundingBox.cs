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
		/// Represents the sides of a BoundingBox.
		/// </summary>
		public enum Side {
			/// <summary>
			/// The left or west side.
			/// </summary>
			LEFT, 
			/// <summary>
			/// The right or east side.
			/// </summary>
			RIGHT
		}

		/// <summary>
		/// The size of the BoundingBox.
		/// </summary>
		private Point size;

		/// <summary>
		/// The offset of the box, with respect to the GameObject's TransformComponent.
		/// </summary>
		private Point offset;

		/// <summary>
		/// The owner of this BoundingBox.
		/// </summary>
		private GameObject owner;

		/// <summary>
		/// Constructor for a new BoundingBox.
		/// </summary>
		public BoundingBox(GameObject owner, Point size, Point offset)
		{
			this.owner = owner;
			this.size = size;
			this.offset = offset;
		}

		/// <summary>
		/// Checks to see if this BoundingBox is colliding with the specified GameObject.
		/// </summary>
		/// <param name="gameObject"> The GameObject that might be colliding with this BoundingBox's GameObject. </param>
		/// <returns>Determines if this BoundingBox is colliding with the specified GameObject.</returns>
		public bool IsCollidingWith(GameObject gameObject)
		{
			// Get all of the coordinates of the potential-colliding GameObject's BoundingBox.
			BoundingBox boundingBox = gameObject.PhysicsComponent.BoundingBox;
			Point potentialBottomLeft = GetBottom(Side.LEFT, gameObject);
			Point potentialBottomRight = GetBottom(Side.RIGHT, gameObject);
			Point potentialTopLeft = GetTop(Side.LEFT, gameObject);
			Point potentialTopRight = GetTop(Side.RIGHT, gameObject);

			// Get all of the coordinates of this BoundingBox.
			Point thisBottomLeft = GetBottom(Side.LEFT, owner);
			Point thisBottomRight = GetBottom(Side.RIGHT, owner);
			Point thisTopLeft = GetTop(Side.LEFT, owner);
			Point thisTopRight = GetTop(Side.RIGHT, owner);

			// Test X-based collision.
			if (potentialBottomLeft.X <= thisBottomLeft.X && thisBottomLeft.X <= potentialBottomRight.X)
			{
				return true;
			}
			else if (potentialTopLeft.X <= thisTopLeft.X && thisTopLeft.X <= potentialTopRight.X)
			{
				return true;
			}

			// Test Y-based collision.
			if (potentialBottomLeft.Y <= thisBottomLeft.Y && thisBottomLeft.Y <= potentialBottomRight.Y)
			{
				return true;
			}
			else if (potentialTopRight.Y <= thisTopRight.Y && thisTopRight.Y <= potentialTopLeft.Y)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Access a bottom point of a GameObject's BoundingBox.
		/// </summary>
		/// <param name="side">The bottom-side of the GameObject's BoundingBox to calculate.</param>
		/// <param name="gameObject">The GameObject whose BoundingBox needs to be analyzed.</param>
		/// <returns>The specified bottom-side of this GameObject's BoundingBox.</returns>
		public static Point GetBottom(Side side, GameObject gameObject)
		{
			BoundingBox boundingBox = gameObject.PhysicsComponent.BoundingBox;
			if (side == Side.LEFT)
			{
				return new Point(boundingBox.offset.X + gameObject.TransformComponent.Position.X, boundingBox.offset.Y + gameObject.TransformComponent.Position.Y);
			}
			else if (side == Side.RIGHT)
			{
				return new Point(boundingBox.offset.X + boundingBox.size.X + gameObject.TransformComponent.Position.X, boundingBox.offset.Y + gameObject.TransformComponent.Position.Y);
			}
			else
			{
				throw new ArgumentException("Must pass a proper type of Side.");
			}
		}

		/// <summary>
		/// Access a top point of a GameObject's BoundingBox.
		/// </summary>
		/// <param name="side">The top-side of the GameObject's BoundingBox to calculate.</param>
		/// <param name="gameObject">The GameObject whose BoundingBox needs to be analyzed.</param>
		/// <returns>The specified top-side of this GameObject's BoundingBox.</returns>
		public static Point GetTop(Side side, GameObject gameObject)
		{
			BoundingBox boundingBox = gameObject.PhysicsComponent.BoundingBox;
			if (side == Side.LEFT)
			{
				return new Point(boundingBox.offset.X + gameObject.TransformComponent.Position.X, boundingBox.offset.Y + boundingBox.size.Y + gameObject.TransformComponent.Position.Y);
			}
			else if (side == Side.RIGHT)
			{
				return new Point(boundingBox.offset.X + boundingBox.size.X + gameObject.TransformComponent.Position.X, boundingBox.offset.Y + boundingBox.size.Y + gameObject.TransformComponent.Position.Y);
			}
			else
			{
				throw new ArgumentException("Must pass a proper type of Side.");
			}
		}
	}
}
