using Paydock_dotnet_sdk.Models;

namespace Paydock_dotnet_sdk.Services
{
    public interface ICharges
    {
        ChargeResponse Add(ChargeRequest request);
        ChargeResponse Archive(string id);
        ChargeItemsResponse Get();
        ChargeItemsResponse Get(string chargeId);
        ChargeItemsResponse Get(GetChargeRequest request);
        ChargeResponse Refund(RefundRequest request);
    }
}