using NUnit.Framework;
using Paydock_dotnet_sdk.Models;
using Paydock_dotnet_sdk.Services;
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
	}
}
