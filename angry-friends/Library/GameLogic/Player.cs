using Library.GameLogic.Persistence;
using Library.Engine.Object;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Library.Engine.Component;
using Library.Engine.Component.Graphic;
using System.Diagnostics;
using Library.Engine.Utilities;

namespace Library.GameLogic
{
    /// <summary>
    /// The state this Player is in.
    /// </summary>
	public enum GameState
	{
		MOVING, AIMING, SHOOTING, IDLE, DEAD, SPECIAL
	}

	public enum MoveState
	{
		LEFT, RIGHT, UP, DOWN
	}

    /// <summary>
    /// Player represents a User that is in-game, on a team, and in a match.
    /// </summary>
	public class Player : GameObject
	{
        /// <summary>
        /// This player's current state.
        /// </summary>
		private GameState currentState;

		/// <summary>
		/// The movement state of the Player.
		/// </summary>
		private MoveState moveState;

		/// <summary>
		/// The Player's maximum movement speed.
		/// </summary>
		private Velocity moveSpeed;

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
        /// A IList of available Ammo.
        /// </summary>
		private IList<Ammo> ammo;

        /// <summary>
        /// The current Ammo selected.
        /// </summary>
		private AmmoType currentAmmo;

		public Player(string name, TransformComponent tc, AudioComponent ac, RenderComponent rc) : base(name, tc, ac, rc)
		{
			currentState = GameState.IDLE;
			moveSpeed = new Velocity(500.0, 0.0);
		}

		public override void Update(double deltaTime)
		{
			if (currentState != GameState.MOVING)
				return;

            deltaTime /= 1000.00;
			switch (moveState)
			{
				case MoveState.LEFT:
					TransformComponent.Translate(new Point(-1.0 * moveSpeed.X * deltaTime, 0.0));
					break;
				case MoveState.RIGHT:
					TransformComponent.Translate(new Point( 1.0 * moveSpeed.X * deltaTime, 0.0));
					break;
				default:
					break;
			}
		}
							

        /// <summary>
        /// Constructor for a new Player.
        /// </summary>
        /// <param name="name">The name of this Player.</param>
		//public Player(string name): base(name){}

        /// <summary>
        /// Aim this player's shot.
        /// </summary>
		public void Aim()
		{
		}

        /// <summary>
        /// Move this player left.
        /// </summary>
		public void MoveLeft()
		{
			currentState = GameState.MOVING;
			moveState = MoveState.LEFT;
		}

		/// <summary>
		/// Move this player right.
		/// </summary>
		public void MoveRight()
		{
			currentState = GameState.MOVING;
			moveState = MoveState.RIGHT;
		}

		/// <summary>
		/// Stop moving the player.
		/// </summary>
		public void Stop()
		{
			currentState = GameState.IDLE;
		}

        /// <summary>
        /// Shot this player's shot.
        /// </summary>
		public void Shoot()
		{
		}

        /// <summary>
        /// Change this player's current Ammo.
        /// </summary>
        /// <param name="newAmmo">The new Ammo to change to.</param>
		public void ChangeAmmo(AmmoType newAmmo)
		{
            currentAmmo = newAmmo;
		}
	}
}
