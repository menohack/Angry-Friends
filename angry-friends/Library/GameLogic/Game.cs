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

		/// <summary>
		/// Constructor for the Game.
		/// </summary>
        public Game()
        {
            EngineObject = EngineObject.Instance;
        }
	}
}