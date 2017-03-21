using Newtonsoft.Json;
using Paydock_dotnet_sdk.Models;
using Paydock_dotnet_sdk.Tools;

namespace Paydock_dotnet_sdk.Services
{
    public class Subscriptions : ISubscriptions
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
        [RequiresConfig]
        public SubscriptionResponse Add(SubscriptionRequest request)
        {
            var requestData = JsonConvert.SerializeObject(request, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var responseJson = _serviceHelper.CallPaydock("subscriptions", HttpMethod.POST, requestData);

            var response = (SubscriptionResponse)JsonConvert.DeserializeObject(responseJson, typeof(SubscriptionResponse));
            response.JsonResponse = responseJson;
            return response;
        }

        /// <summary>
        /// Updates a subscription
        /// </summary>
        /// <param name="request">Subscription data</param>
        /// <returns>updated subscription</returns>
        [RequiresConfig]
        public SubscriptionResponse Update(SubscriptionUpdateRequest request)
        {
            var requestData = JsonConvert.SerializeObject(request, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var responseJson = _serviceHelper.CallPaydock("subscriptions/" + request._id, HttpMethod.POST, requestData);

            var response = (SubscriptionResponse)JsonConvert.DeserializeObject(responseJson, typeof(SubscriptionResponse));
            response.JsonResponse = responseJson;
            return response;
        }

        /// <summary>
        /// Retrieve all subscriptions, limited to 1000
        /// </summary>
        /// <returns>Subscription items</returns>
        [RequiresConfig]
        public SubscriptionItemsResponse Get()
        {
            var responseJson = _serviceHelper.CallPaydock("subscriptions", HttpMethod.GET, "");

            var response = (SubscriptionItemsResponse)JsonConvert.DeserializeObject(responseJson, typeof(SubscriptionItemsResponse));
            response.JsonResponse = responseJson;
            return response;
        }

        /// <summary>
        /// Retrieve filtered list of subscriptions, limited to 1000
        /// </summary>
        /// <param name="request">search paramters for the subscriptions</param>
        /// <returns>list of subscriptions</returns>
        [RequiresConfig]
        public SubscriptionItemsResponse Get(SubscriptionSearchRequest request)
        {
            var url = "subscriptions/";
            url = url.AppendParameter("skip", request.skip);
            url = url.AppendParameter("limit", request.limit);
            url = url.AppendParameter("search", request.search);
            url = url.AppendParameter("sortkey", request.sortkey);
            url = url.AppendParameter("sortdirection", request.sortdirection);
            url = url.AppendParameter("customer_id", request.customer_id);
            url = url.AppendParameter("gateway_id", request.gateway_id);
            url = url.AppendParameter("status", request.status);

            var responseJson = _serviceHelper.CallPaydock(url, HttpMethod.GET, "");

            var response = (SubscriptionItemsResponse)JsonConvert.DeserializeObject(responseJson, typeof(SubscriptionItemsResponse));
            response.JsonResponse = responseJson;
            return response;
        }

        /// <summary>
        /// Retrieve a single subscription
        /// </summary>
        /// <param name="request">id of subscription to return</param>
        /// <returns>subscription information</returns>
        [RequiresConfig]
        public SubscriptionItemResponse Get(string subscriptionId)
        {
            var responseJson = _serviceHelper.CallPaydock("subscriptions/" + subscriptionId, HttpMethod.GET, "");

            var response = (SubscriptionItemResponse)JsonConvert.DeserializeObject(responseJson, typeof(SubscriptionItemResponse));
            response.JsonResponse = responseJson;
            return response;
        }

        /// <summary>
        /// Delete a subscription
        /// </summary>
        /// <param name="subscriptionId">id of the subscription</param>
        /// <returns>information on the subscription</returns>
        public SubscriptionItemResponse Delete(string subscriptionId)
        {
            var responseJson = _serviceHelper.CallPaydock("subscriptions/" + subscriptionId, HttpMethod.DELETE, "");

            var response = (SubscriptionItemResponse)JsonConvert.DeserializeObject(responseJson, typeof(SubscriptionItemResponse));
            response.JsonResponse = responseJson;
            return response;
        }
    }
}
