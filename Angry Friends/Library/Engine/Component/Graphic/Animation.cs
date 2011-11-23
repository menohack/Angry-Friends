using System;
using System.Collections.Generic;
using Library.Engine.Utilities;
namespace Library.Engine.Component.Graphic {
	/// <summary>
	/// Contains all data pertaining to a series of Frames representing an Animation.
	/// </summary>
	public class Animation : IUpdateable {
		/// <summary>
		/// All the Frames of this Animation.
		/// </summary>
		private IList<Frame> frames;
		/// <summary>
		/// The current Frame of this Animation.
		/// </summary>
		public Frame CurrentFrame { get; private set; }
		/// <summary>
		/// The index for the current Frame of this Animation.
		/// </summary>
		private int index;

		/// <summary>
		/// The length, in miliseconds, of this Animation. 
		/// </summary>
		private Double length;
		/// <summary>
		/// The frames-per-second of this Animation.
		/// </summary>
		public int FPS { get; private set; }
		/// <summary>
		/// The name of this Animation.
		/// </summary>
		public String Name { get; private set; }

		/// <summary>
		/// Constructor for a new Animation with multiple frames.
		/// </summary>
		/// <param name="frames">All the Frames of this Animation.</param>
		/// <param name="length">The length, in seconds, of this Animation.</param>
		/// <param name="fps">The frames-per-second of this Animation.</param>
		/// <param name="name">The name of this Animation.</param>
		public Animation(IList<Frame> frames, double length, int fps, String name) {
			this.frames = frames;
			this.length = length;
			FPS = fps;
			this.Name = name;

			index = 0;
			this.CurrentFrame = frames[index];
		}

		/// <summary>
		/// Constructor for a new Animation with a single frame.
		/// </summary>
		/// <param name="frame">The Frame of the Animation.</param>
		/// <param name="name">The name of the Animation.</param>
		public Animation(Frame frame, String name) : this(new List<Frame> { frame }, 0.0, 0, name) {}

		/// <summary>
		/// Updates the Frame of this Animation.
		/// </summary>
		/// <param name="deltaTime">The time in milliseconds from the previous update.</param>
		public void Update(Double deltaTime) {
			index = (index + (int)(deltaTime / length)) % frames.Count;
			CurrentFrame = frames[index];
		}
	}
}