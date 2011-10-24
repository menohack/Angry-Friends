﻿using Friendly_Wars.Engine.Object;

namespace Friendly_Wars.Engine.Component
{
    /// <summary>
    /// BaseComponent is the base for all components, which provides the ability to enable and disable components.
    /// </summary>
    public class BaseComponent
    {
        /// <summary>
        ///   Is this BaseComponent enabled?
        /// </summary>
        private bool isEnabled;

        /// <summary>
        /// The GameObject that is the owner of this component.
        /// </summary>
        private GameObject owner;

        /// <summary>
        /// Creates a new instance of a BaseComponent.
        /// </summary>
        /// <param name="owner">The owner of this BaseComponent.</param>
        public BaseComponent(GameObject owner)
        {
            this.owner = owner;
            this.isEnabled = true;
        }

        /// <summary>
        /// Disable this BaseComponent.
        /// </summary>
        public void Disable()
        {
            isEnabled = false;
        }

        /// <summary>
        /// Enable this BaseComponent.
        /// </summary>
        public void Enable()
        {
            isEnabled = true;
        }
    }
}