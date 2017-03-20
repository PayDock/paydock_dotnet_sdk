using NUnit.Framework;
using Paydock_dotnet_sdk.Services;
using Paydock_dotnet_sdk.Models;
using System;
using System.Linq;

namespace FunctionalTests
{
    [TestFixture]
    public class CustomerServiceTests
    {
        [SetUp]
        public void Init()
        {
            Config.Initialise(Paydock_dotnet_sdk.Services.Environment.Sandbox, TestConfig.SecretKey);
        }

        [Test]
        public void CreateCustomerWithCreditCard()
        {
            var result = CreateBasicCustomer();

            Assert.IsTrue(result.IsSuccess);
        }

        private CustomerResponse CreateBasicCustomer(string email = "")
        {
            var request = new CustomerRequest
            {
                first_name = "john",
                last_name = "smith",
                email = email,
                payment_source = new PaymentSource
                {
                    gateway_id = TestConfig.GatewayId,
                    card_name = "John Smith",
                    card_number = "4111111111111111",
                    card_ccv = "123",
                    expire_month = "10",
                    expire_year = "2020"
                }
            };

            return new Customers().Add(request);
        }

        [Test]
        public void GetCustomers()
        {
            var result = new Customers().Get();

            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public void GetCustomersWithSearch()
        {
            var email = Guid.NewGuid().ToString() + "@email.com";
            var customer = CreateBasicCustomer(email);
            var result = new Customers().Get(new GetCustomersRequest {search = email});

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(1, result.resource.data.Count());
        }
    }
}
