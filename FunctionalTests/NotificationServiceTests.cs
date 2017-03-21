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
            var result = CreateBasicNotificationTemplate();
            Assert.IsTrue(result.IsSuccess);
        }

        private NotificationTemplateResponse CreateBasicNotificationTemplate()
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
            var template = CreateBasicNotificationTemplate();

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
            var template = CreateBasicNotificationTemplate();

            var result = new Notifications().DeleteTemplate(template.resource.data._id);
            Assert.IsTrue(result.IsSuccess);
        }

        private NotificationTriggerResponse CreateBasicNotificationTrigger(string templateId)
        {
            var template = new NotificationTriggerRequest
            {
                type = NotificationTriggerType.email,
                destination = "email@email.com",
                template_id = templateId,
                eventTrigger = NotificationEvent.card_expiration_warning
            };
            var result = new Notifications().AddTrigger(template);
            return result;
        }

        [Test]
        public void AddTrigger()
        {
            var template = CreateBasicNotificationTemplate();
            var result = CreateBasicNotificationTrigger(template.resource.data._id);
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public void GetTriggers()
        {
            var template = CreateBasicNotificationTemplate();
            var trigger = CreateBasicNotificationTrigger(template.resource.data._id);
            var result = new Notifications().GetTriggers();
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public void GetSingleTrigger()
        {
            var template = CreateBasicNotificationTemplate();
            var trigger = CreateBasicNotificationTrigger(template.resource.data._id);
            var result = new Notifications().GetTrigger(trigger.resource.data._id);
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public void DeleteTrigger()
        {
            var template = CreateBasicNotificationTemplate();
            var trigger = CreateBasicNotificationTrigger(template.resource.data._id);
            var result = new Notifications().DeleteTrigger(trigger.resource.data._id);
            Assert.IsTrue(result.IsSuccess);
        }
    }
}
