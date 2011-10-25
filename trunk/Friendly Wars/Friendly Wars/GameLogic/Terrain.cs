﻿using System;
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

using Friendly_Wars.Engine.Object;

namespace Friendly_Wars.GameLogic
{
    /// <summary>
    /// The Terrain class represents the deformable ground.
    /// </summary>
    public class Terrain : UpdateableGameObject
    {
        private WriteableBitmap bitmap;

        /// <summary>
        /// Constructs a Terrain object.
        /// </summary>
        /// <param name="image"></param>
        public Terrain(Image image, String name, String tag = null) : base(name, tag)
        {
            renderComponent.Bitmap = image;

            int width = (int) renderComponent.Bitmap.Width * 2;
            int height = (int) renderComponent.Bitmap.Height * 2;

            WriteableBitmap bitmap = new WriteableBitmap(width, height);

            ///Initialize the terrain
            for (int i = 0; i < width; i++)
            {
                for (int j=0; j < height; j++) {
                    if (j > 3*height/4 + Math.Sin(i/(8.0*Math.PI)) * (height/8))
                        bitmap.Pixels[j*width + i] = ConvertToARGB32(Color.FromArgb(255, 0, 50, 200));
                    else
                        bitmap.Pixels[j * width + i] = ConvertToARGB32(Color.FromArgb(255, 0, 200, 200));
                }
            }


            bitmap.Invalidate();

            renderComponent.Bitmap.Source = bitmap;
        }

        /// <summary>
        /// This function checks if the GameObject is colliding with the terrain.
        /// </summary>
        /// <param name="gameObject"> The colliding object. </param>
        public bool Collide(GameObject gameObject)
        {
            return physicsComponent.Collide(gameObject);
        }

        /// <summary>
        /// Updates the terrain.
        /// </summary>
        public override void Update()
        {
            ///TODO: update the terrain
        }

        /// <summary>
        /// Helper method for converting Colors to ARGB32 integers.
        /// </summary>
        /// <param name="color"> The Color to be converted. </param>
        /// <returns> The Color as an integer. </returns>
        private int ConvertToARGB32(Color color)
        {
            return ((color.R << 16) | (color.G << 8) | (color.B << 0) | (color.A << 24));
        }

    }
}
