using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
namespace Friendly_Wars.GameLogic {
	/// <summary>
	/// Map Information
	/// </summary>
	public class MapInfo {
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