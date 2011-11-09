using System.Windows;
using Friendly_Wars.Engine.Object;
using Friendly_Wars.Engine.Component.Interfaces;
using Friendly_Wars.Engine.Component;
using System;

namespace Friendly_Wars.Engine.Object
{
	/// <summary>
	/// Represents a Camera that handles the positioning of the "viewport"; it essentially determines what the screen should render.
	/// </summary>
	public class Camera : ITransformable
	{
		/// <summary>
		/// The TransformComponent of this Camera
		/// </summary>
		private TransformComponent transformComponent;

		/// <summary>
		/// Constructor for a new Camera.
		/// </summary>
		public Camera()
		{
		}

		/// <summary>
		/// Moves the TransformComponent by a specified amount.
		/// </summary>
		/// <param name="deltaPosition">The amount by which to move.</param>
		public void MoveBy(Point deltaPosition)
		{
			transformComponent.Translate(deltaPosition);
		}

		/// <summary>
		/// Move the TransformComponent to a given value.
		/// </summary>
		/// <param name="absolutePosition">The absolute value in which to move.</param>
		public void MoveTo(Point absolutePosition)
		{
			transformComponent.Position = absolutePosition;
		}

		/// <summary>
		/// Rotates the TransformComponent by a given value.
		/// </summary>
		/// <param name="deltaRotation">The value by which to rotate.</param>
		public void RotateBy(int deltaRotation)
		{
			transformComponent.Rotate(deltaRotation);
		}

		/// <summary>
		/// Rotates the TransformComponent to a given value.
		/// </summary>
		/// <param name="absolutePosition">The value in which to rotate.</param>
		public void RotateTo(int absolutePosition)
		{
			transformComponent.Rotation = absolutePosition;
		}

		/// <summary>
		/// Resizes the TransformComponent by a given factor.
		/// </summary>
		/// <param name="deltaSize">The factor by which to resize.</param>
		public void ResizeBy(Point deltaSize)
		{
			transformComponent.Resize(deltaSize);
		}

		/// <summary>
		/// Resizes the TransformComponent to a specific size.
		/// </summary>
		/// <param name="absoluteSize">The size in which to resize.</param>
		public void ResizeTo(Point absoluteSize)
		{
			transformComponent.Size = absoluteSize;
		}
	}
}
