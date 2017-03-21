using System;

namespace Paydock_dotnet_sdk.Models
{
    public class SubscriptionSearchRequest
    {
        public int? skip { get; set; }
        public int? limit { get; set; }
        public string search { get; set; }
        public string sortkey { get; set; }
        public string sortdirection { get; set; }
        public string customer_id { get; set; }
        public string gateway_id { get; set; }
        public string status { get; set; }
    }
}