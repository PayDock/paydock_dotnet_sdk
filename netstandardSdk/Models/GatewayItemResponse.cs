namespace Paydock_dotnet_sdk.Models
{
    public class GatewayItemResponse : Response
    {
        public Resource resource { get; set; }

        public class Resource
        {
            public string type { get; set; }
            public GatewayData data { get; set; }
        }
    }
}
