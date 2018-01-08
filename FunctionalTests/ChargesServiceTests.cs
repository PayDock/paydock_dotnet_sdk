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

		private Charges CreateSvc(string overideSecretKey)
		{
			if (overideSecretKey != null)
				return new Charges(overideSecretKey);
			else
				return new Charges();

		}

		private ChargeResponse CreateBasicCharge(decimal amount, string gatewayId, string customerEmail = "", string overideSecretKey = null, string reference = null)
		{
			var charge = RequestFactory.CreateChargeRequest(amount, gatewayId, customerEmail);
			charge.reference = reference;

			return CreateSvc(overideSecretKey).Add(charge);
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
			CreateBasicCharge(5, TestConfig.GatewayId, overideSecretKey: overideSecretKey);
			ChargeItemsResponse result;
			result = CreateSvc(overideSecretKey).Get();
			Assert.IsTrue(result.IsSuccess);
		}

		[TestCase(TestConfig.OverideSecretKey)]
		[TestCase(null)]
		public void GetChargesWithSearch(string overideSecretKey)
		{
			var reference = Guid.NewGuid().ToString();
			CreateBasicCharge(6, TestConfig.GatewayId, overideSecretKey: overideSecretKey, reference: reference);
			ChargeItemsResponse result;
			result = CreateSvc(overideSecretKey).Get(new ChargeSearchRequest { gateway_id = TestConfig.GatewayId, reference = reference });
			Assert.IsTrue(result.IsSuccess);
			Assert.AreEqual(1, result.resource.data.Count());
		}

		[TestCase(TestConfig.OverideSecretKey)]
		[TestCase(null)]
		public void GetSingleCharge(string overideSecretKey)
		{
			var charge = CreateBasicCharge(6, TestConfig.GatewayId, overideSecretKey: overideSecretKey);
			ChargeItemResponse result;
			result = CreateSvc(overideSecretKey).Get(charge.resource.data._id);
			Assert.IsTrue(result.IsSuccess);
		}

		[TestCase(TestConfig.OverideSecretKey)]
		[TestCase(null)]
		public void GetSingleChargeWithInvalidId(string overideSecretKey)
		{
			try
			{
				ChargeItemResponse result;
				result = CreateSvc(overideSecretKey).Get("invalid_id_string");
			}
			catch (ResponseException ex)
			{
				Assert.IsTrue(ex.ErrorResponse.Status == 404);
				return;
			}
			Assert.Fail();
		}

		[TestCase(TestConfig.OverideSecretKey, false)]
		[TestCase(null, false)]
		[TestCase(null, true)]
		public void Refund(string overideSecretKey, bool isPartialRefund)
		{
			// NOTE: depending on the gateway, refunds may fail if transactions have not settled
			var charge = CreateBasicCharge(7, TestConfig.GatewayId, overideSecretKey: overideSecretKey);
			ChargeRefundResponse result;
			decimal? refundAmount = null;
			if (isPartialRefund)
				refundAmount = 6;
			result = CreateSvc(overideSecretKey).Refund(charge.resource.data._id, refundAmount);
			Assert.IsTrue(result.IsSuccess);
		}

		[TestCase]
		public void TestTimeout()
		{
			try
			{
				Config.TimeoutMilliseconds = 1;
				var result = CreateBasicCharge(10.1M, TestConfig.GatewayId);
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
		public void Archive(string overideSecretKey)
		{
			var charge = CreateBasicCharge(8, TestConfig.GatewayId, overideSecretKey: overideSecretKey);
			ChargeRefundResponse result;
			result = CreateSvc(overideSecretKey).Archive(charge.resource.data._id);
			Assert.IsTrue(result.IsSuccess);
		}

		[TestCase(TestConfig.OverideSecretKey)]
		[TestCase(null)]
		public void CreateStripeConnectChargeWithTransfer(string overideSecretKey)
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

			ChargeResponse result;
			result = CreateSvc(overideSecretKey).Add(charge);

			Assert.IsTrue(result.IsSuccess);
		}

		[TestCase(TestConfig.OverideSecretKey)]
		[TestCase(null)]
		public void CreateStripeConnectDirectCharge(string overideSecretKey)
		{
			var charge = RequestFactory.CreateBasicStripeConnectCharge();
			charge.meta = new MetaData
			{
				stripe_direct_account_id = "stripe_account_id",
				stripe_application_fee = 2M
			};

			ChargeResponse result;
			result = CreateSvc(overideSecretKey).Add(charge);

			Assert.IsTrue(result.IsSuccess);
		}

		[TestCase(TestConfig.OverideSecretKey)]
		[TestCase(null)]
		public void CreateStripeConnectDestinationCharge(string overideSecretKey)
		{
			var charge = RequestFactory.CreateBasicStripeConnectCharge();
			charge.meta = new MetaData
			{
				stripe_direct_account_id = "stripe_account_id",
				stripe_application_fee = 2M
			};

			ChargeResponse result;
			result = CreateSvc(overideSecretKey).Add(charge);

			Assert.IsTrue(result.IsSuccess);
		}

		[TestCase(TestConfig.OverideSecretKey)]
		[TestCase(null)]
		public void CreateAuthoriseCharge(string overideSecretKey)
		{
			var charge = RequestFactory.CreateChargeRequest(20M, TestConfig.AuthoriseGatewayId);

			ChargeResponse result;
			result = CreateSvc(overideSecretKey).Authorise(charge);

			Assert.IsTrue(result.IsSuccess);
		}

		[TestCase(TestConfig.OverideSecretKey, null)]
		[TestCase(null, null)]
		[TestCase(null, 10)]
		public void AuthoriseAndCaptureCharge(string overideSecretKey, decimal? amount)
		{
			var charge = RequestFactory.CreateChargeRequest(20M, TestConfig.AuthoriseGatewayId);

			Charges svc = CreateSvc(overideSecretKey);

			var chargeResult = svc.Authorise(charge);

			var captureResult = svc.Capture(chargeResult.resource.data._id, amount);

			Assert.IsTrue(captureResult.IsSuccess);
		}

		[TestCase(TestConfig.OverideSecretKey)]
		[TestCase(null)]
		public void AuthoriseAndCancelCharge(string overideSecretKey)
		{
			var charge = RequestFactory.CreateChargeRequest(20M, TestConfig.AuthoriseGatewayId);

			Charges svc = CreateSvc(overideSecretKey);

			var chargeResult = svc.Authorise(charge);

			var captureResult = svc.CancelAuthorisation(chargeResult.resource.data._id);

			Assert.IsTrue(captureResult.IsSuccess);
		}
	}
}
