using Library.Engine.Object;
using System;
namespace Library.Engine.Component {

	/// <summary>
	/// The base for all Components.  It stores the GameObject that owns this Component.
	/// </summary>
	public class BaseComponent {

        /// <summary>
        /// The GameObject that owns this Component.
        /// </summary>
        private GameObject owner = null;

		/// <summary>
		/// The GameObject that owns this Component. This can only be set once.
		/// </summary>
		public GameObject Owner
        {
            get
            {
                return owner;
            }
            set
            {
                if (owner == null)
                {
                    owner = value;
                }
            }
        }

		/// <summary>
		/// Constructor for a new BaseComponent.
		/// </summary>
		/// <param name="owner">The owner of this BaseComponent.</param>
		public BaseComponent(GameObject owner) {
			this.Owner = owner;
		}
	}
}