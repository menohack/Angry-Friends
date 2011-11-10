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
using Friendly_Wars.Engine.Object;

namespace Friendly_Wars.Engine.Component
{
	/// <summary>
	/// The base for all Components.  It stores the GameObject that owns this Component.
	/// </summary>
	public class BaseComponent
	{
		/// <summary>
		/// The GameObject that owns this Component.
		/// </summary>
		public GameObject Owner { get; private set; }

		/// <summary>
		/// Constructor for a new BaseComponent.
		/// </summary>
		/// <param name="owner">The owner of this BaseComponent.</param>
		public BaseComponent(GameObject owner)
		{
			this.Owner = owner;
		}
	}
}
