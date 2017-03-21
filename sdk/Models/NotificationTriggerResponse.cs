namespace Paydock_dotnet_sdk.Models
{
    public class NotificationTriggerResponse : Response
    {
        public Resource resource { get; set; }

        public class Resource
        {
            public string type { get; set; }
            public NotificationTriggerData data { get; set; }
        }
    }
}
