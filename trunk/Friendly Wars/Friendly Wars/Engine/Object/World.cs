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
        /// 60 FPS, or 17 ms between each frame.  Might not be able to actually run at 60 FPS.
        /// TODO: Figure out how to call a function as quickly as possible.
        /// </summary>
        private static readonly int INTERVAL = 17;  

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

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(INTERVAL);
            timer.Tick += new EventHandler(timer_Update);
            timer.Start();
        }

        void timer_Update(object sender, EventArgs e)
        {
            System.Console.WriteLine("Hello, World!");
        }
    }
}
