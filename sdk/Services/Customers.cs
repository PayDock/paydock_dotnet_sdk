using Newtonsoft.Json;
using Paydock_dotnet_sdk.Models;
using Paydock_dotnet_sdk.Tools;
using System;

namespace Paydock_dotnet_sdk.Services
{
    public class Customers : ICustomers
    {
        protected IServiceHelper _serviceHelper;
        protected string _overrideConfigSecretKey = null;

        /// <summary>
        /// Service locator style constructor
        /// <param name="overrideConfigSecretKey">Use a custom secret key rather than the value in shared config, defaults to null</param>
        /// </summary>
        public Customers(string overrideConfigSecretKey = null)
        {
            _serviceHelper = new ServiceHelper();
            _overrideConfigSecretKey = overrideConfigSecretKey;
        }

        /// <summary>
        /// Dependency injection constructor to enable testing
        /// <param name="serviceHelper">Service helper class to perform HTTP requests</param>
        /// <param name="overrideConfigSecretKey">Use a custom secret key rather than the value in shared config, defaults to null</param>
        /// </summary>
        public Customers(IServiceHelper serviceHelper, string overrideConfigSecretKey = null)
        {
            _serviceHelper = serviceHelper;
            _overrideConfigSecretKey = overrideConfigSecretKey;
        }

        /// <summary>
        /// Adds a customer to Paydok
        /// </summary>
        /// <param name="request">Stores the customer information to add</param>
        /// <returns>details of the created customer</returns>
        [RequiresConfig]
        public CustomerResponse Add(CustomerRequest request)
        {
            var requestData = SerializeHelper.Serialize(request);
            var responseJson = _serviceHelper.CallPaydock("customers", HttpMethod.POST, requestData, overrideConfigSecretKey: _overrideConfigSecretKey);

			var response = SerializeHelper.Deserialize<CustomerResponse>(responseJson);
            response.JsonResponse = responseJson;
            return response;
        }

        /// <summary>
        /// Retrieve full list of customers, limited to 1000
        /// </summary>
        /// <returns>list of customers</returns>
        [RequiresConfig]
        public CustomerItemsResponse Get()
        {
            var responseJson = _serviceHelper.CallPaydock("customers", HttpMethod.GET, "", overrideConfigSecretKey: _overrideConfigSecretKey);

			var response = SerializeHelper.Deserialize<CustomerItemsResponse>(responseJson);
            response.JsonResponse = responseJson;
            return response;
        }

        /// <summary>
        /// Retrieve filtered list of customers, limited to 1000
        /// </summary>
        /// <param name="request">search paramters for the customers</param>
        /// <returns>list of customers</returns>
        [RequiresConfig]
        public CustomerItemsResponse Get(CustomerSearchRequest request)
        {
            var url = "customers/";
			url = url.AppendParameter("id", request.id);
			url = url.AppendParameter("skip", request.skip);
            url = url.AppendParameter("limit", request.limit);
            url = url.AppendParameter("search", request.search);
            url = url.AppendParameter("sortkey", request.sortkey);
            url = url.AppendParameter("sortdirection", request.sortdirection);
            url = url.AppendParameter("gateway_id", request.gateway_id);
            url = url.AppendParameter("archived", request.archived);
			url = url.AppendParameter("reference", request.reference);
			url = url.AppendParameter("payment_source_id", request.payment_source_id);

			var responseJson = _serviceHelper.CallPaydock(url, HttpMethod.GET, "", overrideConfigSecretKey: _overrideConfigSecretKey);

			var response = SerializeHelper.Deserialize<CustomerItemsResponse>(responseJson);
            response.JsonResponse = responseJson;
            return response;
        }

        /// <summary>
        /// Retrieve a single customer
        /// </summary>
        /// <param name="request">id of customer to return</param>
        /// <returns>customer information</returns>
        [RequiresConfig]
        public CustomerItemResponse Get(string customerId)
		{
			customerId = Uri.EscapeUriString(customerId);
			var responseJson = _serviceHelper.CallPaydock("customers/" + customerId, HttpMethod.GET, "", overrideConfigSecretKey: _overrideConfigSecretKey);

			var response = SerializeHelper.Deserialize<CustomerItemResponse>(responseJson);
            response.JsonResponse = responseJson;
            return response;
        }

        /// <summary>
        /// Update customer details
        /// </summary>
        /// <param name="request">customers details to change</param>
        /// <returns>customer information</returns>
        [RequiresConfig]
        public CustomerItemResponse Update(CustomerUpdateRequest request)
		{
			var customerId = Uri.EscapeUriString(request.customer_id);
			var requestData = SerializeHelper.Serialize(request);
            var responseJson = _serviceHelper.CallPaydock("customers/" + customerId, HttpMethod.POST, requestData, overrideConfigSecretKey: _overrideConfigSecretKey);

			var response = SerializeHelper.Deserialize<CustomerItemResponse>(responseJson);
            response.JsonResponse = responseJson;
            return response;
        }

        /// <summary>
        /// Deletes a single customer
        /// </summary>
        /// <param name="customerId">id of customer to delete</param>
        /// <returns>customer information</returns>
        [RequiresConfig]
        public CustomerItemResponse Delete(string customerId)
		{
			customerId = Uri.EscapeUriString(customerId);
			var responseJson = _serviceHelper.CallPaydock("customers/" + customerId, HttpMethod.DELETE, "", overrideConfigSecretKey: _overrideConfigSecretKey);

			var response = SerializeHelper.Deserialize<CustomerItemResponse>(responseJson);
            response.JsonResponse = responseJson;
            return response;
        }
    }
}
