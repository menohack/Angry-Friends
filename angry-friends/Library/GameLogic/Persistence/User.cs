using System.Collections.Generic;
using System;
using System.Runtime.Serialization;

namespace Model.GameLogic.Persistence
{
    /// <summary>
    /// User represents someone who uses the Game.  It contains data relating to the User's friends, rank, etc., and contains methods for acting on the game, such as inviting another User to the game.
    /// </summary>
	[DataContract]
    public class User
	{
        /// <summary>
        /// The friends of this User.
        /// </summary>
		[DataMember]
        private List<User> friends;

        /// <summary>
        /// The rank of this User.
        /// </summary>
		[DataMember]
        private Rank rank;

        /// <summary>
        /// The number of games played by this User.
        /// </summary>
		[DataMember]
        private int gamesPlayed;

        /// <summary>
        /// The number of games won by this User.
        /// </summary>
		[DataMember]
        private int gamesWon;

        /// <summary>
        /// This user's inventory, which is comrised of items.
        /// </summary>
		[DataMember]
        private IList<Item> inventory;

        /// <summary>
        /// The amount of currency this User has.
        /// </summary>
		[DataMember]
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
