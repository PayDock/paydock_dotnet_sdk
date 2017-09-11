using Newtonsoft.Json;
using System;

namespace Paydock_dotnet_sdk.Models.Webhooks
{
	public class CardExpirationWebhook
	{
		[JsonProperty("event")]
		public string _event { get; set; }
		public Data data { get; set; }

		public class Data
		{
			public string _id { get; set; }
			public DateTime created_at { get; set; }
			public DateTime updated_at { get; set; }
			public string status { get; set; }
			public string default_source { get; set; }
			public string company_id { get; set; }
			public string user_id { get; set; }
			public string first_name { get; set; }
			public string last_name { get; set; }
			public string email { get; set; }
			public Statistics statistics { get; set; }
			public Payment_Source payment_source { get; set; }
		}

		public class Statistics
		{
			public int total_collected_amount { get; set; }
			public int successful_transactions { get; set; }
			public Currency currency { get; set; }
		}

		public class Currency
		{
			public AUD AUD { get; set; }
		}

		public class AUD
		{
			public int total_amount { get; set; }
			public int count { get; set; }
		}

		public class Payment_Source
		{
			public DateTime updated_at { get; set; }
			public string vault_token { get; set; }
			public string address_country { get; set; }
			public string address_line1 { get; set; }
			public string address_line2 { get; set; }
			public string address_city { get; set; }
			public string address_postcode { get; set; }
			public string gateway_id { get; set; }
			public string card_name { get; set; }
			public int expire_month { get; set; }
			public int expire_year { get; set; }
			public string card_number_last4 { get; set; }
			public string card_scheme { get; set; }
			public string ref_token { get; set; }
			public string status { get; set; }
			public DateTime created_at { get; set; }
			public string _id { get; set; }
			public string type { get; set; }
		}
	}
}
