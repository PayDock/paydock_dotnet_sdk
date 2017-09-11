using NUnit.Framework;
using Paydock_dotnet_sdk.Services;
using System;
using System.IO;

namespace UnitTests
{
	[TestFixture]
	public class WebhookTests
	{
		[Test]
		public void ParseTransaction()
		{
			var tranJson = File.ReadAllText(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "webhooks\\transaction_success.json");

			var tran = (new Webhook()).ParseTransaction(tranJson);

			Assert.AreEqual("transaction_success", tran._event);
		}

		[Test]
		public void ParseSubscription()
		{
			var subscriptionJson = File.ReadAllText(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "webhooks\\subscription_creation_success.json");

			var subscription = (new Webhook()).ParseSubscription(subscriptionJson);

			Assert.AreEqual("subscription_creation_success", subscription._event);
		}

		[Test]
		public void ParseRefund()
		{
			var refundJson = File.ReadAllText(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "webhooks\\refund_requested.json");

			var refund = (new Webhook()).ParseRefund(refundJson);

			Assert.AreEqual("refund_requested", refund._event);
		}


		[Test]
		public void ParseCardExpiration()
		{
			var cardExpirationJson = File.ReadAllText(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "webhooks\\card_expiration_warning.json");

			var cardExpiration = (new Webhook()).ParseRefund(cardExpirationJson);

			Assert.AreEqual("card_expiration_warning", cardExpiration._event);
		}
	}
}
