using System;
using System.Collections.Generic;

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
		public IList<Frame> frames { get; private set; }

		/// <summary>
		/// The current Frame of this Animation.
		/// </summary>
		public Frame currentFrame { get; private set; }

		/// <summary>
		/// The index of the current Frame of this Animation.
		/// </summary>
		private int index;

		/// <summary>
		/// The length, in miliseconds, of this Animation. 
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
		/// The elapsed time between the previous update (for playing Animations).
		/// </summary>
		private Double elapsedTime;

        /// <summary>
        /// Whether the animation is the default
        /// </summary>
        public bool isDefaultAnimation { get; private set; }

		/// <summary>
		/// Constructor for a new Animation.
		/// </summary>
		/// <param name="frames">All the Frames of this Animation.</param>
		/// <param name="length">The length, in seconds, of this Animation.</param>
		/// <param name="FPS">The frames-per-second of this Animation.</param>
		/// <param name="name">The name of this Animation.</param>
		public Animation(IList<Frame> frames, double length, int FPS, String name)
		{
			this.frames = frames;
			this.length = length;
			this.FPS = FPS;
			this.name = name;
			index = 0;
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

		/// <summary>
		/// Updates the frame of this Animation.
		/// </summary>
		/// <param name="deltaTime">The time elapsed from the previous update.</param>
		internal void UpdateFrame(Double deltaTime)
		{
			elapsedTime += deltaTime;
			
			// While frames need to be updated:
			while (elapsedTime >= (1.00 / FPS))
			{
				index++;
				if (index == frames.Count)
				{
					index = 0;
				}
				elapsedTime -= (1.00 / FPS);
			}

			currentFrame = frames[index];
		}
	}
}
