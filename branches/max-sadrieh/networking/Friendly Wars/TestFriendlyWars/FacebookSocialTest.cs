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
            new FacebookPerson(null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void TestOnEmptyId()
        {
            new FacebookPerson(String.Empty);
        }

        [TestMethod]
        public void TestCorrectNiceId()
        {
            FacebookPerson fs = new FacebookPerson("zuck");
            Assert.IsNotNull(fs);
            Assert.IsInstanceOfType(fs, typeof(FacebookPerson));
        }

        [TestMethod]
        public void TestCorrectUglyId()
        {
            FacebookPerson fs = new FacebookPerson("4");
            Assert.IsNotNull(fs);
            Assert.IsInstanceOfType(fs, typeof(FacebookPerson));
        }

        [TestMethod]
        public void TestRetrievalNameNiceId()
        {
            FacebookPerson fs = new FacebookPerson("zuck");
            Assert.IsNotNull(fs.Name);
            Assert.Equals("zuck", fs.Id);
            Assert.Equals("Mark Zuckerberg", fs.Name);
        }

        [TestMethod]
        public void TestRetrievalNameUglyId()
        {
            FacebookPerson fs = new FacebookPerson("4");
            Assert.IsNotNull(fs.Name);
            Assert.Equals("4", fs.Id);
            Assert.Equals("Mark Zuckerberg", fs.Name);
        }

        [TestMethod]
        public void TestPostMessage()
        {
            FacebookPerson fs = new FacebookPerson("msadrieh"); //TODO: Find somebody that won't disable Facebook after this
        }
    }
}
