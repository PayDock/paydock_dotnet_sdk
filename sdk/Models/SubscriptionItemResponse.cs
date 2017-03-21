using System;

namespace Paydock_dotnet_sdk.Models
{
    public class SubscriptionItemResponse : Response
    {
        public Resource resource { get; set; }
        
        public class Resource
        {
            public string type { get; set; }
            public Data data { get; set; }
        }

        public class Data
        {
            public DateTime created_at { get; set; }
            public DateTime updated_at { get; set; }
            public string user_id { get; set; }
            public string company_id { get; set; }
            public decimal amount { get; set; }
            public string description { get; set; }
            public string reference { get; set; }
            public string status { get; set; }
            public string _id { get; set; }
            public bool archived { get; set; }
            public Customer customer { get; set; }
            public Statistics statistics { get; set; }
            public Schedule schedule { get; set; }
            public string currency { get; set; }
        }

        public class Customer
        {
            public string customer_id { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string email { get; set; }
            public string reference { get; set; }
            public Payment_Source payment_source { get; set; }
        }

        public class Payment_Source
        {
            public string gateway_name { get; set; }
            public string gateway_type { get; set; }
            public string gateway_mode { get; set; }
            public DateTime updated_at { get; set; }
            public string address_line1 { get; set; }
            public string address_line2 { get; set; }
            public string address_city { get; set; }
            public string address_state { get; set; }
            public string address_country { get; set; }
            public string address_postcode { get; set; }
            public string gateway_id { get; set; }
            public string account_name { get; set; }
            public int account_bsb { get; set; }
            public string account_number { get; set; }
            public string ref_token { get; set; }
            public string status { get; set; }
            public DateTime created_at { get; set; }
            public string _id { get; set; }
            public string type { get; set; }
        }

        public class Statistics
        {
            public decimal? total_collected_amount { get; set; }
            public int successful_transactions { get; set; }
        }

        public class Schedule
        {
            public string interval { get; set; }
            public DateTime? start_date { get; set; }
            public DateTime? end_date { get; set; }
            public DateTime next_assessment { get; set; }
            public DateTime first_assessment { get; set; }
            public string status { get; set; }
            public bool locked { get; set; }
            public int completed_count { get; set; }
            public int retry_count { get; set; }
            public int frequency { get; set; }
        }
    }
}
