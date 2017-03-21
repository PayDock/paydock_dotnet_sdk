using Paydock_dotnet_sdk.Models;

namespace Paydock_dotnet_sdk.Services
{
    public interface ISubscriptions
    {
        SubscriptionResponse Add(SubscriptionRequest request);
        SubscriptionItemResponse Delete(string subscriptionId);
        SubscriptionItemsResponse Get();
        SubscriptionItemsResponse Get(SubscriptionSearchRequest request);
        SubscriptionItemResponse Get(string subscriptionId);
        SubscriptionResponse Update(SubscriptionUpdateRequest request);
    }
}