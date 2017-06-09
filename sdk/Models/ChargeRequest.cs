using System.Collections.Generic;

namespace Paydock_dotnet_sdk.Models
{
    public class ChargeRequest
    {
        public decimal amount { get; set; }
        public string currency { get; set; }
        public string token { get; set; }
        public string reference { get; set; }
        public string description { get; set; }
		public string customer_id { get; set; }
		public Dictionary<string, string> meta { get; set; }
        public Customer customer { get; set; }
    }
}