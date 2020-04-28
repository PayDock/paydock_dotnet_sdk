using NUnit.Framework;
using Paydock_dotnet_sdk.Services;
using Paydock_dotnet_sdk.Models;
using System.Threading.Tasks;

namespace FunctionalTests
{
	[TestFixture]
	public class ExternalCheckoutServiceTests
	{
		[SetUp]
		public void Init()
		{
			TestConfig.Init();
		}

		[TestCase(TestConfig.OverideSecretKey)]
		[TestCase(null)]
		public async Task CreateLink(string overideSecretKey)
		{
			var request = RequestFactory.CreateExternalCheckoutRequest();

			ExternalCheckoutResponse result;
			if (overideSecretKey != null)
				result = await new ExternalCheckout(overideSecretKey).Create(request);
			else
				result = await new ExternalCheckout().Create(request);

			Assert.IsTrue(result.IsSuccess);
		}
	}
}
