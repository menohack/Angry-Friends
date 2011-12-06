using Library.GameLogic.Persistence;
using Library.Engine.Object;
using System;
using System.Collections.Generic;

namespace Library.GameLogic
{
    /// <summary>
    /// The state this Player is in.
    /// </summary>
	public enum GameState
	{
		MOVING, AIMING, SHOOTING, IDLE, DEAD, SPECIAL
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

        /// <summary>
        /// Constructor for a new Player.
        /// </summary>
        /// <param name="name">The name of this Player.</param>
		public Player(String name): base(name){}

        /// <summary>
        /// Aim this player's shot.
        /// </summary>
		public void Aim()
		{
		}

        /// <summary>
        /// Move this player.
        /// </summary>
		public void Move()
		{
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
