using System;
using System.Collections.Generic;
using System.Windows;
using Friendly_Wars.Engine.Utilities;

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
		/// The index of the current Frame of this Animation.
		/// </summary>
		private int index;

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
		private Double elapsedTime;

		private EngineTimer frameTimer;

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
			index = 0;

			IsPlaying = false;

			//Should I have this test?
			if (frames.Count > 0)
			{
				CurrentFrame = frames[0];
				frameTimer = new EngineTimer(Length, this);
			}
		}

		/// <summary>
		/// Constructor for a new Animation with a single frame.
		/// </summary>
		/// <param name="frame">The frame of the animation.</param>
		/// <param name="name">The name of the animation.</param>
		public Animation(Frame frame, String name)
		{
			Frames = new List<Frame>();
			Frames.Add(frame);
			Length = 0.0;
			FPS = 0;
			Name = name;
			index = 0;

			IsPlaying = false;

			CurrentFrame = frame;
			frameTimer = new EngineTimer(Length, this);
		}

		/// <summary>
		/// Plays this animation.
		/// </summary>
		public void Play()
		{
			//Should I have this test?
			if (CurrentFrame == null)
				return;

			CurrentFrame.Draw();
			IsPlaying = true;
			frameTimer.Start();
		}

		/// <summary>
		/// Stops playing this Animation.
		/// </summary>
		public void Stop()
		{
			if (CurrentFrame == null)
				return;
			CurrentFrame.Hide();
			IsPlaying = false;
			frameTimer.Stop();
		}

		//TODO: I broke this function into two functions for the two possible timers.
		/// <summary>
		/// Updates the frame of this Animation.
		/// </summary>
		/// <param name="deltaTime">The time in milliseconds from the previous update.</param>
		internal void UpdateFrame(Double deltaTime)
		{
			elapsedTime += deltaTime;

			if (Frames.Count < 2)
				return;

			//TODO: Removed by James. What were you trying to do?
			/*
			// While frames need to be updated:
			while (elapsedTime >= (1.00 / FPS))
			{
				//FIXME: Should we be using foreach/enumerators instead of indices? Seems to me like the more C# way. [Max]
				index++;
				if (index == Frames.Count)
					index = 0;

				elapsedTime -= (1.00 / FPS);
			}
			*/

			if (elapsedTime > 1e20)
				elapsedTime -= 1e20;
		}

		/// <summary>
		/// Updates to the correct frame of the animation.
		/// </summary>
		/// <param name="deltaTime">The time since the last update.</param>
		public void Update(double deltaTime)
		{
			if (Frames.Count < 2)
				return;

			//TODO: I am not sure which one of these is more correct. Feel free to change it.
			//index = (int)(elapsedTime / Length) % Frames.Count;
			index = (index + (int)(deltaTime / Length)) % Frames.Count;

			//Hide the old frame
			CurrentFrame.Hide();

			//Switch to the new frame
			CurrentFrame = Frames[index];

			//Render the new frame
			CurrentFrame.Draw();
		}
	}
}
