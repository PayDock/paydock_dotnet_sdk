using NUnit.Framework;
using Paydock_dotnet_sdk.Services;
using Paydock_dotnet_sdk.Models;

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

		private VaultResponse CreateBasicToken(string overrideSecretKey)
		{
			var request = new VaultRequest
			{
				card_name = "John Smith",
				card_number = "4111111111111111",
				expire_month = "10",
				expire_year = "2020"
			};
			
			if (overrideSecretKey != null)
				return new Vault(overrideSecretKey).CreateToken(request);
			else
				return new Vault().CreateToken(request);
		}

		[TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public void CreateToken(string overrideSecretKey)
        {
			var result = CreateBasicToken(overrideSecretKey);

            Assert.IsTrue(result.IsSuccess);
		}

		[TestCase(TestConfig.OverideSecretKey)]
		[TestCase(null)]
		public void GetSingleToken(string overrideSecretKey)
		{
			var token = CreateBasicToken(overrideSecretKey);

			VaultResponse result;
			if (overrideSecretKey != null)
				result = new Vault(overrideSecretKey).GetToken(token.resource.data.vault_token);
			else
				result = new Vault().GetToken(token.resource.data.vault_token);

			Assert.IsTrue(result.IsSuccess);
		}

		[TestCase(TestConfig.OverideSecretKey)]
		[TestCase(null)]
		public void GetTokens(string overrideSecretKey)
		{
			VaultItemsResponse result;
			if (overrideSecretKey != null)
				result = new Vault(overrideSecretKey).GetTokens();
			else
				result = new Vault().GetTokens();

			Assert.IsTrue(result.IsSuccess);
		}
	}
}
