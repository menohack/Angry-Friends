using System.Windows.Controls;
using System.Windows;
namespace Library.Engine.Object {
	/// <summary>
	/// Represents a Camera that handles the positioning of the "viewport"; it essentially determines what the screen should render.
	/// </summary>
	public class Camera {

		/// <summary>
		/// The Silverlight canvas that represents the viewport.
		/// </summary>
		private Canvas canvas;

		/// <summary>
		/// Constructor for a new camera.
		/// </summary>
		/// <param name="canvas">The canvas that the camera represents.</param>
		public Camera(Canvas canvas) {
			this.canvas = canvas;
		}

		/// <summary>
		/// Removes an image from the canvas.
		/// </summary>
		/// <param name="image">The image to remove.</param>
		public void GameObjectRemoved(Image image)
		{
			canvas.Children.Remove(image);
		}

		/// <summary>
		/// Adds an image to the canvas.
		/// </summary>
		/// <param name="image">The image to add.</param>
		public void GameObjectAdded(Image image)
		{
			canvas.Children.Add(image);
		}

		/// <summary>
		/// Moves the camera by a specified offset.
		/// </summary>
		/// <param name="offset">The x and y offsets by which to move the camera.</param>
		public void MoveCamera(Point offset)
		{
			canvas.SetValue(Canvas.LeftProperty, (double)canvas.GetValue(Canvas.LeftProperty) + offset.X);
			canvas.SetValue(Canvas.TopProperty, (double)canvas.GetValue(Canvas.TopProperty) + offset.Y);
		}
	}
}