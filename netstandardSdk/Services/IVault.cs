using System.Threading.Tasks;
using Paydock_dotnet_sdk.Models;

namespace Paydock_dotnet_sdk.Services
{
	public interface IVault
	{
		Task<VaultResponse> Create(VaultRequest request);
		Task<VaultItemsResponse> Get();
		Task<VaultResponse> Get(string vaultToken);
	}
}