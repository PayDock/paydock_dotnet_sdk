using System;

namespace Paydock_dotnet_sdk.Models
{
    public class ChargeResponse : Response
    {
        public Resource resource { get; set; }
        public string JsonResponse { get; set; }

        public class Resource
        {
            public string type { get; set; }
            public Data data { get; set; }
        }

        public class Data
        {
            public string external_id { get; set; }
            public int __v { get; set; }
            public string _id { get; set; }
            public decimal amount { get; set; }
            public string company_id { get; set; }
            public DateTime created_at { get; set; }
            public string currency { get; set; }
            public string reference { get; set; }
            public DateTime updated_at { get; set; }
            public string user_id { get; set; }
            public Transaction[] transactions { get; set; }
            public bool one_off { get; set; }
            public bool archived { get; set; }
            public Customer customer { get; set; }
            public string status { get; set; }
        }

        public class Customer
        {
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string email { get; set; }
            public string reference { get; set; }
            public Payment_Source payment_source { get; set; }
        }

        // TODO: need to look at combining this into other objects
        public class Payment_Source
        {
            public string type { get; set; }
            public string card_name { get; set; }
            public string card_number_last4 { get; set; }
            public int expire_month { get; set; }
            public int expire_year { get; set; }
            public string address_postcode { get; set; }
            public string gateway_id { get; set; }
            public string card_scheme { get; set; }
            public string gateway_name { get; set; }
            public string gateway_type { get; set; }
        }

        public class Transaction
        {
            public string _id { get; set; }
            public decimal amount { get; set; }
            public DateTime created_at { get; set; }
            public string currency { get; set; }
            public string status { get; set; }
            public string type { get; set; }
        }
    }
}