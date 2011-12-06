using System.Windows.Controls;
using Library.Engine.Utilities;
using System.Windows.Threading;

namespace Client {
	public partial class Room : UserControl {
		DispatcherTimer updater = new DispatcherTimer();
		RoomInfo room;
		public Room() {
			InitializeComponent();
		}
		public Room(string fbid, RoomInfo room) {
			InitializeComponent();
			this.room = room;
			updater.Interval = new System.TimeSpan(0, 0, 1); // every one second
			updater.Tick += new System.EventHandler(UpdateList);
			updater.Start();
		}
		void UpdateList(object sender, System.EventArgs e) {
			Network.GetRoomById(this.room.ID, room => {
				this.room = room;
				UpdateRoomDescription();
			});
		}
		void UpdateRoomDescription() {
			string players = string.Empty;
			roomlist.ItemsSource = room.Players;
			progress.Value = 100 * room.Players.Length / (double)room.Total;
			//if(room.Players.Length==room.Total) goto Arena
		}
	}
}