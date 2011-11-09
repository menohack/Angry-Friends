using System.Windows;
using Friendly_Wars.Engine.Object;

namespace Friendly_Wars.Engine.Object
{
	/// <summary>
	/// Represents a Camera that handles the positioning of the "viewport"; it essentially determines what the screen should render.
	/// </summary>
	public class Camera : GameObject
	{

		private static readonly string CAMERA_NAME = "CAMERA";
		private static readonly string CAMERA_TAG = "CAMERA";

		/// <summary>
		/// Constructor for a new Camera.
		/// </summary>
		public Camera() : base(CAMERA_NAME, CAMERA_TAG)
		{
		}

		/// <summary>
		/// Moves the Camera by a given Point.
		/// </summary>
		/// <param name="deltaPosition">The change in units to move Translate this Camera.</param>
		public void Move()
		{
		}
	}
}
