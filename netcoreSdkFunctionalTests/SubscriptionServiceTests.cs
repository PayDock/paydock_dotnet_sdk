using NUnit.Framework;
using Paydock_dotnet_sdk.Services;
using Paydock_dotnet_sdk.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FunctionalTests
{
    [TestFixture]
    public class SubscriptionServiceTests
    {
        [SetUp]
        public void Init()
        {
            TestConfig.Init();
        }

        private async Task<SubscriptionResponse> CreateBasicSubscription(string overideSecretKey = null)
        {
			var request = RequestFactory.CreateSubscriptionRequest();

			if (overideSecretKey != null)
                return await new Subscriptions(overideSecretKey).Add(request);
            else
                return await new Subscriptions().Add(request);
        }

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public async Task CreateSubscription(string overideSecretKey)
        {
            var result = await CreateBasicSubscription(overideSecretKey);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public async Task UpdateSubscription(string overideSecretKey)
        {
            var subscription = await CreateBasicSubscription(overideSecretKey);

            var request = new SubscriptionUpdateRequest
            {
                _id = subscription.resource.data._id,
                amount = 21.0M,
                currency = "AUD",
                description = "this is a test",
                schedule = new SubscriptionSchedule
                {
                    interval = "month",
                    frequency = 2,
                    start_date = DateTime.Now
                }
            };

            SubscriptionResponse result;
            if (overideSecretKey != null)
                result = await new Subscriptions(overideSecretKey).Update(request);
            else
                result = await new Subscriptions().Update(request);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(request.amount, result.resource.data.amount);
            Assert.AreEqual(request.schedule.frequency, result.resource.data.schedule.frequency);
        }

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public async Task GetSubscriptions(string overideSecretKey)
        {
            var subscription = await CreateBasicSubscription(overideSecretKey);
            SubscriptionItemsResponse response;
            if (overideSecretKey != null)
                response = await new Subscriptions(overideSecretKey).Get();
            else
                response = await new Subscriptions().Get();
            Assert.IsTrue(response.IsSuccess);
        }

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public async Task GetSubscriptionsWithSearch(string overideSecretKey)
        {
            var subscription = await CreateBasicSubscription(overideSecretKey);
            var request = new SubscriptionSearchRequest { customer_id = subscription.resource.data.customer.customer_id };
            SubscriptionItemsResponse response;
            if (overideSecretKey != null)
                response = await new Subscriptions(overideSecretKey).Get(request);
            else
                response = await new Subscriptions().Get(request);
            Assert.IsTrue(response.IsSuccess);
            Assert.AreEqual(1, response.resource.data.Count());
        }

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public async Task GetSingleSubscription(string overideSecretKey)
        {
            var subscription = await CreateBasicSubscription(overideSecretKey);
            SubscriptionItemResponse response;
            if (overideSecretKey != null)
                response = await new Subscriptions(overideSecretKey).Get(subscription.resource.data._id);
            else
                response = await new Subscriptions().Get(subscription.resource.data._id);
            Assert.IsTrue(response.IsSuccess);
        }

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public async Task DeleteSubscription(string overideSecretKey)
        {
            var subscription = await CreateBasicSubscription(overideSecretKey);
            SubscriptionItemResponse response;
            if (overideSecretKey != null)
                response = await new Subscriptions(overideSecretKey).Delete(subscription.resource.data._id);
            else
                response = await new Subscriptions().Delete(subscription.resource.data._id);
            Assert.IsTrue(response.IsSuccess);
        }
    }
}
