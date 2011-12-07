using System;
using System.Collections.Generic;
using System.Windows;
using Library.Engine.Component;
using Library.Engine.Component.Graphic;
using System.Windows.Media;
using System.Runtime.Serialization;
using Library.Engine.Utilities;

namespace Library.Engine.Object {

	/// <summary>
	/// GameObject represents a base for all in-game objects. 
	/// GameObject is composed of different Components that provide core game-functionality, such as rendering, audio and movement.
	/// </summary>
	[DataContract]
    public class GameObject : IUpdateable {

        /// <summary>
        /// This GameObject's TransformComponent.
        /// </summary>
        private TransformComponent transformComponent;

		/// <summary>
		/// This GameObject's TransformComponent.
		/// </summary>
        [DataMember]
        public TransformComponent TransformComponent 
        {
            get
            {
                return transformComponent;
            }

			protected set 
            {
                if (transformComponent == null)
                {
                    transformComponent = value;
                    TransformComponent.Owner = this;
                }
            } 
        }

        /// <summary>
        /// This GameObject's AudioComponent.
        /// </summary>
        private AudioComponent audioComponent;


		/// <summary>
		/// The GameObject's AudioComponent.
		/// </summary>
        [DataMember]
        public AudioComponent AudioComponent 
        {
            get
            {
                return audioComponent;
            }

            private set
            {
                if (audioComponent == null)
                {
                    audioComponent = value;
                    AudioComponent.Owner = this;
                }
            } 
        }

        /// <summary>
        /// This GameObject's AudioComponent.
        /// </summary>
        private RenderComponent renderComponent;

		/// <summary>
		/// This GameObject's RenderComponent.
		/// </summary>
        [DataMember]
        public RenderComponent RenderComponent
        {
            get
            {
                return renderComponent;
            }

            private set 
            {
                if (renderComponent == null)
                {
                    renderComponent = value;
                    RenderComponent.Owner = this;
                }
            } 
        }

		/// <summary>
		/// This GameObject's name.
		/// </summary>
        [DataMember]
        public String Name { get; private set; }

		/// <summary>
		/// This GameObject's UID.
		/// </summary>
        [DataMember]
        public int UID { get; private set; }

        /// <summary>
        /// This GameObject's children.
        /// </summary>
        [DataMember]
        public IList<GameObject> Children { get; private set; }

		/// <summary>
		/// The last UID assigned to a GameObject.
		/// </summary>
        [DataMember]
        private static int currentUID;

        /// <summary>
        /// The Constructor for a new GameObject.
        /// </summary>
        /// <param name="name">The GameObject's name.</param>
        /// <param name="transformComponent">The GameObject's TransformComponent.</param>
        /// <param name="audioComponent">The GameObject's AudioComponent.</param>
        /// <param name="renderComponent">The GameObject's RenderComponent.</param>
        public GameObject(String name, TransformComponent transformComponent, AudioComponent audioComponent, RenderComponent renderComponent)
        {
            UID = NextUID();
            Name = name;
            Children = new List<GameObject>();

            TransformComponent = transformComponent;
            AudioComponent = audioComponent;
            RenderComponent = renderComponent;

            EngineObject.Instance.AddGameObject(this);
        }

        /// <summary>
        /// Updates this GameObject everytime EngineObject updates.
        /// </summary>
        /// <param name="deltaTime">The time, in milliseconds, since the last update.</param>
        public virtual void Update(double deltaTime) {}

		/// <summary>
		/// Creates a UID for a GameObject.
		/// </summary>
		/// <returns>Returns a UID for a GameObject</returns>
		private static int NextUID() {
			return ++currentUID;
		}
    }
}