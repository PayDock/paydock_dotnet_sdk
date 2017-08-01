using System.Threading.Tasks;

namespace Paydock_dotnet_sdk.Services
{
	public interface IServiceHelper
	{
		Task<T> Post<T, R>(R request, string endpoint, bool excludeSecretKey = false, string overrideConfigSecretKey = null);
	}
}