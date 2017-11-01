using Paydock_dotnet_sdk.Services;

namespace FunctionalTests
{
    public static class TestConfig
    {
		public const string SecretKey = "";
		public const string GatewayId = "";
		public const string PaypalGatewayId = "";
		public const string PublicKey = "";
		public const string OverideSecretKey = "";
		public const string OveridePublicKey = "";

		public static void Init()
        {
            Config.Initialise(Environment.Sandbox, SecretKey, PublicKey, 60000);
		}
    }
}
