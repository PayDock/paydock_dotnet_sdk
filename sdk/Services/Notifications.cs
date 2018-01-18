using Newtonsoft.Json;
using Paydock_dotnet_sdk.Models;
using Paydock_dotnet_sdk.Tools;
using System;

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
        public NotificationTemplateResponse AddTemplate(NotificationTemplateRequest request)
        {
            var requestData = SerializeHelper.Serialize(request);
            var responseJson = _serviceHelper.CallPaydock("notifications/templates", HttpMethod.POST, requestData, overrideConfigSecretKey: _overrideConfigSecretKey);

			var response = SerializeHelper.Deserialize<NotificationTemplateResponse>(responseJson);
            response.JsonResponse = responseJson;
            return response;
        }

        /// <summary>
        /// Update notification template
        /// </summary>
        /// <param name="request">Notification template data</param>
        /// <returns>Updated notification</returns>
        [RequiresConfig]
        public NotificationTemplateResponse UpdateTemplate(NotificationTemplateUpdateRequest request)
		{
			var templateId = Uri.EscapeUriString(request._id);
			var requestData = SerializeHelper.Serialize(request);
            var responseJson = _serviceHelper.CallPaydock("notifications/templates/" + templateId, HttpMethod.POST, requestData, overrideConfigSecretKey: _overrideConfigSecretKey);

			var response = SerializeHelper.Deserialize<NotificationTemplateResponse>(responseJson);
            response.JsonResponse = responseJson;
            return response;

		}

		/// <summary>
		/// returns all notification templates, limited to 1000
		/// </summary>
		/// <returns>notification triggers</returns>
		[RequiresConfig]
		public NotificationTemplateItemsResponse GetTemplates()
		{
			var responseJson = _serviceHelper.CallPaydock("notifications/templates", HttpMethod.GET, "", overrideConfigSecretKey: _overrideConfigSecretKey);

			var response = SerializeHelper.Deserialize<NotificationTemplateItemsResponse>(responseJson);
			response.JsonResponse = responseJson;
			return response;
		}

		/// <summary>
		/// returns a single notification trigger
		/// </summary>
		/// <param name="notificationTriggerId">id for the trigger</param>
		/// <returns>notification trigger</returns>
		[RequiresConfig]
		public NotificationTemplateResponse GetTemplate(string notificationTemplateId)
		{
			notificationTemplateId = Uri.EscapeUriString(notificationTemplateId);
			var responseJson = _serviceHelper.CallPaydock("notifications/templates/" + notificationTemplateId, HttpMethod.GET, "", overrideConfigSecretKey: _overrideConfigSecretKey);

			var response = SerializeHelper.Deserialize<NotificationTemplateResponse>(responseJson);
			response.JsonResponse = responseJson;
			return response;
		}

		/// <summary>
		/// Delete a notification template
		/// </summary>
		/// <param name="subscriptionId">id of the notification template</param>
		/// <returns>information on the notification template</returns>
		[RequiresConfig]
        public NotificationTemplateResponse DeleteTemplate(string notificationTemplateId)
		{
			notificationTemplateId = Uri.EscapeUriString(notificationTemplateId);
			var responseJson = _serviceHelper.CallPaydock("notifications/templates/" + notificationTemplateId, HttpMethod.DELETE, "", overrideConfigSecretKey: _overrideConfigSecretKey);

			var response = SerializeHelper.Deserialize<NotificationTemplateResponse>(responseJson);
            response.JsonResponse = responseJson;
            return response;
        }

        /// <summary>
        /// Creates a notification trigger
        /// </summary>
        /// <param name="request">data to create the trigger</param>
        /// <returns>the created notification trigger</returns>
        [RequiresConfig]
        public NotificationTriggerResponse AddTrigger(NotificationTriggerRequest request)
        {
            var requestData = SerializeHelper.Serialize(request);
            var responseJson = _serviceHelper.CallPaydock("notifications", HttpMethod.POST, requestData, overrideConfigSecretKey: _overrideConfigSecretKey);

			var response = SerializeHelper.Deserialize<NotificationTriggerResponse>(responseJson);
            response.JsonResponse = responseJson;
            return response;
        }

        /// <summary>
        /// returns all notification triggers, limited to 1000
        /// </summary>
        /// <returns>notification triggers</returns>
        [RequiresConfig]
        public NotificationTriggerItemsResponse GetTriggers()
        {
            var responseJson = _serviceHelper.CallPaydock("notifications", HttpMethod.GET, "", overrideConfigSecretKey: _overrideConfigSecretKey);

			var response = SerializeHelper.Deserialize<NotificationTriggerItemsResponse>(responseJson);
            response.JsonResponse = responseJson;
            return response;
        }

        /// <summary>
        /// returns a single notification trigger
        /// </summary>
        /// <param name="notificationTriggerId">id for the trigger</param>
        /// <returns>notification trigger</returns>
        [RequiresConfig]
        public NotificationTriggerResponse GetTrigger(string notificationTriggerId)
		{
			notificationTriggerId = Uri.EscapeUriString(notificationTriggerId);
			var responseJson = _serviceHelper.CallPaydock("notifications/" + notificationTriggerId, HttpMethod.GET, "", overrideConfigSecretKey: _overrideConfigSecretKey);

			var response = SerializeHelper.Deserialize<NotificationTriggerResponse>(responseJson);
            response.JsonResponse = responseJson;
            return response;
        }

        /// <summary>
        /// deletes a notification trigger
        /// </summary>
        /// <param name="notificationTriggerId">id for the trigger</param>
        /// <returns>notification trigger</returns>
        [RequiresConfig]
        public NotificationTriggerResponse DeleteTrigger(string notificationTriggerId)
		{
			notificationTriggerId = Uri.EscapeUriString(notificationTriggerId);
			var responseJson = _serviceHelper.CallPaydock("notifications/" + notificationTriggerId, HttpMethod.DELETE, "", overrideConfigSecretKey: _overrideConfigSecretKey);

			var response = SerializeHelper.Deserialize<NotificationTriggerResponse>(responseJson);
            response.JsonResponse = responseJson;
            return response;
        }

        /// <summary>
        /// gets notification logs
        /// </summary>
        /// <param name="request">data for the notification logs</param>
        /// <returns>notification logs</returns>
        [RequiresConfig]
        public NotificationLogsResponse GetLogs(NotificationLogRequest request)
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

            var responseJson = _serviceHelper.CallPaydock(url, HttpMethod.GET, "", overrideConfigSecretKey: _overrideConfigSecretKey);

			var response = SerializeHelper.Deserialize<NotificationLogsResponse>(responseJson);
            response.JsonResponse = responseJson;
            return response;
        }

		/// <summary>
		/// deletes a notification log
		/// </summary>
		/// <param name="notificationLogId">id for the notification log</param>
		/// <returns>notification log</returns>
		[RequiresConfig]
        public NotificationLogResponse DeleteLog(string notificationLogId)
		{
			notificationLogId = Uri.EscapeUriString(notificationLogId);
			var responseJson = _serviceHelper.CallPaydock("notifications/logs/" + notificationLogId, HttpMethod.DELETE, "", overrideConfigSecretKey: _overrideConfigSecretKey);

			var response = SerializeHelper.Deserialize<NotificationLogResponse>(responseJson);
            response.JsonResponse = responseJson;
            return response;
		}

		/// <summary>
		/// resends a notification log
		/// </summary>
		/// <param name="notificationLogId">id for the notification log</param>
		/// <returns>notification log</returns>
		[RequiresConfig]
		public NotificationLogResponse ResendNotification(string notificationLogId)
		{
			notificationLogId = Uri.EscapeUriString(notificationLogId);
			var responseJson = _serviceHelper.CallPaydock("notifications/logs/" + notificationLogId, HttpMethod.PUT, "", overrideConfigSecretKey: _overrideConfigSecretKey);

			var response = SerializeHelper.Deserialize<NotificationLogResponse>(responseJson);
			response.JsonResponse = responseJson;
			return response;
		}
	}
}
