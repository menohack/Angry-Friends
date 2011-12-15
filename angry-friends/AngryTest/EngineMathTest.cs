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
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Engine.Utilities;

namespace AngryTest
{
    [TestClass]
    public class EngineMathTest
    {
        [TestMethod]
        public void LinearDistance()
        {
            Double result = EngineMath.Distance(new Point(1, 2), new Point(1, 4));
            Assert.AreEqual(result, 2.0);
        }

        [TestMethod]
        public void NoDistance()
        {
            Double result = EngineMath.Distance(new Point(4, 7), new Point(4, 7));
            Assert.AreEqual(result, 0.0);
        }

        [TestMethod]
        public void Distance2d()
        {
            Double result = EngineMath.Distance(new Point(0, 0), new Point(2, 4));
            Assert.AreEqual(result, Math.Sqrt(20));
        }

    }
}