using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apruve.Models
{
    public class Payment
    {

        //Set by Apruve - do not specify these on POST
        public string id { get; set; }
        public string payment_request_id { get; set; }
        public string status { get; set; }
        public string api_url { get; set; }
        public string view_url { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }

        //Required on POST
        public int amount_cents { get; set; }

        //Optional Params
        public string currency { get; set; }
        public string merchantNotes { get; set; }
        public List<PaymentItem> payment_items { get; set; }

        public static Payment get(string paymentRequestId, string paymentId)
        {
            var apruveClient = ApruveClient.getInstance();
            string uri = "/api/v3/payment_requests/" + paymentRequestId + "/payments/" + paymentId;
            var apruveResponse = apruveClient.get<Payment>(uri);

            // Return the response object, which should be a Payment object
            return apruveResponse;
        }

        public static Payment create(Payment payment)
        {
            ApruveClient apruveClient = ApruveClient.getInstance();
            var requestBody = payment;
            string uri = "/api/v3/payment_requests/" + payment.payment_request_id + "/payments";
            var apruveResponse = apruveClient.post<Payment>(requestBody, uri, System.Net.HttpStatusCode.Created);

            // Return the response object, which should be a Payment object
            return apruveResponse;
        }

        // Paths for API Calls
        public void setApiUrl(string apiUrl)
        {
            this.api_url = apiUrl;
        }

    }
}