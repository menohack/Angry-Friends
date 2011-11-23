using System.Windows;
using System.Windows.Controls;
using Library.Engine.Object;
using Library.Engine.Utilities;
using Library.GameLogic;
namespace Client {
	public partial class MainPage : UserControl {

		/// <summary>
		/// This class represents our Silverlight page.
		/// </summary>
		public MainPage() {
			InitializeComponent();
			Web.Instance.DownloadMap("http://alexanderschiffhauer.com/Friendly_Wars/test.xml", progress);
			Game game = new Game();
			game.World.GameObjectAdded += new World.ObjectEventArgs(GameObjectAdded);
            game.World.GameObjectRemoved += new World.ObjectEventArgs(GameObjectRemoved);
		}

		void GameObjectRemoved(Image image) {
			canvas.Children.Remove(image);
		}
		void GameObjectAdded(Image image) {
			canvas.Children.Add(image);
		}

		/// <summary>
		/// This method is called by the map downloader to check progress and results
		/// </summary>
		/// <param name="percentage">Downloads percentage</param>
		/// <param name="mapInfo">Partial/Complete Map Information</param>
		public void progress(int percentage, MapInfo mapInfo) {
		///	textBlock1.Text = percentage + "% Downloaded";
			///if (percentage == 100) {
				///image1.Source = mapInfo.Images["spritesheet"];
			///}
		}
	}
}