using Library.GameLogic.Persistence;
using Library.Engine.Object;
using System;
using System.Collections.Generic;

namespace Library.GameLogic
{
	public class Player : GameObject
	{
		private Team team;

		private Item topSlot;

		private Item middleSlot;

		private Item bottomSlot;

		private List<Ammo> ammo;

		private Ammo currentAmmo;

		public Player(String name, String tag = null)
			: base(name, tag)
		{
		}
	}
}
