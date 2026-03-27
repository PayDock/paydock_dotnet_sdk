using System.Collections.Generic;
namespace Paydock_dotnet_sdk.Models
{
    public class WalletChargeItem
    {
        public string name { get; set; }
        public decimal amount { get; set; }
        public decimal amount_cent { get; set; }
        public decimal quantity { get; set; }
        public string reference { get; set; }
        public string email { get; set; }
        public string item_uri { get; set; }
        public string image_uri { get; set; }
        public string type { get; set; }

    }
}