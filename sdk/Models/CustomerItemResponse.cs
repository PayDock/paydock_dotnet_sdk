using System;

namespace Paydock_dotnet_sdk.Models
{
    public class CustomerItemResponse : Response
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
            public DateTime created_at { get; set; }
            public DateTime updated_at { get; set; }
            public string status { get; set; }
            public string default_source { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string email { get; set; }
            public string phone { get; set; }
            public int __v { get; set; }
            public Payment_Sources[] payment_sources { get; set; }
            public _Service _service { get; set; }
        }

        public class _Service
        {
            public string default_gateway_id { get; set; }
        }

        public class Payment_Sources
        {
            public DateTime updated_at { get; set; }
            public string vault_token { get; set; }
            public string address_line1 { get; set; }
            public string address_line2 { get; set; }
            public string address_city { get; set; }
            public string address_state { get; set; }
            public string address_country { get; set; }
            public string address_postcode { get; set; }
            public string gateway_id { get; set; }
            public string card_name { get; set; }
            public int expire_month { get; set; }
            public int expire_year { get; set; }
            public string card_number_last4 { get; set; }
            public string card_scheme { get; set; }
            public string account_name { get; set; }
            public string account_number { get; set; }
            public string account_bsb { get; set; }
            public string account_routing { get; set; }
            public string account_type { get; set; }
            public string account_holder_type { get; set; }
            public string account_bank_name { get; set; }
            public string ref_token { get; set; }
            public string status { get; set; }
            public DateTime created_at { get; set; }
            public string _id { get; set; }
            public string type { get; set; }
            public string external_payer_id { get; set; }
            public string wallet_type { get; set; }
            public string card_number_bin { get; set; }
            public string card_issuer { get; set; }
            public string card_funding_method { get; set; }
        }
    }
}
