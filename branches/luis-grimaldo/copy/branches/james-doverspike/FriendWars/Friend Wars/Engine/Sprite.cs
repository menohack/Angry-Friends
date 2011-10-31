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


    public class Sprite : Control
    {
        //private String name;
        private double posX;
        private double posY;
        private Image image;

        private double start = 200.0;
        private double end = 400.0;

        public Sprite(double posX, double posY, Image image)
        {
            this.posX = posX;
            this.posY = posY;

            this.SetValue(Canvas.LeftProperty, posX);
            this.SetValue(Canvas.TopProperty, posY);

            //Uri uri = new Uri(resource, UriKind.Relative);
            //ImageSource imgSrc = new System.Windows.Media.Imaging.BitmapImage(uri);
            //image.Source = imgSrc;
            this.image = image;
        }

        public void Update(int millis) {
            double t = (millis % 1000) / 1000.0;
            double pos = start + (end - start) * t;
            image.SetValue(Canvas.LeftProperty, pos);
        }
    }
