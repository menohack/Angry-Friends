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
    /// This class represents a Bounding Box.
    /// </summary>
    public class BoundingBox
    {
        /// <summary>
        /// The top left point of the box.
        /// </summary>
        private Point topLeft;
        /// <summary>
        /// The bottom right point of the box.
        /// </summary>
        private Point bottomRight;

        private static readonly Double EPSILON = .001;

        /// <summary>
        /// Constructor for a bounding box.
        /// </summary>
        /// <param name="top"> The top. </param>
        /// <param name="bottom"> The bottom. </param>
        /// <param name="left"> The left. </param>
        /// <param name="right"> The right. </param>
        public BoundingBox(double top, double bottom, double left, double right)
        {
            Top = top;
            Bottom = bottom;
            Left = left;
            Right = right;
        }

        public bool Collide(BoundingBox gameObject)
        {
            ///Collide from the top
            if (gameObject.Bottom < Top && gameObject.Top > Bottom)
            {
                if (gameObject.Left < Right && gameObject.Right > Left)
                    return true;
                else
                    return false;
            }

            ///TODO: The other cases.
            return true;
        }

        /// <summary>
        /// Getters and setters for the bottom of the bounding box.
        /// </summary>
        private double Bottom
        {
            get
            {
                return bottomRight.Y;
            }
            set
            {
                bottomRight.Y = value;
            }
        }

        /// <summary>
        /// Getters and setters for the top of the bounding box.
        /// </summary>
        private double Top
        {
            get
            {
                return topLeft.Y;
            }
            set
            {
                topLeft.Y = value;
            }
        }

        /// <summary>
        /// Getters and setters for the right of the bounding box.
        /// </summary>
        private double Right
        {
            get
            {
                return bottomRight.X;
            }
            set
            {
                bottomRight.X = value;
            }
        }

        /// <summary>
        /// Getters and setters for the left of the bounding box.
        /// </summary>
        private double Left
        {
            get
            {
                return topLeft.X;
            }
            set
            {
                topLeft.X = value;
            }
        }

        /// <summary>
        /// Determines if two Doubles are approximately the same value.
        /// </summary>
        /// <param name="number1">The first number.</param>
        /// <param name="number2">The second number.</param>
        /// <returns></returns>
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
