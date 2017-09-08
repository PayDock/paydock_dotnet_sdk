using Newtonsoft.Json;
using Paydock_dotnet_sdk.Models.Webhooks;

namespace Paydock_dotnet_sdk.Services
{
	public class Webhook
	{
		public TransactionWebhook ParseTransaction(string postData)
		{
			return (TransactionWebhook)JsonConvert.DeserializeObject(postData, typeof(TransactionWebhook));
		}
	}
}
