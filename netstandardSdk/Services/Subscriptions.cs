using Newtonsoft.Json;
using Paydock_dotnet_sdk.Models;
using Paydock_dotnet_sdk.Tools;
using System;
using System.Threading.Tasks;

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
        public async Task<SubscriptionResponse> Add(SubscriptionRequest request)
		{
			return await _serviceHelper.Post<SubscriptionResponse, SubscriptionRequest>(request, "subscriptions", overrideConfigSecretKey: _overrideConfigSecretKey);
        }

        /// <summary>
        /// Updates a subscription
        /// </summary>
        /// <param name="request">Subscription data</param>
        /// <returns>updated subscription</returns>
        [RequiresConfig]
        public async Task<SubscriptionResponse> Update(SubscriptionUpdateRequest request)
		{
			var subscriptionId = Uri.EscapeUriString(request._id);
			return await _serviceHelper.Post<SubscriptionResponse, SubscriptionUpdateRequest>(request, "subscriptions/" + subscriptionId, overrideConfigSecretKey: _overrideConfigSecretKey);
        }

        /// <summary>
        /// Retrieve all subscriptions, limited to 1000
        /// </summary>
        /// <returns>Subscription items</returns>
        [RequiresConfig]
        public async Task<SubscriptionItemsResponse> Get()
		{
			return await _serviceHelper.Get<SubscriptionItemsResponse>("subscriptions", overrideConfigSecretKey: _overrideConfigSecretKey);
        }

        /// <summary>
        /// Retrieve filtered list of subscriptions, limited to 1000
        /// </summary>
        /// <param name="request">search paramters for the subscriptions</param>
        /// <returns>list of subscriptions</returns>
        [RequiresConfig]
        public async Task<SubscriptionItemsResponse> Get(SubscriptionSearchRequest request)
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

			return await _serviceHelper.Get<SubscriptionItemsResponse>(url, overrideConfigSecretKey: _overrideConfigSecretKey);
        }

        /// <summary>
        /// Retrieve a single subscription
        /// </summary>
        /// <param name="request">id of subscription to return</param>
        /// <returns>subscription information</returns>
        [RequiresConfig]
        public async Task<SubscriptionItemResponse> Get(string subscriptionId)
		{
			subscriptionId = Uri.EscapeUriString(subscriptionId);
			return await _serviceHelper.Get<SubscriptionItemResponse>("subscriptions/" + subscriptionId, overrideConfigSecretKey: _overrideConfigSecretKey);
        }

        /// <summary>
        /// Delete a subscription
        /// </summary>
        /// <param name="subscriptionId">id of the subscription</param>
        /// <returns>information on the subscription</returns>
        public async Task<SubscriptionItemResponse> Delete(string subscriptionId)
		{
			subscriptionId = Uri.EscapeUriString(subscriptionId);
			return await _serviceHelper.Delete<SubscriptionItemResponse>("subscriptions/" + subscriptionId, overrideConfigSecretKey: _overrideConfigSecretKey);
        }
    }
}
