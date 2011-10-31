using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using System.Xml;
using System.Threading;
namespace Friendly_Wars.Engine.Utilities {
	/// <summary>
	/// Download manager
	/// </summary>
	public class Web {
		static WebClient webclient = new WebClient();
		static AutoResetEvent mre = new AutoResetEvent(false);
		/// <summary>
		/// Constructor for the download manager
		/// </summary>
		public Web() {
			webclient = new WebClient();
		}
		/// <summary>
		/// Contains the server path
		/// </summary>
		public static string Path { get { return "file:///C:/Users/Luis%20Grimaldo/Desktop/oose-game/trunk/Friendly%20Wars/Friendly%20Wars/"; } }
		/// <summary>
		/// Downloads a string sync
		/// </summary>
		/// <param name="url">URL where the string is located</param>
		/// <returns>The resulting string</returns>
		public static string DownloadString(string url) { return DownloadString(url, null); }
		/// <summary>
		/// Downloads a string async/sync
		/// </summary>
		/// <param name="url">URL where the string is located</param>
		/// <param name="onLoaded">Event to be fired once the download is completed</param>
		/// <returns>The resulting string if sync, An empty string if async</returns>
		public static string DownloadString(string url, Action<string> onLoaded) {
			string result = string.Empty;
			webclient.DownloadStringCompleted += (s, events) => {
				result = events.Result;
				if (onLoaded == null) mre.Set();
				else onLoaded(result);
			};
			webclient.DownloadStringAsync(new Uri(url));
			if (onLoaded == null) {
				mre.WaitOne();
				return result;
			}
			else return null;
		}
		/// <summary>
		/// Downloads an image sync
		/// </summary>
		/// <param name="url">URL where the image is located</param>
		/// <returns>The resulting image</returns>
		public static BitmapImage DownloadImage(string url) { return DownloadImage(url, null); }
		/// <summary>
		/// Downloads an image async/sync
		/// </summary>
		/// <param name="url">URL where the image is located</param>
		/// <param name="onLoaded">Event to be fired once the download is completed</param>
		/// <returns>The resulting string</returns>
		public static BitmapImage DownloadImage(string url, Action<BitmapImage> onLoaded) {
			var image = new BitmapImage(new Uri(url)) { CreateOptions = BitmapCreateOptions.None };
			image.ImageOpened += (s, events) => {
				if (onLoaded == null) mre.Set();
				else onLoaded(image);
			};
			image.UriSource = new Uri(url);
			if (onLoaded == null) {
				mre.WaitOne();
				return image;
			}
			else return null;
		}
		/// <summary>
		/// Downloads a sound sync
		/// </summary>
		/// <param name="url">URL where the sound is located</param>
		/// <returns>The resulting string</returns>
		public static MediaElement DownloadSound(string url) { return DownloadSound(url, null); }
		/// <summary>
		/// Downloads a sound sync
		/// </summary>
		/// <param name="url">URL where the sound is located</param>
		/// <param name="onLoaded">Event to be fired once the download is completed</param>
		/// <returns>The resulting string</returns>
		public static MediaElement DownloadSound(string url, Action<MediaElement> onLoaded) {
			MediaElement media = new MediaElement();
			media.Source = new Uri(url);
			media.Loaded += (s, events) => {
				if (onLoaded == null) mre.Set();
				else onLoaded(media);
			};
			if (onLoaded == null) {
				mre.WaitOne();
				return media;
			}
			else return null;
		}
	}
}