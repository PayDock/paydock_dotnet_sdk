namespace Paydock.Net.Sdk.Models
{
    public class CustomerRequest
    {
        public string token { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string reference { get; set; }
        public PaymentSource payment_source { get; set; }
    }
}
