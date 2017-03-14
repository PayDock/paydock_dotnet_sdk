namespace Paydock_dotnet_sdk.Services
{
    public interface IServiceHelper
    {
        string CallPaydock(string endpoint, HttpMethod method, string json);
    }
}