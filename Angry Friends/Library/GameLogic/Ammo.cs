using Library.Engine.Object;
using Library.GameLogic.Persistence;
using System;

namespace Library.GameLogic
{
	public enum AmmoType
	{
		BALLISTIC, EXPLOSIVE
	}

	public class Ammo : Item
	{
		public AmmoType AmmoType { get; private set; }

		public Ammo(String name, String tag)
			: base(name, tag)
		{
		}
	}
}
