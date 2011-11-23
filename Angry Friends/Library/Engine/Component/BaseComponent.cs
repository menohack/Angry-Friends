﻿using Library.Engine.Object;
namespace Library.Engine.Component {
	/// <summary>
	/// The base for all Components.  It stores the GameObject that owns this Component.
	/// </summary>
	public class BaseComponent {
		/// <summary>
		/// The GameObject that owns this Component.
		/// </summary>
		public GameObject Owner { get; private set; }

		/// <summary>
		/// Constructor for a new BaseComponent.
		/// </summary>
		/// <param name="owner">The owner of this BaseComponent.</param>
		public BaseComponent(GameObject owner) {
			this.Owner = owner;
		}
	}
}