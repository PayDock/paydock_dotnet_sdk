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
    }
}
