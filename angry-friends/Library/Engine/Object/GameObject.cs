﻿using System;
using System.Collections.Generic;
using System.Windows;
using Library.Engine.Component;
using Library.Engine.Component.Graphic;
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
		[DataMember]
        public TransformComponent TransformComponent { get; set; }

		/// <summary>
		/// The GameObject's AudioComponent.
		/// </summary>
        [DataMember]
        public AudioComponent AudioComponent { get; set; }

		/// <summary>
		/// This GameObject's RenderComponent.
		/// </summary>
        [DataMember]
        public RenderComponent RenderComponent { get; set; }

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

		//TODO: GameObjectFactory.

        public GameObject(string name)
        {
			this.Name = name;
        }

        public GameObject(string name, TransformComponent tc, AudioComponent ac, RenderComponent rc)
        {
            EngineObject.Instance.AddGameObject(this);
            Name = name;
            UID = NextUID();
            Children = new List<GameObject>();
            TransformComponent = tc;
            TransformComponent.Owner = this;
            AudioComponent = ac;
            AudioComponent.Owner = this;
            RenderComponent = rc;
            RenderComponent.Owner = this;
        }



		/// <summary>
		/// Creates a UID for a GameObject.
		/// </summary>
		/// <returns>Returns a UID for a GameObject</returns>
		private static int NextUID() {
			return ++currentUID;
		}

		/// <summary>
		/// Destroys a given GameObject.
		/// </summary>
		/// <param name="gameObject">The GameObject that will be destroyed.</param>
		public static void Destroy(GameObject gameObject) {
			EngineObject.Instance.RemoveGameObject(gameObject);
		}

        /// <summary>
        /// Updates this GameObject everytime EngineObject updates.
        /// </summary>
        /// <param name="deltaTime">The time, in milliseconds, since the last update.</param>
		public virtual void Update(double deltaTime)
		{
		}

        private TransformComponent CreateDefaultTransformComponent()
        {
			TransformComponent transformComponent = new TransformComponent(new Point(1, 1), 0, new Point(50, 50), this);
			return transformComponent;
        }
    }
}