using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Xml;
using Friendly_Wars.GameLogic;
namespace Friendly_Wars.Engine.Utilities {
	/// <summary>
	/// Download manager
	/// </summary>
	public class Web {

        private static Web instance;
        public static Web Instance
        {
            get
            {
                if (instance == null)
                    instance = new Web();
                return instance;
            }
        }

		WebClient webclient = new WebClient();
		ManualResetEvent mre = new ManualResetEvent(false);
		/// <summary>
		/// Downloads a string async/sync
		/// </summary>
		/// <param name="url">URL where the string is located</param>
		/// <param name="onLoaded">Event to be fired once the download is completed</param>
		/// <returns>The resulting string if sync, An empty string if async</returns>
		public void DownloadString(string url, Action<string> onLoaded) {
			webclient.DownloadStringCompleted += (s, events) => { onLoaded(events.Result); };
			webclient.DownloadStringAsync(new Uri(url));
		}
		/// <summary>
		/// Downloads an image sync
		/// </summary>
		/// <param name="url">URL where the image is located</param>
		/// <returns>The resulting image</returns>
		public BitmapImage DownloadImage(string url) { return DownloadImage(url, null); }
		/// <summary>
		/// Downloads an image async/sync
		/// </summary>
		/// <param name="url">URL where the image is located</param>
		/// <param name="onLoaded">Event to be fired once the download is completed</param>
		/// <returns>The resulting string</returns>
		public BitmapImage DownloadImage(string url, Action<BitmapImage> onLoaded) {
			try {
				var image = new BitmapImage(new Uri(url)) { CreateOptions = BitmapCreateOptions.None };
				image.ImageOpened += (s, events) => {
					if (onLoaded == null) mre.Set();
					else onLoaded(image);
				};
				image.UriSource = new Uri(url);
				if (onLoaded == null) {
					mre.WaitOne(1);
					return image;
				}
			}
			catch { }
			return null;
		}
		/// <summary>
		/// Downloads a sound sync
		/// </summary>
		/// <param name="url">URL where the sound is located</param>
		/// <returns>The resulting string</returns>
		public MediaElement DownloadSound(string url) { return DownloadSound(url, null); }
		/// <summary>
		/// Downloads a sound sync
		/// </summary>
		/// <param name="url">URL where the sound is located</param>
		/// <param name="onLoaded">Event to be fired once the download is completed</param>
		/// <returns>The resulting string</returns>
		public MediaElement DownloadSound(string url, Action<MediaElement> onLoaded) {
			try {
				MediaElement media = new MediaElement();
				media.Source = new Uri(url);
				media.Loaded += (s, events) => {
					if (onLoaded == null) mre.Set();
					else onLoaded(media);
				};
				if (onLoaded == null) {
					mre.WaitOne(1);
					return media;
				}
			}
			catch { }
			return null;
		}
		/// <summary>
		/// Downloads a map asynchronously
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public void DownloadMap(string url, Action<int, MapInfo> progress) {
			DownloadString(url, data => {
				using (XmlReader reader = XmlReader.Create(new StringReader(data))) {
					MapInfo mapInfo = new MapInfo();
					List<Resource> resources = new List<Resource>();
					reader.MoveToContent();
					while (reader.MoveToNextAttribute())
						switch (reader.Name) {
							case "title": mapInfo.Title = reader.Value; break;
						}
					while (reader.Read()) // Reads all the elements
						if (reader.NodeType == XmlNodeType.Element)
							switch (reader.Name) {
								case "resources": // If in 'resources' element
									while (reader.Read()) { // reads all the resources
										switch (reader.NodeType) {
											case XmlNodeType.Element:
												resources.Add(new Resource(reader.GetAttribute("url"), reader.Name, reader.GetAttribute("priority")));
												break;
										}
									}
									break;
							}
					double count = 0, total = resources.Count;
					resources.ForEach(r => {
						switch (r.Type) {
							case Resource.Types.Image:
								BitmapImage image = DownloadImage(r.URL);
								if (image == null)
									throw new Exception(string.Format("The image resource \"{0}\" in the map \"{1}\" was not found.", r.Name, mapInfo.Title));
								else mapInfo.Images.Add(r.Name, image);
								break;
							case Resource.Types.Sound:
								MediaElement sound = DownloadSound(r.URL);
								if (sound == null)
									throw new Exception(string.Format("The sound resource \"{0}\" in the map \"{1}\" was not found.", r.Name, mapInfo.Title));
								else mapInfo.Sounds.Add(r.Name, sound);
								break;
						}
						progress(Convert.ToInt32(100.0 * ++count / total), mapInfo);
					});
				}
			});
		}
	}
}