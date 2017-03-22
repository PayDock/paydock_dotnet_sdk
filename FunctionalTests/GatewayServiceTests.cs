using NUnit.Framework;
using Paydock_dotnet_sdk.Services;
using Paydock_dotnet_sdk.Models;

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

        [Test]
        public void CreateGateway()
        {
            var result = AddGateway();

            Assert.IsTrue(result.IsSuccess);

            // clean up
            new Gateways().Delete(result.resource.data._id);
        }

        private GatewayResponse AddGateway()
        {
            var request = new GatewayRequest
            {
                type = "Brain",
                name = "BraintreeTesting",
                merchant = "r7pcwvkbkgjfzk99",
                username = "n8nktcb42fy8ttgt",
                password = "c865e194d750148b93284c0c026e5f2a"
            };

            return new Gateways().Add(request);

        }

        [Test]
        public void GetGateway()
        {
            var result = new Gateways().Get(TestConfig.GatewayId);
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public void Delete()
        {
            var result = AddGateway();
            new Gateways().Delete(result.resource.data._id);
            Assert.IsTrue(result.IsSuccess);
        }
    }
}
