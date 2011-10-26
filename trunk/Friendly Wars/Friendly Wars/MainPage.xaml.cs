using System.Windows.Controls;
using Friendly_Wars.Engine.Ultilities;
using Friendly_Wars.Engine.Object;

namespace Friendly_Wars {
	public partial class MainPage : UserControl {
		public static MainPage page;
		/// <summary>
		/// This class represents our Silverlight page.
		/// </summary>
		public MainPage() {
			InitializeComponent();
			page = this;
			new World(World.WORLD_NAME);
			new Web();
			var map = MediaLoader.LoadMapXml("file:///C:/Users/Luis%20Grimaldo/Desktop/oose-game/trunk/Friendly%20Wars/Friendly%20Wars/maps/beach.xml");
			; ; ;
		}
	}
}