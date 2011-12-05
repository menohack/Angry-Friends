using System.Collections.Generic;

namespace Library.GameLogic
{
    /// <summary>
    /// Team represents a collection of Players that are on the same side and fighting together.
    /// </summary>
	public class Team
	{
        /// <summary>
        /// The number of Players on this team.
        /// </summary>
		public int Count { get; private set; }

        /// <summary>
        /// The Players on this team.
        /// </summary>
		public List<Player> Players { get; private set; }

        /// <summary>
        /// The name of this team.
        /// </summary>
		public string Name { get; private set; }
	}
}
