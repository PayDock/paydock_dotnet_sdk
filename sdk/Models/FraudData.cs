using System.Collections.Generic;

namespace Paydock_dotnet_sdk.Models
{
    public class FraudData
    {
        public string service_id { get; set; }
        public string token { get; set; }
        public dynamic data { get; set; }
    }
}
