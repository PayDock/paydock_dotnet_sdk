namespace Paydock_dotnet_sdk.Models
{
    public class NotificationTriggerItemsResponse : Response
    {
        public Resource resource { get; set; }

        public class Resource
        {
            public string type { get; set; }
            public int count { get; set; }
            public int limit { get; set; }
            public int skip { get; set; }
            public NotificationTriggerData[] data { get; set; }
        }
    }
}
