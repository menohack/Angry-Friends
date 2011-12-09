namespace Model.Engine.Utilities {
	public class RoomInfo {
		public int ID { get; set; }
		public string[] Players { get; set; }
		public int Total { get; set; }
		public override string ToString() { return ID.ToString(); }
	}
}