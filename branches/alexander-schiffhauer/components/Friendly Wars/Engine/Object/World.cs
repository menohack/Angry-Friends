using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Friendly_Wars.Engine.Utilities;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Friendly_Wars.Engine.Component.Graphic;

namespace Friendly_Wars.Engine.Object
{
	/// <summary>
	/// World can be thought of as the "universe" of a game. 
	/// Not only does it contain all gameObjects, but it also provides functionality for finding gameObjects and drawing gameObjects at appropriate times.
	/// </summary>
	public class World : IUpdateable
	{

		/// <summary>
		/// The single instsance of world.
		/// </summary>
		private static World instance;

		/// <summary>
		/// The EngineTimer that will handle updating this world.
		/// </summary>
		private EngineTimer worldUpdateTimer;
		/// <summary>
		/// World will try to update this many times per second.
		/// </summary>
		private readonly int UPDATES_PER_SECOND = 60;
		
		/// <summary>
		/// All of the gameObjects in the game.
		/// </summary>
		private IList<GameObject> gameObjects;
		/// <summary>
		/// The queue of RenderComponents that need to be re-drawn/updated the next time WordObject updates.
		/// </summary>
		private IList<RenderComponent> redrawQueue;

		/// <summary>
		/// The constructor for a new instance of World.
		/// World uses the singleton pattern; therefore, its constructor's scope is limited to itself.
		/// </summary>
		private World() {
			gameObjects = new List<GameObject>();
			redrawQueue = new List<RenderComponent>();
			
			// Initialize the timing of the updating of the World.
			worldUpdateTimer = new EngineTimer(EngineTimer.FromHertzToMiliSeconds(UPDATES_PER_SECOND), new List<IUpdateable> { this });
			worldUpdateTimer.Start();
		}

		/// <summary>
		/// Accessor for the instance of World.
		/// This accessor follows the singleton pattern for C# provided at Microsoft's MSDN.
		/// </summary>
		public static World Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new World();
				}
				return instance;
			}
		}

		/// <summary>
		/// Updates World as fast as possible; however, updating is capped at UPDATES_PER_SECOND.
		/// </summary>
		/// <param name="deltaTime">The time elapsed since the last update.</param>
		public void Update(double deltaTime)
		{
			Debug.WriteLine("FPS: " + Convert.ToInt32(1000.00 / deltaTime).ToString());

			// Iterate through each RenderComponent in the redrawQueue and redraw it.
			foreach (RenderComponent renderComponent in redrawQueue)
			{
				//Remove previous frame.
				//Draw(renderComponent.CurrentAnimation.CurrentFrame);
				//redrawQueue.Remove(renderComponent);
			}
		}

		/// <summary>
		/// Adds a RenderComponent to the redraw queue.
		/// </summary>
		/// <param name="gameObject">The RenderComponent to add to the queue.</param>
		public void AddToRedrawQueue(RenderComponent renderComponent)
		{
			if (!redrawQueue.Contains(renderComponent))
			{
				redrawQueue.Add(renderComponent);
			}
		}

		/// <summary>
		/// Access the ReadOnlyCollection of GameObjects this World has.
		/// </summary>
		/// <returns>The ReadOnlyCollection of GameObjects this World has.</returns>
		public ReadOnlyCollection<GameObject> GetGameObjects()
		{
			return (ReadOnlyCollection<GameObject>) gameObjects;
		}

		/// <summary>
		/// Adds a GameObject to the world.
		/// </summary>
		/// <param name="gameObject">The GameObject to add.</param>
		public void AddGameObject(GameObject gameObject)
		{
			if (!gameObjects.Contains(gameObject))
			{
				gameObjects.Add(gameObject);
			}
		}

		/// <summary>
		/// Removes a GameObject from the world, if it exists.
		/// </summary>
		/// <param name="gameObject">The GameObject to remove</param>
		public void RemoveGameObject(GameObject gameObject)
		{
			gameObjects.Remove(gameObject);
		}

		/// <summary>
		/// Access all of the GameObjects that contain a specific name.
		/// </summary>
		/// <param name="name">The name of the GameObjects.</param>
		/// <returns>A Collection of GameObjects with that specific name.</returns>
		public ICollection<GameObject> FindGameObjectsWithName(String name)
		{
			ICollection<GameObject> gameObjects = new List<GameObject>();

			foreach (GameObject gameObject in gameObjects)
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

			foreach (GameObject gameObject in gameObjects)
			{
				if (gameObject.Tag == tag)
				{
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
		public GameObject FindGameObjectWithUID(int UID)
		{
			foreach (GameObject gameObject in gameObjects)
			{
				if (gameObject.UID == UID)
				{
					return gameObject;
				}
			}
			return null;
		}
	}
}
