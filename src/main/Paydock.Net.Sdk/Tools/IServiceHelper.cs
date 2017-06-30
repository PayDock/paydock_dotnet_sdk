namespace Paydock.Net.Sdk.Tools
{
    public interface IServiceHelper
    {
        string CallPaydock(string endpoint, HttpMethod method, string json, bool excludeSecretKey = false, string overrideConfigSecretKey = null);
    }
}