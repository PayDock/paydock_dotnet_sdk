using System.Collections.Generic;

namespace Paydock_dotnet_sdk.Models
{
    public class ThreeDSecure
    {
        public string id { get; set; }
        public string charge_id { get; set; }
        public string token { get; set; }
        public string status { get; set; }
        public string version { get; set; }
        public bool custom_implementation { get; set; }
        public IReadOnlyDictionary<string, string> browser_details { get; set; }
    }
}
