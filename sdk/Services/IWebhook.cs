using Paydock_dotnet_sdk.Models.Webhooks;

namespace Paydock_dotnet_sdk.Services
{
	public interface IWebhook
	{
		CardExpirationWebhook ParseCardExpiry(string postData);
		TransactionWebhook ParseRefund(string postData);
		SubscriptionWebhook ParseSubscription(string postData);
		TransactionWebhook ParseTransaction(string postData);
	}
}