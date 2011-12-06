using System;
using System.Collections.Generic;
using System.Windows;
using Library.Engine.Component;
using Library.Engine.Component.Graphic;
using System.Runtime.Serialization;

namespace Library.Engine.Object {

	/// <summary>
	/// GameObject represents a base for all in-game objects. 
	/// GameObject is composed of different Components that provide core game-functionality, such as rendering, audio and movement.
	/// </summary>
	[DataContract]
    public class GameObject {

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

		/// <summary>
		/// The Constructor for a GameObject.
		/// </summary>
		/// <param name="name">The name of the GameObject.</param>
		public GameObject(IDictionary<String, Animation> animations, Animation defaultAnimation, String name) {
			EngineObject.Instance.AddGameObject(this);
			this.Name = name;
			this.UID = NextUID();

			Children = new List<GameObject>();
			RenderComponent = new RenderComponent(animations, defaultAnimation, this);
            EngineObject.Instance.AddGameObject(this);
		}

		/// <summary>
		/// The Constructor for a GameObject. GameObjects are automatically added to the World.
		/// </summary>
		/// <param name="name">The name of the GameObject.</param>
		/// <param name="tag">The tag of the GameObject.</param>
		public GameObject(String name) {
			EngineObject.Instance.AddGameObject(this);
			this.Name = name;
			this.UID = NextUID();

			Children = new List<GameObject>();
            EngineObject.Instance.AddGameObject(this);
		}

		/// <summary>
		/// The Constructor for a GameObject. GameObjects are automatically added to the World.
		/// </summary>
		/// <param name="name">The name of the GameObject.</param>
		/// <param name="position">The starting position of the GameObject.</param>
		/// <param name="rotation">The starting rotation of the GameObject.</param>
		/// <param name="size">The starting size of the GameObject.</param>
		public GameObject(String name, Point position, int rotation, Point size) {
			EngineObject.Instance.AddGameObject(this);
			this.Name = name;
			this.UID = NextUID();

			Children = new List<GameObject>();
			TransformComponent = new TransformComponent(position, rotation, size, this);
            EngineObject.Instance.AddGameObject(this);
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
	}
}