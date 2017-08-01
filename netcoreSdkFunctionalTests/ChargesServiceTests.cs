using NUnit.Framework;
using Paydock_dotnet_sdk.Models;
using Paydock_dotnet_sdk.Services;
using System;
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
				return await new Charges(overideSecretKey).Add(charge);
			else
				return await new Charges().Add(charge);
		}

		[TestCase(TestConfig.OverideSecretKey)]
		[TestCase(null)]
		public void SimpleCharge(string overideSecretKey)
		{
			var chargeResult = CreateBasicCharge(10.1M, TestConfig.GatewayId, overideSecretKey: overideSecretKey);
			chargeResult.Wait();
			Console.WriteLine(chargeResult.Result.resource.data._id);
			Assert.IsTrue(chargeResult.Result.IsSuccess);
		}
	}
}
