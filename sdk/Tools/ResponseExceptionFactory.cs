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
		public static void CreateResponseException(string result, Exception innerException)
		{
			dynamic obj = JsonConvert.DeserializeObject<ExpandoObject>(result, new ExpandoObjectConverter());
			var errorResponse = new ErrorResponse()
			{
				Status = Convert.ToInt32(obj.status),
				ExtendedInformation = obj,
				JsonResponse = result
			};


			var json = JObject.Parse(result);
			if (json["error_summary"] != null)
			{
				if (json["error_summary"]["message"] != null &&
						json["error_summary"]["message"].Count() == 0)
					errorResponse.ErrorMessage = (string)json["error_summary"]["message"];

				else if (json["error_summary"]["message"] != null &&
					json["error_summary"]["message"]["message"].Count() == 0)
					errorResponse.ErrorMessage = (string)json["error_summary"]["message"]["message"];


				if (json["resource"] != null &&
						json["resource"]["type"] != null &&
							Convert.ToString(json["resource"]["type"]) == "charge")
					AddExceptionChargeResponce(errorResponse, result);

				if (json["error_summary"]["status_code"] != null)
					errorResponse.StatusCode = Convert.ToString(json["error_summary"]["status_code"]);

				if (json["error_summary"]["status_code_description"] != null)
					errorResponse.StatusCodeDescription = Convert.ToString(json["error_summary"]["status_code_description"]);

				if (json["error_summary"]["code"] != null)
					errorResponse.ErrorCode = Convert.ToString(json["error_summary"]["code"]);

				if (json["error_summary"]["details"] != null)
					AddErrorDetails(errorResponse, result, obj);

			}
			throw new ResponseException(errorResponse, errorResponse.Status.ToString(), innerException);
		}

		public static void AddExceptionChargeResponce(ErrorResponse errorResponse, string result)
		{
			try
			{
				var chargeResponse = JsonConvert.DeserializeObject<ChargeResponse>(result);
				if (chargeResponse != null)
					errorResponse.ExceptionChargeResponse = chargeResponse;
			}
			catch (Exception) { }
		}

		public static void AddErrorDetails(ErrorResponse errorResponse, string result, dynamic obj)
		{
			try
			{
				errorResponse.ErrorDetails = JsonConvert.DeserializeObject<Details>(
						JsonConvert.SerializeObject(obj.error_summary.details),
						new JsonSerializerSettings
						{
							DateTimeZoneHandling = DateTimeZoneHandling.Utc
						}
				);
			}
			catch (Exception) { }
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
