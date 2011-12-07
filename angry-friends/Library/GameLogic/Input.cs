using System.Windows.Input;
using System.Windows;
using System;
using Library.Engine.Object;

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

		public GameObject Target { get; set; }

		private Input()
		{

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
			MoveEventArgs args = new MoveEventArgs();
			if (MoveEvent != null)
				MoveEvent(sender, args);
		}

		public void OnKeyUp(object sender, KeyEventArgs e)
		{

		}

		public void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
