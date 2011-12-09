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

            Player mario = Factory.Instance.CreateMario();
            EngineObject.Instance.Input.Target = mario;

            Factory.Instance.CreateBackground();
            Factory.Instance.CreateTerrain();

		}
	}
}