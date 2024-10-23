using System.Collections.Generic;

namespace Paydock_dotnet_sdk.Models
{
    public class TransactionThreeDSecure
    {
        public bool? challenge { get; set; }
        public string gateway_result { get; set; }
        public string gateway_status { get; set; }
        public string gateway_recommendation { get; set; }

    }

}

