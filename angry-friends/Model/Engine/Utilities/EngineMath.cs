using System;
using System.Windows;

namespace Model.Engine.Utilities {
	/// <summary>
	/// EngineMath process Engine-related math.
	/// </summary>
	public class EngineMath {

        /// <summary>
        /// Calculates the distance between two points.
        /// </summary>
        /// <param name="a">The first point.</param>
        /// <param name="b">The second point.</param>
        /// <returns>The distance between a and b.</returns>
        public static Double Distance(Point a, Point b)
        {
            double x = a.X - b.X;
            double y = a.Y - b.Y;
            return Math.Sqrt(x * x + y * y);
        }
	}
}