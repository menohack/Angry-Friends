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
		public Point position { get; private set; }

		/// <summary>
		/// Constructor for a new Camera.
		/// </summary>
		public Camera()
		{
			position = new Point();
		}

		/// <summary>
		/// Translates the Camera by a given Point.
		/// </summary>
		/// <param name="deltaPosition">The change in units to move Translate this Camera.</param>
		public void Translate(Point deltaPosition)
		{
			position = new Point(position.X + deltaPosition.X, position.Y + deltaPosition.Y);
		}
	}
}
