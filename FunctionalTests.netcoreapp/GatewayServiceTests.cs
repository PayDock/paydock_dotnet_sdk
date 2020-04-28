using NUnit.Framework;
using Paydock_dotnet_sdk.Services;
using Paydock_dotnet_sdk.Models;
using System.Threading.Tasks;

namespace FunctionalTests
{
	[TestFixture]
	public class GatewayServiceTests
	{
		[SetUp]
		public void Init()
		{
			TestConfig.Init();
		}

		private async Task<GatewayResponse> AddGateway(string overideSecretKey = null)
		{
			var request = RequestFactory.CreateGatewayRequest();

			if (overideSecretKey != null)
				return await new Gateways(overideSecretKey).Add(request);
			else
				return await new Gateways().Add(request);
		}

		[TestCase(TestConfig.OverideSecretKey)]
		[TestCase(null)]
		public async Task CreateGateway(string overideSecretKey)
		{
			var result = await AddGateway(overideSecretKey);

			Assert.IsTrue(result.IsSuccess);

			// clean up
			await new Gateways().Delete(result.resource.data._id);
		}

		[TestCase(TestConfig.OverideSecretKey)]
		[TestCase(null)]
		public async Task GetGateway(string overideSecretKey)
		{
			GatewayItemResponse result;
			if (overideSecretKey != null)
				result = await new Gateways(overideSecretKey).Get(TestConfig.GatewayId);
			else
				result = await new Gateways().Get(TestConfig.GatewayId);
			Assert.IsTrue(result.IsSuccess);
		}

		[TestCase(TestConfig.OverideSecretKey)]
		[TestCase(null)]
		public async Task Delete(string overideSecretKey)
		{
			var newGateway = await AddGateway(overideSecretKey);
			GatewayItemResponse result;

			if (overideSecretKey != null)
				result = await new Gateways(overideSecretKey).Delete(newGateway.resource.data._id);
			else
				result = await new Gateways().Delete(newGateway.resource.data._id);

			Assert.IsTrue(result.IsSuccess);
		}

		[TestCase(TestConfig.OverideSecretKey)]
		[TestCase(null)]
		public async Task Get(string overideSecretKey)
		{
			GatewayItemsResponse result;

			if (overideSecretKey != null)
				result = await new Gateways(overideSecretKey).Get();
			else
				result = await new Gateways().Get();

			Assert.IsTrue(result.IsSuccess);
		}

		[TestCase(TestConfig.OverideSecretKey)]
		[TestCase(null)]
		public async Task Update(string overideSecretKey)
		{
			var newGateway = await AddGateway(overideSecretKey);
			var request = new GatewayUpdateRequest
			{
				_id = newGateway.resource.data._id,
				type = "Brain",
				name = "BraintreeTesting",
				merchant = "r7pcwvkbkgjfzk99",
				username = "n8nktcb42fy8ttgt",
				password = "c865e194d750148b93284c0c026e5f2a"
			};

			GatewayItemResponse result;
			if (overideSecretKey != null)
				result = await new Gateways(overideSecretKey).Update(request);
			else
				result = await new Gateways().Update(request);
			Assert.IsTrue(result.IsSuccess);

			// clean up
			if (overideSecretKey != null)
				await new Gateways(overideSecretKey).Delete(newGateway.resource.data._id);
			else
				await new Gateways().Delete(newGateway.resource.data._id);
		}
	}
}
