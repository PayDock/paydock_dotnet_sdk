using System.Threading.Tasks;
using Paydock_dotnet_sdk.Models;

namespace Paydock_dotnet_sdk.Services
{
	public interface ISubscriptions
	{
		Task<SubscriptionResponse> Add(SubscriptionRequest request);
		Task<SubscriptionItemResponse> Delete(string subscriptionId);
		Task<SubscriptionItemsResponse> Get();
		Task<SubscriptionItemResponse> Get(string subscriptionId);
		Task<SubscriptionItemsResponse> Get(SubscriptionSearchRequest request);
		Task<SubscriptionResponse> Update(SubscriptionUpdateRequest request);
	}
}