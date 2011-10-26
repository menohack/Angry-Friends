using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
namespace Friendly_Wars.GameLogic {
	public class MapInfo {
		public string Title { get; set; }
		public Dictionary<string, BitmapImage> Images { get; set; }
		public Dictionary<string, MediaElement> Sounds { get; set; }
	}
}