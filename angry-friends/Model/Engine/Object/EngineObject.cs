using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Windows.Controls;
using Model.Engine.Object.Cameras;
using Model.Engine.Utilities;
using Model.GameLogic;

namespace Model.Engine.Object {

    /// <summary>
    /// EngineHelper assists by encapsulating required fields for the creation of EngineObject.
    /// </summary>
    public class EngineObjectHelper
    {
        /// <summary>
        /// The accessor for the Viewport of EngineObjectHelper.
        /// </summary>
        public static Viewport Viewport { get; private set; }

        /// <summary>
        /// The constructor for a new EngineObjectHelper.
        /// </summary>
        /// <param name="canvas"></param>
        public EngineObjectHelper(Canvas canvas)
        {
            Viewport = new Viewport();
            canvas.Children.Add(Viewport);
            Game game = new Game();
        }
    }

	/// <summary>
	/// EngineObject is the universe for all GameObjects.
	/// </summary>
    [DataContract]
	public class EngineObject : IUpdateable {

        /// <summary>
        /// The singleton instance of EngineObject.
        /// </summary>
        private static EngineObject instance;

        /// <summary>
        /// The singleton accessor for EngineObject.
        /// </summary>
        public static EngineObject Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EngineObject(EngineObjectHelper.Viewport);
                }
                return instance;
            }
        }

		/// <summary>
		/// The EngineTimer that will handle updating the EngineObject.
		/// </summary>
        [DataMember]
		private EngineTimer worldUpdateTimer;

		/// <summary>
		/// EngineObject will try to update at a fixed interval.
		/// </summary>
        [DataMember]
		private readonly int UPDATES_PER_SECOND = 30;

		/// <summary>
		/// All of the GameObjects this EngineObject has.
		/// </summary>
        [DataMember]
		private IList<GameObject> gameObjects;

        /// <summary>
        /// The Camera that controls moving the Viewport of this EngineObject.
        /// </summary>
        [DataMember]
        public Camera Camera { get; private set; }

        /// <summary>
        /// The Input for this EngineObject.
        /// </summary>
        public Input Input { get; private set; }

		/// <summary>
		/// The constructor for a new instance of EngineObject.
		/// EngineObject uses the singleton pattern; therefore, its constructor's scope is limited to itself.
		/// </summary>
		private EngineObject(Viewport viewport) {
			gameObjects = new List<GameObject>();
            Camera = new Camera(viewport);
			Input = Input.Instance;

			// Initialize the timing of the updating of the World.
			worldUpdateTimer = new EngineTimer(EngineTimer.FromHertzToMiliSeconds(UPDATES_PER_SECOND), new List<IUpdateable> { this });
			worldUpdateTimer.Start();
		}

		/// <summary>
        /// Updates EngineObject as fast as possible; however, updating is capped at UPDATES_PER_SECOND.
		/// </summary>
		/// <param name="deltaTime">The time elapsed, in milliseconds, since the last update.</param>
		public void Update(double deltaTime) {
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Update(deltaTime);
            }
            Camera.Viewport.RedrawGameObjects();
		}

		/// <summary>
        /// Access the ReadOnlyCollection of GameObjects this EngineObject has.
		/// </summary>
        /// <returns>The ReadOnlyCollection of GameObjects this EngineObject has.</returns>
		public ReadOnlyCollection<GameObject> GetGameObjects() {
			return new ReadOnlyCollection<GameObject>(gameObjects);
		}
		/// <summary>
        /// Adds a GameObject to this EngineObject.
		/// </summary>
		/// <param name="gameObject">The GameObject to add.</param>
		public void AddGameObject(GameObject gameObject) {
			if (!gameObjects.Contains(gameObject)) {
				gameObjects.Add(gameObject);
                Camera.Viewport.AddGameObjectToRedrawQueue(gameObject);
			}
		}
		/// <summary>
        /// Removes a GameObject from this EngineObject, if it exists.
		/// </summary>
		/// <param name="gameObject">The GameObject to remove</param>
		public void RemoveGameObject(GameObject gameObject) {
			gameObjects.Remove(gameObject);
		}

		/// <summary>
		/// Access all of the GameObjects that contain a specific name.
		/// </summary>
		/// <param name="name">The name of the GameObjects.</param>
		/// <returns>A Collection of GameObjects with that specific name.</returns>
		public ICollection<GameObject> FindGameObjectsWithName(String name) {
			ICollection<GameObject> gameObjects = new List<GameObject>();

			foreach (GameObject gameObject in gameObjects) {
				if (gameObject.Name == name) {
					gameObjects.Add(gameObject);
				}
			}

			return gameObjects;
		}

		/// <summary>
		/// Access the GameObject that contains a specific UID.
		/// </summary>
		/// <param name="UID">The UID of the gameObject.</param>
		/// <returns>The GameObject with the specific UID.</returns>
		public GameObject FindGameObjectWithUID(int UID) {
			foreach (GameObject gameObject in gameObjects) {
				if (gameObject.UID == UID) {
					return gameObject;
				}
			}
			return null;
		}
	}
}