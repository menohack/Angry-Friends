using System.Windows.Controls;
using System.Windows;
using System.Diagnostics;
namespace Library.Engine.Object {
	/// <summary>
	/// Represents a Camera that handles the positioning of the "viewport"; Camera determines what the screen should render.
	/// </summary>
	public class Camera {

		/// <summary>
		/// The Silverlight Canvas that represents the game's viewport.
		/// </summary>
		private Canvas viewport;

		/// <summary>
		/// Constructor for a new camera.
		/// </summary>
		/// <param name="viewport">The viewport that the camera displays.</param>
		public Camera(Canvas viewport) {
			this.viewport = viewport;
		}

		/// <summary>
		/// Moves the camera by a specified offset.
		/// </summary>
		/// <param name="offset">The x and y offsets by which to move the camera.</param>
		public void MoveCamera(Point offset)
		{
			viewport.SetValue(Canvas.LeftProperty, (double)viewport.GetValue(Canvas.LeftProperty) + offset.X);
			viewport.SetValue(Canvas.TopProperty, (double)viewport.GetValue(Canvas.TopProperty) + offset.Y);
		}

        /// <summary>
        /// Removes an Image from the viewport.
        /// </summary>
        /// <param name="image">The Image to remove.</param>
        public void RemoveImage(Image image)
        {
            viewport.Children.Remove(image);
        }

        /// <summary>
        /// Adds a Image to the viewport.
        /// </summary>
        /// <param name="image">The Image to add.</param>
        public void AddImage(Image image)
        {
            RemoveImage(image);
            viewport.Children.Add(image);
        }
	}
}