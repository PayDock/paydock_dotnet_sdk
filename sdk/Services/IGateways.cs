using Paydock_dotnet_sdk.Models;

namespace Paydock_dotnet_sdk.Services
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