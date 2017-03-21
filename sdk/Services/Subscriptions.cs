using Newtonsoft.Json;
using Paydock_dotnet_sdk.Models;
using Paydock_dotnet_sdk.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paydock_dotnet_sdk.Services
{
    public class Subscriptions
    {
        protected IServiceHelper _serviceHelper;

        /// <summary>
        /// Service locator style constructor
        /// </summary>
        public Subscriptions()
        {
            _serviceHelper = new ServiceHelper();
        }

        /// <summary>
        /// Dependency injection constructor to enable testing
        /// </summary>
        public Subscriptions(IServiceHelper serviceHelper)
        {
            _serviceHelper = serviceHelper;
        }

        /// <summary>
        /// Creates a subscription
        /// </summary>
        /// <param name="request">Subscription data</param>
        /// <returns>created subscription</returns>
        public SubscriptionResponse Add(SubscriptionRequest request)
        {
            var requestData = JsonConvert.SerializeObject(request, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var responseJson = _serviceHelper.CallPaydock("subscriptions", HttpMethod.POST, requestData);

            var response = (SubscriptionResponse)JsonConvert.DeserializeObject(responseJson, typeof(SubscriptionResponse));
            response.JsonResponse = responseJson;
            return response;

        }
    }
}
