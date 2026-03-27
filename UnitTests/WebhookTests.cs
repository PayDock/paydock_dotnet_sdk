using NUnit.Framework;
using Paydock_dotnet_sdk.Models.Webhooks;
using Paydock_dotnet_sdk.Services;
using System.IO;

namespace UnitTests
{
	[TestFixture]
	public class WebhookTests
	{
		private string WebhookFile(string filename) =>
			Path.Combine(TestContext.CurrentContext.TestDirectory, "webhooks", filename);

		[Test]
		public void ParseTransaction()
		{
			var tranJson = File.ReadAllText(WebhookFile("transaction_success.json"));
			var tran = (new Webhook()).Parse<TransactionWebhook>(tranJson);
			Assert.AreEqual("transaction_success", tran._event);
		}

		[Test]
		public void ParseSubscription()
		{
			var subscriptionJson = File.ReadAllText(WebhookFile("subscription_creation_success.json"));
			var subscription = (new Webhook()).Parse<SubscriptionWebhook>(subscriptionJson);
			Assert.AreEqual("subscription_creation_success", subscription._event);
		}

		[Test]
		public void ParseRefund()
		{
			var refundJson = File.ReadAllText(WebhookFile("refund_requested.json"));
			var refund = (new Webhook()).Parse<TransactionWebhook>(refundJson);
			Assert.AreEqual("refund_requested", refund._event);
		}

		[Test]
		public void ParseCardExpiration()
		{
			var cardExpirationJson = File.ReadAllText(WebhookFile("card_expiration_warning.json"));
			var cardExpiration = (new Webhook()).Parse<CardExpirationWebhook>(cardExpirationJson);
			Assert.AreEqual("card_expiration_warning", cardExpiration._event);
		}
	}
}
