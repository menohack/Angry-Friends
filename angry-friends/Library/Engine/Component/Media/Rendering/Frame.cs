using System;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Model.Engine.Component.Media.Rendering {

	/// <summary>
	/// Frame is the base class for an Animation.  
	/// It contains the actual Image of this Frame and its offset.
	/// </summary>
	[DataContract]
    public class Frame {

		/// <summary>
		/// The actual Image of this Frame.
		/// </summary>
		[DataMember]
        public Image Image { get; private set; }

		/// <summary>
		/// The offset of this Frame's Image.
		/// </summary>
        [DataMember]
        public Point Offset { get; private set; }

		/// <summary>
		/// Constructor for a Frame.
		/// </summary>
		/// <param name="image">The Image of this Frame.</param>
		/// <param name="offset">The offset of this Frame's Image.</param>
		public Frame(Image image, Point offset) {
			this.Image = image;
			this.Offset = offset;
		}

		/// <summary>
		/// Constructor for a Frame with no offset.
		/// </summary>
		/// <param name="image">The Image of this Frame.</param>
		public Frame(Image image)
		{
			this.Image = image;
			this.Offset = new Point(0.0, 0.0);
		}

		/// <summary>
		/// Constructor for a solid color box for easy testing.
		/// </summary>
		/// <param name="color">The color of the box.</param>
		/// <param name="size">The size of the box.</param>
		public Frame(Color color, Point size)
		{
			int width = (int)Math.Ceiling(size.X);
			int height = (int)Math.Ceiling(size.Y);

			WriteableBitmap wb = new WriteableBitmap(width, height);
			for (int i = 0; i < width * height; i++)
				wb.Pixels[i] = ConvertToARGB32(color);
			wb.Invalidate();
			Image box = new Image();
			box.Source = wb;

			this.Image = box;
			this.Offset = new Point(0.0, 0.0);
		}

		/// <summary>
		/// TEMPORARY HELPER FUNCTION, DONT KILL ME ALEX
		/// </summary>
		/// <param name="color"></param>
		/// <returns></returns>
		private int ConvertToARGB32(Color color)
		{
			return ((color.R << 16) | (color.G << 8) | (color.B << 0) | (color.A << 24));
		}
	}
}