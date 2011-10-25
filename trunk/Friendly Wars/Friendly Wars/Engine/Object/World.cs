using System;
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

namespace Friendly_Wars.Engine.Object
{
    /// <summary>
    /// World can be thought of the "universe" of a game. 
    /// Not only does it contains all GameObjects, but it also provides functionality for finding GameObjects and drawing GameObjects at appropriate times.
    /// </summary>
    public class World : GameObject
    {
        /// <summary>
        /// The compile-time constant for the World object's name.
        /// </summary>
        public static readonly String WORLD_NAME = "WORLD";

        /// <summary>
        /// The timer that handles updating World.
        /// </summary>
        private DispatcherTimer timer;

        /// <summary>
        /// 30 FPS, or 33 ms between each frame. 
        /// </summary>
        private static readonly int INTERVAL = 33;

        /// <summary>
        /// The DateTime associated with the last frame.
        /// </summary>
        private DateTime previousTime;

        /// <summary>
        /// The change in time from the previous frame, in miliseconds.
        /// </summary>
        private int deltaTime = 0;

        /// <summary>
        /// The queue of GameObjects that need to be re-drawn/updated the next time WordObject updates.
        /// </summary>
        private ICollection<GameObject> redrawQueue;

        /// <summary>
        /// The list of GameObjects that need to be updated at every interval.
        /// </summary>
        private IList<GameObject> updateableGameObjects;

        /// <summary>
        /// The constructor for a new instance of World.
        /// </summary>
        /// <param name="name">The name of</param>
        /// <param name="tag"></param>
        public World(String name, String tag = null) : base(name, tag) {
            redrawQueue = new List<GameObject>();
            updateableGameObjects = new List<GameObject>();
            previousTime = DateTime.Now;

            // Begin updating the world.
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(INTERVAL);
            timer.Tick += new EventHandler(Update);
            timer.Start();
        }

        /// <summary>
        /// Updates the World as fast as possible; updating is capped at 1000/INTERVAL times per second.
        /// The Update is frame-rate-independent and will account for a drop in frame rate.
        /// </summary>
        /// <param name="sender">The Object that called this function.</param>
        /// <param name="e">The event that corresponds to this function.</param>
        void Update(object sender, EventArgs e)
        {
            DateTime currentTime = DateTime.Now;

            deltaTime = currentTime.Millisecond - previousTime.Millisecond;
            // If we elapsed one second
            if (deltaTime <= 0)
            {
                deltaTime = 1000 - previousTime.Millisecond + currentTime.Millisecond;
            }

            previousTime = DateTime.Now;
        }
    }
}
