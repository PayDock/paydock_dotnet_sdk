using Newtonsoft.Json;

namespace Paydock_dotnet_sdk.Models
{
    public class ChargeRefundRequest
    {
        public string id { get; set; }
        public decimal amount { get; set; }
        public dynamic custom_fields { get; set; }
        public string initialization_source { get; set; }
        public Customer customer { get; set; }
    }
}