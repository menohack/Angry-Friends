using System.Collections.Generic;

namespace Library.GameLogic
{
	public class Team
	{
		public int Count { get; private set; }

		public List<Player> Players { get; private set; }

		public string Name { get; private set; }
	}
}
