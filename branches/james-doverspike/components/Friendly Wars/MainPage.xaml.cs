using System.Windows.Controls;
using Friendly_Wars.Engine.Utilities;
using Friendly_Wars.Engine.Object;
using Friendly_Wars.GameLogic;

namespace Friendly_Wars {

	/// <summary>
	/// This class represents all of the objects on our Silverlight page. Interacting with this class is how the application communicates
	/// with Silverlight. For example, any image that needs to be displayed must be added to one of the children of this class.
	/// </summary>
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