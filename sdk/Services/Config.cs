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

        static Config()
        {
            Environment = Environment.Sandbox;
        }

        /// <summary>
        /// Initialise configuration for Paydock
        /// </summary>
        /// <param name="env">Environment to connect to</param>
        /// <param name="secretKey">Secret key for authentication</param>
        public static void Initialise(Environment env, string secretKey)
        {
            Environment = env;
            SecretKey = secretKey;
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
