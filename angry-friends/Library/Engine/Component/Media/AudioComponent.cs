using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Windows.Controls;
using Model.Engine.Object.GameObjects;

namespace Model.Engine.Component.Media {

	/// <summary>
	/// Handles the control of a GameObject's audio.
	/// </summary>
	[DataContract]
    public class AudioComponent : BaseComponent {

		/// <summary>
		/// A Dictionary between the names of audio and the audio, itself.
		/// </summary>
        [DataMember]
        private IDictionary<String, MediaElement> audioClips;

		/// <summary>
		/// Constructor for an audioComponent.
		/// </summary>
		/// <param name="audioClips">The clips that the audioComponent is capable of playing.</param>
		/// <param name="owner">The GameObject that owns this AudioComponent.</param>
		public AudioComponent(IDictionary<String, MediaElement> audioClips, GameObject owner = null) : base(owner) {
			this.audioClips = audioClips;
		}

		/// <summary>
		/// Plays a specific audio clip.
		/// </summary>
		/// <param name="name">The name of the audio clip to play.</param>
		public void Play(String name) {
			MediaElement audioClip;
			Debug.Assert(audioClips.TryGetValue(name, out audioClip), "This AudioClip " + name + " does not exist.");
			audioClip.Play();
		}

		/// <summary>
		/// Stops playing a specific audio clip.
		/// </summary>
		/// <param name="name">The name of the audio clip to stop playing.</param>
		public void Stop(String name) {
			MediaElement audioClip;
			Debug.Assert(audioClips.TryGetValue(name, out audioClip), "This AudioClip " + name + " does not exist.");
			audioClip.Stop();
		}
	}
}