namespace Paydock.Net.Sdk.Models
{
    public class SubscriptionUpdateRequest
    {
        public string _id { get; set; }
        public decimal amount { get; set; }
        public string currency { get; set; }
        public string reference { get; set; }
        public string description { get; set; }
        public string payment_source_id { get; set; }
        public SubscriptionSchedule schedule { get; set; }
    }
}
