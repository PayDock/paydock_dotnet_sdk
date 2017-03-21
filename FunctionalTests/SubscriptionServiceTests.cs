using NUnit.Framework;
using Paydock_dotnet_sdk.Services;
using Paydock_dotnet_sdk.Models;
namespace FunctionalTests
{
    [TestFixture]
    public class SubscriptionServiceTests
    {
        [SetUp]
        public void Init()
        {
            Config.Initialise(Paydock_dotnet_sdk.Services.Environment.Sandbox, TestConfig.SecretKey);
        }

        [Test]
        public void CreateCustomerWithCreditCard()
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
    }
}
