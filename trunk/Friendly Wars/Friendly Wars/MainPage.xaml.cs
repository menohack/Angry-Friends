using System.Windows.Controls;
using Friendly_Wars.Engine.Ultilities;
using Friendly_Wars.Engine.Object;

namespace Friendly_Wars {
	public partial class MainPage : UserControl {

		public static Grid canvas;
		/// <summary>
		/// This class represents our Silverlight page.
		/// </summary>
		public MainPage() {
			InitializeComponent();
			new World(World.WORLD_NAME);
			new Web();
			//var map = MediaLoader.LoadMapXml("file:///C:/Users/Luis%20Grimaldo/Desktop/oose-game/trunk/Friendly%20Wars/Friendly%20Wars/maps/beach.xml");
			//; ; ;
			//var image = new Image();
			//image.Source = Web.DownloadImage("http://www.google.com/intl/en_com/images/srpr/logo3w.png");
			//LayoutRoot.Children.Add(image);
			canvas = LayoutRoot;
		}
	}
}