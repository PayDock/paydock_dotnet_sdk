using Paydock_dotnet_sdk.Models;

namespace Paydock_dotnet_sdk.Services
{
	public interface IVault
	{
		VaultResponse CreateToken(VaultRequest request);
		VaultResponse GetToken(string vaultToken);
		VaultItemsResponse GetTokens();
	}
}