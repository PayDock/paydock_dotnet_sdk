using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Paydock_dotnet_sdk.Models
{
    public enum NotificationEvent
    {
        transaction_success,
        transaction_failure,
        subscription_creation_success,
        subscription_creation_failure,
        subscription_updated,
        subscription_finished,
        refund_requested,
        refund_successful,
        refund_failure,
        card_expiration_warning
    }

    public class NotificationRequest
    {
        public string body { get; set; }
        public string label { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public NotificationEvent notification_event { get; set; }
        public bool html { get; set; }
    }
}
