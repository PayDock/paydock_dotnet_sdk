using System;
using System.Linq;
using System.Net;
using System.IO;
using System.Dynamic;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using Paydock_dotnet_sdk.Models;
using Newtonsoft.Json.Linq;

namespace Paydock_dotnet_sdk.Services
{
    public enum HttpMethod
    {
        GET,
        POST,
        PUT,
        DELETE
    }

    public class ServiceHelper : IServiceHelper
    {
        public string CallPaydock(string endpoint, HttpMethod method, string json)
        {
            var url = Config.BaseUrl() + endpoint;
            var request = HttpWebRequest.Create(url);
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            ServicePointManager.DefaultConnectionLimit = 9999;

            request.ContentType = "application/json";
            request.Headers.Add("x-user-secret-key", Config.SecretKey);
            request.Method = method.ToString();

            string result = "";
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Headers["x-user-secret-key"] = Config.SecretKey;

                try
                {
                    result = client.UploadString(url, method.ToString(), json);
                }
                catch (WebException ex)
                {
                    HandleException(ex);
                }
            }
            
            return result;

        }

        private void HandleException(WebException exception)
        {
            using (var reader = new StreamReader(exception.Response.GetResponseStream()))
            {
                var result = reader.ReadToEnd();
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
                
                throw new ResponseException(errorResponse, exception.Status.ToString());
            }
        }
    }
}
