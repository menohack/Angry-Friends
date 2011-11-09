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

namespace Friendly_Wars.Engine.Component.Interfaces
{
	public interface ITransformable
	{
		/// <summary>
		/// Accessor for a TransformComponent.
		/// </summary>
		private TransformComponent TransformComponent { private get; private set; }
		/// <summary>
		/// Moves the TransformComponent by a specified amount.
		/// </summary>
		/// <param name="deltaPosition">The amount by which to move.</param>
		public void MoveBy(Point deltaPosition);
		/// <summary>
		/// Move the TransformComponent to a given value.
		/// </summary>
		/// <param name="absolutePosition">The absolute value in which to move.</param>
		public void MoveTo(Point absolutePosition);
		/// <summary>
		/// Rotates the TransformComponent by a given value.
		/// </summary>
		/// <param name="deltaRotation">The value by which to rotate.</param>
		public void RotateBy(int deltaRotation);
		/// <summary>
		/// Rotates the TransformComponent to a given value.
		/// </summary>
		/// <param name="absolutePosition">The value in which to rotate.</param>
		public void RotateTo(int absolutePosition);
		/// <summary>
		/// Resizes the TransformComponent by a given factor.
		/// </summary>
		/// <param name="deltaSize">The factor by which to resize.</param>
		public void ResizeBy(Point deltaSize);
		/// <summary>
		/// Resizes the TransformComponent to a specific size.
		/// </summary>
		/// <param name="absoluteSize">The size in which to resize.</param>
		public void ResizeTo(Point absoluteSize);
	}
}
