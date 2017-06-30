namespace Paydock.Net.Sdk.Models
{
    public class CustomerUpdateRequest
    {
        public string customer_id { get; set; }
        public string token { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string reference { get; set; }
        public string default_source { get; set; }
        public PaymentSource payment_source { get; set; }
    }
}
