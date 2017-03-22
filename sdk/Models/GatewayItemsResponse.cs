using System;
namespace Paydock_dotnet_sdk.Models
{
    public class GatewayItemsResponse : Response
    {
        public Resource resource { get; set; }

        public class Resource
        {
            public string type { get; set; }
            public GatewayData[] data { get; set; }
        }
    }
}
