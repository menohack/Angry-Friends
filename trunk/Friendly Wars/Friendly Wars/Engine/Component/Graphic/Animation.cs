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
		public IList<Frame> Frames { get; private set; }

		/// <summary>
		/// The current Frame of this Animation.
		/// </summary>
		public Frame CurrentFrame { get; private set; }

		/// <summary>
		/// The index of the current Frame of this Animation.
		/// </summary>
		private int Index;

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
		/// Is this Animation playing?
		/// </summary>
		public bool IsPlaying { get; private set; }

		/// <summary>
		/// The elapsed time between the previous update (for playing Animations).
		/// </summary>
		private Double ElapsedTime;

		/// <summary>
		/// Constructor for a new Animation.
		/// </summary>
		/// <param name="frames">All the Frames of this Animation.</param>
		/// <param name="length">The length, in seconds, of this Animation.</param>
		/// <param name="FPS">The frames-per-second of this Animation.</param>
		/// <param name="name">The name of this Animation.</param>
		public Animation(IList<Frame> frames, double length, int FPS, String name)
		{
			this.Frames = frames;
			this.Length = length;
			this.FPS = FPS;
			this.Name = name;
			this.Index = 0;
		}

		/// <summary>
		/// Plays this animation.
		/// </summary>
		public void Play()
		{
			IsPlaying = true;
		}

		/// <summary>
		/// Stops playing this Animation.
		/// </summary>
		public void Stop()
		{
			IsPlaying = false;
		}

		/// <summary>
		/// Updates the frame of this Animation.
		/// </summary>
		/// <param name="deltaTime">The time elapsed from the previous update.</param>
		internal void UpdateFrame(Double deltaTime)
		{
			ElapsedTime += deltaTime;
			
			// While frames need to be updated:
			while (ElapsedTime >= (1.00 / FPS))
			{
                //FIXME: Should we be using foreach/enumerators instead of indices? Seems to me like the more C# way. [Max]
				Index++;
				if (Index == Frames.Count)
				{
					Index = 0;
				}
				ElapsedTime -= (1.00 / FPS);
			}

			CurrentFrame = Frames[Index];
		}
	}
}
