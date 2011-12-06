using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Library.Engine.Component.Graphic;
using Library.Engine.Object;
using Library.Engine.Component;
using Library.Engine;
using System.Runtime.Serialization;


namespace Library.GameLogic {
	/// <summary>
	/// This class represents the Game. All game-logic stems from here.
	/// </summary>
    [DataContract]
	public class Game {

		/// <summary>
		/// The instance of the Engine.
		/// </summary>
        [DataMember]
        public World World { get; private set; }

        /// <summary>
        /// The viewport of this Game.
        /// </summary>
        [DataMember]
        public static Canvas Viewport { get; private set; }

		private Player currentPlayer;

		/// <summary>
		/// Constructor for the Game.
		/// </summary>
		public Game(Canvas viewport) {
			Input.Instance.AimEvent += new Input.AimEventHandler(Instance_AimEvent);
            Viewport = viewport;
			World = World.Instance;

			int width = 50;
			int height = 50;

			GameObject go1 = new GameObject("box1", Colors.Green, new Point(100, 0), new Point(width, height), new Point(50, 0));

			GameObject go2 = new GameObject("box2", Colors.Purple, new Point(300, 0), new Point(width, height), new Point(0, 0));

			GameObject go3 = new GameObject("box3", Colors.Red, new Point(200, 0), new Point(width, height), new Point(0, -2));


			currentPlayer = new Player("box4", Colors.Blue, new Point(250, 150), new Point(75, 75), new Point(0, -20));

			Input.Instance.MoveEvent += new Input.MoveEventHandler(Instance_MoveEvent);
		}

		void Instance_MoveEvent(UIElement sender, MoveEventArgs e)
		{
			currentPlayer.Move(sender, e);
		}

		void Instance_AimEvent(UIElement sender, AimEventArgs e)
		{
			
		}

		/// <summary>
		/// TEMPORARY HELPER FUNCTION, DONT KILL ME ALEX
		/// </summary>
		/// <param name="color"></param>
		/// <returns></returns>
		private int ConvertToARGB32(Color color)
		{
			return ((color.R << 16) | (color.G << 8) | (color.B << 0) | (color.A << 24));
		}
	}
}