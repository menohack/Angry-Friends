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
    public class Velocity
    {
        public Velocity()
        {
            X = 0.0;
            Y = 0.0;
        }

        public double X { get; set; }
        public double Y { get; set; }

        public Velocity(double x, double y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return "(" + X + ", " + Y + ")";
        }
    }
}
