using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apruve.Models
{
    public class SubscriptionAdjustment
    {
        // Required on POST
        public string title { get; set; }
        public int amount_cents { get; set; }

        // Optional
        public int quantity { get; set; }
        public int price_ea_cents { get; set; }
        public string merchant_notes { get; set; }
        public string description { get; set; }
        public string variant_info { get; set; }
        public string sku { get; set; }
        public string vendor { get; set; }
        public string view_product_url { get; set; }

        // Set by apruve
        public string id { get; set; }
        public string status { get; set; }


        public static SubscriptionAdjustment get(string subId, string subAdjustId)
        {
            var apruveClient = ApruveClient.getInstance();
            string uri = "/api/v3/subscriptions/" + subId + "/adjustments/" + subAdjustId;
            var apruveResponse = apruveClient.get<SubscriptionAdjustment>(uri);

            // Return the response object, which should be a SubscriptionAdjustment object
            return apruveResponse;
        }

        public static SubscriptionAdjustment create(SubscriptionAdjustment subscriptionAdjustment, string subscriptionId)
        {
            ApruveClient apruveClient = ApruveClient.getInstance();
            var requestBody = subscriptionAdjustment;
            string uri = "/api/v3/subscriptions/" + subscriptionId + "/adjustments";
            var apruveResponse = apruveClient.post<SubscriptionAdjustment>(requestBody, uri, System.Net.HttpStatusCode.OK);

            // Return the response object, which should be a SubscriptionAdjustment object
            return apruveResponse;
        }

        public static SubscriptionAdjustment delete(string subscriptionId, string subscriptionAdjustmentId)
        {
            ApruveClient apruveClient = ApruveClient.getInstance();
            string uri = "/api/v3/subscriptions/" + subscriptionId + "/adjustments/" + subscriptionAdjustmentId;
            var apruveResponse = apruveClient.delete<SubscriptionAdjustment>(uri);

            // Return the response object, which should be a SubscriptionAdjustment object
            return apruveResponse;
        }

    }
}
