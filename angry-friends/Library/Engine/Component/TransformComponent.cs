using System;
using System.Windows;
using Library.Engine.Object;
using Library.Engine.Utilities;
using System.Runtime.Serialization;

namespace Library.Engine.Component {

	public class Velocity
	{
		public Velocity()
		{
			X = 0.0;
			Y = 0.0;
		}

		public Velocity(double x, double y)
		{
			X = x;
			Y = y;
		}

		public double X { get; set; }
		public double Y { get; set; }
	}

	/// <summary>
	/// Handles positioning, size and rotation of a GameObject.
	/// </summary>
	[DataContract]
	public class TransformComponent : BaseComponent {

		public abstract class Path
		{
			protected DateTime startTime;
			public abstract Point CalculatePosition(double deltaTime);
		}

		public class LinearPath : Path
		{
			private Point startPosition;
			private Point endPosition;

			public LinearPath(Point startPosition, Point endPosition)
			{
				this.startPosition = startPosition;
				this.endPosition = endPosition;
			}

			public override Point CalculatePosition(double deltaTime)
			{
				TimeSpan delta = startTime - DateTime.Now;
				double seconds = delta.TotalSeconds;
				return new Point();
			}
		}

		private Path path;

		/// <summary>
		/// The mimimum angle of rotation.
		/// </summary>
        [DataMember]
        private static readonly int MIMIMUM_ROTATION_ANGLE = 0;

		/// <summary>
		/// The maximum angle of rotation.
		/// </summary>
        [DataMember]
        private static readonly int MAXIMUM_ROTATION_ANGLE = 360;

		/// <summary>
		/// The position of this TransformComponent.
		/// </summary>

		private Point position;

        [DataMember]
        private Point currentPosition;
        
        /// <summary>
        /// The previous position of this TransformComponent.
        /// </summary>
        [DataMember]
        private Point previousPosition;

        /// <summary>
        /// The time at which the previous change-in-position was recorded.
        /// </summary>
        [DataMember]
        private DateTime previousPositionTime;

        /// <summary>
        /// The time at which the current change-in-position was recorded.
        /// </summary>
        [DataMember]
        private DateTime currentPositionTime;

        /// <summary>
        /// The velocity of this TransformComponent, in pixels per second.
        /// </summary>
        [DataMember]
        private Velocity velocity;

		/// <summary>
		/// The rotation of this TransformComponent.
		/// </summary>
        [DataMember]
        private int rotation;

		/// <summary>
		/// The size of this TransformComponent.
		/// </summary>
        [DataMember]
        private Point size;

		/// <summary>
		/// The accessor for the velocity of this TransformComponent.
		/// </summary>
        [IgnoreDataMember]
        public Velocity Velocity
        {
            get
            {
                TimeSpan deltaTime = DateTime.Now.TimeOfDay - previousPositionTime.TimeOfDay;
                Point deltaPosition = new Point(currentPosition.X - previousPosition.X, currentPosition.Y - previousPosition.Y);

                velocity = new Velocity(deltaPosition.X / (deltaTime.Milliseconds / 1000.00), deltaPosition.Y / (deltaTime.Milliseconds / 1000.00));
                return velocity;
            }
        }

		/// <summary>
		/// The accessor for the position of the TransformComponent.
		/// </summary>
		[IgnoreDataMember]
        public Point Position {
			get 
            {
				return currentPosition;
			}
			set 
			{
                previousPosition = currentPosition;
                previousPositionTime = currentPositionTime;

				currentPosition = CollisionDetection(new Point(value.X, value.Y));
                currentPositionTime = DateTime.Now;

                if (previousPosition != currentPosition)
                {
                    EngineObject.Instance.Camera.Viewport.AddGameObjectToRedrawQueue(base.Owner);
                }
			}
		}

		/// <summary>
		/// The rotation of this TransformComponent, clamped in degrees: [0, 360].
		/// </summary>
		public int Rotation {
			get 
            {
				return rotation;
			}
			set 
            {
				rotation = value;
				currentPosition = CollisionDetection(currentPosition);
                EngineObject.Instance.Camera.Viewport.AddGameObjectToRedrawQueue(base.Owner);
			}
		}

		/// <summary>
		/// The size of this TransformComponent.
		/// </summary>
		[IgnoreDataMember]
        public Point Size {
			get 
            {
				return size;
			}
			set {
				size = value;
				currentPosition = CollisionDetection(currentPosition);
                EngineObject.Instance.Camera.Viewport.AddGameObjectToRedrawQueue(base.Owner);
			}
		}

		/// <summary>
		/// Constructor for a new instance of TransformComponent.
		/// </summary>
		/// <param name="position">The initial position of this TransformComponent.</param>
		/// <param name="rotation">The initial rotation of this TransformComponent.</param>
		/// <param name="size">The initial size of this TransformComponent.</param>
		/// <param name="owner">The GameObject that owns this TransformComponent.</param>
		/*
		public TransformComponent(Point position, int rotation, Point size, GameObject owner) : base(owner) {
			//First, set the fields
			this.position = position;
			this.size = size;
			this.rotation = rotation;

			//Then check if this is a valid position
			this.Position = position;

			//The GameObject now has physics associated with it so it needs to be updated regularly
			World.Instance.AddToRedrawQueue(owner);
		}
		*/

		/// <summary>
		/// Translates by a given point.
		/// </summary>
		/// <param name="deltaPosition">The change of position.</param>
		public void Translate(Point deltaPosition) {
			Position = new Point(currentPosition.X + deltaPosition.X, currentPosition.Y + deltaPosition.Y);
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

		private DateTime lastTime;

		public void Update(double deltaTime)
		{
			Point newPosition = new Point(Position.X + Velocity.X * deltaTime / 1000.0, Position.Y + Velocity.Y * deltaTime / 1000.0);
			Position = newPosition;
		}

		/// <summary>
		/// If this linear-motion produces collision, CollisionDetection returns the modified value that satisifies non-collision requirements.
		/// </summary>
		/// <param name="desiredPosition">The position to move to, if possible.</param>
		/// <returns>The new position.</returns>
		private Point CollisionDetection(Point desiredPosition) {
			Point tempPosition = desiredPosition;
			Point newPosition = desiredPosition;

			//Find the distance we can move before colliding
			foreach (GameObject gameObject in EngineObject.Instance.GetGameObjects()) {
				//Skip itself
				if (gameObject.TransformComponent == null || gameObject.TransformComponent.Equals(this))
					continue;

				newPosition = Collide(desiredPosition, this, gameObject.TransformComponent);

				//If we bump into something else sooner
				if (Distance(position, newPosition) < Distance(position, tempPosition))
					tempPosition = newPosition;
			}


			return tempPosition;
		}

		/// <summary>
		/// An exception class for when colliding does not make sense.
		/// </summary>
		public class CollisionException : Exception {
			/// <summary>
			/// Create an exception with the specified error message.
			/// </summary>
			/// <param name="message">The error message.</param>
			public CollisionException(String message): base(message) {}
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
			p1 = a.position;
			p2 = desiredPosition;

			//If the object is moving only vertically or horizontally then we check two positions to prevent division by zero
			int i = 0, iMax = 4;
			if (p2.X - p1.X == 0.0)
				i = 2;
			if (p2.Y - p1.Y == 0.0)
				iMax = 2;

			//The four possible positions that a can be in when bumping into b while moving linearly
			double[] x = new double[4];
			double[] y = new double[4];

			//To the left of b
			x[0] = b.position.X - b.size.X / 2 - a.size.X / 2;
			y[0] = (p2.Y - p1.Y) * (x[0] - p1.X) / (p2.X - p1.X) + p1.Y;

			//To the right of b
			x[1] = b.position.X + b.size.X / 2 + a.size.X / 2;
			y[1] = (p2.Y - p1.Y) * (x[1] - p1.X) / (p2.X - p1.X) + p1.Y;

			//Above b
			y[2] = b.position.Y - b.size.Y / 2 - a.size.Y / 2;
			x[2] = (p2.X - p1.X) * (y[2] - p1.Y) / (p2.Y - p1.Y) + p1.X;

			//Below b
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

					//If we hit something set the velocity to zero
					/*
					if (i < 2)
						a.Velocity = new Point(0.0, a.Velocity.Y);
					else
						a.Velocity = new Point(a.Velocity.X, 0.0);
					*/
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
	}
}