using System.Threading.Tasks;
using Paydock_dotnet_sdk.Models;

namespace Paydock_dotnet_sdk.Services
{
	public interface ITokens
	{
		Task<TokenResponse> Create(TokenRequest request);
	}
}