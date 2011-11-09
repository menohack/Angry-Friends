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
using Friendly_Wars.Engine.Component.Graphic;

namespace Friendly_Wars.Engine.Component.Interfaces
{
	/// <summary>
	/// Represents an object that is renderable.
	/// </summary>
	public interface IRenderable
	{
		/// <summary>
		/// Plays a specific Animation.
		/// </summary>
		/// <param name="animationName">The name of the Animation to play.</param>
		void PlayAnimation(String animationName);

		/// <summary>
		/// Stops playing a specific Animation.
		/// </summary>
		/// <param name="animationName">The name of the Animation to stop playing.</param>
		void StopAnimation(String animationName);

		/// <summary>
		/// Access the content that should be rendered.
		/// </summary>
		/// <returns>The content that needs to be rendered.</returns>
		Image GetRenderContent();
	}
}
