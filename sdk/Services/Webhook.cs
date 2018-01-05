using Newtonsoft.Json;
using Paydock_dotnet_sdk.Models.Webhooks;
using Paydock_dotnet_sdk.Tools;

namespace Paydock_dotnet_sdk.Services
{
	public class Webhook : IWebhook
	{
		public TransactionWebhook ParseTransaction(string postData)
		{
			return SerializeHelper.Deserialize<TransactionWebhook>(postData);
		}

		public SubscriptionWebhook ParseSubscription(string postData)
		{
			return SerializeHelper.Deserialize<SubscriptionWebhook>(postData);
		}

		public TransactionWebhook ParseRefund(string postData)
		{
			return SerializeHelper.Deserialize<TransactionWebhook>(postData);
		}

		public CardExpirationWebhook ParseCardExpiry(string postData)
		{
			return SerializeHelper.Deserialize<CardExpirationWebhook>(postData);
		}
	}
}
