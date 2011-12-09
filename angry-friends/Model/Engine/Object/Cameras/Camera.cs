using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;

namespace Model.Engine.Object.Cameras {

	/// <summary>
	/// Represents a Camera that handles the positioning of the "viewport"; Camera determines what the screen should render.
	/// </summary>
	[DataContract]
    public class Camera {

		/// <summary>
		/// The Viewport of this Camera.
		/// </summary>
        [DataMember]
        public Viewport Viewport { get; private set; }

		/// <summary>
		/// Constructor for a new Camera.
		/// </summary>
		/// <param name="viewport">The Viewport that this camera renders.</param>
		public Camera(Viewport viewport) {
			this.Viewport = viewport;
		}

		/// <summary>
		/// Translates by a given point.
		/// </summary>
		/// <param name="offset">The change in position.</param>
		public void Translate(Point deltaPosition)
		{
            Viewport.SetValue(Canvas.LeftProperty, (double) Viewport.GetValue(Canvas.LeftProperty) + deltaPosition.X);
            Viewport.SetValue(Canvas.TopProperty, (double) Viewport.GetValue(Canvas.TopProperty) + deltaPosition.Y);
		}
	}
}