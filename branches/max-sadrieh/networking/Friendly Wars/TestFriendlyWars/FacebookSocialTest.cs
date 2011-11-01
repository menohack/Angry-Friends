using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Friendly_Wars.Networking;

namespace TestFriendlyWars
{
    [TestClass]
    public class FacebookSocialTest
    {
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void TestOnNullId()
        {
            new FacebookSocial(null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void TestOnEmptyId()
        {
            new FacebookSocial(String.Empty);
        }

        [TestMethod]
        public void TestCorrectNiceId()
        {
            FacebookSocial fs = new FacebookSocial("zuck");
            Assert.IsNotNull(fs);
            Assert.IsInstanceOfType(fs, typeof(FacebookSocial));
        }

        [TestMethod]
        public void TestCorrectUglyId()
        {
            FacebookSocial fs = new FacebookSocial("4");
            Assert.IsNotNull(fs);
            Assert.IsInstanceOfType(fs, typeof(FacebookSocial));
        }
    }
}
