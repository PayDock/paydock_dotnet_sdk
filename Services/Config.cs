namespace Paydock_dotnet_sdk.Services
{
    public enum Environment
    {
        Sandbox,
        Production
    }

    public static class Config
    {
        public static Environment Environment { get; private set; }
        public static string SecretKey { get; private set; }

        static Config()
        {
            Environment = Environment.Sandbox;
        }

        public static void Initialise(Environment env, string secretKey)
        {
            Environment = env;
            SecretKey = secretKey;
        }

        public static string BaseUrl()
        {
            return (Environment == Environment.Sandbox ? "https://api-sandbox.paydock.com/v1/" : "https://api.paydock.com/v1/");
        }
    }
}
