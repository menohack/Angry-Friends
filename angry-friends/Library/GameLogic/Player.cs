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
    /// Player represents a User that is in-game, on a team, and in a match.
    /// </summary>
    public class Player : InteractiveGameObject, IUpdateable
	{

        /// <summary>
        /// The interval in which the player updates, looking for gravity.
        /// </summary>
        private static readonly int PHYSICS_UPDATE_INTERAL = 33;

        /// <summary>
        /// The gravitational force of gravity, in pixels.
        /// </summary>
        private static readonly int GRAVITATIONAL_FORCE = 300;

        /// <summary>
        /// The gravitational velocity of this player.
        /// </summary>
        private double gravitationalVelocity;

        /// <summary>
        /// This player's TransformComponent.
        /// </summary>
        private TransformComponent transformComponent;

        /// <summary>
        /// This player's AudioComponent.
        /// </summary>
        private AudioComponent audioComponent;

        /// <summary>
        /// This player's RenderComponent.
        /// </summary>
        private RenderComponent renderComponent;

		/// <summary>
		/// The Constructor for a new Player.
		/// </summary>
		/// <param name="name">The Player's name.</param>
		/// <param name="transformComponent">The Player's TransformComponent.</param>
		/// <param name="audioComponent">The Player's AudioComponent.</param>
		/// <param name="renderComponent">The Player's RenderComponent.</param>
		public Player(string name, Point moveSpeed, TransformComponent transformComponent, AudioComponent audioComponent, RenderComponent renderComponent) : base(name, moveSpeed, transformComponent, audioComponent, renderComponent)
		{
            this.transformComponent = transformComponent;
            EngineTimer engineTimer = new EngineTimer(PHYSICS_UPDATE_INTERAL, new Collection<IUpdateable> { this });
            engineTimer.Start();
		}					

        /// <summary>
        /// Updates gravity for this player.
        /// </summary>
        /// <param name="deltaTime"></param>
        new public void Update(double deltaTime)
        {
            deltaTime /= 1000.00;
            if (this.transformComponent.IsCollidingWith("TERRAIN"))
            {
                gravitationalVelocity = 0;
            }
            else
            {
                gravitationalVelocity += GRAVITATIONAL_FORCE * deltaTime * deltaTime;
                transformComponent.Translate(new Point(0, gravitationalVelocity));
            }
        }
    }
}
