using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using System.Xml;
using Friendly_Wars.GameLogic;
namespace Friendly_Wars.Engine.Ultilities {
	public class MediaLoader {
		public static Dictionary<string, MediaElement> mediaElements;
		public MediaLoader() {
			mediaElements = new Dictionary<string, MediaElement>();
		}
		public static MapInfo LoadMapXml(string filename) {
			XmlReader reader = XmlReader.Create(new StringReader(Web.DownloadString(filename)));
			MapInfo mapInfo = new MapInfo();
			var path = Web.Path + "maps/";
			reader.ReadToFollowing("map");
			while (reader.MoveToNextAttribute()) {
				switch (reader.Name) {
					case "title": mapInfo.Title = reader.Value; break;
					case "resources":
						if (reader.HasAttributes) {
							do {
								string name = reader.Value.Contains(".") ? reader.Value.Split('.')[0] : reader.Value;
								string url = path + "resources/" + reader.Value;
								switch (reader.Name) {
									case "image": mapInfo.Images.Add(name, Web.DownloadImage(url)); break;
									case "sound": mapInfo.Sounds.Add(name, Web.DownloadSound(url)); break;
								}
							}
							while (reader.MoveToNextAttribute());
						}
						break;
				}
			}
			return mapInfo;
		}
	}
}