using System.Windows;
using Friendly_Wars.Engine.Object;
using Friendly_Wars.Engine.Component.Interfaces;
using Friendly_Wars.Engine.Component;
using System;
using Friendly_Wars.Engine.Component.Realizations;

namespace Friendly_Wars.Engine.Object
{
	/// <summary>
	/// Represents a Camera that handles the positioning of the "viewport"; it essentially determines what the screen should render.
	/// </summary>
	public class Camera : Transformable
	{
		/// <summary>
		/// Constructor for a new Camera.
		/// </summary>
		public Camera()
		{
		}
	}
}
