using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestSharp.Serializers;

namespace Apruve.Models
{
    public class PaymentRequestUpdateResponse
    {
        public string id { get; set; }
        public string status { get; set; }
        [SerializeAs(Name = "api_url")]
        public string apiUrl { get; set; }
        [SerializeAs(Name = "view_url")]
        public string viewUrl { get; set; }

    }
}