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

			EngineObject.Instance.Input.Target = redBox;

			TransformComponent tc2 = new TransformComponent(new Point(310, 400), 0, new Point(50, 50), null);
			RenderComponent rc2 = new RenderComponent(new Animation(new Frame(Colors.Blue, new Point(50, 50))), null); ;
			AudioComponent ac2 = new AudioComponent(new Dictionary<String, MediaElement>(), null);
			GameObject blueBox = new GameObject("blueBox", tc2, ac2, rc2);

			

			TransformComponent tc3 = new TransformComponent(new Point(600, 400), 0, new Point(50, 50), null);
			RenderComponent rc3 = new RenderComponent(new Animation(new Frame(Colors.Blue, new Point(50, 50))), null); ;
			AudioComponent ac3 = new AudioComponent(new Dictionary<String, MediaElement>(), null);
			Terrain terrain = new Terrain("terrain", tc3, ac3, rc3);

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