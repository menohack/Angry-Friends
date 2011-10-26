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
using Friendly_Wars.Engine.Object;
using System.Windows.Threading;

namespace Friendly_Wars.Engine.Component.Graphic
{
    /// <summary>
    /// Animation contains all data pertaining to an animation.
    /// </summary>
    public class Animation
    {

        /// <summary>
        /// All the Frames of this Animation.
        /// </summary>
        public ICollection<Frame> frames { get; private set; }

        /// <summary>
        /// The current Frame of this Animation.
        /// </summary>
        public Frame currentFrame { get; private set; }

        /// <summary>
        /// The length, in seconds, of this Animation. 
        /// </summary>
        public Double length { get; private set; }

        /// <summary>
        /// The frames-per-second of this Animation.
        /// </summary>
        public int FPS { get; private set; }

        /// <summary>
        /// The name of this Animation.
        /// </summary>
        public String name { get; private set; }

        /// <summary>
        /// Is this Animation playing?
        /// </summary>
        public bool isPlaying { get; private set; }

        /// <summary>
        /// Is this the default Animation?
        /// </summary>
        public bool isDefaultAnimation { get; private set; }

        /// <summary>
        /// The timer that handles updating this Animation's Frames.
        /// </summary>
        private static DispatcherTimer updateTimer;

        /// <summary>
        /// Constructor for a new Animation.
        /// </summary>
        /// <param name="frames">All the Frames of this Animation.</param>
        /// <param name="length">The length, in seconds, of this Animation.</param>
        /// <param name="FPS">The frames-per-second of this Animation.</param>
        /// <param name="name">The name of this Animation.</param>
        public Animation(ICollection<Frame> frames, Double length, int FPS, String name, bool isDefaultAnimation)
        {
            this.frames = frames;
            this.length = length;
            this.FPS = FPS;
            this.name = name;
            this.isDefaultAnimation = isDefaultAnimation;
        }

        /// <summary>
        /// Plays this animation.
        /// </summary>
        public void Play()
        {
            isPlaying = true;
        }

        /// <summary>
        /// Stops playing this Animation.
        /// </summary>
        public void Stop()
        {
            isPlaying = false;
        }
    }
}
