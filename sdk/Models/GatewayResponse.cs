using System;

namespace Paydock_dotnet_sdk.Models
{
    public class GatewayResponse : Response
    {
        public Resource resource { get; set; }

        public class Resource
        {
            public string type { get; set; }
            public Data data { get; set; }
        }

        public class Data
        {
            public DateTime created_at { get; set; }
            public DateTime updated_at { get; set; }
            public string type { get; set; }
            public string name { get; set; }
            public string username { get; set; }
            public string password { get; set; }
            // TODO: populate additional response parameters, there are likely to be more
            public string _id { get; set; }
            public string mode { get; set; }
        }

    }
}
