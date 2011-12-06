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
		/// The hook for our Silverlight application.
		/// </summary>
		public MainPage() {
            InitializeComponent();
            Initialize();
		}

        /// <summary>
        /// The hook for Engine.
        /// </summary>
        private void Initialize()
        {
            new EngineObjectHelper(canvas);
        }
	}
}