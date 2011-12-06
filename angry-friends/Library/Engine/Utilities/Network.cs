using System;
using System.Collections.Generic;

namespace Library.Engine.Utilities {
	public class Network {
		const string url = "http://luisgrimaldo.com/angryfriends/";
		public static void GetAllRooms(Action<List<RoomInfo>> onLoaded) {
			Web.Instance.DownloadString(url + "room.php?action=get", data => {
				List<RoomInfo> rooms = new List<RoomInfo>();
				string[] lines = data.Contains("\n") ? data.Split('\n') : new[] { data };
				for (int i = 0; i < lines.Length; i++) rooms.Add(ParseRoomInformation(lines[i]));
				onLoaded(rooms);
			});
		}
		public static void GetRoomById(int id, Action<RoomInfo> onLoaded) {
			Web.Instance.DownloadString(url + "room.php?action=get&id=" + id, data =>
				onLoaded(ParseRoomInformation(data)));
		}
		public static void JoinRoom(int id, string fbid, Action<RoomInfo> onLoaded) {
			GetRoomPlayersByIdAsString(id, players => {
				Web.Instance.DownloadString(url + "room.php?action=set&id=" + id + "&players=" + players + "," + fbid, data => onLoaded(ParseRoomInformation(data)));
			});
		}
		public static void CreateRoom(string fbid, Action<RoomInfo> onLoaded) {
			Web.Instance.DownloadString(url + "room.php?action=set", data =>
				JoinRoom(Convert.ToInt32(data), fbid, room => onLoaded(room)));
		}
		static RoomInfo ParseRoomInformation(string data) {
			var info = data.Split(';');
			RoomInfo room = new RoomInfo();
			room.ID = Convert.ToInt32(info[0]);
			room.Total = Convert.ToInt32(info[1]);
			room.Players = info[2].Contains(",") ? info[2].Split(',') : new[] { info[2] };
			return room;
		}
		public static void ChangeRoomSize(int id, int total, Action onLoaded) {
			GetRoomPlayersByIdAsString(id, players => {
				Web.Instance.DownloadString(url + "room.php?action=set&id=" + id + "&players=" + players + "&total=" + total, x => onLoaded());
			});
		}
		public static void GetGameById(int id, Action<GameInfo> onLoaded) {
			Web.Instance.DownloadString(url + "game.php?action=get&id=" + id, data => {
				GameInfo game = new GameInfo();
				game.State = data;
				onLoaded(game);
			});
		}
		public static void JoinGame(int id, Action onLoaded) {
			Web.Instance.DownloadString(url + "game.php?action=set&id=" + id, data => onLoaded());
		}
		static void GetRoomPlayersByIdAsString(int id, Action<string> onLoaded) {
			GetAllRooms(data => {
				data.ForEach(room => {
					if (room.ID == id) {
						string players = string.Empty;
						for (int i = 0; i < room.Players.Length; i++) {
							if (i > 0) players += ",";
							players += room.Players[i];
						}
						onLoaded(players);
						return;
					}
				});
			});
		}
	}
}