using Newtonsoft.Json;

namespace Paydock.Net.Sdk.Models
{
    public class ChargeRefundRequest
    {
        [JsonIgnore]
        public string id { get; set; }
        public decimal amount { get; set; }
    }
}