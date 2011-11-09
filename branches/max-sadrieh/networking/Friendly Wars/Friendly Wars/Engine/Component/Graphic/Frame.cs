using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Friendly_Wars.Engine.Component.Graphic
{
    /// <summary>
    /// Frame contains an image and its offset.
    /// </summary>
    public class Frame
    {
        /// <summary>
        /// The actual image of this Frame.
        /// </summary>
		private Image image { get; set; }

        /// <summary>
        /// The offset of this Frame's image.
        /// </summary>
        public Point offset { get; private set; }

        /// <summary>
        /// Creates an Image on the Silverlight canvas.
        /// </summary>
        /// <param name="image">The image of this Frame.</param>
        /// <param name="offset">The start position of this Frame's image.</param>
        public Frame(Image image, Point offset)
        {
            this.image = image;
            this.offset = offset;

			image.Visibility = Visibility.Collapsed;
			image.RenderTransform = new TranslateTransform() { X = offset.X, Y = offset.Y };

			// TODO: Should we make this more abstract?
			MainPage.mainPage.LayoutRoot.Children.Add(image);
        }

		/// <summary>
		/// Draws a frame by making it visible.
		/// </summary>
		public void Draw()
		{
			image.Visibility = Visibility.Visible;
		}

		/// <summary>
		/// Hides a frame by making it invisible.
		/// </summary>
		public void Hide()
		{
			image.Visibility = Visibility.Collapsed;
		}
    }
}
