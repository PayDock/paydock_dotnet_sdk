using System;

namespace Paydock.Net.Sdk.Models
{
    public class NotificationTemplateResponse : Response
    {
        public Resource resource { get; set; }

        public class Resource
        {
            public string type { get; set; }
            public Data data { get; set; }
        }

        public class Data
        {
            public int __v { get; set; }
            public DateTime created_at { get; set; }
            public DateTime updated_at { get; set; }
            public string label { get; set; }
            public string notification_event { get; set; }
            public string body { get; set; }
            public string _id { get; set; }
        }

    }
}
