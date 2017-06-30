namespace Paydock.Net.Sdk.Models
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
    }
}
