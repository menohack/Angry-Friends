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
using Model.Engine.Object;
using Model.Engine.Component.Transform;
using Model.Engine.Component.Media;
using Model.Engine.Component.Media.Rendering;

namespace Model.GameLogic
{
    /// <summary>
    /// Gravitational GameObject is a GameObject that follows the rules of gravity.
    /// </summary>
    public class GravitationalGameObject : GameObject
    {

        /// <summary>
        /// Is this GravitationalGameObject grounded?  If so, it will not apply gravity.
        /// </summary>
        public bool IsGrounded;

        /// <summary>
        /// The gravitational force of gravity, in pixels.
        /// </summary>
        private static readonly int GRAVITATIONAL_FORCE = 300;

        /// <summary>
        /// The current gravitational velocity of this GameObject.
        /// </summary>
        private double gravitationalVelocity = 0;
        
		/// <summary>
		/// The Constructor for a new GravitationalGameObject.
		/// </summary>
		/// <param name="name">The GameObject's name.</param>
		/// <param name="transformComponent">The GameObjects's TransformComponent.</param>
        /// <param name="audioComponent">The GameObjects's AudioComponent.</param>
        /// <param name="renderComponent">The GameObjects's RenderComponent.</param>
		public GravitationalGameObject(String name, TransformComponent transformComponent, AudioComponent audioComponent, RenderComponent renderComponent) : base(name, transformComponent, audioComponent, renderComponent)
		{
            IsGrounded = false;
		}					

        /// <summary>
        /// Updates gravity for this player.
        /// </summary>
        /// <param name="deltaTime">The time since the last update.</param>
        public override void Update(double deltaTime)
        {
            deltaTime /= 1000.00;

            if (IsGrounded)
            {
                gravitationalVelocity = 0;
            }
            else
            {
                gravitationalVelocity += GRAVITATIONAL_FORCE * deltaTime * deltaTime;
                base.TransformComponent.Translate(new Point(0, gravitationalVelocity));
                base.Update(deltaTime * 1000.00);
            }
        }
    }
}
