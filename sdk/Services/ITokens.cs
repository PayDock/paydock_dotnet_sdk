using Paydock_dotnet_sdk.Models;

namespace Paydock_dotnet_sdk.Services
{
	public interface ITokens
	{
		TokenResponse Create(TokenRequest request);
	}
}