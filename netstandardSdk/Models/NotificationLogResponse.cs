using System;

namespace Paydock_dotnet_sdk.Models
{
    public class NotificationLogResponse : Response
    {
        public Resource resource { get; set; }

        public class Resource
        {
            public string type { get; set; }
            public Datum data { get; set; }
            public int count { get; set; }
        }

        public class Datum
        {
            public int __v { get; set; }
            public String company_id {get; set; }
            public DateTime created_at { get; set; }
            public bool success { get; set; }
            public string type { get; set; }
            public string destination { get; set; }
            public string notification_id { get; set; }
            public string parent_id { get; set; }
            public string _event { get; set; }
            public string response_status { get; set; }
            public string _id { get; set; }
            public bool archived { get; set; }
        }

    }
}