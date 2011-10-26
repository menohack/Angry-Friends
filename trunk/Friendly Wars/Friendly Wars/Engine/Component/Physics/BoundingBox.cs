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
using Friendly_Wars.Engine.Object;

namespace Friendly_Wars.Engine.Component.Physics
{
    /// <summary>
    /// This class represents an axis-aligned rectangle for specifying the bounds of an object.
    /// </summary>
    public class BoundingBox
    {
		/// <summary>
		/// The top-most value of the bounding box.
		/// </summary>
		private double Top { get; set; }
		/// <summary>
		/// The bottom-most value of the bounding box.
		/// </summary>
		private double Bottom { get; set; }
		/// <summary>
		/// The right-most value of the bounding box.
		/// </summary>
		private double Right { get; set; }
		/// <summary>
		/// The left-most value of the bounding box.
		/// </summary>
		private double Left { get; set; }

        private static readonly Double EPSILON = .001;

        /// <summary>
        /// Constructor for a bounding box.
        /// </summary>
        /// <param name="top"> The top value. </param>
        /// <param name="bottom"> The bottom value. </param>
        /// <param name="left"> The left value. </param>
        /// <param name="right"> The right value. </param>
        public BoundingBox(double top, double bottom, double left, double right)
        {
            Top = top;
            Bottom = bottom;
            Left = left;
            Right = right;
        }

		/// <summary>
		/// Checks if two bounding boxes collide.
		/// </summary>
		/// <param name="gameObject"> The other bounding box. </param>
		/// <returns> True if there is a collision. </returns>
        public bool Collide(BoundingBox gameObject)
        {
			if (gameObject.Left > Left && gameObject.Left < Right || gameObject.Right < Right && gameObject.Right > Left)
            {
				if (gameObject.Top > Top && gameObject.Top < Bottom)
					return true;
				else if (gameObject.Bottom < Bottom && gameObject.Bottom > Top)
					return true;
				else
					return false;
            }

            return true;
        }

        /// <summary>
        /// Determines if two Doubles are approximately the same value.
        /// </summary>
        /// <param name="number1">The first number.</param>
        /// <param name="number2">The second number.</param>
        /// <returns> True if the numbers are approximately the same. </returns>
        private bool Approximately(Double number1, Double number2)
        {
            if (Math.Abs(number1 - number2) < EPSILON)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
