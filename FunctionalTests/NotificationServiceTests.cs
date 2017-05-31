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

        private NotificationTemplateResponse CreateBasicNotificationTemplate(string overideSecretKey = null)
        {
            var template = new NotificationTemplateRequest
            {
                body = "body",
                label = "test",
                notification_event = NotificationEvent.card_expiration_warning,
                html = true
            };
            if (overideSecretKey != null)
                return new Notifications(overideSecretKey).AddTemplate(template);
            else
                return new Notifications().AddTemplate(template);
        }

        private NotificationTriggerResponse CreateBasicNotificationTrigger(string templateId, string overideSecretKey = null)
        {
            var template = new NotificationTriggerRequest
            {
                type = NotificationTriggerType.email,
                destination = "email@email.com",
                template_id = templateId,
                eventTrigger = NotificationEvent.card_expiration_warning
            };
            if (overideSecretKey != null)
                return new Notifications(overideSecretKey).AddTrigger(template);
            else
                return new Notifications().AddTrigger(template);
        }

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public void AddTemplate(string overideSecretKey)
        {
            var result = CreateBasicNotificationTemplate(overideSecretKey);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public void UpdateTemplate(string overideSecretKey)
        {
            var template = CreateBasicNotificationTemplate(overideSecretKey);

            var updateTemplate = new NotificationTemplateUpdateRequest
            {
                _id = template.resource.data._id,
                body = "body1",
                label = "test",
                notification_event = NotificationEvent.card_expiration_warning,
                html = true
            };

            NotificationTemplateResponse result;
            if (overideSecretKey != null)
                result = new Notifications(overideSecretKey).UpdateTemplate(updateTemplate);
            else
                result = new Notifications().UpdateTemplate(updateTemplate);
            Assert.AreEqual(updateTemplate.body, result.resource.data.body);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public void DeleteTemplate(string overideSecretKey)
        {
            var template = CreateBasicNotificationTemplate(overideSecretKey);

            NotificationTemplateResponse result;
            if (overideSecretKey != null)
                result = new Notifications(overideSecretKey).DeleteTemplate(template.resource.data._id);
            else
                result = new Notifications().DeleteTemplate(template.resource.data._id);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public void AddTrigger(string overideSecretKey)
        {
            var template = CreateBasicNotificationTemplate(overideSecretKey);
            var result = CreateBasicNotificationTrigger(template.resource.data._id, overideSecretKey);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public void GetTriggers(string overideSecretKey)
        {
            var template = CreateBasicNotificationTemplate(overideSecretKey);
            var trigger = CreateBasicNotificationTrigger(template.resource.data._id, overideSecretKey);
            NotificationTriggerItemsResponse result;
            if (overideSecretKey != null)
                result = new Notifications(overideSecretKey).GetTriggers();
            else
                result = new Notifications().GetTriggers();
            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public void GetSingleTrigger(string overideSecretKey)
        {
            var template = CreateBasicNotificationTemplate(overideSecretKey);
            var trigger = CreateBasicNotificationTrigger(template.resource.data._id, overideSecretKey);
            NotificationTriggerResponse result;
            if (overideSecretKey != null)
                result = new Notifications(overideSecretKey).GetTrigger(trigger.resource.data._id);
            else
                result = new Notifications().GetTrigger(trigger.resource.data._id);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public void DeleteTrigger(string overideSecretKey)
        {
            var template = CreateBasicNotificationTemplate(overideSecretKey);
            var trigger = CreateBasicNotificationTrigger(template.resource.data._id, overideSecretKey);
            NotificationTriggerResponse result;
            if (overideSecretKey != null)
                result = new Notifications(overideSecretKey).DeleteTrigger(trigger.resource.data._id);
            else
                result = new Notifications().DeleteTrigger(trigger.resource.data._id);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public void GetLogs(string overideSecretKey)
        {
            var result = new Notifications(overideSecretKey).GetLogs(new NotificationLogRequest());
            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(TestConfig.OverideSecretKey)]
        [TestCase(null)]
        public void DeleteLog(string overideSecretKey)
        {
            var logs = new Notifications(overideSecretKey).GetLogs(new NotificationLogRequest());
            var result = new Notifications(overideSecretKey).DeleteLog(logs.resource.data.First()._id);
            Assert.IsTrue(result.IsSuccess);
        }
    }
}
