using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Model.Engine.Component.Media;
using Model.Engine.Component.Media.Rendering;
using Model.Engine.Component.Transform;
using Model.Engine.Object;
using Model.Engine.Object.GameObjects;
using System.Windows.Media.Imaging;
using Model.Engine.Utilities;

namespace Model.GameLogic {

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

		/// <summary>
		/// The current player.
		/// </summary>
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
			Player redBox = new Player("redBox", new Point(500.0, 500.0), tc, ac, rc);
			currentPlayer = redBox;

			//Set the player as the target of user input.
			//EngineObject.Instance.Input.Target = redBox;

			TransformComponent tc2 = new TransformComponent(new Point(310, 400), 0, new Point(50, 50), null);
			RenderComponent rc2 = new RenderComponent(new Animation(new Frame(Colors.Blue, new Point(50, 50))), null); ;
			AudioComponent ac2 = new AudioComponent(new Dictionary<String, MediaElement>(), null);
			GameObject blueBox = new GameObject("blueBox", tc2, ac2, rc2);

            TransformComponent tc4 = new TransformComponent(new Point(0, 0), 0, new Point(29, 29), null);
            
            BitmapImage bImage1 = AssetManager.Instance.ExternalAssets["mario_walk_01"].GetBitmapImage();
            Image image1 = new Image();
            image1.Source = bImage1;
            Frame frame1 = new Frame(image1);

            BitmapImage bImage2 = AssetManager.Instance.ExternalAssets["mario_walk_03"].GetBitmapImage();
            Image image2 = new Image();
            image2.Source = bImage2;
            Frame frame2 = new Frame(image2);

            BitmapImage bImage3 = AssetManager.Instance.ExternalAssets["mario_walk_02"].GetBitmapImage();
            Image image3 = new Image();
            image3.Source = bImage3;
            Frame frame3 = new Frame(image3);

            Animation animation = new Animation(new List<Frame> { frame1, frame2, frame3 }, 1, 5, "walk");
            RenderComponent rc4 = new RenderComponent(animation);
            AudioComponent ac4 = new AudioComponent(new Dictionary<String, MediaElement>(), null);

            Player mario = new Player("mario", new Point(500, 500), tc4, ac4, rc4);
            EngineObject.Instance.Input.Target = mario;

            Factory.Instance.CreateBackground();
            Factory.Instance.CreateTerrain();

		}
	}
}