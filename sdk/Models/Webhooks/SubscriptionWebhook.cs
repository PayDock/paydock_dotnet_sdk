﻿using Newtonsoft.Json;
using System;

namespace Paydock_dotnet_sdk.Models.Webhooks
{
	public class SubscriptionWebhook
	{
		[JsonProperty("event")]
		public string _event { get; set; }
		public Data data { get; set; }

		public class Data
		{
			public int __v { get; set; }
			public DateTime created_at { get; set; }
			public DateTime updated_at { get; set; }
			public decimal amount { get; set; }
			public string payment_source_id { get; set; }
			public string status { get; set; }
			public string _id { get; set; }
			public bool archived { get; set; }
			public _Service _service { get; set; }
			public Customer customer { get; set; }
			public Statistics statistics { get; set; }
			public Schedule schedule { get; set; }
			public string currency { get; set; }
		}

		public class _Service
		{
			public string customer_default_gateway_id { get; set; }
			public string tags { get; set; }
		}

		public class Customer
		{
			public string customer_id { get; set; }
			public string first_name { get; set; }
			public string last_name { get; set; }
			public string email { get; set; }
			public string phone { get; set; }
		}

		public class Statistics
		{
			public int total_collected_amount { get; set; }
			public int successful_transactions { get; set; }
		}

		public class Schedule
		{
			public string interval { get; set; }
			public DateTime start_date { get; set; }
			public DateTime next_assessment { get; set; }
			public DateTime next_assessment_planned { get; set; }
			public DateTime first_assessment { get; set; }
			public string status { get; set; }
			public bool locked { get; set; }
			public int completed_count { get; set; }
			public int retry_count { get; set; }
			public int frequency { get; set; }
		}
	}
}
