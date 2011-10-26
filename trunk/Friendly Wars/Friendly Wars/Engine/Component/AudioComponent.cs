﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Friendly_Wars.Engine.Object;
using System.Collections.Generic;

namespace Friendly_Wars.Engine.Component
{
    /// <summary>
    /// Handles the control of a GameObject's audio.
    /// </summary>
    public class AudioComponent
    {
        /// <summary>
        /// A Collection between the names of MediaElements and the actual MediaElement.
        /// </summary>
        private IDictionary<String, MediaElement> audioClips;
        /// <summary>
        /// The constructor for an AudioComponent.
        /// </summary>
        /// <param name="owner">The owner of this AudioComponent.</param>
        public AudioComponent(GameObject owner) : base(owner)
        {
            audioClips = new Dictionary<String, MediaElement>();
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
            AudioClip audioClip;
            if (audioClips.TryGet(name, out audioClip))
            {
                audioClip.Play();
            }
            else
            {
                throw new ApplicationException("No such AudioClip");
            }
        }

        /// <summary>
        /// Stops playing a specific audio clip.
        /// </summary>
        /// <param name="name">The name of the audio clip to stop playing.</param>
        public void Stop(String name)
        {
            AudioClip audioClip;
            if (audioClips.TryGet(name, out audioClip))
            {
                audioClip.Stop();
            }
            else
            {
                throw new ApplicationException("No such AudioClip");
            }
        }

    }
}
