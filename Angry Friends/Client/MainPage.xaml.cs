using System.Windows;
using System.Windows.Controls;
using Library.Engine.Object;
using Library.Engine.Utilities;
using Library.GameLogic;
using System.Windows.Media.Imaging;
using Library.Engine.Component.Graphic;
using System.Collections.Generic;
using System.Diagnostics;
namespace Client {
	public partial class MainPage : UserControl {

		/// <summary>
		/// This class represents our Silverlight page.
		/// </summary>
		public MainPage() {
			InitializeComponent();
			Web.Instance.DownloadMap("http://alexanderschiffhauer.com/Friendly_Wars/test.xml", progress);

			Canvas canvas2electricboogaloo = new Canvas();
			canvas.Children.Add(canvas2electricboogaloo);

			Game game = new Game(canvas2electricboogaloo);
		}



		/// <summary>
		/// This method is called by the map downloader to check progress and results
		/// </summary>
		/// <param name="percentage">Downloads percentage</param>
		/// <param name="mapInfo">Partial/Complete Map Information</param>
		public void progress(int percentage, MapInfo mapInfo) {
            BitmapImage image = mapInfo.Images["spritesheet"];
            //canvas.Children.Add(image);


            ///WTF?!
            Debug.WriteLine(image.PixelHeight);
            Debug.WriteLine(image.PixelWidth);



            //SpriteSheetLoader.SpriteSheet spriteSheet = new SpriteSheetLoader.SpriteSheet(image, 15, 15, 30, 30);
            //IList<Frame> frames = SpriteSheetLoader.Instance.GetFramesFromSpriteSheet(spriteSheet, 0, 0, 3, 3);

		///	textBlock1.Text = percentage + "% Downloaded";
			///if (percentage == 100) {
				///image1.Source = mapInfo.Images["spritesheet"];
			///}
		}
	}
}