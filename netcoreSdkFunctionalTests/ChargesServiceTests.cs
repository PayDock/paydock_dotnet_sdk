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
				ChargeItemResponse result = await CreateSvc(overideSecretKey).Get("invalid_id_string");
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
		public async Task Refund(string overideSecretKey)
		{
			// NOTE: depending on the gateway, refunds may fail if transactions have not settled
			var charge = await CreateBasicCharge(7, TestConfig.GatewayId, overideSecretKey: overideSecretKey);
			ChargeRefundResponse result;
			result = await CreateSvc(overideSecretKey).Refund(charge.resource.data._id, 7);
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
			var charge = RequestFactory.CreateChargeRequest(20M, TestConfig.AuthoriseGatewayId);

			var result = await CreateSvc(overideSecretKey).Authorise(charge);

			Assert.IsTrue(result.IsSuccess);
		}

		[TestCase(TestConfig.OverideSecretKey, null)]
		[TestCase(null, null)]
		[TestCase(null, 10)]
		public async Task AuthoriseAndCaptureCharge(string overideSecretKey, decimal? amount)
		{
			var svc = CreateSvc(overideSecretKey);
			var charge = RequestFactory.CreateChargeRequest(20M, TestConfig.AuthoriseGatewayId);

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
			var charge = RequestFactory.CreateChargeRequest(20M, TestConfig.AuthoriseGatewayId);

			var chargeResponse = await svc.Authorise(charge);

			var cancelAuthoriseResponse = await svc.CancelAuthorisation(chargeResponse.resource.data._id);

			Assert.IsTrue(cancelAuthoriseResponse.IsSuccess);
		}
	}
}
