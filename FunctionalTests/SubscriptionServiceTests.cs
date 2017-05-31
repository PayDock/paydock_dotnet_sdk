using NUnit.Framework;
using Paydock_dotnet_sdk.Services;
using Paydock_dotnet_sdk.Models;
using System;
using System.Linq;

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

        private SubscriptionResponse CreateBasicSubscription(string overideSecretKey = null)
        {
            var request = new SubscriptionRequest
            {
                amount = 20.0M,
                currency = "AUD",
                description = "this is a test",
                customer = new Paydock_dotnet_sdk.Models.Customer
                {
                    first_name = "first",
                    last_name = "last",
                    email = "test@test.com",
                    payment_source = new PaymentSource
                    {
                        gateway_id = TestConfig.GatewayId,
                        card_name = "John Smith",
                        card_number = "4111111111111111",
                        card_ccv = "123",
                        expire_month = "10",
                        expire_year = "2020"
                    }
                },
                schedule = new SubscriptionSchedule
                {
                    interval = "month",
                    frequency = 1
                }
            };

            if (overideSecretKey != null)
                return new Subscriptions(overideSecretKey).Add(request);
            else
                return new Subscriptions().Add(request);
        }

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public void CreateSubscription(string overideSecretKey)
        {
            var result = CreateBasicSubscription(overideSecretKey);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public void UpdateSubscription(string overideSecretKey)
        {
            var subscription = CreateBasicSubscription(overideSecretKey);

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
                result = new Subscriptions(overideSecretKey).Update(request);
            else
                result = new Subscriptions().Update(request);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(request.amount, result.resource.data.amount);
            Assert.AreEqual(request.schedule.frequency, result.resource.data.schedule.frequency);
        }

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public void GetSubscriptions(string overideSecretKey)
        {
            var subscription = CreateBasicSubscription(overideSecretKey);
            SubscriptionItemsResponse response;
            if (overideSecretKey != null)
                response = new Subscriptions(overideSecretKey).Get();
            else
                response = new Subscriptions().Get();
            Assert.IsTrue(response.IsSuccess);
        }

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public void GetSubscriptionsWithSearch(string overideSecretKey)
        {
            var subscription = CreateBasicSubscription(overideSecretKey);
            var request = new SubscriptionSearchRequest { customer_id = subscription.resource.data.customer.customer_id };
            SubscriptionItemsResponse response;
            if (overideSecretKey != null)
                response = new Subscriptions(overideSecretKey).Get(request);
            else
                response = new Subscriptions().Get(request);
            Assert.IsTrue(response.IsSuccess);
            Assert.AreEqual(1, response.resource.data.Count());
        }

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public void GetSingleSubscription(string overideSecretKey)
        {
            var subscription = CreateBasicSubscription(overideSecretKey);
            SubscriptionItemResponse response;
            if (overideSecretKey != null)
                response = new Subscriptions(overideSecretKey).Get(subscription.resource.data._id);
            else
                response = new Subscriptions().Get(subscription.resource.data._id);
            Assert.IsTrue(response.IsSuccess);
        }

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public void DeleteSubscription(string overideSecretKey)
        {
            var subscription = CreateBasicSubscription(overideSecretKey);
            SubscriptionItemResponse response;
            if (overideSecretKey != null)
                response = new Subscriptions(overideSecretKey).Delete(subscription.resource.data._id);
            else
                response = new Subscriptions().Delete(subscription.resource.data._id);
            Assert.IsTrue(response.IsSuccess);
        }
    }
}
