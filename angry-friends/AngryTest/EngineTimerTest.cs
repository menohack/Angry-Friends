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
using System.Collections.Generic;
using System.Threading;

namespace AngryTest
{
    public class Dummy : IUpdateable
    {
        public Boolean GotUpdated { get; private set; }

        public Dummy()
        {
            GotUpdated = false;
        }

        public void Update(Double deltaTime)
        {
            GotUpdated = true;
        }
    }

    [TestClass]
    public class EngineTimerTest
    {
        [TestMethod]
        public void Create()
        {
            ICollection<IUpdateable> collection = new List<IUpdateable>();
            collection.Add(new Dummy());
            EngineTimer timer = new EngineTimer(1, collection);

            Assert.IsNotNull(timer);
        }

        [TestMethod]
        public void CreateAddRemove()
        {
            ICollection<IUpdateable> collection = new List<IUpdateable>();
            EngineTimer timer = new EngineTimer(1, collection);
            Assert.IsNotNull(timer);
            Dummy dummy = new Dummy();
            timer.AddEventListener(dummy);
            timer.RemoveEventListener(dummy);
        }

        [TestMethod]
        public void CreateStartStop()
        {
            EngineTimer timer = new EngineTimer(100, new List<IUpdateable>());
            timer.Start();
            Thread.Sleep(1 * 1000);
            timer.Stop();
        }
    }
}