using Paydock_dotnet_sdk.Services;
using System;
using Env = System.Environment;

namespace FunctionalTests
{
    public static class TestConfig
    {
        public static string SecretKey => Env.GetEnvironmentVariable("PAYDOCK_SECRET_KEY") ?? "";
        public static string GatewayId => Env.GetEnvironmentVariable("PAYDOCK_GATEWAY_ID") ?? "";
        public static string PaypalGatewayId => Env.GetEnvironmentVariable("PAYDOCK_PAYPAL_GATEWAY_ID") ?? "";
        public static string AuthoriseGatewayId => Env.GetEnvironmentVariable("PAYDOCK_AUTHORISE_GATEWAY_ID") ?? "";
        public static string PublicKey => Env.GetEnvironmentVariable("PAYDOCK_PUBLIC_KEY") ?? "";
        public static string OverideSecretKey => Env.GetEnvironmentVariable("PAYDOCK_OVERRIDE_SECRET_KEY");
        public static string OveridePublicKey => Env.GetEnvironmentVariable("PAYDOCK_OVERRIDE_PUBLIC_KEY");
        public static string StripeGatewayId => Env.GetEnvironmentVariable("PAYDOCK_STRIPE_GATEWAY_ID") ?? "";
        public static string StripeAccountId => Env.GetEnvironmentVariable("PAYDOCK_STRIPE_ACCOUNT_ID") ?? "";
        public static string MasterCardGatewayId => Env.GetEnvironmentVariable("PAYDOCK_MASTERCARD_GATEWAY_ID");
        public static string FlypayGatewayId => Env.GetEnvironmentVariable("PAYDOCK_FLYPAY_GATEWAY_ID") ?? "";

        public static void Init()
        {
            Config.Initialise(Paydock_dotnet_sdk.Services.Environment.Sandbox, SecretKey, PublicKey, 60000);
        }
    }
}
