using System.Windows.Controls;
using Model.Engine.Utilities;
using System.Windows.Threading;
using System.Collections.ObjectModel;

namespace Client {
    /// <summary>
    /// The GameLobby -- where players are together in a Lobby, waiting to play a game with each other.
    /// </summary>
	public partial class GameLobby : UserControl, IUpdateable {
        
        /// <summary>
        /// The EngineTimer that polls for Updates.
        /// </summary>
        private EngineTimer engineTimer;

        /// <summary>
        /// The information about this GameLobby.
        /// </summary>
		private RoomInfo room;

        /// <summary>
        /// Constructor for a new GameLobby.
        /// </summary>
		public GameLobby() {
			InitializeComponent();
		}

        /// <summary>
        /// Constructor for a new GameLobby
        /// </summary>
        /// <param name="facebookID">The FacebookID of the user.</param>
        /// <param name="room">The information about the GameLobby.</param>
        public GameLobby(string facebookID, RoomInfo room) : this()
        {
			this.room = room;
            engineTimer = new EngineTimer(1000, new Collection<IUpdateable> { this });
            engineTimer.Start();
		}

        /// <summary>
        /// Updates the GameLobby every one second.
        /// </summary>
        /// <param name="deltaTime">The time elapsed since the last second.</param>
        public void Update(double deltaTime)
        {
            Network.GetRoomById(this.room.ID, room => {
				this.room = room;
				UpdateRoomDescription();
			});
        }

        /// <summary>
        /// Update the description of the GameLobby.
        /// </summary>
		private void UpdateRoomDescription() {
			string players = string.Empty;
			roomlist.ItemsSource = room.Players;
			progress.Value = 100 * room.Players.Length / (double)room.Total;
		}
    }
}