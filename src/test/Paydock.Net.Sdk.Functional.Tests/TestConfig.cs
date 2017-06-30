using Paydock.Net.Sdk.Services;

namespace Paydock.Net.Sdk.Functional.Tests
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
            Config.Initialise(Environment.Sandbox, SecretKey, PublicKey);
        }
    }
}
