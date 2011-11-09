using System;
using System.Windows.Controls;
using Friendly_Wars.Engine.Object;
using System.Collections.Generic;
using System.Diagnostics;

namespace Friendly_Wars.Engine.Component
{
	/// <summary>
	/// Handles the control of a GameObject's audio.
	/// </summary>
	public class AudioComponent
	{
		/// <summary>
		/// A Dictionary between the names of audio and the audio, itself.
		/// </summary>
		private IDictionary<String, MediaElement> audioClips;

		/// <summary>
		/// Constructor for an audioComponent.
		/// </summary>
		/// <param name="audioClips">The clips that the audioComponent is capable of playing.</param>
		public AudioComponent(IDictionary<String, MediaElement> audioClips)
		{
			this.audioClips = audioClips;
		}

		/// <summary>
		/// Plays a specific audio clip.
		/// </summary>
		/// <param name="name">The name of the audio clip to play.</param>
		public void Play(String name)
		{
			MediaElement audioClip;
			Debug.Assert(audioClips.TryGetValue(name, out audioClip), "This AudioClip " + name + " does not exist.");
			audioClip.Play();
		}

		/// <summary>
		/// Stops playing a specific audio clip.
		/// </summary>
		/// <param name="name">The name of the audio clip to stop playing.</param>
		public void Stop(String name)
		{
			MediaElement audioClip;
			Debug.Assert(audioClips.TryGetValue(name, out audioClip), "This AudioClip " + name + " does not exist.");
			audioClip.Stop();
		}
	}
}
