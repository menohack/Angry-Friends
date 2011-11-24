using System;
using System.Windows;
using Library.Engine.Object;
using Library.Engine.Utilities;
namespace Library.Engine.Component {
	/// <summary>
	/// Handles positioning, size and rotation of an Object.
	/// </summary>
	public class TransformComponent : BaseComponent {
		/// <summary>
		/// The mimimum angle of rotation.
		/// </summary>
		private static readonly int MIMIMUM_ROTATION_ANGLE = 0;
		/// <summary>
		/// The maximum angle of rotation.
		/// </summary>
		private static readonly int MAXIMUM_ROTATION_ANGLE = 360;
		/// <summary>
		/// The position of this TransformComponent.
		/// </summary>
		private Point position;
		/// <summary>
		/// The rotation of this TransformComponent.
		/// </summary>
		private int rotation;
		/// <summary>
		/// The size of this TransformComponent.
		/// </summary>
		private Point size;
		/// <summary>
		/// The velocity of this TransformComponent.
		/// </summary>
		public Point Velocity { get; set; }
		/// <summary>
		/// The accessor for the position of the TransformComponent.
		/// </summary>
		public Point Position {
			get {
				return position;
			}
			set {
				position = CollisionDetection(new Point(value.X, value.Y));
			}
		}
		/// <summary>
		/// The rotation of this TransformComponent, clamped in degrees: [0, 360].
		/// </summary>
		public int Rotation {
			get {
				return rotation;
			}
			set {
				rotation = value;
				position = CollisionDetection(position);
			}
		}
		/// <summary>
		/// The size of this TransformComponent.
		/// </summary>
		public Point Size {
			get {
				return size;
			}
			set {
				size = value;
				position = CollisionDetection(position);
			}
		}

		/// <summary>
		/// Constructor for a new instance of TransformComponent.
		/// </summary>
		/// <param name="position">The initial position of this TransformComponent.</param>
		/// <param name="rotation">The initial rotation of this TransformComponent.</param>
		/// <param name="size">The initial size of this TransformComponent.</param>
		/// <param name="owner">The GameObject that owns this TransformComponent.</param>
		public TransformComponent(Point position, int rotation, Point size, GameObject owner)
			: base(owner) {
			this.Size = size;
			//This is intentional
			this.position = position;
			this.Position = position;
			this.Rotation = rotation;
			this.Velocity = new Point(0, 0);
		}

		/// <summary>
		/// Translates by a given point.
		/// </summary>
		/// <param name="deltaPosition">The change of position.</param>
		public void Translate(Point deltaPosition) {
			position = CollisionDetection(new Point(position.X + deltaPosition.X, position.Y + deltaPosition.Y));
		}
		/// <summary>
		/// Rotates by a given number of degrees.
		/// </summary>
		/// <param name="deltaRotation">The change of rotation.</param>
		public void Rotate(int deltaRotation) {
			EngineMath.Clamp(Rotation += deltaRotation, MIMIMUM_ROTATION_ANGLE, MAXIMUM_ROTATION_ANGLE);
		}
		/// <summary>
		/// Resizes by a given factor.
		/// </summary>
		/// <param name="resizeFactor">The factor by which to resize.</param>
		public void Resize(Point resizeFactor) {
			Size = new Point(Size.X * resizeFactor.X, Size.Y * resizeFactor.Y);
		}
		/// <summary>
		/// If this linear-motion produces collision, CollisionDetection returns the modified value that satisifies non-collision requirements.
		/// </summary>
		/// <param name="desiredPosition">The position to move to, if possible.</param>
		/// <returns>The new position.</returns>
		private Point CollisionDetection(Point desiredPosition) {
			Point newPosition = desiredPosition;

			//Find the distance we can move before colliding
			foreach (GameObject gameObject in World.Instance.GetGameObjects()) {
				//Skip itself
				if (gameObject.TransformComponent == null || gameObject.TransformComponent.Equals(this))
					continue;

				newPosition = Collide(desiredPosition, this, gameObject.TransformComponent);

				//If we bump into something else sooner
				if (newPosition.X - position.X < 0 || newPosition.Y - position.Y < 0)
					position = newPosition;
			}


			return newPosition;
		}

		/// <summary>
		/// An exception class for when colliding does not make sense.
		/// </summary>
		public class CollisionException : Exception {
			/// <summary>
			/// Create an exception with the specified error message.
			/// </summary>
			/// <param name="message">The error message.</param>
			public CollisionException(String message)
				: base(message) {
			}
		}

		/// <summary>
		/// Collides a TransformComponent with another TransformComponent along the line between the inital position and desiredPosition.
		/// This function is optimized under the assumption that most of the time that it is called there will be no collision.
		/// </summary>
		/// <param name="desiredPosition">The desired end position of a.</param>
		/// <param name="a">The TransformComponent that is moving.</param>
		/// <param name="b">The TransformComponent that is fixed.</param>
		/// <returns>The closest position towards desiredPosition that a can move before hitting b.</returns>
		private static Point Collide(Point desiredPosition, TransformComponent a, TransformComponent b) {
			//Note that the top left of the screen is (0,0), with x increasing to the right and y increasing downward

			//a's bounding box (before moving)
			double aleft = a.position.X - a.size.X / 2;
			double aright = a.position.X + a.size.X / 2;
			double atop = a.position.Y - a.size.Y / 2;
			double abottom = a.position.Y + a.size.Y / 2;

			//b's bounding box
			double bleft = b.position.X - b.size.X / 2;
			double bright = b.position.X + b.size.X / 2;
			double btop = b.position.Y - b.size.Y / 2;
			double bbottom = b.position.Y + b.size.Y / 2;

			//If the objects overlap to begin with, throw an exception
			bool overlap = true;
			if (aright <= bleft || aleft >= bright || abottom <= btop || atop >= bbottom)
				overlap = false;
			if (overlap)
				throw new CollisionException("Objects collide before movement.");

			//If a doesn't move then we are done colliding
			if (a.position.Equals(desiredPosition))
				return desiredPosition;

			//First see if b is within a box around a in its new position and end position
			//a's path's bounding box
			double top = Math.Min(a.position.Y, desiredPosition.Y) - a.size.Y / 2;
			double bottom = Math.Max(a.position.Y, desiredPosition.Y) + a.size.Y / 2;
			double left = Math.Min(a.position.X, desiredPosition.X) - a.size.X / 2;
			double right = Math.Max(a.position.X, desiredPosition.X) + a.size.X / 2;

			//Return the desired position if b is not within the bounding box
			if (bleft >= right || bright <= left || btop >= bottom || bbottom <= top)
				return desiredPosition;

			//Choose points that describe the top diagonal plane
			Point p1, p2;
			if (a.position.Y <= desiredPosition.Y) {
				if (a.position.X <= desiredPosition.X) {
					p1 = new Point(a.position.X + a.size.X / 2, a.position.Y - a.size.Y / 2);
					p2 = new Point(desiredPosition.X + a.size.X / 2, desiredPosition.Y - a.size.Y / 2);
				}
				else {
					p1 = new Point(desiredPosition.X - a.size.X / 2, desiredPosition.Y - a.size.Y / 2);
					p2 = new Point(a.position.X - a.size.X / 2, a.position.Y - a.size.Y / 2);
				}
			}
			else {
				if (a.position.X <= desiredPosition.X) {
					p1 = new Point(a.position.X - a.size.X / 2, a.position.Y - a.size.Y / 2);
					p2 = new Point(desiredPosition.X - a.size.X / 2, desiredPosition.Y - a.size.Y / 2);
				}
				else {
					p1 = new Point(desiredPosition.X + a.size.X / 2, desiredPosition.Y - a.size.Y / 2);
					p2 = new Point(a.position.X + a.size.X / 2, a.position.Y - a.size.Y / 2);
				}
			}

			//Divide by zero is impossible because it would throw an exception above
			double topDiagonalHeightLeft = (p2.Y - p1.Y) * (bleft - p1.X) / (p2.X - p1.X) + p1.Y;
			double topDiagonalHeightRight = (p2.Y - p1.Y) * (bright - p1.X) / (p2.X - p1.X) + p1.Y;

			//b is within the bounding box, but if it is above the top diagonal plane then there is no collision
			if (bbottom < topDiagonalHeightLeft && bbottom < topDiagonalHeightRight)
				return desiredPosition;

			//Choose points that describe the bottom diagonal plane
			if (a.position.Y <= desiredPosition.Y) {
				if (a.position.X <= desiredPosition.X) {
					p1 = new Point(a.position.X - a.size.X / 2, a.position.Y + a.size.Y / 2);
					p2 = new Point(desiredPosition.X - a.size.X / 2, desiredPosition.Y + a.size.Y / 2);
				}
				else {
					p1 = new Point(desiredPosition.X + a.size.X / 2, desiredPosition.Y + a.size.Y / 2);
					p2 = new Point(a.position.X + a.size.X / 2, a.position.Y + a.size.Y / 2);
				}
			}
			else {
				if (a.position.X <= desiredPosition.X) {
					p1 = new Point(a.position.X + a.size.X / 2, a.position.Y + a.size.Y / 2);
					p2 = new Point(desiredPosition.X + a.size.X / 2, desiredPosition.Y + a.size.Y / 2);
				}
				else {
					p1 = new Point(desiredPosition.X - a.size.X / 2, desiredPosition.Y + a.size.Y / 2);
					p2 = new Point(a.position.X - a.size.X / 2, a.position.Y + a.size.Y / 2);
				}
			}

			//Divide by zero is impossible because it would throw an exception above
			double bottomDiagonalHeightLeft = (p2.Y - p1.Y) * (bleft - p1.X) / (p2.X - p1.X) + p1.Y;
			double bottomDiagonalHeightRight = (p2.Y - p1.Y) * (bright - p1.X) / (p2.X - p1.X) + p1.Y;

			//b is within the bounding box, but if it is below the bottom diagonal plane then there is no collision
			if (btop > bottomDiagonalHeightLeft && btop > bottomDiagonalHeightRight)
				return desiredPosition;

			//There is a collision. Compute the longest distance a can travel before hitting b.
			//throw new CollisionException("This part hasn't been written yet.");
			p1 = a.position;
			p2 = desiredPosition;

			//If the object is moving only vertically or horizontally then we check two positions to prevent division by zero
			int i = 0, iMax = 4;
			if (p2.X - p1.X == 0.0)
				i = 2;
			if (p2.Y - p1.Y == 0.0)
				iMax = 2;

			double[] x = new double[4];
			double[] y = new double[4];

			x[0] = b.position.X - b.size.X / 2 - a.size.X / 2;
			y[0] = (p2.Y - p1.Y) * (x[0] - p1.X) / (p2.X - p1.X) + p1.Y;

			x[1] = b.position.X + b.size.X / 2 + a.size.X / 2;
			y[1] = (p2.Y - p1.Y) * (x[1] - p1.X) / (p2.X - p1.X) + p1.Y;

			y[2] = b.position.Y - b.size.Y / 2 - a.size.Y / 2;
			x[2] = (p2.X - p1.X) * (y[2] - p1.Y) / (p2.Y - p1.Y) + p1.X;

			y[3] = b.position.Y + b.size.Y / 2 + a.size.Y / 2;
			x[3] = (p2.X - p1.X) * (y[3] - p1.Y) / (p2.Y - p1.Y) + p1.X;

			Point newPosition = desiredPosition;
			double distance = Distance(a.position, desiredPosition);
			for (; i < iMax; i++)
			//if (x[i] > a.position.X && x[i] < desiredPosition.X || x[i] < a.position.X || x[i] > desiredPosition.X)
			//if (y[i] > a.position.Y && y[i] < desiredPosition.Y || y[i] < a.position.Y && y[i] > desiredPosition.Y)
					{
				Point tempPosition = new Point(x[i], y[i]);
				//double tempDistance = Math.Sqrt(x[i] * x[i] + y[i] * y[i]);
				double tempDistance = Distance(tempPosition, a.position);
				if (tempDistance < distance) {
					distance = tempDistance;
					newPosition = tempPosition;
				}
			}

			return newPosition;
		}
		/// <summary>
		/// Calculates the distance between two points.
		/// </summary>
		/// <param name="a">A point.</param>
		/// <param name="b">Another point.</param>
		/// <returns>The distance between a and b.</returns>
		private static double Distance(Point a, Point b) {
			double x = a.X - b.X;
			double y = a.Y - b.Y;
			return Math.Sqrt(x * x + y * y);
		}

		public Point Update(double deltaTime)
		{
			double x = Position.X + Velocity.X * deltaTime;
			double y = Position.Y + Velocity.Y * deltaTime;
			if (x > 600)
				Velocity = new Point(Velocity.X * -1.0, Velocity.Y);
			if (y > 400)
				Velocity = new Point(Velocity.X, Velocity.Y * -1.0);

			Point pos = new Point(x, y);
			Position = pos;
			return pos;
		}
	}
}