using NUnit.Framework;
using Paydock_dotnet_sdk.Services;
using Paydock_dotnet_sdk.Models;
using System;
using System.Linq;

namespace FunctionalTests
{
    [TestFixture]
    public class ChargesServiceTests
    {
        [SetUp]
        public void Init()
        {
            TestConfig.Init();
        }

        private ChargeResponse CreateBasicCharge(decimal amount, string gatewayId, string customerEmail = "", string overideSecretKey = null)
        {
            var charge = new ChargeRequest
            {
                amount = amount,
                currency = "AUD",
                customer = new Customer
                {
                    email = customerEmail,
                    payment_source = new PaymentSource
                    {
                        gateway_id = gatewayId,
                        card_name = "Test Name",
                        card_number = "4111111111111111",
                        card_ccv = "123",
                        expire_month = "10",
                        expire_year = "2020"
                    }
                }
            };

            if (overideSecretKey != null)
                return new Charges(overideSecretKey).Add(charge);
            else
                return new Charges().Add(charge);
        }

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public void SimpleCharge(string overideSecretKey)
        {
            var result = CreateBasicCharge(10.1M, TestConfig.GatewayId, overideSecretKey: overideSecretKey);

            Assert.IsTrue(result.IsSuccess);
        }


        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public void GetCharges(string overideSecretKey)
        {
            CreateBasicCharge(5, TestConfig.GatewayId, overideSecretKey : overideSecretKey);
            ChargeItemsResponse result;
            if (overideSecretKey != null)
                result = new Charges(overideSecretKey).Get();
            else
                result = new Charges().Get();
            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public void GetChargesWithSearch(string overideSecretKey)
        {
            var reference = Guid.NewGuid().ToString();
            CreateBasicCharge(6, TestConfig.GatewayId, reference, overideSecretKey: overideSecretKey);
            ChargeItemsResponse result;
            if (overideSecretKey != null)
                result = new Charges(overideSecretKey).Get(new ChargeSearchRequest { gateway_id = TestConfig.GatewayId, search = reference });
            else
                result = new Charges().Get(new ChargeSearchRequest { gateway_id = TestConfig.GatewayId, search = reference });
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(1, result.resource.data.Count());
        }

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public void GetSingleCharge(string overideSecretKey)
        {
            var charge = CreateBasicCharge(6, TestConfig.GatewayId, overideSecretKey: overideSecretKey);
            ChargeItemResponse result;
            if (overideSecretKey != null)
                result = new Charges(overideSecretKey).Get(charge.resource.data._id);
            else
                result = new Charges(overideSecretKey).Get(charge.resource.data._id);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public void GetSingleChargeWithInvalidId(string overideSecretKey)
        {
            try
            {
                ChargeItemResponse result;
                if (overideSecretKey != null)
                    result = new Charges(overideSecretKey).Get("invalid_id_string");
                else
                    result = new Charges().Get("invalid_id_string");
            }
            catch (ResponseException ex)
            {
                Assert.IsTrue(ex.ErrorResponse.Status == 404);
                return;
            }
            Assert.Fail();
        }

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public void Refund(string overideSecretKey)
        {
            // NOTE: depending on the gateway, refunds may fail if transactions have not settled
            var charge = CreateBasicCharge(7, TestConfig.GatewayId, overideSecretKey: overideSecretKey);
            ChargeRefundResponse result;
            if (overideSecretKey != null)
                result = new Charges(overideSecretKey).Refund(charge.resource.data._id, 7);
            else
                result = new Charges().Refund(charge.resource.data._id, 7);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        [Ignore("unable to test this easily with current test gateway")]
        public void Archive(string overideSecretKey)
        {
            var charge = CreateBasicCharge(8, TestConfig.GatewayId, overideSecretKey: overideSecretKey);
            ChargeRefundResponse result;
            if (overideSecretKey != null)
                result = new Charges(overideSecretKey).Archive(charge.resource.data._id);
            else
                result = new Charges().Archive(charge.resource.data._id);
            Assert.IsTrue(result.IsSuccess);
        }
    }
}
