using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Apruve.Models
{
    public class LineItem
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
            valueString.Append(start_at);
            valueString.Append(end_at);
            valueString.Append(canceled_at);
            valueString.Append(last_charge_at);
            valueString.Append(next_charge_at);
            return valueString.ToString();
        }
    }
}