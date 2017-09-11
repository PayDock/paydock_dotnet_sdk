using Newtonsoft.Json;
using Paydock_dotnet_sdk.Models.Webhooks;

namespace Paydock_dotnet_sdk.Services
{
	public class Webhook : IWebhook
	{
		public TransactionWebhook ParseTransaction(string postData)
		{
			return (TransactionWebhook)JsonConvert.DeserializeObject(postData, typeof(TransactionWebhook));
		}

		public SubscriptionWebhook ParseSubscription(string postData)
		{
			return (SubscriptionWebhook)JsonConvert.DeserializeObject(postData, typeof(SubscriptionWebhook));
		}

		public TransactionWebhook ParseRefund(string postData)
		{
			return (TransactionWebhook)JsonConvert.DeserializeObject(postData, typeof(TransactionWebhook));
		}

		public CardExpirationWebhook ParseCardExpiry(string postData)
		{
			return (CardExpirationWebhook)JsonConvert.DeserializeObject(postData, typeof(CardExpirationWebhook));
		}
	}
}
