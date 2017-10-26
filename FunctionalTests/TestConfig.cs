using Paydock_dotnet_sdk.Services;

namespace FunctionalTests
{
    public static class TestConfig
    {
        public const string SecretKey = "fccbf57c8a65a609ed86edd417177905bfd5a99b";
        public const string GatewayId = "58377235377aea03343240cc";
        public const string PaypalGatewayId = "58bf55343c541b5b87f741bd";
        public const string PublicKey = "cc5bedb53a1b64491b5b468a2486b32cc250cda2";
        public const string OverideSecretKey = "fccbf57c8a65a609ed86edd417177905bfd5a99b";
        public const string OveridePublicKey = "cc5bedb53a1b64491b5b468a2486b32cc250cda2";

        public static void Init()
        {
            Config.Initialise(Environment.Sandbox, SecretKey, PublicKey, 60000);
		}
    }
}
