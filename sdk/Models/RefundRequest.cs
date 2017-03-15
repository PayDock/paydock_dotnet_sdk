using Newtonsoft.Json;

namespace Paydock_dotnet_sdk.Models
{
    public class RefundRequest
    {
        [JsonIgnore]
        public string id { get; set; }
        public decimal amount { get; set; }
    }
}