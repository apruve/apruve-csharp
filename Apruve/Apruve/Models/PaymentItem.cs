using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Apruve.Models
{
    public class PaymentItem
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


        // Methods
        public string toValueString()
        {
            StringBuilder valueString = new StringBuilder();
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