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

namespace Library.Engine.Utilities
{
    /// <summary>
    /// The Velocity class stores a x-component and a y-component that represent distance per second.
    /// </summary>
    public class Velocity
    {
        /// <summary>
        /// Default constructor of the zero vector.
        /// </summary>
        public Velocity()
        {
            X = 0.0;
            Y = 0.0;
        }

        /// <summary>
        /// Constructor for velocity.
        /// </summary>
        /// <param name="x">The x-component of the velocity.</param>
        /// <param name="y">The y-component of the velocity.</param>
        public Velocity(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// The x-component of the velocity.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// The y-component of the velocity.
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// An overriden method for printing Velocity.
        /// </summary>
        /// <returns>Returns Velocity parsed into a string.</returns>
        public override string ToString()
        {
            return "(" + X + ", " + Y + ")";
        }

    }
}
