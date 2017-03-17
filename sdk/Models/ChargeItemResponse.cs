using System;

namespace Paydock_dotnet_sdk.Models
{
    public class ChargeItemResponse : Response
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
            public string _id { get; set; }
            public DateTime created_at { get; set; }
            public DateTime updated_at { get; set; }
            public string company_id { get; set; }
            public string user_id { get; set; }
            public decimal amount { get; set; }
            public string currency { get; set; }
            public string subscription_id { get; set; }
            public string reference { get; set; }
            public string description { get; set; }
            public int __v { get; set; }
            public string external_id { get; set; }
            public Transaction[] transactions { get; set; }
            public bool one_off { get; set; }
            public bool archived { get; set; }
            public Customer customer { get; set; }
            public string status { get; set; }
        }

        public class Customer
        {
            public string customer_id { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string email { get; set; }
            public string phone { get; set; }
            public Payment_Source payment_source { get; set; }
        }

        public class Payment_Source
        {
            public string type { get; set; }
            public string card_name { get; set; }
            public string card_number_last4 { get; set; }
            public string card_scheme { get; set; }
            public string expire_month { get; set; }
            public string expire_year { get; set; }
            public string address_line1 { get; set; }
            public string address_line2 { get; set; }
            public string address_city { get; set; }
            public string address_postcode { get; set; }
            public string address_state { get; set; }
            public string address_country { get; set; }
            public string gateway_id { get; set; }
        }

        public class Transaction
        {
            public DateTime created_at { get; set; }
            public decimal amount { get; set; }
            public string currency { get; set; }
            public string _id { get; set; }
            public string status { get; set; }
            public string type { get; set; }
        }

        public class Req
        {
            public string account_id { get; set; }
            public decimal amount { get; set; }
            public string zip { get; set; }
            public string email { get; set; }
            public string currency { get; set; }
            public string country { get; set; }
            public string retain_account { get; set; }
        }
    }
}
