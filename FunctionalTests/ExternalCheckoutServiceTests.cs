using NUnit.Framework;
using Paydock_dotnet_sdk.Services;
using Paydock_dotnet_sdk.Models;
namespace FunctionalTests
{
    [TestFixture]
    public class ExternalCheckoutServiceTests
    {
        [SetUp]
        public void Init()
        {
            TestConfig.Init();
        }

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public void CreateLink(string overideSecretKey)
        {
            var request = new ExternalCheckoutRequest
            {
                gateway_id = TestConfig.PaypalGatewayId,
                mode = "test",
                type = "paypal",
                success_redirect_url = "http://success.com",
                error_redirect_url = "http://failure.com"
            };

            ExternalCheckoutResponse result;
            if (overideSecretKey != null)
                result = new ExternalCheckout(overideSecretKey).Create(request);
            else
                result = new ExternalCheckout().Create(request);

            Assert.IsTrue(result.IsSuccess);
        }
    }
}
