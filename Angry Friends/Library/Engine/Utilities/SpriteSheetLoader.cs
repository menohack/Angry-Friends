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
using System.Collections.Generic;
using Library.Engine.Component.Graphic;
using System.Windows.Media.Imaging;

namespace Library.Engine.Utilities
{
    /// <summary>
    /// SpriteSheetLoader loads the data from a SpriteSheet into readable content.
    /// </summary>
    public class SpriteSheetLoader
    {
        /// <summary>
        /// SpriteSheet stores the data corresponding to a SpriteSheet.
        /// </summary>
        public class SpriteSheet
        {
            /// <summary>
            /// The image of this SpriteSheet.
            /// </summary>
            public Image Image { get; private set; }
            /// <summary>
            /// The number of rows in this SpriteSheet.
            /// </summary>
            public int Rows { get; private set; }
            /// <summary>
            /// The number of columns in this SpriteSheet.
            /// </summary>
            public int Columns { get; private set; }
            /// <summary>
            /// The size of each cell in the horizontal direction.
            /// </summary>
            public int horizontalCellSize { get; private set; }
            /// <summary>
            /// The size of each cell in the vertical direction.
            /// </summary>
            public int verticalCellSize { get; private set; }

            /// <summary>
            /// Constructor for a new SpriteSheet.
            /// </summary>
            /// <param name="image">The Image of this SpriteSheet.</param>
            /// <param name="rows">The number of rows in this SpriteSheet.</param>
            /// <param name="columns">The number of columns in this SpriteSheet.</param>
            /// <param name="horizontalCellSize">The size of each cell in the horizontal direction.</param>
            /// <param name="horizontalVerticalSize">The size of each cell in the vertical direction.</param>
            public SpriteSheet(Image image, int rows, int columns, int horizontalCellSize, int horizontalVerticalSize) {
                this.Image = image;
                this.Rows = rows;
                this.Columns = columns;
                this.horizontalCellSize = horizontalCellSize;
                this.verticalCellSize = horizontalVerticalSize;
            }
        }

        /// <summary>
        /// The single instance of SpriteSheetLoader.
        /// </summary>
        private static SpriteSheetLoader instance;

        /// <summary>
        /// Accessor for the instance of SpriteSheetLoader.
        /// This accessor follows the singleton pattern for C# provided at Microsoft's MSDN.
        /// </summary>
        public static SpriteSheetLoader Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SpriteSheetLoader();
                }
                return instance;
            }
        }

        /// <summary>
        /// Private constructor for SpriteSheetLoader that follows the singleton pattern for C# provided at Microsoft's MSDN.
        /// </summary>
        private SpriteSheetLoader() {}

        /// <summary>
        /// Get the Frames that correspond to supplied paramaters from a given SpriteSheet.
        /// </summary>
        /// <param name="spriteSheet">The SpriteSheet from which to get the Frames.</param>
        /// <param name="initialRow">The row at which to start getting Frames. [0, n). </param>
        /// <param name="initialColumn">The column at which to start getting Frames. [0, n).</param>
        /// <param name="finalRow">The row at which to stop getting Frames. [0, n).</param>
        /// <param name="finalColumn">The column at which to start getting Frames. [0, n).</param>
        /// <returns></returns>
        public IList<Frame> GetFramesFromSpriteSheet(SpriteSheet spriteSheet, int initialRow, int initialColumn, int finalRow, int finalColumn)
        {
            IList<Frame> frames = new List<Frame>();

            int x = (spriteSheet.horizontalCellSize * initialRow) % (int) spriteSheet.Image.Width;
            int y = initialColumn * spriteSheet.verticalCellSize;

            // The number of pixels to move without taking wrap-over into account.
            int distanceToMove = ((initialColumn * (int) spriteSheet.Image.Width) + (initialRow * (int) spriteSheet.horizontalCellSize)) - (finalColumn * (int) spriteSheet.Image.Width) + (finalRow * (int) spriteSheet.horizontalCellSize);

            // Loop through and copy the corresponding pixels into a List of Frames.
            while (distanceToMove > 0)
            {
                if (x >= spriteSheet.Image.Width)
                {
                    x -= (int) spriteSheet.Image.Width;
                }

                WriteableBitmap writeableBitmap = new WriteableBitmap((BitmapSource)spriteSheet.Image.Source);
                writeableBitmap.Crop(x, y, spriteSheet.horizontalCellSize, spriteSheet.verticalCellSize);

                Image frame = new Image();
                frame.Source = writeableBitmap;
                frame.Width = spriteSheet.horizontalCellSize;
                frame.Height = spriteSheet.verticalCellSize;
                frames.Add(new Frame(frame, new Point()));

                distanceToMove -= spriteSheet.horizontalCellSize;
                x += spriteSheet.horizontalCellSize;
            }

            return frames;
        }
    }
}
