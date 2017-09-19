using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Paydock_dotnet_sdk.Models;
using System;
using System.Dynamic;
using System.Linq;

namespace Paydock_dotnet_sdk.Tools
{
	public static class ResponseExceptionFactory
	{
		public static void CreateResponseException(string result)
		{
			dynamic obj = JsonConvert.DeserializeObject<ExpandoObject>(result, new ExpandoObjectConverter());
			var errorResponse = new ErrorResponse()
			{
				Status = Convert.ToInt32(obj.status),
				ExtendedInformation = obj,
				JsonResponse = result
			};

			var json = JObject.Parse(result);
			if (json["error"]["message"].Count() == 0)
			{
				errorResponse.ErrorMessage = (string)json["error"]["message"];
			}
			else if (json["error"]["message"]["message"].Count() == 0)
			{
				errorResponse.ErrorMessage = (string)json["error"]["message"]["message"];
			}

			throw new ResponseException(errorResponse, errorResponse.Status.ToString());
		}

		public static void CreateTimeoutException()
		{
			var errorResponse = new ErrorResponse()
			{
				Status = Convert.ToInt32(408),
				JsonResponse = "",
				ErrorMessage = "Request Timeout"
			};
			throw new ResponseException(errorResponse, errorResponse.Status.ToString());
		}
	}
}
