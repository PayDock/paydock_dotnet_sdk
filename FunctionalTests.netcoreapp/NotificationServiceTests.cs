using NUnit.Framework;
using Paydock_dotnet_sdk.Services;
using Paydock_dotnet_sdk.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

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

        private Notifications CreateSvc(bool useOverrideKey = false)
        {
            return useOverrideKey ? new Notifications(TestConfig.OverideSecretKey) : new Notifications();
        }

        private async Task<NotificationTemplateResponse> CreateBasicNotificationTemplate(bool useOverrideKey = false)
        {
            var template = RequestFactory.CreateNotificationTemplateRequest();
            return await CreateSvc(useOverrideKey).AddTemplate(template);
        }

        private async Task<NotificationTriggerResponse> CreateBasicNotificationTrigger(string templateId, bool useOverrideKey = false)
        {
            var template = RequestFactory.CreateNotificationTriggerRequest(templateId);
            return await CreateSvc(useOverrideKey).AddTrigger(template);
        }

        [Test]
        public async Task AddTemplate()
        {
            Assume.That(TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            var result = await CreateBasicNotificationTemplate(true);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task GetTemplates(bool useOverrideKey)
        {
            Assume.That(!useOverrideKey || TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            var result = await CreateSvc(useOverrideKey).GetTemplates();
            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task GetTemplate(bool useOverrideKey)
        {
            Assume.That(!useOverrideKey || TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            var template = await CreateBasicNotificationTemplate(useOverrideKey);
            var result = await CreateSvc(useOverrideKey).GetTemplate(template.resource.data._id);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task UpdateTemplate(bool useOverrideKey)
        {
            Assume.That(!useOverrideKey || TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            var template = await CreateBasicNotificationTemplate(useOverrideKey);
            var updateTemplate = new NotificationTemplateUpdateRequest
            {
                _id = template.resource.data._id,
                body = "body1",
                label = "test",
                notification_event = NotificationEvent.card_expiration_warning,
                html = true
            };
            var result = await CreateSvc(useOverrideKey).UpdateTemplate(updateTemplate);
            Assert.AreEqual(updateTemplate.body, result.resource.data.body);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task DeleteTemplate(bool useOverrideKey)
        {
            Assume.That(!useOverrideKey || TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            var template = await CreateBasicNotificationTemplate(useOverrideKey);
            var result = await CreateSvc(useOverrideKey).DeleteTemplate(template.resource.data._id);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task AddTrigger(bool useOverrideKey)
        {
            Assume.That(!useOverrideKey || TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            var template = await CreateBasicNotificationTemplate(useOverrideKey);
            var result = await CreateBasicNotificationTrigger(template.resource.data._id, useOverrideKey);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task GetTriggers(bool useOverrideKey)
        {
            Assume.That(!useOverrideKey || TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            var template = await CreateBasicNotificationTemplate(useOverrideKey);
            await CreateBasicNotificationTrigger(template.resource.data._id, useOverrideKey);
            var result = await CreateSvc(useOverrideKey).GetTriggers();
            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task GetSingleTrigger(bool useOverrideKey)
        {
            Assume.That(!useOverrideKey || TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            var template = await CreateBasicNotificationTemplate(useOverrideKey);
            var trigger = await CreateBasicNotificationTrigger(template.resource.data._id, useOverrideKey);
            var result = await CreateSvc(useOverrideKey).GetTrigger(trigger.resource.data._id);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task DeleteTrigger(bool useOverrideKey)
        {
            Assume.That(!useOverrideKey || TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            var template = await CreateBasicNotificationTemplate(useOverrideKey);
            var trigger = await CreateBasicNotificationTrigger(template.resource.data._id, useOverrideKey);
            var result = await CreateSvc(useOverrideKey).DeleteTrigger(trigger.resource.data._id);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task GetLogs(bool useOverrideKey)
        {
            Assume.That(!useOverrideKey || TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            var result = await CreateSvc(useOverrideKey).GetLogs(new NotificationLogRequest());
            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task DeleteLog(bool useOverrideKey)
        {
            Assume.That(!useOverrideKey || TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            var logs = await CreateSvc(useOverrideKey).GetLogs(new NotificationLogRequest());
            var result = await CreateSvc(useOverrideKey).DeleteLog(logs.resource.data.First()._id);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task ResendNotification(bool useOverrideKey)
        {
            Assume.That(!useOverrideKey || TestConfig.OverideSecretKey != null, "PAYDOCK_OVERRIDE_SECRET_KEY not configured");
            var logs = await CreateSvc(useOverrideKey).GetLogs(new NotificationLogRequest());
            var result = await CreateSvc(useOverrideKey).ResendNotification(logs.resource.data.First()._id);
            Assert.IsTrue(result.IsSuccess);
        }
    }
}
