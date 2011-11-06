using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Friendly_Wars.Engine.Utilities;
using System.Windows.Media;

namespace Friendly_Wars.Engine.Object
{
	/// <summary>
	/// World can be thought of as the "universe" of a game. 
	/// Not only does it contain all GameObjects, but it also provides functionality for finding GameObjects and drawing GameObjects at appropriate times.
	/// </summary>
	public class World : IUpdateable
	{
		/// <summary>
		/// The compile-time constant for the World object's name.
		/// </summary>
		public readonly String WORLD_NAME = "WORLD";

		//TODO: Unused variable, disabled by James.
		
		/// <summary>
		/// World will try to update this many times per second.
		/// </summary>
		private readonly int UPDATES_PER_SECOND = 60;
		

		//TODO: Unused variable, removed by James.
		
		/// <summary>
		/// The engineTimer that will handle updating this world.
		/// </summary>
		private EngineTimer engineTimer;
		

		/// <summary>
		/// All of the GameObjects in the game.
		/// </summary>
		private IList<GameObject> GameObjects;

		/// <summary>
		/// The queue of GameObjects that need to be re-drawn/updated the next time WordObject updates.
		/// </summary>
		private IList<GameObject> redrawQueue;

		/// <summary>
		/// The list of GameObjects that need to be updated at every interval.
		/// </summary>
		private IList<GameObject> updateableGameObjects;

		/// <summary>
		/// The name of this world.
		/// </summary>
		private String Name { get; set; }

		/// <summary>
		/// The tag for this world.
		/// </summary>
		private String Tag { get; set; }

		/// <summary>
		/// A Label object for rendering the current FPS.
		/// </summary>
		private Label FPSLabel;


		/// <summary>
		/// The constructor for a new instance of World.
		/// </summary>
		/// <param name="name">The name of the world.</param>
		/// <param name="tag">The tag of the world.</param>
		public World(String name, String tag = null) {
			Name = name;
			Tag = tag;

			redrawQueue = new List<GameObject>();
			updateableGameObjects = new List<GameObject>();
			GameObjects = new List<GameObject>();

			//A label for displaying the FPS
			FPSLabel = new Label();
			
			FPSLabel.HorizontalAlignment = HorizontalAlignment.Center;
			FPSLabel.VerticalAlignment = VerticalAlignment.Center;
			FPSLabel.RenderTransform = new TranslateTransform() { X = 300, Y = 300 };
			
			MainPage.mainPage.LayoutRoot.Children.Add(FPSLabel);

			//TODO: DISABLED TEMPORARILY BY JAMES
			// Initialize the timing of the updating of the World.
			engineTimer = new EngineTimer(EngineTimer.FromHertzToMiliSeconds(UPDATES_PER_SECOND), new List<IUpdateable> { this });
		}

		/// <summary>
		/// Start rendering the world.
		/// </summary>
		public void Start()
		{
			engineTimer.Start();
			FPSLabel.Visibility = Visibility.Visible;
		}

		/// <summary>
		/// Stop rendering the world.
		/// </summary>
		public void Stop()
		{
			engineTimer.Stop();
		}

		/// <summary>
		/// Adds a GameObject to the world.
		/// </summary>
		/// <param name="gameObject">The GameObject to add.</param>
		public void AddGameObject(GameObject gameObject)
		{
			if (!GameObjects.Contains(gameObject))
				GameObjects.Add(gameObject);
		}

		/// <summary>
		/// Removes a GameObject from the world, if it exists.
		/// </summary>
		/// <param name="gameObject">The GameObject to remove</param>
		public void RemoveGameObject(GameObject gameObject)
		{
			GameObjects.Remove(gameObject);
		}

		/// <summary>
		/// Adds a GameObject to be updated every frame.
		/// </summary>
		/// <param name="gameObject">The GameObject to update every frame.</param>
		public void AddUpdateableGameObject(GameObject gameObject)
		{
			if (!updateableGameObjects.Contains(gameObject))
			updateableGameObjects.Add(gameObject);
		}

		/// <summary>
		/// Adds a GameObject to the redraw queue.
		/// </summary>
		/// <param name="gameObject">The GameObject to add to the queue.</param>
		public void AddToRedrawQueue(GameObject gameObject) {
			if (!redrawQueue.Contains(gameObject))
				redrawQueue.Add(gameObject);
		}

		/// <summary>
		/// Access all of the GameObjects that contain a specific name.
		/// </summary>
		/// <param name="name">The name of the GameObjects.</param>
		/// <returns>A Collection of GameObjects with that specific name.</returns>
		public ICollection<GameObject> FindGameObjectsWithName(String name)
		{
			ICollection<GameObject> gameObjects = new List<GameObject>();

			foreach (GameObject gameObject in GameObjects)
			{
				if (gameObject.Name == name)
				{
					gameObjects.Add(gameObject);
				}
			}

			return gameObjects;
		}

		/// <summary>
		/// Access all of the GameObjects that contain a specific tag.
		/// </summary>
		/// <param name="tag">The tag of the GameObjects.</param>
		/// <returns>A Collection of GameObjects with that specific tag.</returns>
		public ICollection<GameObject> FindGameObjectsWithTag(String tag)
		{
			ICollection<GameObject> gameObjects = new List<GameObject>();

			foreach (GameObject gameObject in GameObjects)
			{
				if (gameObject.Tag == tag)
				{
					gameObjects.Add(gameObject);
				}
			}

			return gameObjects;
		}

		/// <summary>
		/// Access all of the GameObjects that contain a specific UID.
		/// </summary>
		/// <param name="UID">The UID of the GameObjects.</param>
		/// <returns>A Collection of GameObjects with that specific UID.</returns>
		public ICollection<GameObject> FindGameObjectsWithUID(int UID)
		{
			ICollection<GameObject> gameObjects = new List<GameObject>();

			foreach (GameObject gameObject in GameObjects)
			{
				if (gameObject.UID == UID)
				{
					gameObjects.Add(gameObject);
				}
			}

			return gameObjects;
		}

		/// <summary>
		/// Updates World as fast as possible; however, updating is capped at UPDATES_PER_SECOND.
		/// </summary>
		/// <param name="deltaTime">The time elapsed since the last update.</param>
		public void Update(double deltaTime)
		{
			int FPS = (int)(1000.00 / deltaTime);
			//MainPage.mainPage.LayoutRoot.Children.Clear();

			FPSLabel.Content = "FPS: " + FPS.ToString();

			// Iterate through each GameObject in updateableGameObjects and update each GameObject.
			foreach (GameObject gameObject in updateableGameObjects)
			{
				gameObject.Update(deltaTime);
			}

			// Iterate through each GameObject in the redrawQueue and update each GameObject.
			foreach (GameObject gameObject in redrawQueue)
			{
				//gameObject.Update(deltaTime);
				//redrawQueue.Remove(gameObject);
			}
		}
	}
}
