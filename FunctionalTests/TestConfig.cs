using Paydock_dotnet_sdk.Services;

namespace FunctionalTests
{
    public static class TestConfig
    {
        public const string SecretKey = "";
        public const string GatewayId = "";
        public const string PaypalGatewayId = "";
        public const string AuthoriseGatewayId = "";
        public const string PublicKey = "";
        public const string OverideSecretKey = "";
        public const string OveridePublicKey = "";
        public const string StripeGatewayId = "";
        public const string StripeAccountId = "";
        public const string MasterCardGatewayId = "";


        public static void Init()
        {
            Config.Initialise(Environment.Sandbox, SecretKey, PublicKey, 60000);
        }
    }
}
