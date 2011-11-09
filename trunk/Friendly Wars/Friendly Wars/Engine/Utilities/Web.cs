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
	/// Download manager. Is responsible for loading media such as images, sound, text or maps.
    /// Has the ability to download media either synchronously or asynchronously depending on the context and needs of the caller.
	/// </summary>
	public class Web {

        /// <summary>
        /// The single instance of Web.
        /// </summary>
        private static Web instance;

        /// <summary>
        /// Accessor for the instance of Web.
        /// This accessor follows the singleton pattern for C# provided at Microsoft's MSDN.
        /// </summary>
        public static Web Instance
        {
            get
            {
                if (instance == null)
                    instance = new Web();
                return instance;
            }
        }

        /// <summary>
        /// The WebClient used to actually generate HTTP requests and process responses.
        /// </summary>
		WebClient webclient = new WebClient();
        /// <summary>
        /// 
        /// </summary>
		ManualResetEvent mre = new ManualResetEvent(false);

		/// <summary>
		/// Downloads a string asynchronously.
		/// </summary>
		/// <param name="url">URL where the string is located</param>
		/// <param name="onLoaded">Event to be fired once the download is completed</param>
		public void DownloadString(string url, Action<string> onLoaded) {
			webclient.DownloadStringCompleted += (s, events) => { onLoaded(events.Result); };
			webclient.DownloadStringAsync(new Uri(url));
		}

		/// <summary>
		/// Downloads an image synchronously
		/// </summary>
		/// <param name="url">URL where the image is located</param>
		/// <returns>The resulting image</returns>
		public BitmapImage DownloadImage(string url)
        {
            return DownloadImage(url, null);
        }

		/// <summary>
		/// Downloads an image either synchronouly or asynchornously
		/// </summary>
		/// <param name="url">URL where the image is located</param>
		/// <param name="onLoaded">Event to be fired once the download is completed or null</param>
		/// <returns>The resulting image if onLoaded is null; otherwise null</returns>
		public BitmapImage DownloadImage(string url, Action<BitmapImage> onLoaded)
        {
			BitmapImage image = new BitmapImage(new Uri(url)) { CreateOptions = BitmapCreateOptions.None };

			image.ImageOpened += (s, events) =>
            {
				if (onLoaded == null) mre.Set();
				else onLoaded(image);
			};

			image.UriSource = new Uri(url);

            // If we are downloading the image synchronously, we wait 1 millisecond which we *GUESS* is enough time for the download FIXME FIXME FIXME!!!@!!@!@@!!11!11one
			if (onLoaded == null)
            {
				mre.WaitOne(1);
				return image;
			}
			return null;
		}

		/// <summary>
		/// Downloads a sound synchronously
		/// </summary>
		/// <param name="url">URL where the sound is located</param>
		/// <returns>The resulting sound</returns>
		public MediaElement DownloadSound(string url)
        {
            return DownloadSound(url, null);
        }

		/// <summary>
		/// Downloads a sound either synchronously or asynchronously
		/// </summary>
		/// <param name="url">URL where the sound is located</param>
		/// <param name="onLoaded">Event to be fired once the download is completed or null</param>
		/// <returns>The resulting sound if onLoaded is null; otherwise null</returns>
		public MediaElement DownloadSound(string url, Action<MediaElement> onLoaded)
        {
			MediaElement media = new MediaElement();
			media.Source = new Uri(url);

			media.Loaded += (s, events) =>
            {
				if (onLoaded == null) mre.Set();
				else onLoaded(media);
			};

			if (onLoaded == null)
            {
				mre.WaitOne(1); // FIXME FIXME FIXME TODO
				return media;
			}
			return null;
		}

		/// <summary>
		/// Downloads a map asynchronously
		/// </summary>
		/// <param name="url">URL where the xml file describing the map is located</param>
		/// <returns></returns>
		public void DownloadMap(string url, Action<int, MapInfo> progress)
        {
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