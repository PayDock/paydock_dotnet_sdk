using System.Collections.Generic;

namespace Paydock_dotnet_sdk.Models
{
    public class FraudResponse
    {
        public string status { get; set; }
        public string specific_code { get; set; }
        public string specific_message { get; set; }
        public string score { get; set; }
    }
}
