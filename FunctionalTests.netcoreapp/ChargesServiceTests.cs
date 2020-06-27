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
					email = customerEmail
				};


				TokenResponse tokenResult = await new Tokens().Create(tokenRequest);
				ChargeRequest threeDSrequest =  RequestFactory.Init3DSRequest(10M, tokenResult.resource.data);

				var result = await CreateSvc(overideSecretKey).Init3DS(threeDSrequest);
				
				Assert.IsTrue(result.IsSuccess);
			
		}


		//[TestCase(TestConfig.GatewayId, "4040404040404040")]
		[TestCase(TestConfig.MasterCardGatewayId, "5123450000000008", "test@test.com")]
		public async Task CreateFailedCharge( string gatewayId, string cardNumber, string customerEmail = "", string overideSecretKey = null)
		{
			var charge = RequestFactory.CreateChargeRequest(1.1M, gatewayId, customerEmail);			
			try
			{
				charge.customer.payment_source.card_number = cardNumber;
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
		public async Task CreateFailedChargeWith3DSAuth(string chargeId="", string overideSecretKey = null)
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

		[TestCase("aa5fa9fa-bc15-4aa5-9245-8b61bc614e44")]
		public async Task GetChargesWith3DSId(string threeDSId, string overideSecretKey = null)
		{
			
			ChargeItemResponse result = await CreateSvc(overideSecretKey).GetWith3DSId(threeDSId);
			Assert.IsTrue(result.IsSuccess);
		}
	}
}
