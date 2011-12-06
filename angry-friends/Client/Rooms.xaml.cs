using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Library.Engine.Utilities;
namespace Client {
	public partial class Rooms : UserControl {
		DispatcherTimer updater = new DispatcherTimer();
		string fbid = "1234423";
		int room_id = -1;
		/// <summary>
		/// This class represents our Silverlight page.
		/// </summary>
		public Rooms() {
			InitializeComponent();
            Initialize();
		}

		public Rooms(string fbid) {
			InitializeComponent();
			Initialize();
			this.fbid = fbid;
		}

        /// <summary>
        /// The hook for the Engine.
        /// </summary>
		public void Initialize() {
			updater.Interval = new System.TimeSpan(0, 0, 1); // every one second
			updater.Tick += new System.EventHandler(UpdateList);
			updater.Start();
		}

		void UpdateList(object sender, System.EventArgs e) {
			Network.GetAllRooms(rooms => {
				var currentRooms = rooms.Where(r => r.ID == room_id);
				if (room_id >= 0 && currentRooms.Count()>0) UpdateRoomDescription(currentRooms.ToArray()[0]);
				else roominfo.Text = "";
				roomlist.ItemsSource = rooms;
			});
		}

		void UpdateRoomDescription(RoomInfo room) {
			string players = string.Empty;
			for (int i = 0; i < room.Players.Length; i++) {
				if (i > 0) players += ",";
				players += room.Players[i];
			}
			roominfo.Text = string.Format("Room name: {0}\nCapacity: {1}/{2}\nPlayers: {3}", room.ID, room.Players.Length,room.Total, players);
		}

		private void room_selected(object sender, SelectionChangedEventArgs e) {
			RoomInfo room = roomlist.SelectedItem as RoomInfo;
			if (room != null) {
				room_id = room.ID;
				UpdateRoomDescription(room);
			}
		}

		private void join_room(object sender, RoutedEventArgs e) {
			Network.JoinRoom(room_id, fbid, room => App.SwitchTo(new Room(fbid,room)));
		}

		private void create_room(object sender, RoutedEventArgs e) {
			Network.CreateRoom(fbid, room => App.SwitchTo(new Room(fbid,room)));
		}
	}
}