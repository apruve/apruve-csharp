using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Apruve;
using Apruve.Models;
using Moq;

namespace ApruveTest
{
    public class MockPaymentRequest : PaymentRequest
    {
        private static LineItem lineItem1 = new LineItem() { title = "A Line Item", amount_cents = 100 };
        private static LineItem lineItem2 = new LineItem() { title = "Another Line Item", amount_cents = 100, description = "A description for this line", sku = "A_SKU_NUMBER" };
        public static PaymentRequest getPaymentRequestSimple()
        {
            PaymentRequest paymentRequest = new PaymentRequest() { merchant_id = "testMerchantId" };
            paymentRequest.amount_cents = 100;
            paymentRequest.line_items = new List<LineItem> { lineItem1 };
            return paymentRequest;
        }

        public static PaymentRequest getPaymentRequestComplex()
        {
            PaymentRequest paymentRequest = new PaymentRequest(){ merchant_id = "testMerchantId" };
            paymentRequest.amount_cents = 100;
            paymentRequest.line_items = new List<LineItem> { lineItem1, lineItem2 };
            paymentRequest.expires_at = "2014-07-13T18:02:49Z";
            return paymentRequest;
        }
    }
    [TestClass]
    public class PaymentRequestTest
    {
        private static string apiKey = "testApiKey";
        private static string simpleValueString = "testMerchantId100A Line Item100";
        private static string complexValueString = "testMerchantId1002014-07-13T18:02:49ZA Line Item100Another Line Item100A description for this lineA_SKU_NUMBER";

        [TestMethod]
        public void testConstructor()
        {
            ApruveClient.initToNull();
            PaymentRequest requestBody = new PaymentRequest();
            Assert.IsNotNull(requestBody, "Payment Request does not exist, please ensure");
        }

        [TestMethod]
        // Integration test involving the PaymentRequest.toSecureHash() and PaymentRequest.toValueString() methods
        public void testToSecureHash()
        {
            string secureHash = "d836d877e45b4142e1d01eb77336bea38d2ec59414b4f7986e5ecd7d96fbcb71";
            ApruveClient.init(apiKey, ApruveEnvironment.testEnvironment());
            PaymentRequest mockPaymentRequest = MockPaymentRequest.getPaymentRequestSimple();

            // does the mock payment request get converted to the value string we expect?
            Assert.AreEqual(simpleValueString, mockPaymentRequest.testToValueString());

            // does the value string get converted to the secure hash we expect?
            Assert.AreEqual(secureHash, mockPaymentRequest.toSecureHash());
        }

        [TestMethod]
        public void testToSecureHashComplex()
        {
            string secureHash = "b09f2f70e827d67deeff30f49467d20235ab339e9b3b2aec40f3fa257ff455bf";
            ApruveClient.init(apiKey, ApruveEnvironment.testEnvironment());
            PaymentRequest mockPaymentRequest = MockPaymentRequest.getPaymentRequestComplex();

            // does the mock payment request get converted to the value string we expect?
            Assert.AreEqual(complexValueString, mockPaymentRequest.testToValueString());

            // does the value string get converted to the secure hash we expect?
            Assert.AreEqual(secureHash, mockPaymentRequest.toSecureHash());

        }
    }
}
