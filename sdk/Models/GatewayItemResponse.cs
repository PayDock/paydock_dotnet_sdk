using System;
namespace Paydock_dotnet_sdk.Models
{
    public class GatewayItemResponse : Response
    {
        public Resource resource { get; set; }

        public class Resource
        {
            public string type { get; set; }
            public Data data { get; set; }
        }

        public class Data
        {
            public string _id { get; set; }
            public DateTime created_at { get; set; }
            public string merchant { get; set; }
            public string name { get; set; }
            public string password { get; set; }
            public string type { get; set; }
            public DateTime updated_at { get; set; }
            // TODO: handle extra fields that are returned
            public string username { get; set; }
            public string mode { get; set; }
            public int active_subscriptions { get; set; }
        }

    }
}
