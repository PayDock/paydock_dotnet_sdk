using Paydock.Net.Sdk.Models;

namespace Paydock.Net.Sdk.Services
{
    public interface ICharges
    {
        ChargeResponse Add(ChargeRequest request);
        ChargeItemsResponse Get();
        ChargeItemResponse Get(string chargeId);
        ChargeItemsResponse Get(ChargeSearchRequest request);
    }
}