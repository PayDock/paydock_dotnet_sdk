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

		private async Task<NotificationTemplateResponse> CreateBasicNotificationTemplate(string overideSecretKey = null)
		{
			var template = RequestFactory.CreateNotificationTemplateRequest();
			if (overideSecretKey != null)
				return await new Notifications(overideSecretKey).AddTemplate(template);
			else
				return await new Notifications().AddTemplate(template);
		}

		private async Task<NotificationTriggerResponse> CreateBasicNotificationTrigger(string templateId, string overideSecretKey = null)
		{
			var template = RequestFactory.CreateNotificationTriggerRequest(templateId);
			if (overideSecretKey != null)
				return await new Notifications(overideSecretKey).AddTrigger(template);
			else
				return await new Notifications().AddTrigger(template);
		}

		[TestCase(TestConfig.OverideSecretKey)]
		//[TestCase(null)]
		public async Task AddTemplate(string overideSecretKey)
		{
			var result = await CreateBasicNotificationTemplate(overideSecretKey);
			Assert.IsTrue(result.IsSuccess);
		}

		[TestCase(TestConfig.OverideSecretKey)]
		[TestCase(null)]
		public async Task GetTemplates(string overideSecretKey)
		{
			NotificationTemplateItemsResponse result;
			if (overideSecretKey != null)
				result = await new Notifications(overideSecretKey).GetTemplates();
			else
				result = await new Notifications().GetTemplates();
			Assert.IsTrue(result.IsSuccess);
		}

		[TestCase(TestConfig.OverideSecretKey)]
		[TestCase(null)]
		public async Task GetTemplate(string overideSecretKey)
		{
			var template = await CreateBasicNotificationTemplate(overideSecretKey);

			NotificationTemplateResponse result;
			if (overideSecretKey != null)
				result = await new Notifications(overideSecretKey).GetTemplate(template.resource.data._id);
			else
				result = await new Notifications().GetTemplate(template.resource.data._id);
			Assert.IsTrue(result.IsSuccess);
		}

		[TestCase(TestConfig.OverideSecretKey)]
		[TestCase(null)]
		public async Task UpdateTemplate(string overideSecretKey)
		{
			var template = await CreateBasicNotificationTemplate(overideSecretKey);

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
				result = await new Notifications(overideSecretKey).UpdateTemplate(updateTemplate);
			else
				result = await new Notifications().UpdateTemplate(updateTemplate);
			Assert.AreEqual(updateTemplate.body, result.resource.data.body);
			Assert.IsTrue(result.IsSuccess);
		}

		[TestCase(TestConfig.OverideSecretKey)]
		[TestCase(null)]
		public async Task DeleteTemplate(string overideSecretKey)
		{
			var template = await CreateBasicNotificationTemplate(overideSecretKey);

			NotificationTemplateResponse result;
			if (overideSecretKey != null)
				result = await new Notifications(overideSecretKey).DeleteTemplate(template.resource.data._id);
			else
				result = await new Notifications().DeleteTemplate(template.resource.data._id);
			Assert.IsTrue(result.IsSuccess);
		}

		[TestCase(TestConfig.OverideSecretKey)]
		[TestCase(null)]
		public async Task AddTrigger(string overideSecretKey)
		{
			var template = await CreateBasicNotificationTemplate(overideSecretKey);
			var result = await CreateBasicNotificationTrigger(template.resource.data._id, overideSecretKey);
			Assert.IsTrue(result.IsSuccess);
		}

		[TestCase(TestConfig.OverideSecretKey)]
		[TestCase(null)]
		public async Task GetTriggers(string overideSecretKey)
		{
			var template = await CreateBasicNotificationTemplate(overideSecretKey);
			var trigger = await CreateBasicNotificationTrigger(template.resource.data._id, overideSecretKey);
			NotificationTriggerItemsResponse result;
			if (overideSecretKey != null)
				result = await new Notifications(overideSecretKey).GetTriggers();
			else
				result = await new Notifications().GetTriggers();
			Assert.IsTrue(result.IsSuccess);
		}

		[TestCase(TestConfig.OverideSecretKey)]
		[TestCase(null)]
		public async Task GetSingleTrigger(string overideSecretKey)
		{
			var template = await CreateBasicNotificationTemplate(overideSecretKey);
			var trigger = await CreateBasicNotificationTrigger(template.resource.data._id, overideSecretKey);
			NotificationTriggerResponse result;
			if (overideSecretKey != null)
				result = await new Notifications(overideSecretKey).GetTrigger(trigger.resource.data._id);
			else
				result = await new Notifications().GetTrigger(trigger.resource.data._id);
			Assert.IsTrue(result.IsSuccess);
		}

		[TestCase(TestConfig.OverideSecretKey)]
		[TestCase(null)]
		public async Task DeleteTrigger(string overideSecretKey)
		{
			var template = await CreateBasicNotificationTemplate(overideSecretKey);
			var trigger = await CreateBasicNotificationTrigger(template.resource.data._id, overideSecretKey);
			NotificationTriggerResponse result;
			if (overideSecretKey != null)
				result = await new Notifications(overideSecretKey).DeleteTrigger(trigger.resource.data._id);
			else
				result = await new Notifications().DeleteTrigger(trigger.resource.data._id);
			Assert.IsTrue(result.IsSuccess);
		}

		[TestCase(TestConfig.OverideSecretKey)]
		[TestCase(null)]
		public async Task GetLogs(string overideSecretKey)
		{
			var result = await new Notifications(overideSecretKey).GetLogs(new NotificationLogRequest());
			Assert.IsTrue(result.IsSuccess);
		}

		[TestCase(TestConfig.OverideSecretKey)]
		[TestCase(null)]
		public async Task DeleteLog(string overideSecretKey)
		{
			var logs = await new Notifications(overideSecretKey).GetLogs(new NotificationLogRequest());
			var result = await new Notifications(overideSecretKey).DeleteLog(logs.resource.data.First()._id);
			Assert.IsTrue(result.IsSuccess);
		}

		[TestCase(TestConfig.OverideSecretKey)]
		[TestCase(null)]
		public async Task ResendNotification(string overideSecretKey)
		{
			var logs = await new Notifications(overideSecretKey).GetLogs(new NotificationLogRequest());
			var result = await new Notifications(overideSecretKey).ResendNotification(logs.resource.data.First()._id);
			Assert.IsTrue(result.IsSuccess);
		}
	}
}
