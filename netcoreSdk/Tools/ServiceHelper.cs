using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace Paydock_dotnet_sdk.Services
{
    public class ServiceHelper : IServiceHelper
	{
		//public async Task<T> Get<T>(string url, bool excludeSecretKey = false, string overrideConfigSecretKey = null)
		//{

		//}

		//public async Task<T> Put<T, R>(R request, bool excludeSecretKey = false, string overrideConfigSecretKey = null)
		//{

		//}

		//public async Task<T> Delete<T, R>(string url, bool excludeSecretKey = false, string overrideConfigSecretKey = null)
		//{

		//}
		
		// TODO: add error handling
		// TODO: implement remainder of HTTP methods

		public async Task<T> Post<T, R>(R request, string endpoint, bool excludeSecretKey = false, string overrideConfigSecretKey = null)
		{
			var json = JsonConvert.SerializeObject(request, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
			var requestResult = BuildRequest(HttpMethod.Post, endpoint, json, excludeSecretKey, overrideConfigSecretKey);

			var response = await requestResult.httpClient.SendAsync(requestResult.httpRequest);
			var responseString = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<T>(responseString);
		}

		private (HttpRequestMessage httpRequest, HttpClient httpClient) BuildRequest(HttpMethod method, string endpoint, string jsonBody = null,
			bool excludeSecretKey = false, string overrideConfigSecretKey = null)
		{
			var httpClient = new HttpClient();
			httpClient.DefaultRequestHeaders.Accept.Clear();
			var httpRequest = new HttpRequestMessage()
			{
				RequestUri = new Uri(Config.BaseUrl() + endpoint),
				Method = method
			};

			// body
			if (jsonBody != null)
			{
				httpRequest.Content = new StringContent(jsonBody);
				httpRequest.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
			}

			// authentication
			if (!excludeSecretKey)
			{
				if (string.IsNullOrEmpty(overrideConfigSecretKey))
					httpRequest.Headers.Add("x-user-secret-key", Config.SecretKey);
				else
					httpRequest.Headers.Add("x-user-secret-key", overrideConfigSecretKey);
			}

			return (httpRequest: httpRequest, httpClient: httpClient);
		}
	}
}
