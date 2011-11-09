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
		private Image image;
		/// <summary>
		/// The offset of this Frame's Image.
		/// </summary>
		private Point offset;

		/// <summary>
		/// Constructor for a Frame.
		/// </summary>
		/// <param name="image">The Image of this Frame.</param>
		/// <param name="offset">The offset of this Frame's Image.</param>
		public Frame(Image image, Point offset)
		{
			this.image = image;
			this.offset = offset;
		}
	}
}
