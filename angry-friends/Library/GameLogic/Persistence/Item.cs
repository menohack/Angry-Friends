using System.Collections.Generic;
using Library.Engine.Object;
using System;

namespace Library.GameLogic.Persistence
{
    /// <summary>
    /// Attributes for an Item.
    /// </summary>
	public enum Attribute
	{
		HEALTH, ARMOR, DAMAGE, SPEED
	}

    /// <summary>
    /// The types of Item.
    /// </summary>
	public enum ItemType
	{
		TOP, MIDDLE, BOTTOM, AMMO, MISC
	}

    /// <summary>
    /// Item represents an item that a Player can have.
    /// </summary>
	public class Item : GameObject
	{
        /// <summary>
        /// The type of this item.
        /// </summary>
		private ItemType itemType;

        /// <summary>
        /// The requirements needed for using this item.
        /// </summary>
		private List<Requirement> restrictions;
        
        /// <summary>
        /// A dictionary that stores the values of this item's attributes.
        /// </summary>
		private Dictionary<Attribute, double> attributes;

        /// <summary>
        /// Constructor for a new item.
        /// </summary>
        /// <param name="name">The name of this item.</param>
		public Item(String name) : base(name)
		{
		}
	}
}
