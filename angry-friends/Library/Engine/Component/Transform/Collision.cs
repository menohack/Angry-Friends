using System;
using System.Windows;
using Model.Engine.Utilities;

namespace Model.Engine.Component.Transform
{
    /// <summary>
    /// Handles the collision of a TransformComponent.
    /// </summary>
    public class CollisionHelper
    {

        /// <summary>
        /// The singleton instance of CollisionHelper.
        /// </summary>
        private static CollisionHelper instance;

        /// <summary>
        /// The singleton accessor of CollisionHelper.
        /// </summary>
        public static CollisionHelper Instance { 
            get 
            {
                if (instance == null) {
                    instance = new CollisionHelper();
                }

                return instance;
            }
        }

        private CollisionHelper() { }

		/// <summary>
		/// Collides a TransformComponent with another TransformComponent along the line between the inital position and desiredPosition.
		/// This function is optimized under the assumption that most of the time that it is called there will be no collision.
		/// </summary>
		/// <param name="desiredPosition">The desired end position of a.</param>
		/// <param name="a">The TransformComponent that is moving.</param>
		/// <param name="b">The TransformComponent that is fixed.</param>
		/// <returns>The closest position towards desiredPosition that a can move before hitting b.</returns>
		public Point Collide(Point desiredPosition, TransformComponent a, TransformComponent b) {
			//Note that the top left of the screen is (0,0), with x increasing to the right and y increasing downward

			//a's bounding box (before moving)
			double aleft = a.Position.X - a.Size.X / 2;
			double aright = a.Position.X + a.Size.X / 2;
			double atop = a.Position.Y - a.Size.Y / 2;
			double abottom = a.Position.Y + a.Size.Y / 2;

			//b's bounding box
			double bleft = b.Position.X - b.Size.X / 2;
			double bright = b.Position.X + b.Size.X / 2;
			double btop = b.Position.Y - b.Size.Y / 2;
			double bbottom = b.Position.Y + b.Size.Y / 2;

			//If the objects overlap to begin with, throw an exception
			bool overlap = true;
			if (aright <= bleft || aleft >= bright || abottom <= btop || atop >= bbottom)
				overlap = false;
			if (overlap)
				throw new CollisionException("Objects collide before movement.");

			//If a doesn't move then we are done colliding
			if (a.Position.Equals(desiredPosition))
				return desiredPosition;

			//First see if b is within a box around a in its new position and end position
			//a's path's bounding box
			double top = Math.Min(a.Position.Y, desiredPosition.Y) - a.Size.Y / 2;
			double bottom = Math.Max(a.Position.Y, desiredPosition.Y) + a.Size.Y / 2;
			double left = Math.Min(a.Position.X, desiredPosition.X) - a.Size.X / 2;
			double right = Math.Max(a.Position.X, desiredPosition.X) + a.Size.X / 2;

			//Return the desired position if b is not within the bounding box
			if (bleft >= right || bright <= left || btop >= bottom || bbottom <= top)
				return desiredPosition;

			//Choose points that describe the top diagonal plane
			Point p1, p2;
			if (a.Position.Y <= desiredPosition.Y) {
				if (a.Position.X <= desiredPosition.X) {
					p1 = new Point(a.Position.X + a.Size.X / 2, a.Position.Y - a.Size.Y / 2);
					p2 = new Point(desiredPosition.X + a.Size.X / 2, desiredPosition.Y - a.Size.Y / 2);
				}
				else {
					p1 = new Point(desiredPosition.X - a.Size.X / 2, desiredPosition.Y - a.Size.Y / 2);
					p2 = new Point(a.Position.X - a.Size.X / 2, a.Position.Y - a.Size.Y / 2);
				}
			}
			else {
				if (a.Position.X <= desiredPosition.X) {
					p1 = new Point(a.Position.X - a.Size.X / 2, a.Position.Y - a.Size.Y / 2);
					p2 = new Point(desiredPosition.X - a.Size.X / 2, desiredPosition.Y - a.Size.Y / 2);
				}
				else {
					p1 = new Point(desiredPosition.X + a.Size.X / 2, desiredPosition.Y - a.Size.Y / 2);
					p2 = new Point(a.Position.X + a.Size.X / 2, a.Position.Y - a.Size.Y / 2);
				}
			}

			//Divide by zero is impossible because it would throw an exception above
			double topDiagonalHeightLeft = (p2.Y - p1.Y) * (bleft - p1.X) / (p2.X - p1.X) + p1.Y;
			double topDiagonalHeightRight = (p2.Y - p1.Y) * (bright - p1.X) / (p2.X - p1.X) + p1.Y;

			//b is within the bounding box, but if it is above the top diagonal plane then there is no collision
			if (bbottom < topDiagonalHeightLeft && bbottom < topDiagonalHeightRight)
				return desiredPosition;

			//Choose points that describe the bottom diagonal plane
			if (a.Position.Y <= desiredPosition.Y) {
				if (a.Position.X <= desiredPosition.X) {
					p1 = new Point(a.Position.X - a.Size.X / 2, a.Position.Y + a.Size.Y / 2);
					p2 = new Point(desiredPosition.X - a.Size.X / 2, desiredPosition.Y + a.Size.Y / 2);
				}
				else {
					p1 = new Point(desiredPosition.X + a.Size.X / 2, desiredPosition.Y + a.Size.Y / 2);
					p2 = new Point(a.Position.X + a.Size.X / 2, a.Position.Y + a.Size.Y / 2);
				}
			}
			else {
				if (a.Position.X <= desiredPosition.X) {
					p1 = new Point(a.Position.X + a.Size.X / 2, a.Position.Y + a.Size.Y / 2);
					p2 = new Point(desiredPosition.X + a.Size.X / 2, desiredPosition.Y + a.Size.Y / 2);
				}
				else {
					p1 = new Point(desiredPosition.X - a.Size.X / 2, desiredPosition.Y + a.Size.Y / 2);
					p2 = new Point(a.Position.X - a.Size.X / 2, a.Position.Y + a.Size.Y / 2);
				}
			}

			//Divide by zero is impossible because it would throw an exception above
			double bottomDiagonalHeightLeft = (p2.Y - p1.Y) * (bleft - p1.X) / (p2.X - p1.X) + p1.Y;
			double bottomDiagonalHeightRight = (p2.Y - p1.Y) * (bright - p1.X) / (p2.X - p1.X) + p1.Y;

			//b is within the bounding box, but if it is below the bottom diagonal plane then there is no collision
			if (btop > bottomDiagonalHeightLeft && btop > bottomDiagonalHeightRight)
				return desiredPosition;

			//There is a collision. Compute the longest distance a can travel before hitting b.
			p1 = a.Position;
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
			x[0] = b.Position.X - b.Size.X / 2 - a.Size.X / 2;
			y[0] = (p2.Y - p1.Y) * (x[0] - p1.X) / (p2.X - p1.X) + p1.Y;

			//To the right of b
			x[1] = b.Position.X + b.Size.X / 2 + a.Size.X / 2;
			y[1] = (p2.Y - p1.Y) * (x[1] - p1.X) / (p2.X - p1.X) + p1.Y;

			//Above b
			y[2] = b.Position.Y - b.Size.Y / 2 - a.Size.Y / 2;
			x[2] = (p2.X - p1.X) * (y[2] - p1.Y) / (p2.Y - p1.Y) + p1.X;

			//Below b
			y[3] = b.Position.Y + b.Size.Y / 2 + a.Size.Y / 2;
			x[3] = (p2.X - p1.X) * (y[3] - p1.Y) / (p2.Y - p1.Y) + p1.X;

			Point newPosition = desiredPosition;
            double distance = EngineMath.Distance(a.Position, desiredPosition);
			for (; i < iMax; i++)
			//if (x[i] > a.position.X && x[i] < desiredPosition.X || x[i] < a.position.X || x[i] > desiredPosition.X)
			//if (y[i] > a.position.Y && y[i] < desiredPosition.Y || y[i] < a.position.Y && y[i] > desiredPosition.Y)
					{
				Point tempPosition = new Point(x[i], y[i]);
				//double tempDistance = Math.Sqrt(x[i] * x[i] + y[i] * y[i]);
				double tempDistance = EngineMath.Distance(tempPosition, a.Position);
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
        /// An exception class for when colliding does not make sense.
        /// </summary>
        public class CollisionException : Exception
        {
            /// <summary>
            /// Create an exception with the specified error message.
            /// </summary>
            /// <param name="message">The error message.</param>
            public CollisionException(String message) : base(message) { }
        }
    }
}
