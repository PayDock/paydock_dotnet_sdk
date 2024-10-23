using System.Collections.Generic;

namespace Paydock_dotnet_sdk.Models
{
    public class ChargeRequest : ChargeRequestBase
    {
		public Dictionary<string, string> meta { get; set; }
        public dynamic custom_fields { get; set; }
        public WalletChargeItem[] items { get; set; }
    }
}