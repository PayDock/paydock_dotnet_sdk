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
            var request = HttpWebRequest.Create(Config.Environment + endpoint);

            // TODO: force TLS version

            request.Headers.Add("content-type", "application/json");
            request.Headers.Add("x-user-secret-key", Config.SecretKey);
            request.Method = method.ToString();

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(request);
            }

            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                return streamReader.ReadToEnd();
            }

            // TODO: set timeout
            // TODO: add error handling for HTTP responses
        }
    }
}
