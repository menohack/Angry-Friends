using System;
using System.Collections.Generic;

namespace Model.Engine.Utilities {
	public class Network {
		const string url = "http://luisgrimaldo.com/angryfriends/";
		public static void GetAllRooms(Action<List<RoomInfo>> onLoaded) {
			AssetManager.Instance.Download(url + "room.php?action=get", ExternalAsset.ExternalAssetType.String, data => {
                string dataString = (string) data.Value;
				List<RoomInfo> rooms = new List<RoomInfo>();
                string[] lines = dataString.Contains("\n") ? dataString.Split('\n') : new[] { dataString };
				for (int i = 0; i < lines.Length; i++) rooms.Add(ParseRoomInformation(lines[i]));
				onLoaded(rooms);
			});
		}
        public static void GetRoomById(int id, Action<RoomInfo> onLoaded)
        {
            AssetManager.Instance.Download(url + "room.php?action=get&id=" + id, ExternalAsset.ExternalAssetType.String, data =>
            {
                string dataString = (string)data.Value;
                onLoaded(ParseRoomInformation(dataString));
            });
        }
		public static void JoinRoom(int id, string fbid, Action<RoomInfo> onLoaded) {
			GetRoomPlayersByIdAsString(id, players => {
				AssetManager.Instance.Download(url + "room.php?action=set&id=" + id + "&players=" + players + "," + fbid, ExternalAsset.ExternalAssetType.String, data => onLoaded(ParseRoomInformation((string)data.Value)));
			});
		}
		public static void CreateRoom(string fbid, Action<RoomInfo> onLoaded) {
			AssetManager.Instance.Download(url + "room.php?action=set", ExternalAsset.ExternalAssetType.String, data =>
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
				AssetManager.Instance.Download(url + "room.php?action=set&id=" + id + "&players=" + players + "&total=" + total, ExternalAsset.ExternalAssetType.String, x => onLoaded());
			});
		}
		public static void GetGameById(int id, Action<GameInfo> onLoaded) {
			AssetManager.Instance.Download(url + "game.php?action=get&id=" + id, ExternalAsset.ExternalAssetType.String, data => {
				GameInfo game = new GameInfo();
				game.State = (string)data.Value;
				onLoaded(game);
			});
		}
		public static void JoinGame(int id, Action onLoaded) {
			AssetManager.Instance.Download(url + "game.php?action=set&id=" + id, ExternalAsset.ExternalAssetType.String, data => onLoaded());
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