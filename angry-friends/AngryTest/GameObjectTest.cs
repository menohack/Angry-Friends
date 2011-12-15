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
using Model.Engine.Object;
using Model.Engine.Component;
using Model.Engine.Component.Media;
using Model.Engine.Component.Media.Rendering;
using Model.Engine.Component.Transform;
using System.Collections.Generic;

namespace AngryTest
{
    [TestClass]
    public class GameObjectTest
    {
        [TestMethod]
        public void CreateNoComponent()
        {
            GameObject go = new GameObject("test", null, null, null);
            Assert.IsNotNull(go);
        }

        [TestMethod]
        public void CreateWithComponents()
        {
            GameObject go = new GameObject("test",
                new TransformComponent(new Point(1, 1), new Point(1, 1)),
                new AudioComponent(new Dictionary<string, MediaElement>()),
                new RenderComponent(new Animation(new Frame(new Image(), new Point(0, 0)))));
        }

        [TestMethod]
        public void CreateAndUpdate()
        {
            GameObject go = new GameObject("test",
                new TransformComponent(new Point(1, 1), new Point(1, 1)),
                new AudioComponent(new Dictionary<string, MediaElement>()),
                new RenderComponent(new Animation(new Frame(new Image(), new Point(0, 0)))));
            go.Update(100.0);
        }
    }
}