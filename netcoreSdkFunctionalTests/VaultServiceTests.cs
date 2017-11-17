using NUnit.Framework;
using Paydock_dotnet_sdk.Services;
using Paydock_dotnet_sdk.Models;
using System.Threading.Tasks;

namespace FunctionalTests
{
    [TestFixture]
    public class VaultServiceTests
    {
        [SetUp]
        public void Init()
        {
            TestConfig.Init();
		}

		private async Task<VaultResponse> CreateBasicToken(string overrideSecretKey)
		{
			var request = RequestFactory.CreateVaultRequest();
			
			if (overrideSecretKey != null)
				return await new Vault(overrideSecretKey).Create(request);
			else
				return await new Vault().Create(request);
		}

		[TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public async Task Create(string overridePrivateKey)
        {
			var result = await CreateBasicToken(overridePrivateKey);

			Assert.IsTrue(result.IsSuccess);
		}

		[TestCase(TestConfig.OverideSecretKey)]
		[TestCase(null)]
		public async Task GetSingleToken(string overridePrivateKey)
		{
			var result = await CreateBasicToken(overridePrivateKey);

			VaultResponse response;
			if (overridePrivateKey != null)
				response = await new Vault(overridePrivateKey).Get(result.resource.data.vault_token);
			else
				response = await new Vault().Get(result.resource.data.vault_token);

			Assert.IsTrue(response.IsSuccess);
		}

		[TestCase(TestConfig.OverideSecretKey)]
		[TestCase(null)]
		public async Task GetTokens(string overridePrivateKey)
		{
			VaultItemsResponse response;
			if (overridePrivateKey != null)
				response = await new Vault(overridePrivateKey).Get();
			else
				response = await new Vault().Get();

			Assert.IsTrue(response.IsSuccess);
		}
	}
}
