using System.Windows;
using System.Windows.Controls;
using Library.Engine.Object;
using Library.Engine.Utilities;
using Library.GameLogic;
using System.Windows.Media.Imaging;
using Library.Engine.Component.Graphic;
using System.Collections.Generic;
using System.Diagnostics;
using Library.Engine;
namespace Client {
	public partial class MainPage : UserControl {

		/// <summary>
		/// This class represents our Silverlight page.
		/// </summary>
		public MainPage() {
			InitializeComponent();

			Canvas canvas2electricboogaloo = new Canvas();
			canvas.Children.Add(canvas2electricboogaloo);

			Game game = new Game(canvas2electricboogaloo);
            new Tests();
		}
	}
}