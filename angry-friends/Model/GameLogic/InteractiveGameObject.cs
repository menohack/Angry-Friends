using System.Windows;
using System.Windows.Input;
using Model.Engine.Component.Media;
using Model.Engine.Component.Media.Rendering;
using Model.Engine.Component.Transform;
using Model.Engine.Object;
using System;
using System.Windows.Controls;

namespace Model.GameLogic
{
	/// <summary>
	/// InteractiveGameObject is a GameObject that can be controlled by user input.
	/// </summary>
	public class InteractiveGameObject : GravitationalGameObject
	{

        private Point velocityVector;

        /// <summary>
        /// The vector corresponding to this InteractiveGameObject's current velocity.
        /// </summary>
        public Point VelocityVector 
        { 
            get 
            {
                return velocityVector;
            }
            private set 
            {
                this.velocityVector = value;
            }
        }

		/// <summary>
		/// The InteractiveGameObject's movement speed.
		/// </summary>
		private Point velocity;

		/// <summary>
		/// The Constructor for a new InteractiveGameObject.
		/// </summary>
		/// <param name="name">The InteractiveGameObject's name.</param>
		/// <param name="transformComponent">The InteractiveGameObject's TransformComponent.</param>
		/// <param name="audioComponent">The InteractiveGameObject's AudioComponent.</param>
		/// <param name="renderComponent">The InteractiveGameObject's RenderComponent.</param>
		public InteractiveGameObject(String name, Point velocity, TransformComponent tc, AudioComponent ac, RenderComponent rc) : base(name, tc, ac, rc)
		{
			this.velocity = velocity;
            this.VelocityVector = new Point();
		}

		/// <summary>
		/// Processes the logic corresponding to a key press.
		/// </summary>
		/// <param name="key">The key that was pressed.</param>
        public virtual void OnKeyPressed(Key key)
		{
            if (key.Equals(Key.A) || key.Equals(Key.Left))
            {
                MoveLeft();
            }
            if (key.Equals(Key.D) || key.Equals(Key.Right))
            {
                MoveRight();
            }
            if (key.Equals(Key.W) || key.Equals(Key.Up))
            {
                MoveUp();
            }
			if (key.Equals(Key.S) || key.Equals(Key.Down)) {
				MoveDown();
            }
		}

		/// <summary>
		/// Stops movement of the InteractiveGameObject in a particular direction.
		/// </summary>
		/// <param name="key">The key that was released.</param>
        public virtual void OnKeyReleased(Key key)
		{
			if (key.Equals(Key.A) || key.Equals(Key.Left) || key.Equals(Key.D) || key.Equals(Key.Right))
            {
                StopHorizontal();
            } 
            if (key.Equals(Key.W) || key.Equals(Key.Up) || key.Equals(Key.S) || key.Equals(Key.Down)) {
                StopVertical();
            }
		}

		/// <summary>
		/// OnKeyPressed the InteractiveGameObject left.
		/// </summary>
        protected virtual void MoveLeft()
		{
            velocityVector.X = -velocity.X;
		}

		/// <summary>
		/// OnKeyPressed the InteractiveGameObject right.
		/// </summary>
        protected void MoveRight()
		{
            velocityVector.X = velocity.X;
		}

		/// <summary>
		/// OnKeyReleased moving the InteractiveGameObject horizontally.
		/// </summary>
        protected virtual void StopHorizontal()
		{
            velocityVector.X = 0;
		}

		/// <summary>
		/// OnKeyPressed the InteractiveGameObject up.
		/// </summary>
        protected virtual void MoveUp()
		{
            velocityVector.Y = -velocity.Y;
		}

		/// <summary>
		/// OnKeyPressed the InteractiveGameObject down.
		/// </summary>
        protected virtual void MoveDown()
		{
            velocityVector.Y = velocity.Y;
		}

		/// <summary>
		/// OnKeyReleased moving the InteractiveGameObject vertically.
		/// </summary>
		protected virtual void StopVertical()
		{
            velocityVector.Y = 0;
		}

        /// <summary>
        /// Updates gravity for this player.
        /// </summary>
        /// <param name="deltaTime">The time since the last update.</param>
        public override void Update(double deltaTime)
        {
            deltaTime /= 1000.00;
            base.Update(deltaTime * 1000);
        }
	}
}
