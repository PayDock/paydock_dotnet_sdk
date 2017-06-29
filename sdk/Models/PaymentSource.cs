using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Paydock_dotnet_sdk.Models
{
    public enum PaymentType
    {
        card,
        bank_account,
        bsb,
        checkout
    }

    public class PaymentSource
    {
        public string gateway_id { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public PaymentType type { get; set; }
        public string account_name { get; set; }
        public string account_bsb { get; set; }
        public string account_number { get; set; }
        public string account_routing { get; set; }
        public string account_holder_type { get; set; }
        public string account_bank_name { get; set; }
        public string card_name { get; set; }
        public string card_number { get; set; }
        public string expire_year { get; set; }
        public string expire_month { get; set; }
        public string card_ccv { get; set; }
        public string address_line1 { get; set; }
        public string address_line2 { get; set; }
        public string state { get; set; }
        public string address_country { get; set; }
        public string address_city { get; set; }
        public string address_postcode { get; set; }
		public string checkout_holder { get; set; }
		public string checkout_email { get; set; }
    }
}
