namespace Paydock_dotnet_sdk.Models
{
    public class ExternalCheckoutRequest
    {
        public string mode { get; set; }
        public string type { get; set; }
        public string gateway_id { get; set; }
        public string success_redirect_url { get; set; }
        public string error_redirect_url { get; set; }
        public string description { get; set; }
    }
}
