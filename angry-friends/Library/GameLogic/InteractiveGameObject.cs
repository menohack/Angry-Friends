using System.Windows;
using System.Windows.Input;
using Model.Engine.Component.Media;
using Model.Engine.Component.Media.Rendering;
using Model.Engine.Component.Transform;
using Model.Engine.Object.GameObjects;

namespace Model.GameLogic
{

	/// <summary>
	/// The horizontal movement state base class.
	/// </summary>
	public abstract class HorizontalMoveState
	{
		/// <summary>
		/// Computes the horizontal distance to translate.
		/// </summary>
		/// <param name="moveSpeed">The movement speed of the object.</param>
		/// <param name="deltaTime">The amount of time, in seconds, for which the object has been moving.</param>
		/// <returns>The distance to translate.</returns>
		public abstract double Translate(double moveSpeed, double deltaTime);
	}

	/// <summary>
	/// The vertical movement state base class.
	/// </summary>
	public abstract class VerticalMoveState
	{
		/// <summary>
		/// Computes the vertical distance to translate.
		/// </summary>
		/// <param name="moveSpeed">The movement speed of the object.</param>
		/// <param name="deltaTime">The amount of time, in seconds, for which the object has been moving.</param>
		/// <returns>The distance to translate.</returns>
		public abstract double Translate(double moveSpeed, double deltaTime);
	}

	/// <summary>
	/// This state represents no movement horizontally.
	/// </summary>
	public class HorizontalIdleState : HorizontalMoveState
	{
		/// <summary>
		/// Computes the horizontal distance to translate.
		/// </summary>
		/// <param name="moveSpeed">The movement speed of the object.</param>
		/// <param name="deltaTime">The amount of time, in seconds, for which the object has been moving.</param>
		/// <returns>The distance to translate.</returns>
		public override double Translate(double moveSpeed, double deltaTime)
		{
			return 0.0;
		}
	}

	/// <summary>
	/// This state represents movement to the right.
	/// </summary>
	public class RightMoveState : HorizontalMoveState
	{
		/// <summary>
		/// Computes the distance to translate.
		/// </summary>
		/// <param name="moveSpeed">The movement speed of the object.</param>
		/// <param name="deltaTime">The amount of time, in seconds, for which the object has been moving.</param>
		/// <returns>The distance to translate.</returns>
		public override double Translate(double moveSpeed, double deltaTime)
		{
			return 1.0 * moveSpeed * deltaTime;
		}
	}

	/// <summary>
	/// This state represents movement to the left.
	/// </summary>
	public class LeftMoveState : HorizontalMoveState
	{
		/// <summary>
		/// Computes the distance to translate.
		/// </summary>
		/// <param name="moveSpeed">The movement speed of the object.</param>
		/// <param name="deltaTime">The amount of time, in seconds, for which the object has been moving.</param>
		/// <returns>The distance to translate.</returns>
		public override double Translate(double moveSpeed, double deltaTime)
		{
			return -1.0 * moveSpeed * deltaTime;
		}
	}

	/// <summary>
	/// This state represents no movement vertically.
	/// </summary>
	public class VerticalIdleState : VerticalMoveState
	{
		/// <summary>
		/// Computes the distance to translate.
		/// </summary>
		/// <param name="moveSpeed">The movement speed of the object.</param>
		/// <param name="deltaTime">The amount of time, in seconds, for which the object has been moving.</param>
		/// <returns>The distance to translate.</returns>
		public override double Translate(double moveSpeed, double deltaTime)
		{
			return 0.0;
		}
	}

	/// <summary>
	/// This state represents upward movement.
	/// </summary>
	public class UpMoveState : VerticalMoveState
	{
		/// <summary>
		/// Computes the distance to translate.
		/// </summary>
		/// <param name="moveSpeed">The movement speed of the object.</param>
		/// <param name="deltaTime">The amount of time, in seconds, for which the object has been moving.</param>
		/// <returns>The distance to translate.</returns>
		public override double Translate(double moveSpeed, double deltaTime)
		{
			return -1.0 * moveSpeed * deltaTime;
		}
	}

	/// <summary>
	/// This state represents downward movement.
	/// </summary>
	public class DownMoveState : VerticalMoveState
	{
		/// <summary>
		/// Computes the distance to translate.
		/// </summary>
		/// <param name="moveSpeed">The movement speed of the object.</param>
		/// <param name="deltaTime">The amount of time, in seconds, for which the object has been moving.</param>
		/// <returns>The distance to translate.</returns>
		public override double Translate(double moveSpeed, double deltaTime)
		{
			return 1.0 * moveSpeed * deltaTime;
		}
	}

	/// <summary>
	/// InteractiveGameObject is a GameObject that can be controlled by user input.
	/// </summary>
	public class InteractiveGameObject : GameObject
	{

		/// <summary>
		/// The vertical movement state of the InteractiveGameObject.
		/// </summary>
		private VerticalMoveState verticalMoveState;

		/// <summary>
		/// The horizontal movement state of the InteractiveGameObject.
		/// </summary>
		private HorizontalMoveState horizontalMoveState;

		/// <summary>
		/// The InteractiveGameObject's maximum movement speed.
		/// </summary>
		private Point moveSpeed;

		/// <summary>
		/// The Constructor for a new InteractiveGameObject.
		/// </summary>
		/// <param name="name">The InteractiveGameObject's name.</param>
		/// <param name="transformComponent">The InteractiveGameObject's TransformComponent.</param>
		/// <param name="audioComponent">The InteractiveGameObject's AudioComponent.</param>
		/// <param name="renderComponent">The InteractiveGameObject's RenderComponent.</param>
		public InteractiveGameObject(string name, Point moveSpeed, TransformComponent tc, AudioComponent ac, RenderComponent rc) : base(name, tc, ac, rc)
		{
			verticalMoveState = new VerticalIdleState();
			horizontalMoveState = new HorizontalIdleState();
			this.moveSpeed = moveSpeed;
		}

		/// <summary>
		/// Updates the InteractiveGameObject.
		/// </summary>
		/// <param name="deltaTime">The amount of time, in milliseconds, since the last update.</param>
		public override void Update(double deltaTime)
		{
			deltaTime /= 1000.00;

			double x = horizontalMoveState.Translate(moveSpeed.X, deltaTime);
			double y = verticalMoveState.Translate(moveSpeed.Y, deltaTime);

			TransformComponent.Translate(new Point(x, y));
		}

		/// <summary>
		/// Move the InteractiveGameObject based on a key press.
		/// </summary>
		/// <param name="key">The key that was pressed.</param>
		public void Move(Key key)
		{
			if (key.Equals(Key.A) || key.Equals(Key.Left))
				MoveLeft();
			else if (key.Equals(Key.D) || key.Equals(Key.Right))
				MoveRight();
			else if (key.Equals(Key.W) || key.Equals(Key.Up))
				MoveUp();
			else if (key.Equals(Key.S) || key.Equals(Key.Down))
				MoveDown();
		}

		/// <summary>
		/// Stops movement of the InteractiveGameObject in a particular direction.
		/// </summary>
		/// <param name="key">The key that was released.</param>
		public void Stop(Key key)
		{
			if (key.Equals(Key.A) || key.Equals(Key.Left) || key.Equals(Key.D) || key.Equals(Key.Right))
			{
				if (!horizontalMoveState.GetType().Equals(typeof(HorizontalIdleState)))
					StopHorizontal();
			}
			else if (key.Equals(Key.W) || key.Equals(Key.Up) || key.Equals(Key.S) || key.Equals(Key.Down))
			{
				if (!verticalMoveState.GetType().Equals(typeof(VerticalIdleState)))
					StopVertical();
			}

		}

		/// <summary>
		/// Move the InteractiveGameObject left.
		/// </summary>
		private void MoveLeft()
		{
			horizontalMoveState = new LeftMoveState();
		}

		/// <summary>
		/// Move the InteractiveGameObject right.
		/// </summary>
		private void MoveRight()
		{
			horizontalMoveState = new RightMoveState();
		}

		/// <summary>
		/// Stop moving the InteractiveGameObject horizontally.
		/// </summary>
		public void StopHorizontal()
		{
			horizontalMoveState = new HorizontalIdleState();
		}

		/// <summary>
		/// Move the InteractiveGameObject up.
		/// </summary>
		private void MoveUp()
		{
			verticalMoveState = new UpMoveState();
		}

		/// <summary>
		/// Move the InteractiveGameObject down.
		/// </summary>
		private void MoveDown()
		{
			verticalMoveState = new DownMoveState();
		}

		/// <summary>
		/// Stop moving the InteractiveGameObject vertically.
		/// </summary>
		public void StopVertical()
		{
			verticalMoveState = new VerticalIdleState();
		}
	}
}
