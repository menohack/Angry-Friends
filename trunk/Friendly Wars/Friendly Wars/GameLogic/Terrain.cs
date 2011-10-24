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
using System.Windows.Media.Imaging;

namespace Friendly_Wars.GameLogic
{
    public class Terrain
    {
        private WriteableBitmap bitmap;
        private int width;
        private int height;
        private int posX = 0;
        private int posY = 0;
        

        public Terrain(Image image)
        {
            width = (int)image.Width;
            height = (int)image.Height;
            bitmap = new WriteableBitmap(width, height);
            for (int i = 0; i < width; i++)
            {
                for (int j=0; j < height/4 + Math.Sin(i*Math.PI) * (height/4) && j < height; j++)
                    bitmap.Pixels[j*width + i] = ConvertToARGB32(Color.FromArgb(255, 255, 0, 0));
            }

            bitmap.Invalidate();

            //image.SetValue(Canvas.LeftProperty, 400);
            //image.SetValue(Canvas.TopProperty, 400);

            //image.Height = height;
            //image.Width = width;

            

            image.Source = bitmap;
        }

        private int ConvertToARGB32(Color color)
        {
            return ((color.R << 16) | (color.G << 8) | (color.B << 0) | (color.A << 24));
        }

    }
}
