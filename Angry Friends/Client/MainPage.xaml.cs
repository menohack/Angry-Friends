﻿using System.Windows;
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
			if (percentage == 100) {
                Image image = new Image();
                image.Source = mapInfo.Images["spritesheet"];
                Dispatcher.BeginInvoke(delegate()
                {
                    // HALF OF THE TIME THIS DOESN'T WORK AND APPEARS 0.
                    // <3 SILVERLIGHT
                    canvas.Children.Add(image);
                    double height = image.ActualHeight;
                    double width = image.ActualWidth;
                    SpriteSheetLoader.SpriteSheet spriteSheet = new SpriteSheetLoader.SpriteSheet(image, 15, 15, 30, 30);
                    IList<Frame> frames = SpriteSheetLoader.Instance.GetFramesFromSpriteSheet(spriteSheet, 0, 0, 3, 0);
                });
			}
		}
	}
}