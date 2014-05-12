using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Apruve;

namespace ApruveTest
{
    [TestClass]
    public class ApruveEnvironmentTest
    {
        [TestMethod]
        public void testGetBaseUrl()
        {
            Assert.AreEqual("https://www.apruve.com", ApruveEnvironment.prodEnvironment().getBaseUrl());
            Assert.AreEqual("https://test.apruve.com", ApruveEnvironment.testEnvironment().getBaseUrl());
        }


        [TestMethod]
        public void testGetJsUrl()
        {
            Assert.AreEqual("https://www.apruve.com/js/apruve.js", ApruveEnvironment.prodEnvironment().getJsUrl());
            Assert.AreEqual("https://test.apruve.com/js/apruve.js", ApruveEnvironment.testEnvironment().getJsUrl());
        }

        [TestMethod]
        public void testGetJsTag()
        {
            Assert.AreEqual("<script src=\"https://www.apruve.com/js/apruve.js\" type=\"text/javascript\"></script>", ApruveEnvironment.prodEnvironment().getJsTag());
            Assert.AreEqual("<script src=\"https://test.apruve.com/js/apruve.js\" type=\"text/javascript\"></script>", ApruveEnvironment.testEnvironment().getJsTag());
        }
    }
}

