﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Windows;
using Model.Engine.Object;
using Model.Engine.Utilities;
using System.Diagnostics;

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

                Point velocity = new Point(deltaPosition.X / (deltaTime.TotalSeconds), deltaPosition.Y / (deltaTime.TotalSeconds));

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
        /// Determines if this TransformComponent is colliding with another GameObject.
        /// </summary>
        /// <param name="name">The name of all GameObjects to check.</param>
        /// <returns>True if this TransformComponent is colliding with the given GameObject; otherwise, false.</returns>
        public bool IsCollidingWith(String name)
        {
            ICollection<GameObject> gameObjects = EngineObject.Instance.FindGameObjectsWithName(name);
            
            foreach (GameObject gameObject in gameObjects)
            {
                if (collidingGameObjects.ContainsKey(gameObject.UID))
                {
                    return IsCollidingWith(gameObject.UID);
                }
            }
            return false;
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