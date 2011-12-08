namespace Model.GameLogic.Persistence
{
    /// <summary>
    /// All in-game ranks.
    /// </summary>
	public enum Rank
	{
		NOOB, ROOKIE, PLAYER, VETERAN, LEGEND
	}

    /// <summary>
    /// Represents what requirement are needed.
    /// </summary>
	public class Requirement
	{
        /// <summary>
        /// The requirement on curreny value.
        /// </summary>
		private double currencyValue;
        /// <summary>
        /// The requirement on rank.
        /// </summary>
		private Rank rank;
        /// <summary>
        /// The requirement on the number of friends.
        /// </summary>
		private int friendCount;
        /// <summary>
        /// The requirement of a win percentage.
        /// </summary>
		private double winPercentage;
	}
}
