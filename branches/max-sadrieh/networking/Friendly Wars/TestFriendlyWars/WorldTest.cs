using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Friendly_Wars.Engine.Object;
using Friendly_Wars;

namespace TestFriendlyWars
{
    [TestClass]
    public class WorldTest
    {
        [TestMethod]
        public void Intialization()
        {
            World w = new World("My world"); 
        }

        [TestMethod]
        public void IntializationWithNull()
        {
            World w = new World(null);
        }

        [TestMethod]
        public void InitializationWithTag()
        {
            World w = new World("My world", "tag");
        }

        [TestMethod]
        public void InitilizationWithNullTag()
        {
            World w = new World("My world", null);
        }
    }
}
