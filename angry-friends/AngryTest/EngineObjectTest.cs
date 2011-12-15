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
using System.Collections.Generic;

namespace AngryTest
{
    [TestClass]
    public class EngineObjectTest
    {
        [TestMethod]
        public void InstanceExists()
        {
            Assert.IsNotNull(EngineObject.Instance);
        }

        [TestMethod]
        public void CorrectSingleton()
        {
            Assert.AreSame(EngineObject.Instance, EngineObject.Instance);
        }

        [TestMethod]
        public void AddGameObject()
        {
            GameObject go = new GameObject("nameofgo", null, null, null);
            EngineObject.Instance.AddGameObject(go);
            Assert.IsTrue(EngineObject.Instance.GetGameObjects().Contains(go));
        }

        [TestMethod]
        public void AddAndFinGameObject()
        {
            GameObject go = new GameObject("name", null, null, null);
            EngineObject.Instance.AddGameObject(go);
            ICollection<GameObject> collection = EngineObject.Instance.FindGameObjectsWithName("name");
            Assert.IsTrue(collection.Contains(go));
            Assert.AreSame(go, EngineObject.Instance.FindGameObjectWithUID(go.UID));
        }

        [TestMethod]
        public void Update()
        {
            EngineObject.Instance.Update(100.0);
        }
    }
}