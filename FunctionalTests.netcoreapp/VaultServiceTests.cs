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

        private Vault CreateSvc(bool useOverrideKey = false)
        {
            return useOverrideKey ? new Vault(TestConfig.OverideSecretKey) : new Vault();
        }

        private async Task<VaultResponse> CreateBasicToken(bool useOverrideKey = false)
        {
            var request = RequestFactory.CreateVaultRequest();
            return await CreateSvc(useOverrideKey).Create(request);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task Create(bool useOverrideKey)
        {
            Assume.That(!useOverrideKey || TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            var result = await CreateBasicToken(useOverrideKey);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task GetSingleToken(bool useOverrideKey)
        {
            Assume.That(!useOverrideKey || TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            var result = await CreateBasicToken(useOverrideKey);
            var response = await CreateSvc(useOverrideKey).Get(result.resource.data.vault_token);
            Assert.IsTrue(response.IsSuccess);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task GetTokens(bool useOverrideKey)
        {
            Assume.That(!useOverrideKey || TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            var response = await CreateSvc(useOverrideKey).Get();
            Assert.IsTrue(response.IsSuccess);
        }
    }
}
