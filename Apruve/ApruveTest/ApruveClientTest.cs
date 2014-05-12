using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Apruve;
using Apruve.Models;
using RestSharp;
using Moq;

namespace ApruveTest
{
    // Build out an ApruveClient mock
    public class MockApruveClient : ApruveClient
    {
        public MockApruveClient()
            : base("mockApiKey", ApruveEnvironment.testEnvironment())
        {

        }

        public T testResponseHandler<T>() where T:new()
        {
            ApruveClient.init("testApiKey", ApruveEnvironment.testEnvironment());
            var mock = new Mock<RestClient>();
            mock.Setup(x => x.Execute<T>(It.IsAny<RestRequest>()))
                // For testing purposes, we're going to assume a mistyped ID resulting in requests returning a 404
                .Returns(new RestResponse<T> { StatusCode = System.Net.HttpStatusCode.NotFound });
            return this.responseHandler<T>(mock.Object, new RestRequest(), System.Net.HttpStatusCode.OK);
        } 
    }

    [TestClass]
    public class ApruveClientTest
    {
        // create api key and environment object for initializing ApruveClient      
        private static string apiKey = "testApiKey-001";
        private static ApruveEnvironment env = ApruveEnvironment.testEnvironment();

        [TestMethod]
        /**
         * Attempt to call getInstance() without initializing ApruveClient. 
         * This should throw an ApruveException exception. 
         */
        [ExpectedException(typeof(ApruveException))]
        public void testGetInstanceBeforeInit()
        {
            ApruveClient.getInstance();
        }

        [TestMethod]
        // Make sure that the init() method creates an instance of ApruveClient
        public void testInit()
        {
            ApruveClient.init(apiKey, env);
            Assert.IsNotNull(ApruveClient.getInstance());

        }
        [TestMethod]
        // Make sure the instance we get has the correct api key and environment
        public void testGetInstance()
        {
            ApruveClient.init(apiKey, env);
            Assert.AreEqual(apiKey, ApruveClient.getInstance().api_key);
            Assert.AreSame(env, ApruveClient.getInstance().env);
        }

        [TestMethod]
        /**
         * Making HTTP requests to incorrect endpoints, e.g. request made with URI built
         * from mistyped payment request or payment ID, will return error status codes.
         * Those codes should trigger a mismatch with pre-defined success status codes
         * set on Payment Request and Payment class methods. When that happens, an 
         * ApruveException should be thrown.
         */
        [ExpectedException(typeof(ApruveException))]
        public void testResponseHandlerBadRequest()
        {
            new MockApruveClient().testResponseHandler<PaymentRequest>();
        }
    }
}
