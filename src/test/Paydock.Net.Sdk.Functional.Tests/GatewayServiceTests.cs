using NUnit.Framework;
using Paydock.Net.Sdk.Models;
using Paydock.Net.Sdk.Services;

namespace Paydock.Net.Sdk.Functional.Tests
{
    [TestFixture]
    public class GatewayServiceTests
    {
        [SetUp]
        public void Init()
        {
            TestConfig.Init();
        }

        private GatewayResponse AddGateway(string overideSecretKey = null)
        {
            var request = new GatewayRequest
            {
                type = "Brain",
                name = "BraintreeTesting",
                merchant = "r7pcwvkbkgjfzk99",
                username = "n8nktcb42fy8ttgt",
                password = "c865e194d750148b93284c0c026e5f2a"
            };

            if (overideSecretKey != null)
                return new Gateways(overideSecretKey).Add(request);
            else
                return new Gateways().Add(request);
        }

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public void CreateGateway(string overideSecretKey)
        {
            var result = AddGateway(overideSecretKey);

            Assert.IsTrue(result.IsSuccess);

            // clean up
            new Gateways().Delete(result.resource.data._id);
        }

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public void GetGateway(string overideSecretKey)
        {
            GatewayItemResponse result;
            if (overideSecretKey != null)
                result = new Gateways(overideSecretKey).Get(TestConfig.GatewayId);
            else
                result = new Gateways().Get(TestConfig.GatewayId);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public void Delete(string overideSecretKey)
        {
            var newGateway = AddGateway(overideSecretKey);
            GatewayItemResponse result;

            if (overideSecretKey != null)
                result = new Gateways(overideSecretKey).Delete(newGateway.resource.data._id);
            else
                result = new Gateways().Delete(newGateway.resource.data._id);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public void Get(string overideSecretKey)
        {
            GatewayItemsResponse result;

            if (overideSecretKey != null)
                result = new Gateways(overideSecretKey).Get();
            else
                result = new Gateways().Get();

            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public void Update(string overideSecretKey)
        {
            var newGateway = AddGateway(overideSecretKey);
            var request = new GatewayUpdateRequest
            {
                _id = newGateway.resource.data._id,
                type = "Brain",
                name = "BraintreeTesting",
                merchant = "r7pcwvkbkgjfzk99",
                username = "n8nktcb42fy8ttgt",
                password = "c865e194d750148b93284c0c026e5f2a"
            };

            GatewayItemResponse result;
            if (overideSecretKey != null)
                result = new Gateways(overideSecretKey).Update(request);
            else
                result = new Gateways().Update(request);
            Assert.IsTrue(result.IsSuccess);

            // clean up
            if (overideSecretKey != null)
                new Gateways(overideSecretKey).Delete(newGateway.resource.data._id);
            else
                new Gateways().Delete(newGateway.resource.data._id);
        }
    }
}
