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
			Web.Instance.DownloadMap("http://alexanderschiffhauer.com/Friendly_Wars/test.xml", progress);
			Game game = new Game();
		}

		/// <summary>
		/// This method is called by the map downloader to check progress and results
		/// </summary>
		/// <param name="percentage">Downloads percentage</param>
		/// <param name="mapInfo">Partial/Complete Map Information</param>
		public void progress(int percentage, MapInfo mapInfo) {
			textBlock1.Text = percentage + "% Downloaded";
			if (percentage == 100) {
				image1.Source = mapInfo.Images["spritesheet"];
			}
		}
	}
}