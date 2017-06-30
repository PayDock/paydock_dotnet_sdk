using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paydock.Net.Sdk.Models
{
    public class NotificationLogsResponse : Response
    {
        public Resource resource { get; set; }

        public class Resource
        {
            public string type { get; set; }
            public Datum[] data { get; set; }
            public int count { get; set; }
            public int limit { get; set; }
            public int skip { get; set; }
        }

        public class Datum
        {
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
