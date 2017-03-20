using System.Collections.Generic;

namespace Paydock_dotnet_sdk.Models
{
    public class ChargeRequest
    {
        public decimal amount { get; set; }
        public string currency { get; set; }
        public string token { get; set; }
        public string reference { get; set; }
        public string description { get; set; }
        public Dictionary<string, string> meta { get; set; }
        public Customer customer { get; set; }

        public class Customer
        {
            public string customer_id { get; set; }
            public string first_name { get; set; }
            public string last_Name { get; set; }
            public string email { get; set; }
            public string phone { get; set; }
            public string payment_source_id { get; set; }
            public PaymentSource payment_source { get; set; }
        }
    }
}