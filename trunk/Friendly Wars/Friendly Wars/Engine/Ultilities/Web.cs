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
namespace Friendly_Wars.Engine.Ultilities {
	public class Web {
		static WebClient webclient = new WebClient();
		static AutoResetEvent mre = new AutoResetEvent(false);
		public Web() {
			webclient = new WebClient();
		}
		public static string Path { get { return "file:///C:/Users/Luis%20Grimaldo/Desktop/oose-game/trunk/Friendly%20Wars/Friendly%20Wars/"; } }
		public static string DownloadString(string url) { return DownloadString(url, null); }
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
		public static BitmapImage DownloadImage(string url) { return DownloadImage(url, null); }
		public static BitmapImage DownloadImage(string url, Action<BitmapImage> onLoaded) {
			var image = new BitmapImage() { CreateOptions = BitmapCreateOptions.None };
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
		public static MediaElement DownloadSound(string url) { return DownloadSound(url, null); }
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