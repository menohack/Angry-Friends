using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Friendly_Wars.Engine.Component
{
	/// <summary>
	/// Represents a Camera that handles rendering of objects and only renders what is in view.
	/// </summary>
	public class Camera
	{
		/// <summary>
		/// The position of the camera, in pixels.
		/// </summary>
		public Point position { get; private set; }
	}
}
