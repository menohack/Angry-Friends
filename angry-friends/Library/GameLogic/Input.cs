using System.Windows.Input;
using System.Windows;
using System;
using Library.Engine.Object;
using System.Collections.Generic;

namespace Library.GameLogic
{


	public class MoveEventArgs : InputEventArgs
	{
		public MoveEventArgs()
		{

		}
	}

	public class AimEventArgs : InputEventArgs
	{
	}

	public class InputEventArgs : EventArgs
	{
	}

	/// <summary>
	/// The Input class handles keyboard and mouse events and translates them into events that the game logic can understand.
	/// </summary>
	public class Input
	{
		private static Input instance;


		public event AimEventHandler AimEvent;
		public event MoveEventHandler MoveEvent;

		public delegate void AimEventHandler(UIElement sender, AimEventArgs e);
		public delegate void MoveEventHandler(UIElement sender, MoveEventArgs e);

		public Player Target { get; set; }

		private List<Key> depressed;

		private Input()
		{
			depressed = new List<Key>();
		}

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


		public void OnKeyDown(UIElement sender, KeyEventArgs e)
		{
			if (Target == null)
				return;

			depressed.Add(e.Key);

			switch (e.Key)
			{
				case Key.A:
					Target.MoveLeft();
					break;
				case Key.D:
					Target.MoveRight();
					break;
				default:
					break;
			}
		}

		public void OnKeyUp(object sender, KeyEventArgs e)
		{
			if (Target == null)
				return;

			if (depressed.Remove(e.Key))
				Target.Stop();
		}

		public void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{

		}

		public void OnMouseRightButtonDown(object sender, MouseButtonEventArgs e)
		{

		}

		public void OnMouseMove(UIElement sender, MouseEventArgs e)
		{
			AimEventArgs args = new AimEventArgs();
			if (AimEvent != null)
				AimEvent(sender, args);
		}
	}
}
