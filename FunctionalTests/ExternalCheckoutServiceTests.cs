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

        [Test]
        public void CreateLink()
        {
            var request = new ExternalCheckoutRequest
            {
                gateway_id = TestConfig.PaypalGatewayId,
                mode = "test",
                type = "paypal",
                success_redirect_url = "http://success.com",
                error_redirect_url = "http://failure.com"
            };

            var result = new ExternalCheckout().Create(request);

            Assert.IsTrue(result.IsSuccess);
        }
    }
}
