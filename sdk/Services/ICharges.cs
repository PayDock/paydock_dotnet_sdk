using Paydock_dotnet_sdk.Models;

namespace Paydock_dotnet_sdk.Services
{
    public interface ICharges
    {
        ChargeResponse Add(ChargeRequestBase request);
        ChargeItemsResponse Get();
        ChargeItemResponse Get(string chargeId);
        ChargeItemsResponse Get(ChargeSearchRequest request);
    }
}