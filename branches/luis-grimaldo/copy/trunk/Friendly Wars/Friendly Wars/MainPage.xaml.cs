using System.Windows.Controls;
using Friendly_Wars.Engine.Utilities;
using Friendly_Wars.Engine.Object;
using Friendly_Wars.GameLogic;

namespace Friendly_Wars {
	public partial class MainPage : UserControl {

		/// <summary>
		/// A reference to the instance of our MainPage; this way, we can access non-static fields.
		/// </summary>
		public static MainPage mainPage;

		/// <summary>
		/// This class represents our Silverlight page.
		/// </summary>
		public MainPage() {
			InitializeComponent();
			mainPage = this;
			Web.DownloadMap("http://luisgrimaldo.com/test.xml", progress);
		}
		/// <summary>
		/// This method is called by the map downloader to check progress and results
		/// </summary>
		/// <param name="percentage">Downloads percentage</param>
		/// <param name="mapInfo">Partial/Complete Map Information</param>
		public void progress(int percentage, MapInfo mapInfo) {
			textBlock1.Text = percentage + "% Downloaded";
			if (percentage == 100) {
				image1.Source = mapInfo.Images["hw2"];
				image2.Source = mapInfo.Images["hw4"];
				image3.Source = mapInfo.Images["bg_0"];
				image4.Source = mapInfo.Images["non-word"];
				image5.Source = mapInfo.Images["word"];
				image6.Source = mapInfo.Images["logo_0"];
				image7.Source = mapInfo.Images["blur_0"];
				image8.Source = mapInfo.Images["plus_0"];
				image9.Source = mapInfo.Images["check_0"];
				image10.Source = mapInfo.Images["blur"];
			}
		}
	}
}