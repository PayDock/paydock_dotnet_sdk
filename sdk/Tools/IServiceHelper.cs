namespace Paydock_dotnet_sdk.Tools
{
    public interface IServiceHelper
    {
        string CallPaydock(string endpoint, HttpMethod method, string json, bool excludeSecretKey = false, string overrideConfigSecretKey = null);
    }
}