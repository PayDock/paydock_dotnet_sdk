using System;
using System.Linq;
using NUnit.Framework;
using Paydock.Net.Sdk.Models;
using Paydock.Net.Sdk.Services;

namespace Paydock.Net.Sdk.Functional.Tests
{
    [TestFixture]
    public class CustomerServiceTests
    {
        [SetUp]
        public void Init()
        {
            TestConfig.Init();
        }

        private CustomerResponse CreateBasicCustomer(string email = "", string overideSecretKey = null)
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


            if (overideSecretKey != null)
                return new Customers(overideSecretKey).Add(request);
            else
                return new Customers().Add(request);
        }

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public void CreateCustomerWithCreditCard(string overideSecretKey)
        {
            var result = CreateBasicCustomer(overideSecretKey: overideSecretKey);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public void GetCustomers(string overideSecretKey)
        {
            CustomerItemsResponse result;
            if (overideSecretKey != null)
                result = new Customers(overideSecretKey).Get();
            else
                result = new Customers().Get();

            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public void GetCustomersWithSearch(string overideSecretKey)
        {
            var email = Guid.NewGuid().ToString() + "@email.com";
            var customer = CreateBasicCustomer(email, overideSecretKey: overideSecretKey);
            CustomerItemsResponse result;
            if (overideSecretKey != null)
                result = new Customers(overideSecretKey).Get(new CustomerSearchRequest { search = email });
            else
                result = new Customers().Get(new CustomerSearchRequest { search = email });

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(1, result.resource.data.Count());
        }

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public void GetSingleCustomer(string overideSecretKey)
        {
            var customer = CreateBasicCustomer(overideSecretKey: overideSecretKey);
            CustomerItemResponse result;
            if (overideSecretKey != null)
                result = new Customers(overideSecretKey).Get(customer.resource.data._id);
            else
                result = new Customers().Get(customer.resource.data._id);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public void GetSingleCustomerWithInvalidId(string overideSecretKey)
        {
            try
            {
                CustomerItemResponse result;
                if (overideSecretKey != null)
                    result = new Customers(overideSecretKey).Get("invalid_id_string");
                else
                    result = new Customers().Get("invalid_id_string");
            }
            catch (ResponseException ex)
            {
                Assert.AreEqual(404, ex.ErrorResponse.Status);
                return;
            }
            Assert.Fail();
        }

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public void UpdateCustomer(string overideSecretKey)
        {
            var customer = CreateBasicCustomer(overideSecretKey: overideSecretKey);
            CustomerItemResponse getCustomer;
            if (overideSecretKey != null)
                getCustomer = new Customers(overideSecretKey).Get(customer.resource.data._id);
            else
                getCustomer = new Customers().Get(customer.resource.data._id);

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

            CustomerItemResponse result;
            if (overideSecretKey != null)
                result = new Customers(overideSecretKey).Update(request);
            else
                result = new Customers().Update(request);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public void DeleteCustomer(string overideSecretKey)
        {
            var customer = CreateBasicCustomer(overideSecretKey: overideSecretKey);
            CustomerItemResponse result;
            if (overideSecretKey != null)
                result = new Customers(overideSecretKey).Delete(customer.resource.data._id);
            else
                result = new Customers().Delete(customer.resource.data._id);
            Assert.IsTrue(result.IsSuccess);
        }
    }
}
