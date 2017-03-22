using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paydock_dotnet_sdk.Models
{
    public class GatewayRequest
    {
        public string type { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string mode { get; set; }
        public string merchant { get; set; }
        public string key { get; set; }
        public string signature { get; set; }
        // TODO: fix the payway gateway implementationS
    }
}
