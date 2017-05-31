using System;

namespace Paydock_dotnet_sdk.Models
{
    public class ChargeSearchRequest
    {
        public int? skip { get; set; }
        public int? limit { get; set; }
        public string subscription_id { get; set; }
        public string gateway_id { get; set; }
        public string company_id { get; set; }
        public DateTime? created_at_from { get; set; }
        public DateTime? created_at_to { get; set; }
        public string search { get; set; }
        public string status { get; set; }
        public bool? archived { get; set; }
        public string transaction_external_id { get; set; }
    }
}