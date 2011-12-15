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
    public class AssetManagerTest
    {
        [TestMethod]
        public void InstanceExists()
        {
            Assert.IsNotNull(AssetManager.Instance);
        }

        [TestMethod]
        public void CorrectSingleton()
        {
            Assert.AreSame(AssetManager.Instance, AssetManager.Instance);
        }

        [TestMethod]
        public void DownloadBasic()
        {
            AssetManager.Instance.Download(new Uri("http://alexanderschiffhauer.com/index.html").ToString(), ExternalAsset.ExternalAssetType.String, (a) =>
            {
                Assert.IsNotNull(a);
                Assert.IsTrue(a.GetString().Length != 0);
            });
        }
    }
}