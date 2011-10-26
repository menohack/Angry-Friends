﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Windows.Threading;
using System.Diagnostics;
using Friendly_Wars.Engine.Ultilities;

namespace Friendly_Wars.Engine.Object
{
    /// <summary>
    /// World can be thought of as the "universe" of a game. 
    /// Not only does it contains all GameObjects, but it also provides functionality for finding GameObjects and drawing GameObjects at appropriate times.
    /// </summary>
    public class World : GameObject, IUpdateable
    {
        /// <summary>
        /// The compile-time constant for the World object's name.
        /// </summary>
        public static readonly String WORLD_NAME = "WORLD";

        /// <summary>
        /// World will try to update 30 times per second.
        /// </summary>
        private static readonly int UPDATES_PER_SECOND = 30;

        /// <summary>
        /// The EngineTimer that will handle updating this world.
        /// </summary>
        private EngineTimer engineTimer;

        /// <summary>
        /// All of the GameObjects in the game.
        /// </summary>
        private static ICollection<GameObject> gameObjects;

        /// <summary>
        /// The queue of GameObjects that need to be re-drawn/updated the next time WordObject updates.
        /// </summary>
        private static ICollection<GameObject> redrawQueue;

        /// <summary>
        /// The list of GameObjects that need to be updated at every interval.
        /// </summary>
        private static ICollection<GameObject> updateableGameObjects;


        /// <summary>
        /// The constructor for a new instance of World.
        /// </summary>
        /// <param name="name">The name of</param>
        /// <param name="tag"></param>
        public World(String name, String tag = null) : base(name, tag) {
            redrawQueue = new List<GameObject>();
            updateableGameObjects = new List<GameObject>();
            gameObjects = new List<GameObject>();

            // Initialize the timing of the updating of the World.
            engineTimer = new EngineTimer(EngineTimer.FromHertzToMiliSeconds(UPDATES_PER_SECOND), new List<IUpdateable> { this });
            engineTimer.Start();
        }

        /// <summary>
        /// Adds a GameObject to the redraw queue.
        /// </summary>
        /// <param name="gameobject">The GameObject to add to the queue.</param>
        public void AddToRedrawQueue(GameObject gameobject) {
            redrawQueue.Add(gameobject);
        }

        /// <summary>
        /// Access all of the GameObjects that contain a specific name.
        /// </summary>
        /// <param name="name">The name of the GameObjects.</param>
        /// <returns>A Collection of GameObjects with that specific name.</returns>
        public static ICollection<GameObject> FindGameObjectsWithName(String name)
        {
            ICollection<GameObject> gameObjects = new List<GameObject>();

            foreach (GameObject gameObject in World.gameObjects)
            {
                if (gameObject.name == name)
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
        public static ICollection<GameObject> FindGameObjectsWithTag(String tag)
        {
            ICollection<GameObject> gameObjects = new List<GameObject>();

            foreach (GameObject gameObject in World.gameObjects)
            {
                if (gameObject.tag == tag)
                {
                    gameObjects.Add(gameObject);
                }
            }

            return gameObjects;
        }

        /// <summary>
        /// Access all of the GameObjects that contain a specific tag.
        /// </summary>
        /// <param name="UID">The UID of the GameObjects.</param>
        /// <returns>A Collection of GameObjects with that specific tag.</returns>
        public static ICollection<GameObject> FindGameObjectsWithUID(int UID)
        {
            ICollection<GameObject> gameObjects = new List<GameObject>();

            foreach (GameObject gameObject in World.gameObjects)
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
            Debug.WriteLine("World is being updated!");

            // Iterate through each GameObject in updateableGameObjects and update each GameObject.
            foreach (GameObject gameObject in updateableGameObjects)
            {
                //gameObject.renderComponent.forceRedraw();
            }

            // Iterate through each GameObject in the redrawQueue and update each GameObject.
            foreach (GameObject gameObject in redrawQueue)
            {
                //gameObject.renderComponent.forceRedraw();
            }
        }
    }
}
