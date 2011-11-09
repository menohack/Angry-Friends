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

namespace Friendly_Wars.Engine.Component.Interfaces
{
	/// <summary>
	/// Represents an object that is audible.
	/// </summary>
	public interface IAudible
	{
		/// <summary>
		/// Plays a specific AudioClip.
		/// </summary>
		/// <param name="name">The AudioClip to play.</param>
		void PlayAudioClip(String name);

		/// <summary>
		/// Stops playing a specific AudioClip.
		/// </summary>
		/// <param name="name">The AudioClip to stop playing.</param>
		void StopAudioClip(String name);
	}
}
