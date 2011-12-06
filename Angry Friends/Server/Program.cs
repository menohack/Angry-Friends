using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Library;
using Library.GameLogic;
using Library.Engine.Utilities;
namespace Server {
	class Program {
		static void Main(string[] args)
		{
			Console.WriteLine("Rooms Available");
			Console.WriteLine("ID\tTotal\tPlayers");
			Network.GetAllRooms(rooms => {
				rooms.ForEach(room => Console.WriteLine(string.Format("{0}\t{1}\t{2}",
					room.ID, room.Total, room.Players)));
			});
			Console.Read();
		}
	}
}