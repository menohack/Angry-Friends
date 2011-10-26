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
using Friendly_Wars.Engine.Ultilities;

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
		/// The elapsed time from the last Update that needs to be accounted for animations.
		/// </summary>
		private Double elapsedTime;

		/// <summary>
		/// Constructor for a new Animation.
		/// </summary>
		/// <param name="frames">All the Frames of this Animation.</param>
		/// <param name="length">The length, in seconds, of this Animation.</param>
		/// <param name="FPS">The frames-per-second of this Animation.</param>
		/// <param name="name">The name of this Animation.</param>
		public Animation(IList<Frame> frames, Double length, int FPS, String name, bool isDefaultAnimation)
		{
			this.frames = frames;
			this.length = length;
			this.FPS = FPS;
			this.name = name;
			this.isDefaultAnimation = isDefaultAnimation;
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
				elapsedTime -= (1.00 / FPS);
			}

			if (index == frames.Count)
			{
				index = 0;
			}

			currentFrame = frames[index];
		}
	}
}
