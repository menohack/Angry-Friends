using Model.Engine.Object;
using Model.GameLogic.Persistence;
using System;

namespace Model.GameLogic
{
    /// <summary>
    /// The types of Ammo in-game.
    /// </summary>
	public enum AmmoType
	{
		BALLISTIC, EXPLOSIVE
	}

    /// <summary>
    /// Represents what can be used to shoot other enemies.
    /// </summary>
	public class Ammo : Item
	{
        /// <summary>
        /// The type of Ammo.
        /// </summary>
		public AmmoType AmmoType { get; private set; }

        /// <summary>
        /// Constructor for a new Ammo.
        /// </summary>
        /// <param name="name">The name of this type of Ammo.</param>
		public Ammo(String name) 
		{
		}
	}
}
