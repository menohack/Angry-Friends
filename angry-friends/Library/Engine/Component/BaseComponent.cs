using Library.Engine.Object;
using System;
namespace Library.Engine.Component {

	/// <summary>
	/// The base for all Components.  It stores the GameObject that owns this Component.
	/// </summary>
	public class BaseComponent {

        private GameObject _owner = null;

		/// <summary>
		/// The GameObject that owns this Component.
		/// </summary>
		public GameObject Owner
        {
            get
            {
                return _owner;
            }
            set
            {
                if (_owner != null)
                    throw new Exception("Can't reset owner in component");
                else
                    _owner = value;
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