using NUnit.Framework;
using Paydock_dotnet_sdk.Models;
using Paydock_dotnet_sdk.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

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

        private Charges CreateSvc(bool useOverrideKey = false)
        {
            return useOverrideKey ? new Charges(TestConfig.OverideSecretKey) : new Charges();
        }

        private async Task<ChargeResponse> CreateBasicCharge(decimal amount, string gatewayId, string customerEmail = "", bool useOverrideKey = false)
        {
            var charge = RequestFactory.CreateChargeRequest(amount, gatewayId, customerEmail);
            return await CreateSvc(useOverrideKey).Add(charge);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task SimpleCharge(bool useOverrideKey)
        {
            Assume.That(!useOverrideKey || TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            var chargeResult = await CreateBasicCharge(10.1M, TestConfig.GatewayId, useOverrideKey: useOverrideKey);
            Assert.IsTrue(chargeResult.IsSuccess);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task GetCharges(bool useOverrideKey)
        {
            Assume.That(!useOverrideKey || TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            await CreateBasicCharge(5, TestConfig.GatewayId, useOverrideKey: useOverrideKey);
            var result = await CreateSvc(useOverrideKey).Get();
            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task GetChargesWithSearch(bool useOverrideKey)
        {
            Assume.That(!useOverrideKey || TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            var reference = Guid.NewGuid().ToString();
            await CreateBasicCharge(6, TestConfig.GatewayId, reference, useOverrideKey: useOverrideKey);
            var result = await CreateSvc(useOverrideKey).Get(new ChargeSearchRequest { gateway_id = TestConfig.GatewayId, search = reference });
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(1, result.resource.data.Count());
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task GetSingleCharge(bool useOverrideKey)
        {
            Assume.That(!useOverrideKey || TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            var charge = await CreateBasicCharge(6, TestConfig.GatewayId, useOverrideKey: useOverrideKey);
            var result = await CreateSvc(useOverrideKey).Get(charge.resource.data._id);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task GetSingleChargeWithInvalidId(bool useOverrideKey)
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

        [TestCase(true, false)]
        [TestCase(false, false)]
        [TestCase(false, true)]
        public async Task Refund(bool useOverrideKey, bool isPartialRefund)
        {
            Assume.That(!useOverrideKey || TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            // NOTE: depending on the gateway, refunds may fail if transactions have not settled
            var charge = await CreateBasicCharge(7, TestConfig.GatewayId, useOverrideKey: useOverrideKey);
            decimal? refundAmount = isPartialRefund ? (decimal?)6 : null;
            var result = await CreateSvc(useOverrideKey).Refund(charge.resource.data._id, refundAmount);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task RefundWithCustomFields(bool isPartialRefund)
        {
            // NOTE: depending on the gateway, refunds may fail if transactions have not settled
            var charge = await CreateBasicCharge(7, TestConfig.GatewayId);
            decimal? refundAmount = isPartialRefund ? (decimal?)6 : null;
            var customFields = new { keyR1 = "valueRefund", keyR2 = "valueRefund2" };
            var result = await CreateSvc().Refund(charge.resource.data._id, refundAmount, customFields);
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public async Task UpdateCustomFields()
        {
            var charge = await CreateBasicCharge(7, TestConfig.GatewayId);
            var customFields = new { keyU1 = "valueUpdate", keyU2 = "valueUpdate2" };
            var result = await CreateSvc().UpdateCustomFields(charge.resource.data._id, charge.resource.data.transactions[0]._id, customFields);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(result.resource.data.custom_fields.keyU2 == "valueUpdate2");
        }

        [Test]
        public async Task TestTimeout()
        {
            try
            {
                Config.TimeoutMilliseconds = 1;
                await CreateBasicCharge(10.1M, TestConfig.GatewayId);
            }
            catch (ResponseException ex)
            {
                Assert.IsTrue(ex.ErrorResponse.Status == 408);
                Assert.IsTrue(ex.ErrorResponse.ErrorMessage == "Request Timeout");
                TestConfig.Init();
                return;
            }
            TestConfig.Init();
            Assert.Fail();
        }

        [TestCase(false)]
        [TestCase(true)]
        [Ignore("unable to test this easily with current test gateway")]
        public async Task Archive(bool useOverrideKey)
        {
            Assume.That(!useOverrideKey || TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            var charge = await CreateBasicCharge(8, TestConfig.GatewayId, useOverrideKey: useOverrideKey);
            var result = await CreateSvc(useOverrideKey).Archive(charge.resource.data._id);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task CreateStripeConnectChargeWithTransfer(bool useOverrideKey)
        {
            Assume.That(!useOverrideKey || TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            var charge = RequestFactory.CreateBasicStripeConnectCharge();
            charge.transfer = new Transfer
            {
                stripe_transfer_group = "group_id",
                items = new Transfer.TransferItems[] {
                    new Transfer.TransferItems { amount = 100, currency = "AUD", destination = "stripe_account_id" },
                    new Transfer.TransferItems { amount = 30, currency = "AUD", destination = "stripe_account_id2" }
                }
            };
            var result = await CreateSvc(useOverrideKey).Add(charge);
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public async Task InitialiseWallet()
        {
            Assume.That(TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            var charge = RequestFactory.CreateWalletRequest();
            var result = await CreateSvc(true).InitializeWallet(charge);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task CreateStripeConnectDirectCharge(bool useOverrideKey)
        {
            Assume.That(!useOverrideKey || TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            var charge = RequestFactory.CreateBasicStripeConnectCharge();
            charge.meta = new MetaData { stripe_direct_account_id = "stripe_account_id", stripe_application_fee = 2M };
            var result = await CreateSvc(useOverrideKey).Add(charge);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task CreateStripeConnectDestinationCharge(bool useOverrideKey)
        {
            Assume.That(!useOverrideKey || TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            var charge = RequestFactory.CreateBasicStripeConnectCharge();
            charge.meta = new MetaData { stripe_direct_account_id = "stripe_account_id", stripe_application_fee = 2M };
            var result = await CreateSvc(useOverrideKey).Add(charge);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task CreateAuthoriseCharge(bool useOverrideKey)
        {
            Assume.That(!useOverrideKey || TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            Assume.That(TestConfig.MasterCardGatewayId != null, "PAYDOCK_MASTERCARD_GATEWAY_ID not configured");
            var charge = RequestFactory.CreateChargeRequest(20M, TestConfig.MasterCardGatewayId);
            var result = await CreateSvc(useOverrideKey).Authorise(charge);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(true, null)]
        [TestCase(false, null)]
        [TestCase(false, 10)]
        public async Task AuthoriseAndCaptureCharge(bool useOverrideKey, decimal? amount)
        {
            Assume.That(!useOverrideKey || TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            Assume.That(TestConfig.MasterCardGatewayId != null, "PAYDOCK_MASTERCARD_GATEWAY_ID not configured");
            var svc = CreateSvc(useOverrideKey);
            var charge = RequestFactory.CreateChargeRequest(20M, TestConfig.MasterCardGatewayId);
            var chargeResponse = await svc.Authorise(charge);
            var authoriseResponse = await svc.Capture(chargeResponse.resource.data._id, amount);
            Assert.IsTrue(authoriseResponse.IsSuccess);
        }

        [TestCase(null)]
        [TestCase(10)]
        public async Task AuthoriseAndCaptureChargeWithCustomFields(decimal? amount)
        {
            Assume.That(TestConfig.MasterCardGatewayId != null, "PAYDOCK_MASTERCARD_GATEWAY_ID not configured");
            var svc = CreateSvc();
            var charge = RequestFactory.CreateChargeRequest(20M, TestConfig.MasterCardGatewayId);
            var customFields = new { kay1 = "value", key2 = "value2" };
            var chargeResponse = await svc.Authorise(charge);
            var authoriseResponse = await svc.Capture(chargeResponse.resource.data._id, amount, customFields);
            Assert.IsTrue(authoriseResponse.IsSuccess);
        }

        [TestCase(true, null)]
        [TestCase(false, null)]
        [TestCase(false, 10)]
        public async Task AuthoriseAndCancelCharge(bool useOverrideKey, decimal? amount)
        {
            Assume.That(!useOverrideKey || TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            Assume.That(TestConfig.MasterCardGatewayId != null, "PAYDOCK_MASTERCARD_GATEWAY_ID not configured");
            var svc = CreateSvc(useOverrideKey);
            var charge = RequestFactory.CreateChargeRequest(20M, TestConfig.MasterCardGatewayId);
            var chargeResponse = await svc.Authorise(charge);
            var cancelAuthoriseResponse = await svc.CancelAuthorisation(chargeResponse.resource.data._id);
            Assert.IsTrue(cancelAuthoriseResponse.IsSuccess);
        }

        [TestCase(true, null)]
        [TestCase(false, null)]
        [TestCase(false, 10)]
        public async Task AuthoriseAndCancelChargeWithCustomFields(bool useOverrideKey, decimal? amount)
        {
            Assume.That(!useOverrideKey || TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            Assume.That(TestConfig.MasterCardGatewayId != null, "PAYDOCK_MASTERCARD_GATEWAY_ID not configured");
            var svc = CreateSvc(useOverrideKey);
            var charge = RequestFactory.CreateChargeRequest(20M, TestConfig.MasterCardGatewayId);
            var chargeResponse = await svc.Authorise(charge);
            var customFields = new { key1 = "valueCancel", key2 = "valueCancel2" };
            var cancelAuthoriseResponse = await svc.CancelAuthorisation(chargeResponse.resource.data._id, customFields);
            Assert.IsTrue(cancelAuthoriseResponse.IsSuccess);
        }

        [Test]
        public async Task Initiate3DS()
        {
            Assume.That(TestConfig.MasterCardGatewayId != null, "PAYDOCK_MASTERCARD_GATEWAY_ID not configured");
            var tokenRequest = new TokenRequest
            {
                gateway_id = TestConfig.MasterCardGatewayId,
                card_name = "John Smith",
                card_number = "5500005555555559",
                card_ccv = "123",
                expire_month = "10",
                expire_year = "2023",
                email = "test@test.com",
                address_line1 = ""
            };
            var tokenResult = await new Tokens().Create(tokenRequest);
            var threeDSrequest = RequestFactory.Init3DSRequest(10M, tokenResult.resource.data);
            try
            {
                var result = await CreateSvc().Init3DS(threeDSrequest);
                Assert.IsTrue(result.IsSuccess);
            }
            catch (ResponseException ex)
            {
                Assert.IsTrue(ex.ErrorResponse.Status == 400);
            }
        }

        [Test]
        public async Task CreateFailedCharge()
        {
            Assume.That(TestConfig.MasterCardGatewayId != null, "PAYDOCK_MASTERCARD_GATEWAY_ID not configured");
            var charge = RequestFactory.CreateChargeRequest(1.1M, TestConfig.MasterCardGatewayId, "test@test.com");
            try
            {
                charge.customer.payment_source.card_number = "5123450000000008";
                charge.reference = "12345655555555555";
                await CreateSvc().Add(charge);
            }
            catch (ResponseException ex)
            {
                Assert.IsTrue(ex.ErrorResponse.Status == 400);
                Assert.IsTrue(ex.ErrorResponse.ExceptionChargeResponse != null);
            }
        }

        [TestCase("5123450000000008")]
        [TestCase("2223000000000007")]
        [TestCase("4508750015741019")]
        [TestCase("30123400000000")]
        public async Task CreateCharge(string cardNumber)
        {
            Assume.That(TestConfig.MasterCardGatewayId != null, "PAYDOCK_MASTERCARD_GATEWAY_ID not configured");
            var charge = RequestFactory.CreateChargeRequest(1.1M, TestConfig.MasterCardGatewayId, "test@test.com");
            charge.customer.payment_source.card_number = cardNumber;
            var result = await CreateSvc().Add(charge);
            Assert.IsTrue(result.status == 201);
        }

        [TestCase("5b83eebc6d52ca1af1dd12df")]
        public async Task CreateFailedChargeWith3DSAuth(string chargeId)
        {
            var charge = RequestFactory.CreateChargeRequest3DS(chargeId);
            try
            {
                await CreateSvc().Add(charge);
            }
            catch (ResponseException ex)
            {
                Assert.IsTrue(ex.ErrorResponse.Status == 400);
                Assert.IsTrue(ex.ErrorResponse.ExceptionChargeResponse != null);
            }
        }

        [TestCase("d0abbdf6-b02e-4cd9-80e9-18beebb5a9b1")]
        public async Task CreateFailedChargeWith3DSAuthValidation(string id)
        {
            var charge = RequestFactory.CreateChargeRequest3DSwithUUID(id);
            try
            {
                charge.reference = "12345678901234567890123456";
                await CreateSvc().Add(charge);
            }
            catch (ResponseException ex)
            {
                Assert.IsTrue(ex.ErrorResponse.Status == 400);
                Assert.IsTrue(ex.ErrorResponse.ExceptionChargeResponse != null);
            }
        }

        [TestCase("aa5fa9fa-bc15-4aa5-9245-8b61bc614e44")]
        public async Task CreateChargeWith3DSAuth(string id)
        {
            var charge = RequestFactory.CreateChargeRequest3DSwithUUID(id);
            try
            {
                var result = await CreateSvc().Add(charge);
                Assert.IsTrue(result.IsSuccess);
            }
            catch (ResponseException)
            {
            }
        }

        [TestCase("579462bb-f119-4e19-890b-4116e9f680bc")]
        public async Task GetChargesWith3DSId(string threeDSId)
        {
            var result = await CreateSvc().GetWith3DSId(threeDSId);
            Assert.IsTrue(result.IsSuccess);
        }
    }
}
