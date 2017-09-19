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
		
		private async Task<ChargeResponse> CreateBasicCharge(decimal amount, string gatewayId, string customerEmail = "", string overideSecretKey = null)
		{
			var charge = RequestFactory.CreateChargeRequest(amount, gatewayId, customerEmail);

			if (overideSecretKey != null)
				return await new Charges(overideSecretKey).Add(charge);
			else
				return await new Charges().Add(charge);
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
			if (overideSecretKey != null)
				result = await new Charges(overideSecretKey).Get();
			else
				result = await new Charges().Get();
			Assert.IsTrue(result.IsSuccess);
		}

		[TestCase(TestConfig.OverideSecretKey)]
		[TestCase(null)]
		public async Task GetChargesWithSearch(string overideSecretKey)
		{
			var reference = Guid.NewGuid().ToString();
			await CreateBasicCharge(6, TestConfig.GatewayId, reference, overideSecretKey: overideSecretKey);
			ChargeItemsResponse result;
			if (overideSecretKey != null)
				result = await new Charges(overideSecretKey).Get(new ChargeSearchRequest { gateway_id = TestConfig.GatewayId, search = reference });
			else
				result = await new Charges().Get(new ChargeSearchRequest { gateway_id = TestConfig.GatewayId, search = reference });
			Assert.IsTrue(result.IsSuccess);
			Assert.AreEqual(1, result.resource.data.Count());
		}

		[TestCase(TestConfig.OverideSecretKey)]
		[TestCase(null)]
		public async Task GetSingleCharge(string overideSecretKey)
		{
			var charge = await CreateBasicCharge(6, TestConfig.GatewayId, overideSecretKey: overideSecretKey);
			ChargeItemResponse result;
			if (overideSecretKey != null)
				result = await new Charges(overideSecretKey).Get(charge.resource.data._id);
			else
				result = await new Charges(overideSecretKey).Get(charge.resource.data._id);
			Assert.IsTrue(result.IsSuccess);
		}

		[TestCase(TestConfig.OverideSecretKey)]
		[TestCase(null)]
		public async Task GetSingleChargeWithInvalidId(string overideSecretKey)
		{
			try
			{
				ChargeItemResponse result;
				if (overideSecretKey != null)
					result = await new Charges(overideSecretKey).Get("invalid_id_string");
				else
					result = await new Charges().Get("invalid_id_string");
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
			if (overideSecretKey != null)
				result = await new Charges(overideSecretKey).Refund(charge.resource.data._id, 7);
			else
				result = await new Charges().Refund(charge.resource.data._id, 7);
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
			if (overideSecretKey != null)
				result = await new Charges(overideSecretKey).Archive(charge.resource.data._id);
			else
				result = await new Charges().Archive(charge.resource.data._id);
			Assert.IsTrue(result.IsSuccess);
		}
	}
}
