﻿using System;
using System.Windows;
using Friendly_Wars.Engine.Object;
using Friendly_Wars.Engine.Utilities;

namespace Friendly_Wars.Engine.Component
{
	/// <summary>
	/// Handles positioning, size and rotation of an Object.
	/// </summary>
	public class TransformComponent : BaseComponent
	{
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
		/// The accessor for the position of the TransformComponent.
		/// </summary>
		public Point Position { 
			get {
				return position;	
			}
			set
			{
				position = CollisionDetection(position, new Point(position.X + value.X, position.Y + value.Y));
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
				position = CollisionDetection(position, position);
			}
		}
		/// <summary>
		/// The size of this TransformComponent.
		/// </summary>
		public Point Size {
			get
			{
				return size;
			}
			set
			{
				size = value;
				position = CollisionDetection(position, position);
			}
		}

		/// <summary>
		/// Constructor for a new instance of TransformComponent.
		/// </summary>
		/// <param name="position">The initial position of this TransformComponent.</param>
		/// <param name="rotation">The initial rotation of this TransformComponent.</param>
		/// <param name="size">The initial size of this TransformComponent.</param>
		/// <param name="owner">The GameObject that owns this TransformComponent.</param>
		public TransformComponent(Point position, int rotation, Point size, GameObject owner) : base(owner)
		{
			this.Position = position;
			this.Rotation = rotation;
			this.Size = size;
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
		/// Rotates by a given number of degrees.
		/// </summary>
		/// <param name="deltaRotation">The change of rotation.</param>
		public void Rotate(int deltaRotation)
		{
			EngineMath.Clamp(Rotation += deltaRotation, MIMIMUM_ROTATION_ANGLE, MAXIMUM_ROTATION_ANGLE);
		}

		/// <summary>
		/// Resizes by a given factor.
		/// </summary>
		/// <param name="resizeFactor">The factor by which to resize.</param>
		public void Resize(Point resizeFactor)
		{
			Size = new Point(Size.X * resizeFactor.X, Size.Y * resizeFactor.Y);
		}

		/// <summary>
		/// If this linear-motion produces collision, CollisionDetection returns the modified value that satisifies non-collision requirements.
		/// </summary>
		private Point CollisionDetection(Point currentPosition, Point desiredPosition)
		{
			//foreach (GameObject gameObject : World.Instance.GameObjects) {}
			return currentPosition;
		}
	}
}
