using System.Threading.Tasks;
using Paydock_dotnet_sdk.Models;

namespace Paydock_dotnet_sdk.Services
{
	public interface IExternalCheckout
	{
		Task<ExternalCheckoutResponse> Create(ExternalCheckoutRequest request);
	}
}