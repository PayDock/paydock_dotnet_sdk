using Paydock_dotnet_sdk.Models;
using Paydock_dotnet_sdk.Tools;
using System;
using System.Threading.Tasks;

namespace Paydock_dotnet_sdk.Services
{
	public class Notifications : INotifications
	{
		protected IServiceHelper _serviceHelper;
		protected string _overrideConfigSecretKey = null;

		/// <summary>
		/// Service locator style constructor
		/// </summary>
		public Notifications(string overrideConfigSecretKey = null)
		{
			_serviceHelper = new ServiceHelper();
			_overrideConfigSecretKey = overrideConfigSecretKey;
		}

		/// <summary>
		/// Dependency injection constructor to enable testing
		/// <param name="serviceHelper">Service helper class to perform HTTP requests</param>
		/// <param name="overrideConfigSecretKey">Use a custom secret key rather than the value in shared config, defaults to null</param>
		/// </summary>
		public Notifications(IServiceHelper serviceHelper, string overrideConfigSecretKey = null)
		{
			_serviceHelper = serviceHelper;
			_overrideConfigSecretKey = overrideConfigSecretKey;
		}

		/// <summary>
		/// Add notification template
		/// </summary>
		/// <param name="request">Notification template data</param>
		/// <returns>Created notification</returns>
		[RequiresConfig]
		public async Task<NotificationTemplateResponse> AddTemplate(NotificationTemplateRequest request)
		{
			return await _serviceHelper.Post<NotificationTemplateResponse, NotificationTemplateRequest>(request, "notifications/templates", overrideConfigSecretKey: _overrideConfigSecretKey);
		}

		/// <summary>
		/// Update notification template
		/// </summary>
		/// <param name="request">Notification template data</param>
		/// <returns>Updated notification</returns>
		[RequiresConfig]
		public async Task<NotificationTemplateResponse> UpdateTemplate(NotificationTemplateUpdateRequest request)
		{
			var templateId = Uri.EscapeUriString(request._id);
			return await _serviceHelper.Post<NotificationTemplateResponse, NotificationTemplateUpdateRequest>(request, "notifications/templates/" + templateId, overrideConfigSecretKey: _overrideConfigSecretKey);
		}

		/// <summary>
		/// Delete a notification template
		/// </summary>
		/// <param name="subscriptionId">id of the notification template</param>
		/// <returns>information on the notification template</returns>
		[RequiresConfig]
		public async Task<NotificationTemplateResponse> DeleteTemplate(string notificationTemplateId)
		{
			notificationTemplateId = Uri.EscapeUriString(notificationTemplateId);
			return await _serviceHelper.Delete<NotificationTemplateResponse>("notifications/templates/" + notificationTemplateId, overrideConfigSecretKey: _overrideConfigSecretKey);
		}

		/// <summary>
		/// Delete a notification template
		/// </summary>
		/// <param name="subscriptionId">id of the notification template</param>
		/// <returns>information on the notification template</returns>
		[RequiresConfig]
		public async Task<NotificationTemplateResponse> GetTemplate(string notificationTemplateId)
		{
			notificationTemplateId = Uri.EscapeUriString(notificationTemplateId);
			return await _serviceHelper.Get<NotificationTemplateResponse>("notifications/templates/" + notificationTemplateId, overrideConfigSecretKey: _overrideConfigSecretKey);
		}

		/// <summary>
		/// Delete a notification template
		/// </summary>
		/// <param name="subscriptionId">id of the notification template</param>
		/// <returns>information on the notification template</returns>
		[RequiresConfig]
		public async Task<NotificationTemplateItemsResponse> GetTemplates()
		{
			return await _serviceHelper.Get<NotificationTemplateItemsResponse>("notifications/templates", overrideConfigSecretKey: _overrideConfigSecretKey);
		}

		/// <summary>
		/// Creates a notification trigger
		/// </summary>
		/// <param name="request">data to create the trigger</param>
		/// <returns>the created notification trigger</returns>
		[RequiresConfig]
		public async Task<NotificationTriggerResponse> AddTrigger(NotificationTriggerRequest request)
		{
			return await _serviceHelper.Post<NotificationTriggerResponse, NotificationTriggerRequest>(request, "notifications", overrideConfigSecretKey: _overrideConfigSecretKey);
		}

		/// <summary>
		/// returns all notification triggers, limited to 1000
		/// </summary>
		/// <returns>notification triggers</returns>
		[RequiresConfig]
		public async Task<NotificationTriggerItemsResponse> GetTriggers()
		{
			return await _serviceHelper.Get<NotificationTriggerItemsResponse>("notifications", overrideConfigSecretKey: _overrideConfigSecretKey);
		}

		/// <summary>
		/// returns a single notification trigger
		/// </summary>
		/// <param name="notificationTriggerId">id for the trigger</param>
		/// <returns>notification trigger</returns>
		[RequiresConfig]
		public async Task<NotificationTriggerResponse> GetTrigger(string notificationTriggerId)
		{
			notificationTriggerId = Uri.EscapeUriString(notificationTriggerId);
			return await _serviceHelper.Get<NotificationTriggerResponse>("notifications/" + notificationTriggerId, overrideConfigSecretKey: _overrideConfigSecretKey);
		}

		/// <summary>
		/// deletes a notification trigger
		/// </summary>
		/// <param name="notificationTriggerId">id for the trigger</param>
		/// <returns>notification trigger</returns>
		[RequiresConfig]
		public async Task<NotificationTriggerResponse> DeleteTrigger(string notificationTriggerId)
		{
			notificationTriggerId = Uri.EscapeUriString(notificationTriggerId);
			return await _serviceHelper.Delete<NotificationTriggerResponse>("notifications/" + notificationTriggerId, overrideConfigSecretKey: _overrideConfigSecretKey);
		}

		/// <summary>
		/// gets notification logs
		/// </summary>
		/// <param name="request">data for the trigger</param>
		/// <returns>notification logs</returns>
		[RequiresConfig]
		public async Task<NotificationLogsResponse> GetLogs(NotificationLogRequest request)
		{
			var url = "notifications/logs/";
			url = url.AppendParameter("_id", request._id);
			url = url.AppendParameter("success", request.success);
			url = url.AppendParameter("event", request.eventTrigger);
			url = url.AppendParameter("type", request.type);
			url = url.AppendParameter("created_at.from", request.created_at_from);
			url = url.AppendParameter("created_at.to", request.created_at_to);
			url = url.AppendParameter("parent_id", request.parent_id);
			url = url.AppendParameter("destination", request.destination);

			return await _serviceHelper.Get<NotificationLogsResponse>(url, overrideConfigSecretKey: _overrideConfigSecretKey);
		}

		/// <summary>
		/// deletes a notification trigger
		/// </summary>
		/// <param name="notificationTriggerId">id for the trigger</param>
		/// <returns>notification trigger</returns>
		[RequiresConfig]
		public async Task<NotificationTriggerResponse> DeleteLog(string notificationLogId)
		{
			notificationLogId = Uri.EscapeUriString(notificationLogId);
			return await _serviceHelper.Delete<NotificationTriggerResponse>("notifications/logs/" + notificationLogId, overrideConfigSecretKey: _overrideConfigSecretKey);
		}
	}
}
