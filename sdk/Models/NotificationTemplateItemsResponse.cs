using System;

namespace Paydock_dotnet_sdk.Models
{
    public class NotificationTemplateItemsResponse : Response
    {
        public Resource resource { get; set; }

        public class Resource
        {
            public string type { get; set; }
            public Data[] data { get; set; }
			public int count { get; set; }
			public int limit { get; set; }
			public int skip { get; set; }
		}

        public class Data
        {
            public DateTime created_at { get; set; }
            public DateTime updated_at { get; set; }
            public string label { get; set; }
            public string notification_event { get; set; }
            public string body { get; set; }
            public string _id { get; set; }
        }

    }
}
