namespace Paydock.Net.Sdk.Models
{
    public class CustomerSearchRequest
    {
        public int? skip { get; set; }
        public int? limit { get; set; }
        public string search { get; set; }
        public string sortkey { get; set; }
        public string sortdirection { get; set; }
        public string gateway_id { get; set; }
        public bool? archived { get; set; }
		public string reference { get; set; }
		public string payment_source_id { get; set; }
	}
}