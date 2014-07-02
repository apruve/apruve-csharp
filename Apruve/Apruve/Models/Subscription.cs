﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apruve.Models
{
    public class Subscription : LineItem
    {
        
        // required on create:
        public string title { get; set; }
        public int amount_cents { get; set; }

        // optional:
        public int? quantity { get; set; }
        public int? price_ea_cents { get; set; }
        public string merchant_notes { get; set; }
        public string description { get; set; }
        public string variant_info { get; set; }
        public string sku { get; set; }
        public string vendor { get; set; }
        public string view_product_url { get; set; }

        // for subscriptions:
        public string plan_code { get; set; }
        public string start_at { get; set; }
        public string end_at { get; set; }
        public string canceled_at { get; set; }
        public string last_charge_at { get; set; }
        public string next_charge_at { get; set; }

        // set by apruve:
        public string id { get; set; }
        public string payment_request_id { get; set; }

        // Methods
        public static Subscription get(string subscriptionId)
        {
            var apruveClient = ApruveClient.getInstance();
            string uri = "/api/v3/subscriptions/" + subscriptionId;
            var apruveResponse = apruveClient.get<Subscription>(uri);

            // Return the response object, which should be a Subscription object
            return apruveResponse;
        }

        public string toValueString()
        {
            StringBuilder valueString = new StringBuilder();
            valueString.Append(id);
            valueString.Append(this.title);
            if (amount_cents != null)
            {
                valueString.Append(amount_cents);
            }
            if (price_ea_cents != null)
            {
                valueString.Append(price_ea_cents);
            }
            if (quantity != null)
            {
                valueString.Append(quantity);
            }
            valueString.Append(description);
            valueString.Append(variant_info);
            valueString.Append(sku);
            valueString.Append(vendor);
            if (view_product_url != null)
            {
                valueString.Append(view_product_url);
            }
            return valueString.ToString();
        }
    }


}