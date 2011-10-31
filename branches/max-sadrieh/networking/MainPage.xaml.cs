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

			// HOOK
			new Game();
		}
	}
}