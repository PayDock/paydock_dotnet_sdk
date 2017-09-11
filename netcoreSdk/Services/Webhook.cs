using Newtonsoft.Json;

namespace Paydock_dotnet_sdk.Services
{
    public class Webhook
	{
		public T Parse<T>(string postData)
		{
			return JsonConvert.DeserializeObject<T>(postData);
		}
	}
}
