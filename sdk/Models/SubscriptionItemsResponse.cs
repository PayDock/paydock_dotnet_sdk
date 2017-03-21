using System;

namespace Paydock_dotnet_sdk.Models
{
    public class SubscriptionItemsResponse : Response
    {
        public string JsonResponse { get; set; }
        public Resource resource { get; set; }

        public class Resource
        {
            public string type { get; set; }
            public Datum[] data { get; set; }
            public int count { get; set; }
            public int limit { get; set; }
            public int skip { get; set; }
        }

        public class Datum
        {
            public string _id { get; set; }
            public decimal amount { get; set; }
            public string company_id { get; set; }
            public DateTime created_at { get; set; }
            public string status { get; set; }
            public DateTime updated_at { get; set; }
            public string user_id { get; set; }
            public bool archived { get; set; }
            public Customer customer { get; set; }
            public Statistics statistics { get; set; }
            public Schedule schedule { get; set; }
            public string currency { get; set; }
            public string gateway_type { get; set; }
            public string gateway_name { get; set; }
            public string gateway_mode { get; set; }
        }

        public class Customer
        {
            public string customer_id { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string email { get; set; }
            public string reference { get; set; }
        }

        public class Statistics
        {
            public decimal total_collected_amount { get; set; }
            public int successful_transactions { get; set; }
        }

        public class Schedule
        {
            public decimal? end_amount_total { get; set; }
            public DateTime first_assessment { get; set; }
            public string interval { get; set; }
            public DateTime last_assessment { get; set; }
            public DateTime next_assessment { get; set; }
            public DateTime? start_date { get; set; }
            public string status { get; set; }
            public bool locked { get; set; }
            public int completed_count { get; set; }
            public int retry_count { get; set; }
            public int frequency { get; set; }
        }

    }
}
