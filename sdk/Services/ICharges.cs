using Paydock_dotnet_sdk.Models;

namespace Paydock_dotnet_sdk.Services
{
    public interface ICharges
    {
        ChargeResponse Add(ChargeRequest request);
        ChargeItemsResponse Get();
        ChargeItemResponse Get(string chargeId);
        ChargeItemsResponse Get(GetChargeRequest request);
        // TODO: implement refund and archive
    }
}