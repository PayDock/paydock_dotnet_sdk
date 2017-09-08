using NUnit.Framework;
using Paydock_dotnet_sdk.Services;

namespace UnitTests
{
	[TestFixture]
	public class WebhookTests
	{
		[Test]
		public void ParseTransaction()
		{
			var tranJson = @"{
    ""event"": ""transaction_success"",
    ""data"": {
			""external_id"": ""ch_A5UQxvnd7B1aC2"",
        ""_id"": ""589c848fc1ecdd2a29fd2197"",
        ""created_at"": ""2017-02-09T15:02:39.971Z"",
        ""updated_at"": ""2017-02-09T15:02:41.498Z"",
        ""company_id"": ""5584018a27b2cf0b1e4f1a6c"",
        ""user_id"": ""5584018a27b2cf0b1e4f1a6b"",
        ""amount"": 111,
        ""currency"": ""AUD"",
        ""reference"": ""Test reference"",
        ""description"": ""Test reference"",	
        ""__v"": 1,
        ""transactions"": [
            {
                ""created_at"": ""2017-02-09T15:02:39.968Z"",
                ""amount"": 111,
                ""currency"": ""AUD"",
                ""_id"": ""589c848fc1ecdd2a29fd2198"",
                ""status"": ""complete"",
                ""type"": ""sale""

			}
        ],
        ""one_off"": true,
        ""archived"": false,
        ""customer"": {
            ""first_name"": ""Test"",
            ""last_name"": ""Test"",
            ""email"": ""test@test.com"",
            ""phone"": ""+61000000000"",
            ""reference"": ""Test reference"",
            ""payment_source"": {
                ""type"": ""card"",
                ""card_name"": ""Test Test"",
                ""card_number_last4"": ""4444"",
                ""expire_month"": 1,
                ""expire_year"": 2019,
                ""address_line1"": ""Test address"",
                ""address_line2"": ""Test address"",
                ""address_city"": ""Sydney"",
                ""address_postcode"": ""111"",
                ""address_state"": null,
                ""address_country"": ""AU"",
                ""gateway_id"": ""56a8ab12d6bd4e7d628576aa"",
                ""card_scheme"": ""mastercard"",
                ""gateway_name"": ""stripe"",
                ""gateway_type"": ""Stripe""
            }
        },
        ""status"": ""complete""
    }
}";

			var tran = (new Webhook()).ParseTransaction(tranJson);

			Assert.AreEqual("transaction_success", tran._event);
		}
	}
}
