namespace Paydock_dotnet_sdk.Models
{
    public class Customer
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string payment_source_id { get; set; }
        public PaymentSource payment_source { get; set; }
    }
}
