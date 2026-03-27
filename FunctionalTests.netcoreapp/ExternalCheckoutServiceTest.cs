using NUnit.Framework;
using Paydock_dotnet_sdk.Services;
using Paydock_dotnet_sdk.Models;
using System.Threading.Tasks;

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

        [TestCase(false)]
        [TestCase(true)]
        public async Task CreateLink(bool useOverrideKey)
        {
            Assume.That(!useOverrideKey || TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            var request = RequestFactory.CreateExternalCheckoutRequest();
            var svc = useOverrideKey ? new ExternalCheckout(TestConfig.OverideSecretKey) : new ExternalCheckout();
            var result = await svc.Create(request);
            Assert.IsTrue(result.IsSuccess);
        }
    }
}
