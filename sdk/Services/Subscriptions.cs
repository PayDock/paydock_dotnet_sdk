using Newtonsoft.Json;
using Paydock_dotnet_sdk.Models;
using Paydock_dotnet_sdk.Tools;
using System;

namespace Paydock_dotnet_sdk.Services
{
    public class Subscriptions : ISubscriptions
    {
        protected IServiceHelper _serviceHelper;
        protected string _overrideConfigSecretKey = null;

        /// <summary>
        /// Service locator style constructor
        /// </summary>
        public Subscriptions(string overrideConfigSecretKey = null)
        {
            _serviceHelper = new ServiceHelper();
            _overrideConfigSecretKey = overrideConfigSecretKey;
        }

        /// <summary>
        /// Dependency injection constructor to enable testing
        /// <param name="serviceHelper">Service helper class to perform HTTP requests</param>
        /// <param name="overrideConfigSecretKey">Use a custom secret key rather than the value in shared config, defaults to null</param>
        /// </summary>
        public Subscriptions(IServiceHelper serviceHelper, string overrideConfigSecretKey = null)
        {
            _serviceHelper = serviceHelper;
            _overrideConfigSecretKey = overrideConfigSecretKey;
        }

        /// <summary>
        /// Creates a subscription
        /// </summary>
        /// <param name="request">Subscription data</param>
        /// <returns>created subscription</returns>
        [RequiresConfig]
        public SubscriptionResponse Add(SubscriptionRequest request)
        {
            var requestData = SerializeHelper.Serialize(request);
            var responseJson = _serviceHelper.CallPaydock("subscriptions", HttpMethod.POST, requestData, overrideConfigSecretKey: _overrideConfigSecretKey);

			var response = SerializeHelper.Deserialize<SubscriptionResponse>(responseJson);
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
			var subscriptionId = Uri.EscapeUriString(request._id);
			var requestData = SerializeHelper.Serialize(request);
            var responseJson = _serviceHelper.CallPaydock("subscriptions/" + subscriptionId, HttpMethod.POST, requestData, overrideConfigSecretKey: _overrideConfigSecretKey);

			var response = SerializeHelper.Deserialize<SubscriptionResponse>(responseJson);
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
            var responseJson = _serviceHelper.CallPaydock("subscriptions", HttpMethod.GET, "", overrideConfigSecretKey: _overrideConfigSecretKey);

			var response = SerializeHelper.Deserialize<SubscriptionItemsResponse>(responseJson);
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

            var responseJson = _serviceHelper.CallPaydock(url, HttpMethod.GET, "", overrideConfigSecretKey: _overrideConfigSecretKey);

			var response = SerializeHelper.Deserialize<SubscriptionItemsResponse>(responseJson);
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
			subscriptionId = Uri.EscapeUriString(subscriptionId);
			var responseJson = _serviceHelper.CallPaydock("subscriptions/" + subscriptionId, HttpMethod.GET, "", overrideConfigSecretKey: _overrideConfigSecretKey);

			var response = SerializeHelper.Deserialize<SubscriptionItemResponse>(responseJson);
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
			subscriptionId = Uri.EscapeUriString(subscriptionId);
			var responseJson = _serviceHelper.CallPaydock("subscriptions/" + subscriptionId, HttpMethod.DELETE, "", overrideConfigSecretKey: _overrideConfigSecretKey);

			var response = SerializeHelper.Deserialize<SubscriptionItemResponse>(responseJson);
            response.JsonResponse = responseJson;
            return response;
        }
    }
}
