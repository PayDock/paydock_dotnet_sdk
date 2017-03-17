using NUnit.Framework;
using Paydock_dotnet_sdk.Services;
using Paydock_dotnet_sdk.Models;

namespace FunctionalTests
{
    [TestFixture]
    public class ChargesServiceTests
    {
        string secretKey = "";
        string gatewayId = "";

        [SetUp]
        public void Init()
        {
            Config.Initialise(Paydock_dotnet_sdk.Services.Environment.Sandbox, secretKey);
        }

        [Test]
        public void SimpleCharge()
        {
            var result = CreateBasicCharge(10.1M, gatewayId);

            Assert.IsTrue(result.IsSuccess);
        }

        private ChargeResponse CreateBasicCharge(decimal amount, string gatewayId)
        {
            var charge = new ChargeRequest
            {
                amount = amount,
                currency = "AUD",
                customer = new Customer
                {
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
            CreateBasicCharge(5, gatewayId);

            var result = new Charges().Get();
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public void GetChargesWithSearch()
        {
            CreateBasicCharge(6, gatewayId);
            var result = new Charges().Get(new GetChargeRequest { gateway_id = gatewayId });
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public void GetSingleCharge()
        {
            var charge = CreateBasicCharge(6, gatewayId);
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
            var charge = CreateBasicCharge(7, gatewayId);
            var result = new Charges().Refund(charge.resource.data._id, 7);
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public void Archive()
        {
            var charge = CreateBasicCharge(8, gatewayId);
            var result = new Charges().Archive(charge.resource.data._id);
            Assert.IsTrue(result.IsSuccess);
        }
    }
}
