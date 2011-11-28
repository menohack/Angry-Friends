﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Xml;
using Library.GameLogic;
namespace Library.Engine.Utilities {
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
		public static Web Instance {
			get {
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
		/// Downloads any type of resource
		/// </summary>
		/// <param name="url">URL where the string is located</param>
		/// <param name="type">The type of the download file</param>
		/// <param name="onLoaded">Event to be fired once the download is in progress or completed</param>
		public void Download(string url, Resource.Types type, Action<Resource> onLoaded) {
			switch (type) {
				case Resource.Types.Image: DownloadImage(url, data => onLoaded(new Resource(url, data))); break;
				case Resource.Types.Sound: DownloadSound(url, data => onLoaded(new Resource(url, data))); break;
			}
		}
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
		public BitmapImage DownloadImage(string url) {
			return DownloadImage(url, null);
		}
		/// <summary>
		/// Downloads an image either synchronouly or asynchornously
		/// </summary>
		/// <param name="url">URL where the image is located</param>
		/// <param name="onLoaded">Event to be fired once the download is completed or null</param>
		/// <returns>The resulting image if onLoaded is null; otherwise null</returns>
		public BitmapImage DownloadImage(string url, Action<BitmapImage> onLoaded) {
			BitmapImage image = new BitmapImage(new Uri(url)) { CreateOptions = BitmapCreateOptions.None };

			image.ImageOpened += (s, events) => {
				if (onLoaded == null) mre.Set();
				else onLoaded(image);
			};

			image.UriSource = new Uri(url);

			// If we are downloading the image synchronously, we wait 1 millisecond which we *GUESS* is enough time for the download FIXME FIXME FIXME!!!@!!@!@@!!11!11one
			if (onLoaded == null) {
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
		public MediaElement DownloadSound(string url) {
			return DownloadSound(url, null);
		}
		/// <summary>
		/// Downloads a sound either synchronously or asynchronously
		/// </summary>
		/// <param name="url">URL where the sound is located</param>
		/// <param name="onLoaded">Event to be fired once the download is completed or null</param>
		/// <returns>The resulting sound if onLoaded is null; otherwise null</returns>
		public MediaElement DownloadSound(string url, Action<MediaElement> onLoaded) {
			MediaElement media = new MediaElement();
			media.Source = new Uri(url);

			media.Loaded += (s, events) => {
				if (onLoaded == null) mre.Set();
				else onLoaded(media);
			};

			if (onLoaded == null) {
				mre.WaitOne(1); // FIXME FIXME FIXME TODO
				return media;
			}
			return null;
		}
		void LoadResources(int index, List<Resource> resources, MapInfo mapInfo, Action<int, MapInfo> progress) {
			Download(resources[index].URL, resources[index].Type, res => {
				switch (res.Type) {
					case Resource.Types.Image: mapInfo.Images.Add(res.Name, (BitmapImage)res.Value); break;
					case Resource.Types.Sound: mapInfo.Sounds.Add(res.Name, (MediaElement)res.Value); break;
				}
				progress(Convert.ToInt32(100.0 * ++index / resources.Count), mapInfo);
				if (index < resources.Count) LoadResources(index, resources, mapInfo, progress);
			});
		}
		/// <summary>
		/// Downloads a map asynchronously
		/// </summary>
		/// <param name="url">URL where the xml file describing the map is located</param>
		/// <param name="progress">Progress of the download</param>
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
					LoadResources(0, resources, mapInfo, progress);
				};
			});
		}
	}
}