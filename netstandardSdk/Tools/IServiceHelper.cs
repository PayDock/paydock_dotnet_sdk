using System.Threading.Tasks;

namespace Paydock_dotnet_sdk.Services
{
	public interface IServiceHelper
	{
		Task<T> Put<T, R>(R request, string endpoint, bool excludeSecretKey = false, string overrideConfigSecretKey = null);
		Task<T> Post<T, R>(R request, string endpoint, bool excludeSecretKey = false, string overrideConfigSecretKey = null);
		Task<T> Get<T>(string endpoint, bool excludeSecretKey = false, string overrideConfigSecretKey = null);
		Task<T> Delete<T>(string endpoint, bool excludeSecretKey = false, string overrideConfigSecretKey = null);
		Task<T> Delete<T, R>(R request, string endpoint, bool excludeSecretKey = false, string overrideConfigSecretKey = null);
	}
}