using NUnit.Framework;
using Paydock_dotnet_sdk.Services;
using Paydock_dotnet_sdk.Models;
using System.Threading.Tasks;

namespace FunctionalTests
{
    [TestFixture]
    public class TokenServiceTests
    {
        [SetUp]
        public void Init()
        {
            TestConfig.Init();
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task CreateToken(bool useOverrideKey)
        {
            Assume.That(!useOverrideKey || TestConfig.OveridePublicKey != null, "PAYDOCK_OVERRIDE_PUBLIC_KEY not configured");
            var request = new TokenRequest
            {
                gateway_id = TestConfig.GatewayId,
                card_name = "John Smith",
                card_number = "4111111111111111",
                card_ccv = "123",
                expire_month = "10",
                expire_year = "2030"
            };
            var tokens = useOverrideKey ? new Tokens(TestConfig.OveridePublicKey) : new Tokens();
            var result = await tokens.Create(request);
            Assert.IsTrue(result.IsSuccess);
        }
    }
}
