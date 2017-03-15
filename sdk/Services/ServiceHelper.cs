using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

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

                // TODO: set timeout
                // TODO: return the error code
                try
                {
                    result = client.UploadString(url, method.ToString(), "");
                }
                catch (WebException ex)
                {
                    using (var reader = new StreamReader(ex.Response.GetResponseStream()))
                    {
                        result = reader.ReadToEnd();
                    }
                }
            }
            
            return result;

        }
    }
}
