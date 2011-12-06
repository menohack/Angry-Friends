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
            Initialize();
		}

        /// <summary>
        /// The hook for the Engine.
        /// </summary>
        public void Initialize()
        {
            Canvas viewport = new Canvas();
			canvas.Children.Add(viewport);

			Game game = new Game(viewport);
        }
	}
}