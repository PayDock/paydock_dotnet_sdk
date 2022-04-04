using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Paydock_dotnet_sdk.Models
{
    public enum NotificationTriggerType
    {
        webhook,
        email,
        sms
    }

    public class NotificationTriggerRequest
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public NotificationTriggerType type { get; set; }
        public string destination { get; set; }
        public string template_id { get; set; }
        public bool? transaction_only { get; set; }
        [JsonConverter(typeof(StringEnumConverter)), JsonProperty(PropertyName = "event")]
        public NotificationEvent eventTrigger { get; set; }

    }
}
