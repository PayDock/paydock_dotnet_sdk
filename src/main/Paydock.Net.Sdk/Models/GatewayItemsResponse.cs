namespace Paydock.Net.Sdk.Models
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
