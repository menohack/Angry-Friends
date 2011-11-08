
using Friendly_Wars.Engine.Object;
using Friendly_Wars.Engine.Utilities;
using Friendly_Wars.Engine.Component.Graphic;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Controls;
using System;
using System.Windows;

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
		/// The Web of the game.
		/// </summary>
		public Web web { get; private set; }

		/// <summary>
		/// Constructor for the Game.
		/// </summary>
		public Game()
		{
			world = World.Instance;
			web = new Web();
		}
	}
}
