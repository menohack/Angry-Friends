using System.Collections.Generic;
using System.Windows;
using Model.Engine.Component.Media;
using Model.Engine.Component.Media.Rendering;
using Model.Engine.Component.Transform;
using Model.Engine.Utilities;
using System.Collections.ObjectModel;

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

            if (base.TransformComponent.IsCollidingWith(Factory.Instance.Terrain.UID)) 
            {
                base.IsGrounded = true;
            }
            else
            {
                base.IsGrounded = false;
            }

            if (base.TransformComponent.IsCollidingWith(Factory.NAME_OF_APPLE))
            {
                int i;
            }

            base.TransformComponent.Translate(new Point(base.VelocityVector.X * deltaTime, base.VelocityVector.Y * deltaTime));
		    base.Update(deltaTime * 1000);
        }
    }
}