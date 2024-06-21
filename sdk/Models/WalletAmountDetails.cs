using System.Collections.Generic;
namespace Paydock_dotnet_sdk.Models
{
    public class WalletAmountDetails
    {
        public decimal amount { get; set; }
        public decimal amount_points { get; set; }
        public string currency { get; set; }
        public PaymentSource payment_source { get; set; }
        public class PaymentSource
        {
            public string type { get; set; }
            public string identifier { get; set; }
            public string provider { get; set; }
            public string card_scheme { get; set; }
            public string expire_month { get; set; }
            public string expire_year { get; set; }
            public string card_number_last4 { get; set; }
            public string masked_pan { get; set; }
        }
    }
}
