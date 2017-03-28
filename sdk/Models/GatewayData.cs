using System;

namespace Paydock_dotnet_sdk.Models
{
    public class GatewayData
    {
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string _id { get; set; }
        public string mode { get; set; }
        public int active_subscriptions { get; set; }
    }
}
