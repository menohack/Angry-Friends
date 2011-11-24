using System.Collections.Generic;
using Library.Engine.Object;
using System;

namespace Library.GameLogic.Persistence
{

	public enum Attribute
	{
		HEALTH, ARMOR, DAMAGE, SPEED
	}

	public enum ItemType
	{
		TOP, MIDDLE, BOTTOM, AMMO, MISC
	}

	public class Item : GameObject
	{
		private ItemType itemType;

		private List<Restriction> restrictions;

		private Dictionary<Attribute, double> attributes;

		public Item(String name, String tag = null)
			: base(name, tag)
		{
		}
	}
}
