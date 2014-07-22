using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Text;
using System.Security.Cryptography;
using System.ComponentModel;

namespace Apruve.Models
{
    public class PaymentRequest
    {

        // required on create
        public string merchant_id { get; set; }
        public List<LineItem> line_items { get; set; }

        // specified by server
        public string id { get; set; }
        public string username { get; set; }
        public string status { get; set; }
        public string api_url { get; set; }
        public string view_url { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string expires_at { get; set; }

        // optional params
        public string merchant_order_id { get; set; }
        public int? amount_cents { get; set; }
        public int? tax_cents { get; set; }
        public int? shipping_cents { get; set; }
        public string currency { get; set; }

        // Methods for API calls
        public static PaymentRequest get(string paymentRequestId)
        {
            var apruveClient = ApruveClient.getInstance();
            string uri = "/api/v3/payment_requests/" + paymentRequestId;
            var apruveResponse = apruveClient.get<PaymentRequest>(uri);

            // Return the response object, which should be a PaymentRequest object
            return apruveResponse;
        }

        public PaymentRequestUpdateResponse update()
        {
            var apruveClient = ApruveClient.getInstance();
            string uri = "/api/v3/payment_requests/" + this.id;
            var apruveResponse = apruveClient.put<PaymentRequestUpdateResponse>(uri, this);

            // Return the response object, which should be a PaymentRequestUpdateResponse object
            return apruveResponse;
        }

        public PaymentRequestUpdateResponse finalize()
        {
            var apruveClient = ApruveClient.getInstance();
            string uri = "/api/v3/payment_requests/" + this.id + "/finalize";
            var apruveResponse = apruveClient.post<PaymentRequestUpdateResponse>(null, uri, System.Net.HttpStatusCode.OK);

            // Return the response object, which should be a PaymentRequestUpdateResponse object
            return apruveResponse;
        }

        public void addLineItem(LineItem item)
        {
            this.line_items.Add(item);
        }

        public string toJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public string toSecureHash()
        {
            string apiKey = ApruveClient.getInstance().api_key;
            string shaInput = apiKey + this.toValueString();
            byte[] bytes = Encoding.UTF8.GetBytes(shaInput);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string secureHash = string.Empty;
            foreach (byte b in hash)
            {
                secureHash += String.Format("{0:x2}", b);
            }
            return secureHash;
        }
        protected string toValueString()
        {
            StringBuilder valueString = new StringBuilder();
            valueString.Append(merchant_id);
            if (merchant_order_id != null)
            {
                valueString.Append(merchant_order_id);
            }
            if (amount_cents != null)
            {
                valueString.Append(amount_cents);
            }
            if (currency != null)
            {
                valueString.Append(currency);
            }
            if (tax_cents != null)
            {
                valueString.Append(tax_cents);
            }
            if (shipping_cents != null)
            {
                valueString.Append(shipping_cents);
            }
            foreach (LineItem line in line_items)
            {
                valueString.Append(line.toValueString());
            }
            if (expires_at != null)
            {
                valueString.Append(expires_at);
            }
            return valueString.ToString();
        }

        // for testing purposes 
        public string testToValueString()
        {
            return toValueString();
        }

    }
}