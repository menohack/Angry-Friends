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
	public interface IRenderable
	{
		/// <summary>
		/// Accessor for a RenderComponent.
		/// </summary>
		private RenderComponent RenderComponent { get; set; }
		/// <summary>
		/// Access the content that should be rendered.
		/// </summary>
		/// <returns>The content that needs to be rendered.</returns>
		public Image GetRenderContent();
	}
}
