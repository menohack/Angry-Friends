namespace Library.GameLogic.Persistence
{
	public enum Rank
	{
		NOOB, ROOKIE, PLAYER, VETERAN, LEGEND
	}

	public class Restriction
	{
		private double currencyValue;

		private Rank rank;

		private int friendCount;

		private double winPercentage;
	}
}
