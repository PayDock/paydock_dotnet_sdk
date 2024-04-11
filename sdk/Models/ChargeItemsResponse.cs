using System;

namespace Paydock_dotnet_sdk.Models
{
    public class ChargeItemsResponse : Response
    {
        public Resource resource { get; set; }

        public class Resource
        {
            public string type { get; set; }
            public Data[] data { get; set; }
            public int count { get; set; }
            public int limit { get; set; }
            public int skip { get; set; }
        }

        public class Data
        {
            public string _id { get; set; }
            public decimal amount { get; set; }
            public WalletChargeItem[] items { get; set; }
            public DateTime created_at { get; set; }
            public string currency { get; set; }
            public string description { get; set; }
            public string descriptor { get; set; }
            public string external_id { get; set; }
            public string reference { get; set; }
            public string reference2 { get; set; }
            public string initialization_source { get; set; }
            public DateTime updated_at { get; set; }
            public bool one_off { get; set; }
            public bool archived { get; set; }
            public Customer customer { get; set; }
            public string status { get; set; }
            public string type { get; set; }
            public FraudBase fraud { get; set; }
            public ShippingData shipping { get; set; }
            public ThreeDSecure _3ds { get; set; }
        }

        public class Customer
        {
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string email { get; set; }
            public string reference { get; set; }
            public string phone { get; set; }
            public string phone2 { get; set; }
            public Payment_Source payment_source { get; set; }
        }

        public class Payment_Source
        {
            public string card_name { get; set; }
            public string card_number_last4 { get; set; }
            public string expire_month { get; set; }
            public string expire_year { get; set; }
            public string account_name { get; set; }
            public string account_number { get; set; }
            public string account_bsb { get; set; }
            public string account_routing { get; set; }
            public string account_type { get; set; }
            public string account_holder_type { get; set; }
            public string account_bank_name { get; set; }
            public string address_line1 { get; set; }
            public string address_line2 { get; set; }
            public string address_line3 { get; set; }
            public string address_city { get; set; }
            public string address_postcode { get; set; }
            public string address_state { get; set; }
            public string address_country { get; set; }
            public string address_company { get; set; }
            public string gateway_id { get; set; }
            public string card_scheme { get; set; }
            public string gateway_name { get; set; }
            public string gateway_type { get; set; }
            public string checkout_holder { get; set; }
            public string checkout_email { get; set; }
            public string vault_token { get; set; }
            public string external_payer_id { get; set; }
            public string wallet_type { get; set; }
            public string card_number_bin { get; set; }
            public string card_issuer { get; set; }
            public string card_funding_method { get; set; }

        }
    }
}