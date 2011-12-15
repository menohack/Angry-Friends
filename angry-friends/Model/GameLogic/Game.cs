using System.Runtime.Serialization;
using Model.Engine.Object;
using Model.Engine.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System;

namespace Model.GameLogic {

	/// <summary>
	/// This class represents the Game. All game-logic stems from here.
	/// </summary>
    [DataContract]
	public class Game : IUpdateable {

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

            Yoshi Yoshi = Factory.Instance.CreateYoshi();
            EngineObject.Instance.Input.Target = Yoshi;

            Factory.Instance.CreateBackground();
            Factory.Instance.CreateTerrain();

            EngineTimer engineTimer = new EngineTimer(3000, new Collection<IUpdateable> { this });
            engineTimer.Start();
		}

        /// <summary>
        /// Updates every 3 seconds to create apples.
        /// </summary>
        /// <param name="deltaTime">The time since the last apple.</param>
        public void Update(double deltaTime)
        {
            Random randomNumberGenerator = new Random();
            Point randomPoint = new Point(randomNumberGenerator.Next(0, 801), 0);
            Factory.Instance.CreateApple(randomPoint);
        }
    }
}