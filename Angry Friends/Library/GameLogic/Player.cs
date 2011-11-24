using Library.GameLogic.Persistence;
using Library.Engine.Object;
using System;
using System.Collections.Generic;

namespace Library.GameLogic
{

	public enum GameState
	{
		MOVING, AIMING, SHOOTING, IDLE, DEAD, SPECIAL
	}

	public class Player : GameObject
	{
		private GameState currentState;

		private Team team;

		private Item topSlot;

		private Item middleSlot;

		private Item bottomSlot;

		private Dictionary<AmmoType, Ammo> ammo;

		private AmmoType currentAmmo;

		public Player(String name, String tag = null)
			: base(name, tag)
		{
		}

		public void Aim()
		{
		}

		public void Move()
		{
		}

		public void Shoot()
		{
		}

		public void ChangeAmmo(AmmoType newAmmo)
		{
			if (ammo.ContainsKey(newAmmo))
				currentAmmo = newAmmo;
		}
	}
}
