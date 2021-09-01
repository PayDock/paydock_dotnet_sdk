using NUnit.Framework; using Paydock_dotnet_sdk.Models; using Paydock_dotnet_sdk.Services; using System; using System.Linq; using System.Threading.Tasks;  namespace FunctionalTests {
    [TestFixture]
    public class ChargesServiceTests
    {
        [SetUp]
        public void Init()
        {
            TestConfig.Init();
        }

        private Charges CreateSvc(string overideSecretKey)
        {
            if (overideSecretKey != null)
                return new Charges(overideSecretKey);
            else
                return new Charges();
        }

        private async Task<ChargeResponse> CreateBasicCharge(decimal amount, string gatewayId, string customerEmail = "", string overideSecretKey = null)
        {
            var charge = RequestFactory.CreateChargeRequest(amount, gatewayId, customerEmail);

            return await CreateSvc(overideSecretKey).Add(charge);
        }

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public async Task SimpleCharge(string overideSecretKey)
        {
            var chargeResult = await CreateBasicCharge(10.1M, TestConfig.GatewayId, overideSecretKey: overideSecretKey);
            Assert.IsTrue(chargeResult.IsSuccess);
        }


        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public async Task GetCharges(string overideSecretKey)
        {
            await CreateBasicCharge(5, TestConfig.GatewayId, overideSecretKey: overideSecretKey);
            ChargeItemsResponse result;
            result = await CreateSvc(overideSecretKey).Get();
            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public async Task GetChargesWithSearch(string overideSecretKey)
        {
            var reference = Guid.NewGuid().ToString();
            await CreateBasicCharge(6, TestConfig.GatewayId, reference, overideSecretKey: overideSecretKey);
            ChargeItemsResponse result;
            result = await CreateSvc(overideSecretKey).Get(new ChargeSearchRequest { gateway_id = TestConfig.GatewayId, search = reference });
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(1, result.resource.data.Count());
        }

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public async Task GetSingleCharge(string overideSecretKey)
        {
            var charge = await CreateBasicCharge(6, TestConfig.GatewayId, overideSecretKey: overideSecretKey);
            ChargeItemResponse result;
            result = await CreateSvc(overideSecretKey).Get(charge.resource.data._id);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public async Task GetSingleChargeWithInvalidId(string overideSecretKey)
        {
            try
            {
                ChargeItemResponse result = await CreateSvc(overideSecretKey).Get("5b83eebc6d52ca1af1dd12df");
            }
            catch (ResponseException ex)
            {
                Assert.AreEqual(404, ex.ErrorResponse.Status);
                return;
            }
            Assert.Fail();
        }

        [TestCase(TestConfig.OverideSecretKey, false)]
        [TestCase(null, false)]
        [TestCase(null, true)]
        public async Task Refund(string overideSecretKey, bool isPartialRefund)
        {
            // NOTE: depending on the gateway, refunds may fail if transactions have not settled
            var charge = await CreateBasicCharge(7, TestConfig.GatewayId, overideSecretKey: overideSecretKey);
            ChargeRefundResponse result;
            decimal? refundAmount = null;
            if (isPartialRefund)
                refundAmount = 6;
            result = await CreateSvc(overideSecretKey).Refund(charge.resource.data._id, refundAmount);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(null, false)]
        [TestCase(null, true)]
        public async Task RefundWithCustomFields(string overideSecretKey, bool isPartialRefund)
        {
            // NOTE: depending on the gateway, refunds may fail if transactions have not settled
            var charge = await CreateBasicCharge(7, TestConfig.GatewayId, overideSecretKey: overideSecretKey);
            ChargeRefundResponse result;
            decimal? refundAmount = null;
            if (isPartialRefund)
                refundAmount = 6;

            var customFields = new
            {
                keyR1 = "valueRefund",
                keyR2 = "valueRefund2"
            };

            result = await CreateSvc(overideSecretKey).Refund(charge.resource.data._id, refundAmount, customFields);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(null, true)]
        public async Task UpdateCustomFields(string overideSecretKey, bool isPartialRefund)
        {
            var charge = await CreateBasicCharge(7, TestConfig.GatewayId, overideSecretKey: overideSecretKey);
            TransactionResponse result;


            var customFields = new
            {
                keyU1 = "valueUpdate",
                keyU2 = "valueUpdate2"
            };

            result = await CreateSvc(overideSecretKey).UpdateCustomFields(charge.resource.data._id, charge.resource.data.transactions[0]._id, customFields);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(result.resource.data.custom_fields.keyU2 == "valueUpdate2");
        }

        [TestCase]
        public async Task TestTimeout()
        {
            try
            {
                Config.TimeoutMilliseconds = 1;
                var result = await CreateBasicCharge(10.1M, TestConfig.GatewayId);
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

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        [Ignore("unable to test this easily with current test gateway")]
        public async Task Archive(string overideSecretKey)
        {
            var charge = await CreateBasicCharge(8, TestConfig.GatewayId, overideSecretKey: overideSecretKey);
            ChargeRefundResponse result;
            result = await CreateSvc(overideSecretKey).Archive(charge.resource.data._id);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public async Task CreateStripeConnectChargeWithTransfer(string overideSecretKey)
        {
            var charge = RequestFactory.CreateBasicStripeConnectCharge();
            charge.transfer = new Transfer
            {
                stripe_transfer_group = "group_id",
                items = new Transfer.TransferItems[] {
                        new Transfer.TransferItems { amount = 100, currency = "AUD", destination = "stripe_account_id" },
                        new Transfer.TransferItems { amount = 30, currency = "AUD", destination = "stripe_account_id2" }
                    }
            };

            var result = await CreateSvc(overideSecretKey).Add(charge);

            Assert.IsTrue(result.IsSuccess);
        }


        [TestCase(TestConfig.OverideSecretKey)]
        public async Task InitialiseWallet(string overideSecretKey)
        {
           
                var charge = RequestFactory.CreateWalletRequest();

                var result = await CreateSvc(overideSecretKey).InitializeWallet(charge);

                Assert.IsTrue(result.IsSuccess);
            
          
        }

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public async Task CreateStripeConnectDirectCharge(string overideSecretKey)
        {
            var charge = RequestFactory.CreateBasicStripeConnectCharge();
            charge.meta = new MetaData
            {
                stripe_direct_account_id = "stripe_account_id",
                stripe_application_fee = 2M
            };

            var result = await CreateSvc(overideSecretKey).Add(charge);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public async Task CreateStripeConnectDestinationCharge(string overideSecretKey)
        {
            var charge = RequestFactory.CreateBasicStripeConnectCharge();
            charge.meta = new MetaData
            {
                stripe_direct_account_id = "stripe_account_id",
                stripe_application_fee = 2M
            };

            var result = await CreateSvc(overideSecretKey).Add(charge);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public async Task CreateAuthoriseCharge(string overideSecretKey)
        {
            var charge = RequestFactory.CreateChargeRequest(20M, TestConfig.MasterCardGatewayId);

            var result = await CreateSvc(overideSecretKey).Authorise(charge);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(TestConfig.OverideSecretKey, null)]
        [TestCase(null, null)]
        [TestCase(null, 10)]
        public async Task AuthoriseAndCaptureCharge(string overideSecretKey, decimal? amount)
        {
            var svc = CreateSvc(overideSecretKey);
            var charge = RequestFactory.CreateChargeRequest(20M, TestConfig.MasterCardGatewayId);

            var chargeResponse = await svc.Authorise(charge);

            var authoriseResponse = await svc.Capture(chargeResponse.resource.data._id, amount);

            Assert.IsTrue(authoriseResponse.IsSuccess);
        }

        [TestCase(null, null)]
        [TestCase(null, 10)]
        public async Task AuthoriseAndCaptureChargeWithCutomFields(string overideSecretKey, decimal? amount)
        {
            var svc = CreateSvc(overideSecretKey);
            var charge = RequestFactory.CreateChargeRequest(20M, TestConfig.MasterCardGatewayId);
            var customFields = new
            {
                kay1 = "value",
                key2 = "value2"
            };

            var chargeResponse = await svc.Authorise(charge);



            var authoriseResponse = await svc.Capture(chargeResponse.resource.data._id, amount, customFields);

            Assert.IsTrue(authoriseResponse.IsSuccess);
        }


        [TestCase(TestConfig.OverideSecretKey, null)]
        [TestCase(null, null)]
        [TestCase(null, 10)]
        public async Task AuthoriseAndCancelCharge(string overideSecretKey, decimal? amount)
        {
            var svc = CreateSvc(overideSecretKey);
            var charge = RequestFactory.CreateChargeRequest(20M, TestConfig.MasterCardGatewayId);

            var chargeResponse = await svc.Authorise(charge);

            var cancelAuthoriseResponse = await svc.CancelAuthorisation(chargeResponse.resource.data._id);

            Assert.IsTrue(cancelAuthoriseResponse.IsSuccess);
        }



        [TestCase(TestConfig.OverideSecretKey, null)]
        [TestCase(null, null)]
        [TestCase(null, 10)]
        public async Task AuthoriseAndCancelChargeWithCustomFields(string overideSecretKey, decimal? amount)
        {
            var svc = CreateSvc(overideSecretKey);
            var charge = RequestFactory.CreateChargeRequest(20M, TestConfig.MasterCardGatewayId);

            var chargeResponse = await svc.Authorise(charge);

            var customFields = new
            {
                key1 = "valueCancel",
                key2 = "valueCancel2"
            };

            var cancelAuthoriseResponse = await svc.CancelAuthorisation(chargeResponse.resource.data._id, customFields);

            Assert.IsTrue(cancelAuthoriseResponse.IsSuccess);
        }


        [TestCase(TestConfig.MasterCardGatewayId, "5500005555555559", "test@test.com")]
        public async Task Initiate3DS(string gatewayId, string cardNumber, string customerEmail = "", string overideSecretKey = null)
        {

            var tokenRequest = new TokenRequest
            {
                gateway_id = gatewayId,
                card_name = "John Smith",
                card_number = cardNumber,
                card_ccv = "123",
                expire_month = "10",
                expire_year = "2023",
                email = customerEmail,
                address_line1="",

            };


            TokenResponse tokenResult = await new Tokens().Create(tokenRequest);
            ChargeRequest threeDSrequest = RequestFactory.Init3DSRequest(10M, tokenResult.resource.data);

            
            try
            {
                var result = await CreateSvc(overideSecretKey).Init3DS(threeDSrequest);
                Assert.IsTrue(result.IsSuccess);
            }
            catch (ResponseException ex)
            {
                Assert.IsTrue(ex.ErrorResponse.Status == 400);
            }
           

        }


        //[TestCase(TestConfig.GatewayId, "4040404040404040")]
        [TestCase(TestConfig.MasterCardGatewayId, "5123450000000008", "test@test.com")]
        public async Task CreateFailedCharge(string gatewayId, string cardNumber, string customerEmail = "", string overideSecretKey = null)
        {
            var charge = RequestFactory.CreateChargeRequest(1.1M, gatewayId, customerEmail);
            try
            {
                charge.customer.payment_source.card_number = cardNumber;
                charge.reference = "12345655555555555";
                var result = await CreateSvc(overideSecretKey).Add(charge);
            }
            catch (ResponseException ex)
            {
                Assert.IsTrue(ex.ErrorResponse.Status == 400);
                Assert.IsTrue(ex.ErrorResponse.ExceptionChargeResponse != null);
            }
        }

        [TestCase(TestConfig.MasterCardGatewayId, "5123450000000008", "test@test.com")]
        [TestCase(TestConfig.MasterCardGatewayId, "2223000000000007", "test@test.com")]
        [TestCase(TestConfig.MasterCardGatewayId, "4508750015741019", "test@test.com")]
        [TestCase(TestConfig.MasterCardGatewayId, "30123400000000", "test@test.com")]
        public async Task CreateCharge(string gatewayId, string cardNumber, string customerEmail = "", string overideSecretKey = null)
        {
            var charge = RequestFactory.CreateChargeRequest(1.1M, gatewayId, customerEmail);

            charge.customer.payment_source.card_number = cardNumber;
            var result = await CreateSvc(overideSecretKey).Add(charge);

            Assert.IsTrue(result.status == 201);
        }

        [TestCase("5b83eebc6d52ca1af1dd12df")]
        public async Task CreateFailedChargeWith3DSAuth(string chargeId = "", string overideSecretKey = null)
        {
            var charge = RequestFactory.CreateChargeRequest3DS(chargeId);
            try
            {
                var result = await CreateSvc(overideSecretKey).Add(charge);
            }
            catch (ResponseException ex)
            {
                Assert.IsTrue(ex.ErrorResponse.Status == 400);
                Assert.IsTrue(ex.ErrorResponse.ExceptionChargeResponse != null);
            }
        }

        [TestCase("d0abbdf6-b02e-4cd9-80e9-18beebb5a9b1")]
        public async Task CreateFailedChargeWith3DSAuthValidation(string id = "", string overideSecretKey = null)
        {
            var charge = RequestFactory.CreateChargeRequest3DSwithUUID(id);
            try
            {
                charge.reference = "12345678901234567890123456";
                var result = await CreateSvc(overideSecretKey).Add(charge);
            }
            catch (ResponseException ex)
            {
                Assert.IsTrue(ex.ErrorResponse.Status == 400);
                Assert.IsTrue(ex.ErrorResponse.ExceptionChargeResponse != null);
            }
        }

        [TestCase("aa5fa9fa-bc15-4aa5-9245-8b61bc614e44")]
        public async Task CreateChargeWith3DSAuth(string id = "", string overideSecretKey = null)
        {
            var charge = RequestFactory.CreateChargeRequest3DSwithUUID(id);
            try
            {
                var result = await CreateSvc(overideSecretKey).Add(charge);
                Assert.IsTrue(result.IsSuccess);
            }
            catch (ResponseException)
            {

            }
        }

        [TestCase("579462bb-f119-4e19-890b-4116e9f680bc")]
        public async Task GetChargesWith3DSId(string threeDSId, string overideSecretKey = null)
        {

            ChargeItemResponse result = await CreateSvc(overideSecretKey).GetWith3DSId(threeDSId);
            Assert.IsTrue(result.IsSuccess);
        }
    } } 