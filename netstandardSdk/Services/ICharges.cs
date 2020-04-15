using System.Threading.Tasks;
using Paydock_dotnet_sdk.Models;

namespace Paydock_dotnet_sdk.Services
{
	public interface ICharges
	{
		Task<ChargeResponse> Add(ChargeRequestBase request);
		Task<ChargeRefundResponse> Archive(string chargeId);
		Task<ChargeItemsResponse> Get();
		Task<ChargeItemsResponse> Get(ChargeSearchRequest request);
		Task<ChargeItemResponse> Get(string chargeId);
		Task<ChargeRefundResponse> Refund(string chargeId, decimal? amount);
	}
}