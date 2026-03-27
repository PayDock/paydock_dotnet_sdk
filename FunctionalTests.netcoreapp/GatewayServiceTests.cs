using NUnit.Framework;
using Paydock_dotnet_sdk.Services;
using Paydock_dotnet_sdk.Models;
using System.Threading.Tasks;

namespace FunctionalTests
{
    [TestFixture]
    public class GatewayServiceTests
    {
        [SetUp]
        public void Init()
        {
            TestConfig.Init();
        }

        private Gateways CreateSvc(bool useOverrideKey = false)
        {
            return useOverrideKey ? new Gateways(TestConfig.OverideSecretKey) : new Gateways();
        }

        private async Task<GatewayResponse> AddGateway(bool useOverrideKey = false)
        {
            var request = RequestFactory.CreateGatewayRequest();
            return await CreateSvc(useOverrideKey).Add(request);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task CreateGateway(bool useOverrideKey)
        {
            Assume.That(!useOverrideKey || TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            var result = await AddGateway(useOverrideKey);
            Assert.IsTrue(result.IsSuccess);
            // clean up
            await new Gateways().Delete(result.resource.data._id);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task GetGateway(bool useOverrideKey)
        {
            Assume.That(!useOverrideKey || TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            var result = await CreateSvc(useOverrideKey).Get(TestConfig.GatewayId);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task Delete(bool useOverrideKey)
        {
            Assume.That(!useOverrideKey || TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            var newGateway = await AddGateway(useOverrideKey);
            var result = await CreateSvc(useOverrideKey).Delete(newGateway.resource.data._id);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task Get(bool useOverrideKey)
        {
            Assume.That(!useOverrideKey || TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            var result = await CreateSvc(useOverrideKey).Get();
            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task Update(bool useOverrideKey)
        {
            Assume.That(!useOverrideKey || TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            var newGateway = await AddGateway(useOverrideKey);
            var request = new GatewayUpdateRequest
            {
                _id = newGateway.resource.data._id,
                type = "Brain",
                name = "BraintreeTesting",
                merchant = "r7pcwvkbkgjfzk99",
                username = "n8nktcb42fy8ttgt",
                password = "c865e194d750148b93284c0c026e5f2a"
            };
            var result = await CreateSvc(useOverrideKey).Update(request);
            Assert.IsTrue(result.IsSuccess);
            // clean up
            await CreateSvc(useOverrideKey).Delete(newGateway.resource.data._id);
        }
    }
}
