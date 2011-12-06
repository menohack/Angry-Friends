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
            /// The Image of this SpriteSheet.
            /// </summary>
            public BitmapImage Image { get; private set; }

            /// <summary>
            /// The size of this SpriteSheet.
            /// </summary>
            public Point Size { get; private set; }

            /// <summary>
            /// The size of each Frame in this SpriteSheet.
            /// </summary>
            public Point FrameSize { get; private set; }

            /// <summary>
            /// Constructor for a new SpriteSheet.
            /// </summary>
            /// <param name="image">The BitmapImage of the actual SpriteSheet.</param>
            /// <param name="size">The pixel dimensions of the actual SpriteSheet.</param>
            /// <param name="frameSize">The size of each Frame in this SpriteSheet.</param>
            public SpriteSheet(BitmapImage image, Point size, Point frameSize) {
                this.Image = image;
                this.Size = size;
                this.FrameSize = frameSize;
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
        /// <param name="initialPosition">The initial row and column. [0, n) </param>
        /// <param name="finalPosition">The final row and column. [0, n) </param>
        /// <returns></returns>
        public IList<Frame> GetFramesFromSpriteSheet(SpriteSheet spriteSheet, Point initialPosition, Point finalPosition)
        {
            IList<Frame> frames = new List<Frame>();

            int x = (int) (spriteSheet.FrameSize.X * initialPosition.X) % (int) spriteSheet.Size.X;
            int y = (int) (spriteSheet.FrameSize.Y * initialPosition.Y);
            
            // The number of pixels to move without taking wrap-over into account.
            int distanceToMove = Convert.ToInt32((finalPosition.Y * spriteSheet.Size.X) + (finalPosition.X * spriteSheet.FrameSize.X) - ((initialPosition.Y * spriteSheet.Size.X) + (initialPosition.X * spriteSheet.FrameSize.X)));

            // Loop through and copy the corresponding pixels into a List of Frames.
            while (distanceToMove > 0)
            {
                if (x >= spriteSheet.Size.X)
                {
                    x -= (int)spriteSheet.Size.X;
                }

                WriteableBitmap spriteSheetWriteableBitmap = new WriteableBitmap((BitmapSource)spriteSheet.Image);
                spriteSheetWriteableBitmap = spriteSheetWriteableBitmap.Copy();
                WriteableBitmap desiredFrame = new WriteableBitmap((int) spriteSheet.FrameSize.X, (int) spriteSheet.FrameSize.Y);

                desiredFrame.Blit(new Rect(0, 0, spriteSheet.FrameSize.X, spriteSheet.FrameSize.Y), spriteSheetWriteableBitmap, new Rect(x, y, spriteSheet.FrameSize.X, spriteSheet.FrameSize.Y));

                Image frame = new Image();
                frame.Source = desiredFrame;
                frame.Width = spriteSheet.FrameSize.X;
                frame.Height = spriteSheet.FrameSize.Y;
                frames.Add(new Frame(frame, new Point()));

                distanceToMove -= (int) spriteSheet.FrameSize.X;
                x += (int) spriteSheet.FrameSize.X;
            }

            return frames;
        }
    }
}
