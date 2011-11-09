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
	public class AudioComponent : BaseComponent
	{
		/// <summary>
		/// A Dictionary between the names of audio and the audio, itself.
		/// </summary>
		private IDictionary<String, MediaElement> audioClips;

		/// <summary>
		/// Constructor for an AudioComponent.
		/// </summary>
		/// <param name="owner">The owner of this AudioComponent.</param>
		/// <param name="audioClips">The clips that the AudioComponent is capable of playing.</param>
		public AudioComponent(GameObject owner, IDictionary<String, MediaElement> audioClips) : base(owner)
		{
			this.audioClips = audioClips;
		}

		/// <summary>
		/// Adds an audio clip to this AudioComponent.
		/// </summary>
		/// <param name="name">The name of the audio clip.</param>
		/// <param name="audioClip">The audio clip, in the form of a MediaElement. </param>
		public void AddAudioClip(String name, MediaElement audioClip) {
			audioClips.Add(name, audioClip);
		}

		/// <summary>
		/// Removes an audio clip from this AudioComponent.
		/// </summary>
		/// <param name="name">The name of the audio clip to remove.</param>
		public void RemoveAudioClip(String name)
		{
			audioClips.Remove(name);
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
