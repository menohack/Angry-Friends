using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Friendly_Wars.GameLogic;

namespace Friendly_Wars_Test
{
    [TestClass]
    public class GameTest
    {
        [TestMethod]
        public void Create()
        {
            Game g = new Game();
        }
    }
}
