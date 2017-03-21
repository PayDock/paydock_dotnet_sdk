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
            var result = CreateBasicNotification();
            Assert.IsTrue(result.IsSuccess);
        }

        private NotificationTemplateResponse CreateBasicNotification()
        {
            var template = new NotificationTemplateRequest
            {
                body = "body",
                label = "test",
                notification_event = NotificationEvent.card_expiration_warning,
                html = true
            };
            var result = new Notifications().AddTemplate(template);
            return result;
        }

        [Test]
        public void UpdateTemplate()
        {
            var template = CreateBasicNotification();

            var updateTemplate = new NotificationTemplateUpdateRequest
            {
                _id = template.resource.data._id,
                body = "body1",
                label = "test",
                notification_event = NotificationEvent.card_expiration_warning,
                html = true
            };

            var result = new Notifications().UpdateTemplate(updateTemplate);
            Assert.AreEqual(updateTemplate.body, result.resource.data.body);
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public void DeleteTemplate()
        {
            var template = CreateBasicNotification();
            Assert.IsTrue(template.IsSuccess);

            var result = new Notifications().DeleteTemplate(template.resource.data._id);
            Assert.IsTrue(result.IsSuccess);
        }
    }
}
