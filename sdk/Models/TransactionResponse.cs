using System;

namespace Paydock_dotnet_sdk.Models
{
    public class TransactionResponse : Response
    {
        public Resource resource { get; set; }

        public class Resource
        {
            public string type { get; set; }
            public Data data { get; set; }
        }

        public class Data
        {
            public string _id { get; set; }
            public decimal amount { get; set; }
            public DateTime created_at { get; set; }
            public DateTime updated_at { get; set; }
            public string currency { get; set; }
            public string status { get; set; }
            public string status_code { get; set; }
            public string error_code { get; set; }
            public string error_message { get; set; }
            public string gateway_specific_code { get; set; }
            public string gateway_specific_description { get; set; }
            public string _source_ip_address { get; set; }
            public string type { get; set; }
            public FraudResponse fraud { get; set; }
            public dynamic custom_fields { get; set; }
            public string external_id { get; set; }
            public string external_reference { get; set; }
        }

    }
}