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
			var request = RequestFactory.CreateExternalCheckoutRequest();

			ExternalCheckoutResponse result;
            if (overideSecretKey != null)
                result = new ExternalCheckout(overideSecretKey).Create(request);
            else
                result = new ExternalCheckout().Create(request);

            Assert.IsTrue(result.IsSuccess);
        }
    }
}
