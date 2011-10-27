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
		/// The right direction of the game, the global-relative direction of right.
		/// </summary>
		private static readonly Point GLOBAL_RIGHT = new Point(1, 0);
		/// <summary>
		/// The up direction of the game, the global-relative direction of up.
		/// </summary>
		private static readonly Point GLOBAL_UP = new Point(0, 1);

		/// <summary>
		/// The position of the TransformComponent.
		/// </summary>
		public Point Position { get; set; }

		/// <summary>
		/// The size of the TransformComponent.
		/// </summary>
		public Point Size { get; set; }

		/// <summary>
		/// The rotation of the TransformComponent [0, 360].
		/// </summary>
		public int Rotation { get; set; }

		/// <summary>
		/// Constructor for a new instance of TransformComponent.
		/// </summary>
		/// <param name="owner">The owner of this TransformComponent.</param>
		public TransformComponent(GameObject owner, Point position, Point size) : base(owner)
		{
			this.Position = position;
			this.Size = size;
			this.Rotation = 0;
		}

		/// <summary>
		/// Access the direction (1, 0) with respect to this TransformComponent.
		/// </summary>
		/// <returns>Returns the Point that corresponds to the right direction of this TransformComponent.</returns>
		public Point LocalRight()
		{
			// normalizedRotation e [0, 1].  This represents the percent it is rotated.
			Double normalizedRotation = (Rotation % 90)/90.0;

			// This code isn't completely right, but the idea is there.
			if (Rotation > 0 && Rotation <= 90)
			{
				return new Point(1, normalizedRotation);
			}
			else if (Rotation > 90 && Rotation <= 180)
			{
				return new Point(-1, normalizedRotation);
			}
			else if (Rotation > 180 && Rotation <= 270)
			{
				return new Point(-1, -normalizedRotation);
			}
			else if (Rotation > 270 && Rotation <= 359)
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
			Rotation += deltaAngle;
		}

		/// <summary>
		/// Translates by a given point.
		/// </summary>
		/// <param name="deltaPosition">The change of position.</param>
		public void Translate(Point deltaPosition)
		{
			Position = new Point(Position.X + deltaPosition.X, Position.Y + deltaPosition.Y);
		}

		/// <summary>
		/// Resize by a given amount.
		/// </summary>
		/// <param name="resizeAmount"></param>
		public void Resize(Point resizeFactor)
		{
			Size = new Point(Size.X * resizeFactor.X, Size.Y * resizeFactor.Y);
		}
	}
}
