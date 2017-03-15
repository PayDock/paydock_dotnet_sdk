using NUnit.Framework;
using Paydock_dotnet_sdk.Services;
using Paydock_dotnet_sdk.Models;

namespace FunctionalTests
{
    [TestFixture]
    public class ChargesServiceTests
    {
        [Test]
        public void SimpleCharge()
        {
            var secretKey = "your_secret_key";
            var gatewayId = "your_gateway_id";

            Config.Initialise(Environment.Sandbox, secretKey);

            var charge = new ChargeRequest
            {
                amount = 10,
                currency = "AUD",
                customer = new Customer
                {
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

            var chargeSvc = new Charges();
            var result = chargeSvc.Add(charge);
        }
    }
}
