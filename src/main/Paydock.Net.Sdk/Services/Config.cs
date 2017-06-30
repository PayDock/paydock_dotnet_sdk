﻿namespace Paydock.Net.Sdk.Services
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
		public static int Timeout { get; private set; }

		static Config()
        {
            Environment = Environment.Sandbox;
			Timeout = 60000;

		}

		/// <summary>
		/// Initialise configuration for Paydock
		/// </summary>
		/// <param name="env">Environment to connect to</param>
		/// <param name="secretKey">Secret key for authentication</param>
		/// <param name="publicKey">Public key for authentication</param>
		/// <param name="timeout">timeout for calls to the API</param>
		public static void Initialise(Environment env, string secretKey, string publicKey, int timeout = 60000)
        {
            Environment = env;
            SecretKey = secretKey;
            PublicKey = publicKey;
        }

        /// <summary>
        /// Base address for the API, based on environment
        /// </summary>
        /// <returns>URL for the environment</returns>
        public static string BaseUrl()
        {
            return (Environment == Environment.Sandbox ? "https://api-sandbox.paydock.com/v1/" : "https://api.paydock.com/v1/");
        }
    }
}
