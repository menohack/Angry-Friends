using System.Collections.Generic;
using System;

namespace Library.GameLogic.Persistence
{
	public class User : Player
	{
		private List<User> friends;

		private Rank rank;

		private int gamesPlayed;
		private int gamesWon;

		private List<Item> inventory;

		private int currency;

		public User(String name, String tag = null) : base(name, tag)
		{
		}

		public void InviteToPlay(List<User> users)
		{
		}

		public double SellItem(Item item)
		{
			return 0.0;
		}
	}
}
