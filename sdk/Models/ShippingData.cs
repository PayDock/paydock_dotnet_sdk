using System.Collections.Generic;

namespace Paydock_dotnet_sdk.Models
{
    public class ShippingData
    {
        public string address_line1 { get; set; }
        public string address_line2 { get; set; }
        public string address_city { get; set; }
        public string address_state { get; set; }
        public string address_country { get; set; }
        public string address_postcode { get; set; }
        public string address_company { get; set; }
        public string origin_address_postcode { get; set; }
        public Contact contact { get; set; }
    }

    public class Contact
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string phone2 { get; set; }

    }
}
