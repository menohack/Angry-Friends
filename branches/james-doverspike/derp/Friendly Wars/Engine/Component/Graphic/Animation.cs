using System;
using System.Collections.Generic;
using System.Windows;
using Friendly_Wars.Engine.Utilities;
using System.Collections;

namespace Friendly_Wars.Engine.Component.Graphic
{
	/// <summary>
	/// Animation contains all data pertaining to an animation.
	/// </summary>
	public class Animation : IUpdateable
	{
		/// <summary>
		/// All the Frames of this Animation.
		/// </summary>
		public IList<Frame> Frames { get; private set; }

		/// <summary>
		/// The current Frame of this Animation.
		/// </summary>
		public Frame CurrentFrame { get; private set; }

		/// <summary>
		/// The enumerator for the current Frame of this Animation.
		/// </summary>
		private IEnumerator index;

		/// <summary>
		/// The length, in miliseconds, of this Animation. 
		/// </summary>
		public Double Length { get; private set; }

		/// <summary>
		/// The frames-per-second of this Animation.
		/// </summary>
		public int FPS { get; private set; }

		/// <summary>
		/// The name of this Animation.
		/// </summary>
		public String Name { get; private set; }

		/// <summary>
		/// The elapsed time between the previous update (for playing Animations).
		/// </summary>
		private Double elapsedTime;

		/// <summary>
		/// Constructor for a new Animation with multiple frames.
		/// </summary>
		/// <param name="frames">All the Frames of this Animation.</param>
		/// <param name="length">The length, in seconds, of this Animation.</param>
		/// <param name="fps">The frames-per-second of this Animation.</param>
		/// <param name="name">The name of this Animation.</param>
		public Animation(IList<Frame> frames, double length, int fps, String name)
		{
			Frames = frames;
			Length = length;
			FPS = fps;
			Name = name;
			index = Frames.GetEnumerator();
		}

		/// <summary>
		/// Constructor for a new Animation with a single frame.
		/// </summary>
		/// <param name="frame">The frame of the animation.</param>
		/// <param name="name">The name of the animation.</param>
		public Animation(Frame frame, String name) : this(new List<Frame> {frame}, 0.0, 0, name )
		{}

		/// <summary>
		/// Updates the frame of this Animation.
		/// </summary>
		/// <param name="deltaTime">The time in milliseconds from the previous update.</param>
		public void Update(Double deltaTime)
		{
			elapsedTime += deltaTime;

			// While frames need to be updated:
			while (elapsedTime >= (1.00 / FPS))
			{
				if (!index.MoveNext())
				{
					index.Reset();
					index.MoveNext();
				}

				elapsedTime -= (1.00 / FPS);
			}
		}
	}
}
