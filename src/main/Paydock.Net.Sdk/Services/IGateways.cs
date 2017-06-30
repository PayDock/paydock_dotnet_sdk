using Paydock.Net.Sdk.Models;

namespace Paydock.Net.Sdk.Services
{
    public interface IGateways
    {
        GatewayResponse Add(GatewayRequest request);
        GatewayItemResponse Delete(string gatewayid);
        GatewayItemsResponse Get();
        GatewayItemResponse Get(string gatewayid);
        GatewayItemResponse Update(GatewayUpdateRequest request);
    }
}