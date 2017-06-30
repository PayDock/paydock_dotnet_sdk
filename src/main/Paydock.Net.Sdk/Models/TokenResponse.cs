namespace Paydock.Net.Sdk.Models
{
    public class TokenResponse : Response
    {
        public Resource resource { get; set; }

        public class Resource
        {
            public string type { get; set; }
            public string data { get; set; }
        }

    }
}
