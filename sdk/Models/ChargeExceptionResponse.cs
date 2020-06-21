using System;

namespace Paydock_dotnet_sdk.Models
{

    public class ChargeExceptionResponse
    {
        public int status { get; set; }
        public Error error { get; set; }
        public Resource resource { get; set; }

        public class Error
        {
            public string message { get; set; }
            public string code { get; set; }
            public Detail[] details { get; set; }
        }

        public class Detail
        {
            public string gateway_specific_code { get; set; }
            public string gateway_specific_description { get; set; }
            public string param_name { get; set; }
            public string description { get; set; }
        }

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
            public float amount { get; set; }
            public string currency { get; set; }
            public int __v { get; set; }
            public Transaction[] transactions { get; set; }
            public bool one_off { get; set; }
            public bool archived { get; set; }
            public Customer customer { get; set; }
            public ThreeDSecure _3ds { get; set; }
            public bool capture { get; set; }
            public string status { get; set; }
            public object[] items { get; set; }
            public Transfer transfer { get; set; }
        }

        public class Customer
        {
            public string email { get; set; }
            public Payment_Source payment_source { get; set; }
        }

        public class Payment_Source
        {
            public string type { get; set; }
            public string card_name { get; set; }
            public string card_number_last4 { get; set; }
            public int expire_month { get; set; }
            public int expire_year { get; set; }
            public string gateway_id { get; set; }
            public string card_scheme { get; set; }
            public string gateway_name { get; set; }
            public string gateway_type { get; set; }
        }

        public class Transfer
        {
            public object[] items { get; set; }
        }

        public class Transaction
        {
            public DateTime created_at { get; set; }
            public float amount { get; set; }
            public string currency { get; set; }
            public string _id { get; set; }
            public string error_code { get; set; }
            public string error_message { get; set; }
            public string gateway_specific_description { get; set; }
            public string gateway_specific_code { get; set; }
            public string _source_ip_address { get; set; }
            public object[] service_logs { get; set; }
            public string status { get; set; }
            public string type { get; set; }
        }
    }

}


