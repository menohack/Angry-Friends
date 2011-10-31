﻿using System.Windows;
using System.Windows.Controls;

namespace Friendly_Wars.Engine.Component.Graphic
{
    /// <summary>
    /// Frame contains an image and its offset.
    /// </summary>
    public class Frame
    {
        /// <summary>
        /// The actual image of this Frame.
        /// </summary>
        public Image image { get; private set; }

        /// <summary>
        /// The offset of this Frame's image.
        /// </summary>
        public Point offset { get; private set; }

        /// <summary>
        /// Constructor for a new Frame.
        /// </summary>
        /// <param name="image">The image of this Frame.</param>
        /// <param name="offset">The offset of this Frame's image.</param>
        public Frame(Image image, Point offset)
        {
            this.image = image;
            this.offset = offset;
        }
    }
}