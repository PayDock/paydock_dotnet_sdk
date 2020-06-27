using System.Collections.Generic;

namespace Paydock_dotnet_sdk.Models
{
    public class ThreeDSecure
    {
        public string id { get; set; }
        public string charge_id { get; set; }
        public string token { get; set; }
        public Dictionary<string, string> browser_details { get; set; }
    }
}
