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
        public NotificationResponse AddTemplate(NotificationRequest request)
        {
            var requestData = JsonConvert.SerializeObject(request, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var responseJson = _serviceHelper.CallPaydock("notifications/templates", HttpMethod.POST, requestData);

            var response = (NotificationResponse)JsonConvert.DeserializeObject(responseJson, typeof(NotificationResponse));
            response.JsonResponse = responseJson;
            return response;

        }
    }
}
