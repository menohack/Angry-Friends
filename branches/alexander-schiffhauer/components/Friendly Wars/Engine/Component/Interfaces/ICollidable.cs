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
using Friendly_Wars.Engine.Component;

namespace Friendly_Wars.Engine.Component.Interfaces
{
	public interface ICollidable
	{
		/// <summary>
		/// Accessor for a CollisionComponent.
		/// </summary>
		private CollisionComponent CollisionComponent { private get; private set; }
		/// <summary>
		/// Determines if there is any collision from a linear path from the currentPosition to the desiredPosition.
		/// </summary>
		/// <param name="currentPosition">The current position.</param>
		/// <param name="desiredPosition">The position that is desired.</param>
		/// <returns>Returns desiredPosition if there is no collision.
		/// If there is collision, it returns the closest modified position, such that there is no collision.</returns>
		public Point CheckCollision(Point currentPosition, Point desiredPosition);
	}
}
