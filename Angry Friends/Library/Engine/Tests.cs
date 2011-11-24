using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Library.Engine.Utilities;
using Library.GameLogic;
using System.Windows.Threading;
using System.Collections.Generic;
using Library.Engine.Component.Graphic;
using System.Windows.Media.Imaging;

namespace Library.Engine
{
    public class Tests
    {
        public Tests() {
            Web.Instance.DownloadImage("http://alexanderschiffhauer.com/Friendly_Wars/spritesheet.png", data =>
            {
                SpriteSheetLoader.SpriteSheet spriteSheet = new SpriteSheetLoader.SpriteSheet(data, new Point(data.PixelWidth, data.PixelHeight), new Point(30, 30));
                IList<Frame> frames = SpriteSheetLoader.Instance.GetFramesFromSpriteSheet(spriteSheet, new Point(0, 0), new Point(3, 0));
            });
            //Web.Instance.DownloadMap("http://alexanderschiffhauer.com/Friendly_Wars/test.xml", progress);
        }

        /// <summary>
        /// This method is called by the map downloader to check progress and results
        /// </summary>
        /// <param name="percentage">Downloads percentage</param>
        /// <param name="mapInfo">Partial/Complete Map Information</param>
        public void progress(int percentage, MapInfo mapInfo)
        {
            if (percentage == 100)
            {
                Image image = new Image();
                image.Source = mapInfo.Images["spritesheet"];
               // Dispatcher.BeginInvoke(delegate()
               // {
                    // HALF OF THE TIME THIS DOESN'T WORK AND APPEARS 0.
                    // <3 SILVERLIGHT
                    //canvas.Children.Add(image);
                    //double height = image.ActualHeight;
                    //double width = image.ActualWidth;
                    //SpriteSheetLoader.SpriteSheet spriteSheet = new SpriteSheetLoader.SpriteSheet(image, 15, 15, 30, 30);
                    //IList<Frame> frames = SpriteSheetLoader.Instance.GetFramesFromSpriteSheet(spriteSheet, 0, 0, 3, 0);
               // });
            }
        }

    }
}
