using System.Runtime.Serialization;
using Model.Engine.Object;

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
		/// Constructor for the Game.
		/// </summary>
		public Game()
		{
			EngineObject = EngineObject.Instance;

            Mario mario = Factory.Instance.CreateMario();
            EngineObject.Instance.Input.Target = mario;

            Factory.Instance.CreateBackground();
            Factory.Instance.CreateTerrain();
		}
	}
}