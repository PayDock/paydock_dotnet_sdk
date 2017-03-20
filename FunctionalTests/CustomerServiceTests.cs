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
            var result = new Customers().Get(new CustomerSearchRequest { search = email });

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(1, result.resource.data.Count());
        }

        [Test]
        public void GetSingleCustomer()
        {
            var customer = CreateBasicCustomer();
            var result = new Customers().Get(customer.resource.data._id);

            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public void GetSingleCustomerWithInvalidId()
        {
            try
            {
                var result = new Customers().Get("invalid_id_string");
            }
            catch (ResponseException ex)
            {
                Assert.AreEqual(404, ex.ErrorResponse.Status);
                return;
            }
            Assert.Fail();
        }

        [Test]
        public void UpdateCustomer()
        {
            var customer = CreateBasicCustomer();
            var getCustomer = new Customers().Get(customer.resource.data._id);

            var request = new CustomerUpdateRequest
            {
                customer_id = getCustomer.resource.data._id,
                first_name = "john1",
                last_name = "smith1",
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

            var result = new Customers().Update(request);
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public void DeleteCustomer()
        {
            var customer = CreateBasicCustomer();
            var result = new Customers().Delete(customer.resource.data._id);
            Assert.IsTrue(result.IsSuccess);
        }
    }
}
