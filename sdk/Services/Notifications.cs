using Newtonsoft.Json;
using Paydock_dotnet_sdk.Models;
using Paydock_dotnet_sdk.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paydock_dotnet_sdk.Services
{
    public class Notifications
    {
        protected IServiceHelper _serviceHelper;

        /// <summary>
        /// Service locator style constructor
        /// </summary>
        public Notifications()
        {
            _serviceHelper = new ServiceHelper();
        }

        /// <summary>
        /// Dependency injection constructor to enable testing
        /// </summary>
        public Notifications(IServiceHelper serviceHelper)
        {
            _serviceHelper = serviceHelper;
        }

        /// <summary>
        /// Add notification template
        /// </summary>
        /// <param name="request">Notification template data</param>
        /// <returns>Created notification</returns>
        [RequiresConfig]
        public NotificationTemplateResponse AddTemplate(NotificationTemplateRequest request)
        {
            var requestData = JsonConvert.SerializeObject(request, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var responseJson = _serviceHelper.CallPaydock("notifications/templates", HttpMethod.POST, requestData);

            var response = (NotificationTemplateResponse)JsonConvert.DeserializeObject(responseJson, typeof(NotificationTemplateResponse));
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
            var requestData = JsonConvert.SerializeObject(request, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var responseJson = _serviceHelper.CallPaydock("notifications/templates/" + request._id, HttpMethod.POST, requestData);

            var response = (NotificationTemplateResponse)JsonConvert.DeserializeObject(responseJson, typeof(NotificationTemplateResponse));
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
            var responseJson = _serviceHelper.CallPaydock("notifications/templates/" + notificationTemplateId, HttpMethod.POST, "");

            var response = (NotificationTemplateResponse)JsonConvert.DeserializeObject(responseJson, typeof(NotificationTemplateResponse));
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
            var requestData = JsonConvert.SerializeObject(request, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var responseJson = _serviceHelper.CallPaydock("notifications", HttpMethod.POST, requestData);

            var response = (NotificationTriggerResponse)JsonConvert.DeserializeObject(responseJson, typeof(NotificationTriggerResponse));
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
            var responseJson = _serviceHelper.CallPaydock("notifications", HttpMethod.GET, "");

            var response = (NotificationTriggerItemsResponse)JsonConvert.DeserializeObject(responseJson, typeof(NotificationTriggerItemsResponse));
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
            var responseJson = _serviceHelper.CallPaydock("notifications/" + notificationTriggerId, HttpMethod.GET, "");

            var response = (NotificationTriggerResponse)JsonConvert.DeserializeObject(responseJson, typeof(NotificationTriggerResponse));
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
            var responseJson = _serviceHelper.CallPaydock("notifications/" + notificationTriggerId, HttpMethod.DELETE, "");

            var response = (NotificationTriggerResponse)JsonConvert.DeserializeObject(responseJson, typeof(NotificationTriggerResponse));
            response.JsonResponse = responseJson;
            return response;
        }

        /// <summary>
        /// gets notification logs
        /// </summary>
        /// <param name="request">data for the trigger</param>
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

            var responseJson = _serviceHelper.CallPaydock(url, HttpMethod.GET, "");

            var response = (NotificationLogsResponse)JsonConvert.DeserializeObject(responseJson, typeof(NotificationLogsResponse));
            response.JsonResponse = responseJson;
            return response;
        }

        /// <summary>
        /// deletes a notification trigger
        /// </summary>
        /// <param name="notificationTriggerId">id for the trigger</param>
        /// <returns>notification trigger</returns>
        [RequiresConfig]
        public NotificationTriggerResponse DeleteLog(string notificationLogId)
        {
            var responseJson = _serviceHelper.CallPaydock("notifications/logs/" + notificationLogId, HttpMethod.DELETE, "");

            var response = (NotificationTriggerResponse)JsonConvert.DeserializeObject(responseJson, typeof(NotificationTriggerResponse));
            response.JsonResponse = responseJson;
            return response;
        }
    }
}
