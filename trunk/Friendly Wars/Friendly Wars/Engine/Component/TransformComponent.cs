using System;
using System.Windows;
using Friendly_Wars.Engine.Object;

namespace Friendly_Wars.Engine.Component
{
	/// <summary>
	/// TransformComponent handles positioning, size and rotation of a GameObject.
	/// </summary>
	public class TransformComponent : BaseComponent
	{
		/// <summary>
		/// The right direction of the game.
		/// </summary>
		private static readonly Point globalRight = new Point(1, 0);
		/// <summary>
		/// The up direction of the game.
		/// </summary>
		private static readonly Point globalUp = new Point(0, 1);

		/// <summary>
		/// The position of the TransformComponent.
		/// </summary>
		public Point position { get; set; }

		/// <summary>
		/// The size of the TransformComponent.
		/// </summary>
		public Point size { get; set; }

		/// <summary>
		/// The rotation of the TransformComponent [0, 360].
		/// </summary>
		public int rotation { get; set; }

		/// <summary>
		/// Constructor for a new instance of TransformComponent.
		/// </summary>
		/// <param name="owner">The owner of this TransformComponent.</param>
		public TransformComponent(GameObject owner, Point position, Point size) : base(owner)
		{
			this.position = position;
			this.size = size;
			rotation = 0;
		}
		
		/// <summary>
		/// Access the direction (1, 0).
		/// </summary>
		/// <returns>Returns the global-relative direction of right.</returns>
		public static Point GlobalRight()
		{
			return globalRight;
		}

		/// <summary>
		/// Access the direction (0, 1).
		/// </summary>
		/// <returns>Returns a Point (0, 1).</returns>
		public static Point GlobalUp()
		{
			return globalUp;
		}

		/// <summary>
		/// Access the direction (1, 0) with respect to this TransformComponent.
		/// </summary>
		/// <returns>Returns the Point that corresponds to the right direction of this TransformComponent.</returns>
		public Point LocalRight()
		{
			// normalizedRotation e [0, 1].  This represents the percent it is rotated.
			Double normalizedRotation = (rotation % 90)/90.0;

			// This code isn't completely right, but the idea is there.
			if (rotation > 0 && rotation <= 90)
			{
				return new Point(1, normalizedRotation);
			}
			else if (rotation > 90 && rotation <= 180)
			{
				return new Point(-1, normalizedRotation);
			}
			else if (rotation > 180 && rotation <= 270)
			{
				return new Point(-1, -normalizedRotation);
			}
			else if (rotation > 270 && rotation <= 359)
			{
				return new Point(1, -normalizedRotation);
			}
			else
			{
				return new Point(1, 0);
			}
		}

		/// <summary>
		/// Access the direction (0, 1) with respect to this TransformComponent.
		/// </summary>
		/// <returns>Returns the Point that corresponds to the up direction of this TransformComponent.</returns>
		public Point LocalUp()
		{
			return new Point();
		}

		/// <summary>
		/// Rotates by a given number of degrees.
		/// </summary>
		/// <param name="deltaAngle">The change of rotation.</param>
		public void Rotate(int deltaAngle)
		{
			rotation += deltaAngle;
		}

		/// <summary>
		/// Translates by a given point.
		/// </summary>
		/// <param name="deltaPosition">The change of position.</param>
		public void Translate(Point deltaPosition)
		{
			position = new Point(position.X + deltaPosition.X, position.Y + deltaPosition.Y);
		}

		/// <summary>
		/// Resize by a given amount.
		/// </summary>
		/// <param name="resizeAmount"></param>
		public void Resize(Point resizeFactor)
		{
			size = new Point(size.X * resizeFactor.X, size.Y * resizeFactor.Y);
		}
	}
}
