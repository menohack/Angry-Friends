using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Friendly_Wars.Engine.Component.Graphic
{
	/// <summary>
	/// Frame is the base class for an Animation.  
	/// It contains the actual Image of this Frame and its offset.
	/// </summary>
	public class Frame
	{
		/// <summary>
		/// The actual Image of this Frame.
		/// </summary>
		public Image Image { get; private set; }
		/// <summary>
		/// The offset of this Frame's Image.
		/// </summary>
		public Point Offset { get; private set; }

		/// <summary>
		/// Constructor for a Frame.
		/// </summary>
		/// <param name="image">The Image of this Frame.</param>
		/// <param name="offset">The offset of this Frame's Image.</param>
		public Frame(Image image, Point offset)
		{
			this.Image = image;
			this.Offset = offset;
		}
	}
}
