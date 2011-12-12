using System.Collections.Generic;
using System.Windows;
using Model.Engine.Component.Media;
using Model.Engine.Component.Media.Rendering;
using Model.Engine.Component.Transform;
using Model.Engine.Utilities;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Model.GameLogic
{
    /// <summary>
    /// Yoshi represents the protagonist of the Game.
    /// </summary>
    public class Yoshi : InteractiveGameObject
	{

		/// <summary>
		/// The Constructor for a new Yoshi.
		/// </summary>
		/// <param name="name">The Player's name.</param>
		/// <param name="transformComponent">The Player's TransformComponent.</param>
		/// <param name="audioComponent">The Player's AudioComponent.</param>
		/// <param name="renderComponent">The Player's RenderComponent.</param>
		public Yoshi(string name, Point moveSpeed, TransformComponent transformComponent, AudioComponent audioComponent, RenderComponent renderComponent) : base(name, moveSpeed, transformComponent, audioComponent, renderComponent) {}					

        /// <summary>
        /// Updates gravity for this player.
        /// </summary>
        /// <param name="deltaTime">The time since the last update.</param>
        public override void Update(double deltaTime)
        {
            deltaTime /= 1000;

            // Handle collision with terrain
            if (base.TransformComponent.IsCollidingWith(Factory.Instance.Terrain.UID)) 
            {
                if (base.VelocityVector.X == 0)
                {
                    base.RenderComponent.Play(Factory.YOSHI_IDLE_ANIMATION_NAME);
                }
                else
                {
                    base.RenderComponent.Play(Factory.YOSHI_WALK_ANIMATION_NAME);
                }
                base.IsGrounded = true;
            }
            // If not colliding with the terrain, apply gravity and play a jump animation.
            else
            {
                base.IsGrounded = false;
                if (base.TransformComponent.Velocity.Y != 0)
                {
                    base.RenderComponent.Play(Factory.YOSHI_JUMP_ANIMATION_NAME);
                }
            }

            base.TransformComponent.Translate(new Point(base.VelocityVector.X * deltaTime, base.VelocityVector.Y * deltaTime));
		    base.Update(deltaTime * 1000);
        }

        /// <summary>
        /// Specific logic for Yoshi when he needs to move left.
        /// </summary>
        protected override void MoveLeft()
        {
            // Play the walk animation if we are grounded are are moving left.
            if (IsGrounded)
            {
                base.RenderComponent.Play(Factory.YOSHI_WALK_ANIMATION_NAME);
            }
            base.MoveLeft();
        }

        /// <summary>
        /// Specific logic for Yoshi when he needs to move right.
        /// </summary>
        protected override void MoveRight()
        {
            // Play the walk animation if we are grounded and are moving right.
            if (IsGrounded)
            {
                base.RenderComponent.Play(Factory.YOSHI_WALK_ANIMATION_NAME);
            }
            base.MoveRight();
        }

        /// <summary>
        /// Specific logic for Yoshi when he needs to stop moving in the horizontal direction.
        /// </summary>
        protected override void StopHorizontal()
        {
            // Play the idle animation if we stopped moving and we are grounded.
            if (IsGrounded)
            {
                base.RenderComponent.Play(Factory.YOSHI_IDLE_ANIMATION_NAME);
            }
            base.StopHorizontal();
        }

        /// <summary>
        /// Specific logic for Yoshi when he needs to move Up.
        /// </summary>
        protected override void MoveUp()
        {
            base.RenderComponent.Play(Factory.YOSHI_JUMP_ANIMATION_NAME);
            base.MoveUp();
        }

        /// <summary>
        /// Specific logic for Yoshi when he needs to move Down.  Note, we don't allow Yoshi to move down!
        /// </summary>
        protected override void MoveDown()
        {
        }

        /// <summary>
        /// Specific logic for Yoshi when he needs to stop moving in the vertical direction.
        /// </summary>
        protected override void StopVertical()
        {
            // Play the idle animation if we stopped moving and we are grounded.
            if (IsGrounded)
            {
                base.RenderComponent.Play(Factory.YOSHI_IDLE_ANIMATION_NAME);
            }
            base.StopVertical();
        }
    }
}