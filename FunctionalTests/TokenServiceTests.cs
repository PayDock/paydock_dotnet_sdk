using NUnit.Framework;
using Paydock_dotnet_sdk.Services;
using Paydock_dotnet_sdk.Models;

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

        [TestCase(TestConfig.OveridePublicKey)]
        [TestCase(null)]
        public void CreateToken(string overidePublicKey)
        {
            var request = new TokenRequest
            {
                gateway_id = TestConfig.GatewayId,
                card_name = "John Smith",
                card_number = "4111111111111111",
                card_ccv = "123",
                expire_month = "10",
                expire_year = "2020"
            };

            TokenResponse result;
            if (overidePublicKey != null)
                result = new Tokens(overidePublicKey).Create(request);
            else
                result = new Tokens().Create(request);

            Assert.IsTrue(result.IsSuccess);
        }
    }
}
