using NUnit.Framework;
using Paydock_dotnet_sdk.Services;
using Paydock_dotnet_sdk.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FunctionalTests
{
    [TestFixture]
    public class CustomerServiceTests
    {
        [SetUp]
        public void Init()
        {
            TestConfig.Init();
        }

        private Customers CreateSvc(bool useOverrideKey = false)
        {
            return useOverrideKey ? new Customers(TestConfig.OverideSecretKey) : new Customers();
        }

        private async Task<CustomerResponse> CreateBasicCustomer(string email = "", bool useOverrideKey = false)
        {
            var request = RequestFactory.CreateCustomerRequest(email);
            return await CreateSvc(useOverrideKey).Add(request);
        }

        private async Task<CustomerResponse> CreateBasicFailedCustomer(string email = "", bool useOverrideKey = false)
        {
            var request = RequestFactory.CreateCustomerRequest(email);
            request.payment_source.card_number = "4242424242420000";
            return await CreateSvc(useOverrideKey).Add(request);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task CreateCustomerWithCreditCard(bool useOverrideKey)
        {
            Assume.That(!useOverrideKey || TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            var result = await CreateBasicCustomer(useOverrideKey: useOverrideKey);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task CreateCustomerWithFailedCreditCard(bool useOverrideKey)
        {
            Assume.That(!useOverrideKey || TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            try
            {
                await CreateBasicFailedCustomer(useOverrideKey: useOverrideKey);
            }
            catch (ResponseException ex)
            {
                Assert.IsTrue(ex.ErrorResponse.Status == 400);
                Assert.IsTrue(ex.ErrorResponse.ErrorDetails != null);
            }
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task GetCustomers(bool useOverrideKey)
        {
            Assume.That(!useOverrideKey || TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            var result = await CreateSvc(useOverrideKey).Get();
            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task GetCustomersWithSearch(bool useOverrideKey)
        {
            Assume.That(!useOverrideKey || TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            var email = Guid.NewGuid().ToString() + "@email.com";
            await CreateBasicCustomer(email, useOverrideKey: useOverrideKey);
            var result = await CreateSvc(useOverrideKey).Get(new CustomerSearchRequest { search = email });
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(1, result.resource.data.Count());
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task GetCustomersWithSearchById(bool useOverrideKey)
        {
            Assume.That(!useOverrideKey || TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            var email = Guid.NewGuid().ToString() + "@email.com";
            var customer = await CreateBasicCustomer(email, useOverrideKey: useOverrideKey);
            var result = await CreateSvc(useOverrideKey).Get(new CustomerSearchRequest { id = customer.resource.data._id });
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(1, result.resource.data.Count());
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task GetSingleCustomer(bool useOverrideKey)
        {
            Assume.That(!useOverrideKey || TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            var customer = await CreateBasicCustomer(useOverrideKey: useOverrideKey);
            var result = await CreateSvc(useOverrideKey).Get(customer.resource.data._id);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task GetSingleCustomerWithInvalidId(bool useOverrideKey)
        {
            Assume.That(!useOverrideKey || TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            try
            {
                await CreateSvc(useOverrideKey).Get("5b83eebc6d52ca1af1dd12df");
            }
            catch (ResponseException ex)
            {
                Assert.AreEqual(404, ex.ErrorResponse.Status);
                return;
            }
            Assert.Fail();
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task UpdateCustomer(bool useOverrideKey)
        {
            Assume.That(!useOverrideKey || TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            var customer = await CreateBasicCustomer(useOverrideKey: useOverrideKey);
            var getCustomer = await CreateSvc(useOverrideKey).Get(customer.resource.data._id);
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
                    expire_year = "2030"
                }
            };
            var result = await CreateSvc(useOverrideKey).Update(request);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task DeleteCustomer(bool useOverrideKey)
        {
            Assume.That(!useOverrideKey || TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            var customer = await CreateBasicCustomer(useOverrideKey: useOverrideKey);
            var result = await CreateSvc(useOverrideKey).Delete(customer.resource.data._id);
            Assert.IsTrue(result.IsSuccess);
        }
    }
}
