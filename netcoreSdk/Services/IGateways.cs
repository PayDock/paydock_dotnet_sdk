using System.Threading.Tasks;
using Paydock_dotnet_sdk.Models;

namespace Paydock_dotnet_sdk.Services
{
	public interface IGateways
	{
		Task<GatewayResponse> Add(GatewayRequest request);
		Task<GatewayItemResponse> Delete(string gatewayid);
		Task<GatewayItemsResponse> Get();
		Task<GatewayItemResponse> Get(string gatewayid);
		Task<GatewayItemResponse> Update(GatewayUpdateRequest request);
	}
}