using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Model.Engine.Utilities;
using System.Collections.Generic;
namespace Client {

    /// <summary>
    /// This class represents the Lobby, where users interact and join games.
    /// </summary>
	public partial class Lobby : UserControl, IUpdateable {

        /// <summary>
        /// Handles updating the list of games by polling.
        /// </summary>
        private EngineTimer gameListPoller;

        /// <summary>
        /// Allows EngineTimer to update once a second.
        /// </summary>
        private static readonly int UPDATE_INTERVAL = 1000;

        /// <summary>
        /// The Facebook ID of this user.
        /// </summary>
		private string facebookID = "TEST_FACEBOOK_ID";

        /// <summary>
        /// The ID of the room joined.
        /// </summary>
		private int gameLobbyID = -1;

        /// <summary>
        /// An empty constructor for Lobby.
        /// </summary>
		public Lobby() {
			InitializeComponent();
            Initialize();
		}

        /// <summary>
        /// Constructor for a new Lobby.
        /// </summary>
        /// <param name="fbid">The Facebook ID for this user.</param>
		public Lobby(string facebookID) {
			InitializeComponent();
			Initialize();
			this.facebookID = facebookID;
		}

        /// <summary>
        /// Initializes logic for running the Lobby.
        /// </summary>
		public void Initialize() {
            gameListPoller = new EngineTimer(UPDATE_INTERVAL, new List<IUpdateable> { this });
            gameListPoller.Start();
		}

        /// <summary>
        /// Updates the Lobby.
        /// </summary>
        /// <param name="deltaTime">The time, in milliseconds, since the last update.</param>
        public void Update(double deltaTime)
        {
            //Network.GetAllRooms(rooms => {
				//IEnumerable<RoomInfo> currentRooms = rooms.Where(r => r.ID == gameLobbyID);
                //if (gameLobbyID >= 0 && currentRooms.Count() > 0)
                //{
                  //  UpdateRoomDescription(currentRooms.ToArray()[0]);
                //}
                //else
              //  {
               //     roominfo.Text = "";
               // }
				//roomlist.ItemsSource = rooms;
			//});
        }

        /// <summary>
        /// Updates the description of a given room.
        /// </summary>
        /// <param name="room">The room to update</param>
		void UpdateRoomDescription(RoomInfo room) {
			string players = string.Empty;

			for (int i = 0; i < room.Players.Length; i++) {
                if (i > 0)
                {
                    players += ",";
                }
				players += room.Players[i];
			}

			roominfo.Text = string.Format("Room name: {0}\nCapacity: {1}/{2}\nPlayers: {3}", room.ID, room.Players.Length,room.Total, players);
		}

        /// <summary>
        /// A room has been selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void room_selected(object sender, SelectionChangedEventArgs e) {
			RoomInfo room = roomlist.SelectedItem as RoomInfo;
			if (room != null) {
				gameLobbyID = room.ID;
				UpdateRoomDescription(room);
			}
		}

        /// <summary>
        /// Join this room.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void join_room(object sender, RoutedEventArgs e) {
			//Network.JoinRoom(room_id, fbid, room => App.SwitchTo(new Room(fbid,room)));
		}
        
        /// <summary>
        /// Create this room.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void create_room(object sender, RoutedEventArgs e) {
			//Network.CreateRoom(fbid, room => App.SwitchTo(new Room(fbid,room)));
		}
    }
}