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

        [Test]
        public void CreateSubscription()
        {
            var result = CreateBasicSubscription();

            Assert.IsTrue(result.IsSuccess);
        }

        private SubscriptionResponse CreateBasicSubscription()
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

            return new Subscriptions().Add(request);
        }

        [Test]
        public void UpdateSubscription()
        {
            var subscription = CreateBasicSubscription();

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
            var result = new Subscriptions().Update(request);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(request.amount, result.resource.data.amount);
            Assert.AreEqual(request.schedule.frequency, result.resource.data.schedule.frequency);
        }

        [Test]
        public void GetSubscriptions()
        {
            var subscription = CreateBasicSubscription();
            var response = new Subscriptions().Get();
            Assert.IsTrue(response.IsSuccess);
        }

        [Test]
        public void GetSubscriptionsWithSearch()
        {
            var subscription = CreateBasicSubscription();
            var response = new Subscriptions().Get(new SubscriptionSearchRequest { customer_id = subscription.resource.data.customer.customer_id });
            Assert.IsTrue(response.IsSuccess);
            Assert.AreEqual(1, response.resource.data.Count());
        }

        [Test]
        public void GetSingleSubscription()
        {
            var subscription = CreateBasicSubscription();
            var response = new Subscriptions().Get(subscription.resource.data._id);
            Assert.IsTrue(response.IsSuccess);
        }

        [Test]
        public void DeleteSubscription()
        {
            var subscription = CreateBasicSubscription();
            var response = new Subscriptions().Delete(subscription.resource.data._id);
            Assert.IsTrue(response.IsSuccess);
        }
    }
}
