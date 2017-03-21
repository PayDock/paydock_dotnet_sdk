using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Paydock_dotnet_sdk.Models
{
    public class SubscriptionSchedule
    {
        public string interval { get; set; }
        public int frequency { get; set; }
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime? start_date { get; set; }
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime? end_date { get; set; }
        public decimal? end_amount_after { get; set; }
        public decimal? end_amount_before { get; set; }
        public int? end_transactions { get; set; }
    }
}
