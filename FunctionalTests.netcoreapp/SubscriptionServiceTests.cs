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

        private Subscriptions CreateSvc(bool useOverrideKey = false)
        {
            return useOverrideKey ? new Subscriptions(TestConfig.OverideSecretKey) : new Subscriptions();
        }

        private async Task<SubscriptionResponse> CreateBasicSubscription(bool useOverrideKey = false)
        {
            var request = RequestFactory.CreateSubscriptionRequest();
            return await CreateSvc(useOverrideKey).Add(request);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task CreateSubscription(bool useOverrideKey)
        {
            Assume.That(!useOverrideKey || TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            var result = await CreateBasicSubscription(useOverrideKey);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task UpdateSubscription(bool useOverrideKey)
        {
            Assume.That(!useOverrideKey || TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            var subscription = await CreateBasicSubscription(useOverrideKey);
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
            var result = await CreateSvc(useOverrideKey).Update(request);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(request.amount, result.resource.data.amount);
            Assert.AreEqual(request.schedule.frequency, result.resource.data.schedule.frequency);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task GetSubscriptions(bool useOverrideKey)
        {
            Assume.That(!useOverrideKey || TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            await CreateBasicSubscription(useOverrideKey);
            var response = await CreateSvc(useOverrideKey).Get();
            Assert.IsTrue(response.IsSuccess);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task GetSubscriptionsWithSearch(bool useOverrideKey)
        {
            Assume.That(!useOverrideKey || TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            var subscription = await CreateBasicSubscription(useOverrideKey);
            var request = new SubscriptionSearchRequest { customer_id = subscription.resource.data.customer.customer_id };
            var response = await CreateSvc(useOverrideKey).Get(request);
            Assert.IsTrue(response.IsSuccess);
            Assert.AreEqual(1, response.resource.data.Count());
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task GetSingleSubscription(bool useOverrideKey)
        {
            Assume.That(!useOverrideKey || TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            var subscription = await CreateBasicSubscription(useOverrideKey);
            var response = await CreateSvc(useOverrideKey).Get(subscription.resource.data._id);
            Assert.IsTrue(response.IsSuccess);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task DeleteSubscription(bool useOverrideKey)
        {
            Assume.That(!useOverrideKey || TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            var subscription = await CreateBasicSubscription(useOverrideKey);
            var response = await CreateSvc(useOverrideKey).Delete(subscription.resource.data._id);
            Assert.IsTrue(response.IsSuccess);
        }
    }
}
