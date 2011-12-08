using System.Collections.Generic;
using System.Windows;
using Model.Engine.Component.Media;
using Model.Engine.Component.Media.Rendering;
using Model.GameLogic.Persistence;
using Model.Engine.Component.Transform;

namespace Model.GameLogic
{

	/// <summary>
	/// The states that a Player can be in.
	/// </summary>
	public enum GameState
	{
		MOVING, AIMING, SHOOTING, IDLE, DEAD, SPECIAL
	}

    /// <summary>
    /// Player represents a User that is in-game, on a team, and in a match.
    /// </summary>
	public class Player : InteractiveGameObject
	{

		/// <summary>
		/// This player's current state.
		/// </summary>
		private GameState currentState;

        /// <summary>
        /// This Player's team.
        /// </summary>
		private Team team;

        /// <summary>
        /// The player's top slot item.
        /// </summary>
		private Item topSlot;

        /// <summary>
        /// The player's middle slot item.
        /// </summary>
		private Item middleSlot;

        /// <summary>
        /// The player's bottom slot item.
        /// </summary>
		private Item bottomSlot;

		/// <summary>
		/// The Constructor for a new Player.
		/// </summary>
		/// <param name="name">The Player's name.</param>
		/// <param name="transformComponent">The Player's TransformComponent.</param>
		/// <param name="audioComponent">The Player's AudioComponent.</param>
		/// <param name="renderComponent">The Player's RenderComponent.</param>
		public Player(string name, Point moveSpeed, TransformComponent tc, AudioComponent ac, RenderComponent rc) : base(name, moveSpeed, tc, ac, rc)
		{
		}					

        /// <summary>
        /// Aim this player's shot.
        /// </summary>
		public void Aim()
		{
		}

        /// <summary>
        /// Shot this player's shot.
        /// </summary>
		public void Shoot()
		{
		}
	}
}
