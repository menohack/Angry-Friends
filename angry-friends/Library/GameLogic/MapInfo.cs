using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
namespace Library.GameLogic {
	/// <summary>
	/// Map Information
	/// </summary>
	public class MapInfo {
		/// <summary>
		/// Constructor for the Map Information
		/// </summary>
		public MapInfo() {
			Images = new Dictionary<string, BitmapImage>();
			Sounds = new Dictionary<string, MediaElement>();
		}
		/// <summary>
		/// Title of the map
		/// </summary>
		public string Title { get; set; }
		/// <summary>
		/// Dictionary containing all the images used in the map
		/// </summary>
		public Dictionary<string, BitmapImage> Images { get; set; }
		/// <summary>
		/// Dictionary containing all the sounds used in the map
		/// </summary>
		public Dictionary<string, MediaElement> Sounds { get; set; }
	}
}