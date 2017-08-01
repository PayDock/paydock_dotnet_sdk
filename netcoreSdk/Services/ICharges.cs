using System.Threading.Tasks;
using Paydock_dotnet_sdk.Models;

namespace Paydock_dotnet_sdk.Services
{
	public interface ICharges
	{
		Task<ChargeResponse> Add(ChargeRequest request);
	}
}