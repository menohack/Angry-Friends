using System.Windows;

namespace Friendly_Wars.Engine.Component
{
	/// <summary>
	/// Represents a Camera that handles the positioning of the "viewport"; it essentially determines what the screen should render.
	/// </summary>
	public class Camera
	{
		/// <summary>
		/// The position of the camera, in pixels.
		/// </summary>
		public Point Position { get; private set; }

		/// <summary>
		/// Constructor for a new Camera.
		/// </summary>
		public Camera()
		{
			Position = new Point();
		}

		/// <summary>
		/// Translates the Camera by a given Point.
		/// </summary>
		/// <param name="deltaPosition">The change in units to move Translate this Camera.</param>
		public void Translate(Point deltaPosition)
		{
			Position = new Point(Position.X + deltaPosition.X, Position.Y + deltaPosition.Y);
		}
	}
}
