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

namespace Library.Engine.Utilities
{
    /// <summary>
    /// Provides extensions to WriteableBitmap.
    /// </summary>
    public static class WriteableBitmapExtension
    {
        /// <summary>
        /// Returns a copy of the WriteableBitmap.
        /// </summary>
        /// <param name="bitmap">The WriteableBitmap to copy.</param>
        /// <returns>Returns a copy of the given WriteableBitmap.</returns>
        public static WriteableBitmap Copy(this WriteableBitmap bitmap)
        {
            WriteableBitmap bmp = new WriteableBitmap(bitmap.PixelWidth, bitmap.PixelHeight);
            return bmp.FromByteArray(bitmap.ToByteArray());
        }
    }
}