using System;
using System.Net;

namespace Paydock_dotnet_sdk.Services
{
    public enum Environment
    {
        Sandbox,
        Production
    }

    /// <summary>
    /// Controls startup configuration
    /// </summary>
    public static class Config
    {
        public static Environment Environment { get; private set; }
        public static string SecretKey { get; private set; }
        public static string PublicKey { get; private set; }
        public static IWebProxy WebProxy { get; private set; }
		public static int TimeoutMilliseconds { get; set; }
        public static string CustomUrl { get; private set; }

    static Config()
        {
            Environment = Environment.Sandbox;
			TimeoutMilliseconds = 60000;
		}

        /// <summary>
        /// Initialise configuration for Paydock
        /// </summary>
        /// <param name="env">Environment to connect to</param>
        /// <param name="secretKey">Secret key for authentication</param>
        /// <param name="publicKey">Public key for authentication</param>
        /// <param name="webProxy"></param>
        /// <param name="timeoutMilliseconds">timeout for calls to the API</param>
        /// <param name="customUrl">Custom Base Url for API</param>
        public static void Initialise(Environment env, string secretKey, string publicKey, int timeoutMilliseconds = 60000, IWebProxy webProxy = null, string customUrl = null)
        {
            Environment = env;
            SecretKey = secretKey;
            PublicKey = publicKey;
            WebProxy = webProxy;
			TimeoutMilliseconds = timeoutMilliseconds;
            CustomUrl = customUrl;
		}

        /// <summary>
        /// Base address for the API, based on environment
        /// </summary>
        /// <returns>URL for the environment</returns>
        public static string BaseUrl()
        {
            if (CustomUrl!=null) return CustomUrl;
            return (Environment == Environment.Sandbox ? "https://api-sandbox.paydock.com/v1/" : "https://api.paydock.com/v1/");
        }
    }
}
