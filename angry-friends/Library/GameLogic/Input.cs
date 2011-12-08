using System.Windows.Input;
using System.Windows;
using System;
using Library.Engine.Object;
using System.Collections.Generic;

namespace Library.GameLogic
{
	/// <summary>
	/// The Input class handles keyboard and mouse events and translates them into events that the game logic can understand.
	/// </summary>
	public class Input
	{
		/// <summary>
		/// The static instance of Input. There can only be one.
		/// </summary>
		private static Input instance;

		/// <summary>
		/// The Target is the object affected by input.
		/// </summary>
		public InteractiveGameObject Target { get; set; }

		/// <summary>
		/// A list of currently depressed keys.
		/// </summary>
		private List<Key> depressed;

		/// <summary>
		/// The Singleton Input constructor.
		/// </summary>
		private Input()
		{
			depressed = new List<Key>();
		}

		/// <summary>
		/// The Singleton getters and setters for the single instance of Input.
		/// </summary>
		public static Input Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new Input();
				}
				return instance;
			}
		}

		/// <summary>
		/// This method is called by the Silverlight application when a key is pressed.
		/// </summary>
		/// <param name="sender">The UIElement that was focused when the key was pressed.</param>
		/// <param name="e">The event arguments of the key press.</param>
		public void OnKeyDown(UIElement sender, KeyEventArgs e)
		{
			if (Target == null)
				return;

			depressed.Add(e.Key);

			Target.Move(e.Key);
		}

		/// <summary>
		/// This method is called by the Silverlight application when a key is released.
		/// </summary>
		/// <param name="sender">The UIElement that was focused when the key was released.</param>
		/// <param name="e">The event arguments of the key release.</param>
		public void OnKeyUp(UIElement sender, KeyEventArgs e)
		{
			if (Target == null)
				return;

			if (depressed.Remove(e.Key))
				Target.Stop(e.Key);
		}

		/// <summary>
		/// Thsi method is called by the Silverlight application when the left mouse button is depressed.
		/// </summary>
		/// <param name="sender">The UIElement that was focused when the button was released.</param>
		/// <param name="e">The event arguments of the button press.</param>
		public void OnMouseLeftButtonDown(UIElement sender, MouseButtonEventArgs e)
		{

		}

		/// <summary>
		/// Thsi method is called by the Silverlight application when the right mouse button is depressed.
		/// </summary>
		/// <param name="sender">The UIElement that was focused when the button was released.</param>
		/// <param name="e">The event arguments of the button press.</param>
		public void OnMouseRightButtonDown(UIElement sender, MouseButtonEventArgs e)
		{

		}

		/// <summary>
		/// Thsi method is called by the Silverlight application when the mouse moves.
		/// </summary>
		/// <param name="sender">The UIElement that was focused when the mouse moved.</param>
		/// <param name="e">The event arguments of the mouse movement.</param>
		public void OnMouseMove(UIElement sender, MouseEventArgs e)
		{

		}
	}
}
