using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Windows;
using Model.Engine.Object;
using Model.Engine.Utilities;

namespace Model.Engine.Component.Transform {
	/// <summary>
	/// Handles positioning, size and rotation of a GameObject.
	/// </summary>
	[DataContract]
	public class TransformComponent : BaseComponent {

		/// <summary>
		/// The position of this TransformComponent.
		/// </summary>
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
        /// The maximum amount of miliseconds from a change-in-time we still assume the GameObject is moving.
        /// We interpolate that the TransformComponent is Translating if the change-in-time since the last move is less than or equal to 1/15 of a second.
        /// Worst case scenario is we say the GameObject has velocity for 1/30 of a second when it, in fact, does not.
        /// </summary>
        private readonly int VELOCITY_INTERPOLATION = 68;

        /// <summary>
        /// A list of a GameObjects's UID's to the distance from its original collision, with which this TransformComponent is still colliding.
        /// </summary>
        private IDictionary<Double, Double> collidingGameObjects;

		/// <summary>
		/// The accessor for the velocity of this TransformComponent.
		/// </summary>
        [IgnoreDataMember]
        public Point Velocity
        {
            get
            {
                Point deltaPosition = new Point(currentPosition.X - previousPosition.X, currentPosition.Y - previousPosition.Y);
                TimeSpan deltaTime = DateTime.Now.TimeOfDay - previousPositionTime.TimeOfDay;

                if (deltaTime.TotalMilliseconds <= VELOCITY_INTERPOLATION)
                {
                    deltaTime = currentPositionTime.TimeOfDay - previousPositionTime.TimeOfDay;
                }

                return new Point(deltaPosition.X / (deltaTime.TotalMilliseconds / 1000.00), deltaPosition.Y / (deltaTime.TotalMilliseconds / 1000.00));
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
		/// The size of this TransformComponent.
		/// </summary>
        [IgnoreDataMember]
        public Point Size { get; private set; }

		/// <summary>
		/// Constructor for a new instance of TransformComponent.
		/// </summary>
		/// <param name="position">The initial position of this TransformComponent.</param>
		/// <param name="rotation">The initial rotation of this TransformComponent.</param>
		/// <param name="size">The initial size of this TransformComponent.</param>
		public TransformComponent(Point position, Point size) : base() {
            previousPosition = position;
            previousPositionTime = DateTime.Now;
            currentPosition = position;
            currentPositionTime = previousPositionTime;

            this.Size = size;
            collidingGameObjects = new Dictionary<Double, Double>();
		}
		
		/// <summary>
		/// Translates by a given point.
		/// </summary>
		/// <param name="deltaPosition">The change of position.</param>
		public void Translate(Point deltaPosition) 
        {
			Position = new Point(currentPosition.X + deltaPosition.X, currentPosition.Y + deltaPosition.Y);
		}

        /// <summary>
        /// Determines if this TransformComponent is colliding with another GameObject.
        /// </summary>
        /// <param name="UID">The UID of the GameObject that is in question.</param>
        /// <returns>True if this TransformComponent is colliding with the given GameObject; otherwise, false.</returns>
        public bool IsCollidingWith(int UID)
        {
            if (collidingGameObjects == null || collidingGameObjects.Count == 0 || !collidingGameObjects.ContainsKey(UID))
            {
                return false;
            }
            else
            {
                GameObject gameObject = EngineObject.Instance.FindGameObjectWithUID(UID);
                double oldDistance = collidingGameObjects[UID];
                double currentDistance = EngineMath.Distance(this.Position, gameObject.TransformComponent.Position);

                // If we've moved, and the new position hasn't been added, then we are no longer colliding with this GameObject.
                if (oldDistance != currentDistance)
                {
                    collidingGameObjects.Remove(UID);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// If this linear-motion produces collision, CollisionDetection returns the modified value that satisifies non-collision requirements.
        /// </summary>
        /// <param name="desiredPosition">The position to move to, if possible.</param>
        /// <returns>The final position after processing collision.</returns>
        private Point CollisionDetection(Point desiredPosition)
        {
            Point temporaryPosition = desiredPosition;
            Point finalPosition = desiredPosition;

            //Find the distance we can move before colliding
            foreach (GameObject gameObject in EngineObject.Instance.GetGameObjects())
            {
                //Skip itself
                if (gameObject.TransformComponent == null || gameObject.TransformComponent.Equals(this))
                {
                    continue;
                }

                finalPosition = CollisionHelper.Instance.Collide(desiredPosition, this, gameObject.TransformComponent);

                //If we bump into something else sooner
                if (EngineMath.Distance(Position, finalPosition) < EngineMath.Distance(Position, temporaryPosition))
                {
                    temporaryPosition = finalPosition;
                    // Store a reference to the GameObject's UID and the distance between the two positions.
                    collidingGameObjects[gameObject.UID] = EngineMath.Distance(temporaryPosition, gameObject.TransformComponent.Position);
                }
            }
            return temporaryPosition;
        }
	}
}