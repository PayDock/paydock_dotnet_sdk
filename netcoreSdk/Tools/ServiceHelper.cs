using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Paydock_dotnet_sdk.Tools;

namespace Paydock_dotnet_sdk.Services
{
    public class ServiceHelper : IServiceHelper
	{
		public async Task<T> Get<T>(string endpoint, bool excludeSecretKey = false, string overrideConfigSecretKey = null)
		{
			var requestResult = BuildRequest(HttpMethod.Get, endpoint, null, excludeSecretKey, overrideConfigSecretKey);

			var response = await SendRequest(requestResult.httpClient, requestResult.httpRequest);
			return await ProcessResponse<T>(response);
		}

		public async Task<T> Put<T, R>(R request, string endpoint, bool excludeSecretKey = false, string overrideConfigSecretKey = null)
		{
			var json = JsonConvert.SerializeObject(request, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
			var requestResult = BuildRequest(HttpMethod.Put, endpoint, json, excludeSecretKey, overrideConfigSecretKey);

			var response = await SendRequest(requestResult.httpClient, requestResult.httpRequest);
			return await ProcessResponse<T>(response);
		}

		public async Task<T> Delete<T>(string endpoint, bool excludeSecretKey = false, string overrideConfigSecretKey = null)
		{
			var requestResult = BuildRequest(HttpMethod.Delete, endpoint, null, excludeSecretKey, overrideConfigSecretKey);

			var response = await SendRequest(requestResult.httpClient, requestResult.httpRequest);
			return await ProcessResponse<T>(response);
		}

		public async Task<T> Post<T, R>(R request, string endpoint, bool excludeSecretKey = false, string overrideConfigSecretKey = null)
		{
			var json = JsonConvert.SerializeObject(request, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
			var requestResult = BuildRequest(HttpMethod.Post, endpoint, json, excludeSecretKey, overrideConfigSecretKey);

			var response = await SendRequest(requestResult.httpClient, requestResult.httpRequest);
			return await ProcessResponse<T>(response);
		}

		private static async Task<HttpResponseMessage> SendRequest(HttpClient httpClient, HttpRequestMessage request)
		{
			try
			{
				return await httpClient.SendAsync(request);
			}
			// handle timeouts
			catch (TaskCanceledException)
			{
				ResponseExceptionFactory.CreateTimeoutException();
			}

			return null;
		}

		private static async Task<T> ProcessResponse<T>(HttpResponseMessage response)
		{
			var responseString = await response.Content.ReadAsStringAsync();

			if (!response.IsSuccessStatusCode)
				ResponseExceptionFactory.CreateResponseException(responseString);

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
			httpClient.Timeout = new TimeSpan(0, 0, 0, 0, Config.TimeoutMilliseconds);

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
