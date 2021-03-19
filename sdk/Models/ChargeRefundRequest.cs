using Newtonsoft.Json;

namespace Paydock_dotnet_sdk.Models
{
    public class ChargeRefundRequest
    {
        [JsonIgnore]
        public string id { get; set; }
        public decimal amount { get; set; }
        public dynamic custom_fields { get; set; }
    }
}