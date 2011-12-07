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
		/// The instance of the EngineObject.
		/// </summary>
        [DataMember]
        public EngineObject EngineObject { get; private set; }

		private Player currentPlayer;

		/// <summary>
		/// Constructor for the Game.
		/// </summary>
		public Game()
		{
			EngineObject = EngineObject.Instance;

			TransformComponent tc = new TransformComponent(new Point(200, 200), 0, new Point(50, 50), null);
			RenderComponent rc = new RenderComponent(new Animation(new Frame(Colors.Red, new Point(50, 50))), null); ;
			AudioComponent ac = new AudioComponent(new Dictionary<String, MediaElement>(), null);
			Player redBox = new Player("redBox", tc, ac, rc);
			currentPlayer = redBox;

			EngineObject.Instance.controller.Target = redBox;

			TransformComponent tc2 = new TransformComponent(new Point(300, 400), 0, new Point(50, 50), null);
			RenderComponent rc2 = new RenderComponent(new Animation(new Frame(Colors.Blue, new Point(50, 50))), null); ;
			AudioComponent ac2 = new AudioComponent(new Dictionary<String, MediaElement>(), null);
			GameObject blueBox = new GameObject("blueBox", tc2, ac2, rc2);

			TransformComponent tc3 = new TransformComponent(new Point(600, 400), 0, new Point(50, 50), null);
			RenderComponent rc3 = new RenderComponent(new Animation(new Frame(Colors.Blue, new Point(50, 50))), null); ;
			AudioComponent ac3 = new AudioComponent(new Dictionary<String, MediaElement>(), null);
			Terrain terrain = new Terrain("terrain", tc3, ac3, rc3);
		}

		/*
		public Game(Canvas viewport) {
			EngineObject = EngineObject.Instance;
			//Input.Instance.AimEvent += new Input.AimEventHandler(Instance_AimEvent);
            //Viewport = viewport;
			//World = World.Instance;

			int width = 50;
			int height = 50;

			GameObject go1 = new GameObject("box1", Colors.Green, new Point(100, 0), new Point(width, height), new Point(50, 0));

			GameObject go2 = new GameObject("box2", Colors.Purple, new Point(300, 0), new Point(width, height), new Point(0, 0));

			GameObject go3 = new GameObject("box3", Colors.Red, new Point(200, 0), new Point(width, height), new Point(0, -2));


			currentPlayer = new Player("box4", Colors.Blue, new Point(250, 150), new Point(75, 75), new Point(0, -20));

			//Input.Instance.MoveEvent += new Input.MoveEventHandler(Instance_MoveEvent);
		}
		*/

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