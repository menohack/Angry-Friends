using System.Collections.Generic;
using System;

namespace Library.GameLogic.Persistence
{
    /// <summary>
    /// User represents someone who uses the Game.  It contains data relating to the User's friends, rank, etc., and contains methods for acting on the game, such as inviting another User to the game.
    /// </summary>
	public class User
	{
        /// <summary>
        /// The friends of this User.
        /// </summary>
		private List<User> friends;

        /// <summary>
        /// The rank of this User.
        /// </summary>
		private Rank rank;

        /// <summary>
        /// The number of games played by this User.
        /// </summary>
		private int gamesPlayed;

        /// <summary>
        /// The number of games won by this User.
        /// </summary>
		private int gamesWon;

        /// <summary>
        /// This user's inventory, which is comrised of items.
        /// </summary>
		private IList<Item> inventory;

        /// <summary>
        /// The amount of currency this User has.
        /// </summary>
		private int currency;

        /// <summary>
        /// Constructor for this User.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="tag"></param>
        public User() { }

        /// <summary>
        /// Invites a User to play the game.
        /// </summary>
        /// <param name="users">A IList of Users to invite to play.</param>
		public void InviteToPlay(IList<User> users)
		{
		}
	}
}
