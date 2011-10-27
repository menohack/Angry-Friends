using Friendly_Wars.Engine.Object;

namespace Friendly_Wars.Engine.Component
{
	/// <summary>
	/// BaseComponent is the base for all components, which provides the ability to enable and disable components.
	/// BaseComponent also contains the owner, in the form of a GameObject, for this component.
	/// </summary>
	public class BaseComponent
	{
		/// <summary>
		///	Is this BaseComponent enabled?
		/// </summary>
		public bool IsEnabled { get; private set; }

		/// <summary>
		/// The GameObject that is the owner of this component.
		/// </summary>
		public GameObject Owner { get; private set; }

		/// <summary>
		/// Creates a new instance of a BaseComponent.
		/// </summary>
		/// <param name="owner">The owner of this BaseComponent.</param>
		public BaseComponent(GameObject owner)
		{
			this.Owner = owner;
			IsEnabled = true;
		}

		/// <summary>
		/// Disable this BaseComponent.
		/// </summary>
		public void Disable()
		{
			IsEnabled = false;
		}

		/// <summary>
		/// Enable this BaseComponent.
		/// </summary>
		public void Enable()
		{
			IsEnabled = true;
		}
	}
}
