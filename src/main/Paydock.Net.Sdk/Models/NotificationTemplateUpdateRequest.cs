using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Paydock.Net.Sdk.Models
{
    public class NotificationTemplateUpdateRequest
    {
        public string _id { get; set; }
        public string body { get; set; }
        public string label { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public NotificationEvent notification_event { get; set; }
        public bool html { get; set; }
    }
}
