﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Model.Engine.Component.Media;
using Model.Engine.Component.Media.Rendering;
using Model.Engine.Component.Transform;
using Model.Engine.Utilities;

namespace Model.Engine.Object {

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

			private set 
            {
                if (transformComponent == null)
                {
                    transformComponent = value;
                    if(value != null)
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
                    if(value != null)
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
                    if(value != null)
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
		/// The last UID assigned to a GameObject.
		/// </summary>
        [DataMember]
        private static int currentUID = 0;

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