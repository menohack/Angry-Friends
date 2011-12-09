using System;
using System.Windows.Input;
using System.Windows;

namespace Library.Engine.Object
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
		public event AimEventHandler AimEvent;
		public event MoveEventHandler MoveEvent;

		public delegate void AimEventHandler(UIElement sender, AimEventArgs e);
		public delegate void MoveEventHandler(UIElement sender, MoveEventArgs e);

		public GameObject Target
		{
			get;
			set;
		}

		public Input()
		{

		}



		public void OnKeyDown(UIElement sender, KeyEventArgs e)
		{
			if (Target != null)
			{
				Point direction = new Point(0,0);
				if (e.Key.Equals(Key.A))
					direction.X = -5.0;
				else if (e.Key.Equals(Key.D))
					direction.X = 5.0;


				Target.TransformComponent.Translate(direction);
			}

			/*
			MoveEventArgs args = new MoveEventArgs();
			if (MoveEvent != null)
				MoveEvent(sender, args);
			*/
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
