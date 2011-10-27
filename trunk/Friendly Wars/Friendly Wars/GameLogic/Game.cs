using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using Friendly_Wars.Engine.Object;
using Friendly_Wars.Engine.Ultilities;

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
		/// The terrain of the game.
		/// </summary>
		public Terrain terrain {get; private set; }

		/// <summary>
		/// The Web of the game.
		/// </summary>
		public Web web { get; private set; }

		/// <summary>
		/// Constructor for the Game.
		/// </summary>
		public Game()
		{
			world = new World(World.WORLD_NAME);
			web = new Web();
		}
	}
}
