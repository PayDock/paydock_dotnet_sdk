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
            Config.Initialise(Paydock_dotnet_sdk.Services.Environment.Sandbox, TestConfig.SecretKey);
        }

        [Test]
        public void SimpleCharge()
        {
            var result = CreateBasicCharge(10.1M, TestConfig.GatewayId);

            Assert.IsTrue(result.IsSuccess);
        }

        private ChargeResponse CreateBasicCharge(decimal amount, string gatewayId, string customerEmail = "")
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

            return new Charges().Add(charge);
        }


        [Test]
        public void GetCharges()
        {
            CreateBasicCharge(5, TestConfig.GatewayId);
            var result = new Charges().Get();
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public void GetChargesWithSearch()
        {
            var reference = Guid.NewGuid().ToString();
            CreateBasicCharge(6, TestConfig.GatewayId, reference);
            var result = new Charges().Get(new GetChargeRequest { gateway_id = TestConfig.GatewayId, search = reference });
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(1, result.resource.data.Count());
        }

        [Test]
        public void GetSingleCharge()
        {
            var charge = CreateBasicCharge(6, TestConfig.GatewayId);
            var result = new Charges().Get(charge.resource.data._id);
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public void GetSingleChargeWithInvalidID()
        {
            try
            {
                var result = new Charges().Get("invalid_id_string");
            }
            catch (ResponseException ex)
            {
                Assert.IsTrue(ex.ErrorResponse.Status == 404);
                return;
            }
            Assert.Fail();
        }

        [Test]
        public void Refund()
        {
            // NOTE: depending on the gateway, refunds may fail if transactions have not settled
            var charge = CreateBasicCharge(7, TestConfig.GatewayId);
            var result = new Charges().Refund(charge.resource.data._id, 7);
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public void Archive()
        {
            var charge = CreateBasicCharge(8, TestConfig.GatewayId);
            var result = new Charges().Archive(charge.resource.data._id);
            Assert.IsTrue(result.IsSuccess);
        }
    }
}
