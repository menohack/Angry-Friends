using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using System.Xml;
using Friendly_Wars.GameLogic;
namespace Friendly_Wars.Engine.Utilities {
	/// <summary>
	/// Loads the media
	/// </summary>
	public class MediaLoader {
		public MediaLoader() {

		}
		/// <summary>
		/// Loads a map's xml description
		/// </summary>
		/// <param name="filename">XML filename</param>
		/// <returns>Map Information</returns>
		public static MapInfo LoadMapXml(string text) {
			//XmlReader reader = XmlReader.Create(new StringReader(Web.DownloadString(filename)));
			using (XmlReader reader = XmlReader.Create(new StringReader(text))) {
				MapInfo mapInfo = new MapInfo();
				var path = Web.Path + "maps/";
				reader.ReadToFollowing("map");
				reader.MoveToAttribute("title");
				mapInfo.Title = reader.Value;
				reader.MoveToContent();
				reader.Read();
				while (reader.Read()) {
					switch (reader.Name) {
						case "resources":
							while (reader.Read()) {
								reader.Read();
								string type = reader.Name;
								reader.MoveToAttribute("url");
								string name = reader.Value.Contains(".") ? reader.Value.Split('.')[0] : reader.Value;
								string url = path + "resources/" + reader.Value;
								switch (type) {
									case "image": mapInfo.Images.Add(name, Web.DownloadImage(url)); break;
									case "sound": mapInfo.Sounds.Add(name, Web.DownloadSound(url)); break;
								}
							}
							break;
					}
				}
				return mapInfo;
			}
		}
	}
}