
using Friendly_Wars.Engine.Object;
using System.Windows;
using System.Diagnostics;

namespace Friendly_Wars.GameLogic
{
	/// <summary>
	/// This class represents the Game. All game-logic stems from here.
	/// </summary>
	public class Game
	{
		/// <summary>
		/// The world of the game.
		/// </summary>
		private World world;

		/// <summary>
		/// Constructor for the Game.
		/// </summary>
		public Game()
		{
			world = World.Instance;
		}
	}
}
