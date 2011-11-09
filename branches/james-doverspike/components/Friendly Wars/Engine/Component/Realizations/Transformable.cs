using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Friendly_Wars.Engine.Component.Interfaces;

namespace Friendly_Wars.Engine.Component.Realizations
{
	/// <summary>
	/// Realization of a generic Transformable class that uses a TransformComponent to fulfill the supplied methods.
	/// </summary>
	public class Transformable : ITransformable
	{
		/// <summary>
		/// This GameObject's TransformComponent.
		/// </summary>
		private TransformComponent transformComponent;

		/// <summary>
		/// Constructor for a new Transformable-type.
		/// </summary>
		public Transformable()
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
