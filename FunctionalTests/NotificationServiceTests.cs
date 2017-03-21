using NUnit.Framework;
using Paydock_dotnet_sdk.Services;
using Paydock_dotnet_sdk.Models;
using System;
using System.Linq;

namespace FunctionalTests
{
    [TestFixture]
    public class NotificationServiceTests
    {
        [SetUp]
        public void Init()
        {
            TestConfig.Init();
        }

        [Test]
        public void AddTemplate()
        {
            var template = new NotificationRequest
            {
                body = "body",
                label = "test",
                notification_event = NotificationEvent.card_expiration_warning,
                html = true
            };
            var result = new Notifications().AddTemplate(template);
            Assert.IsTrue(result.IsSuccess);
        }
    }
}
