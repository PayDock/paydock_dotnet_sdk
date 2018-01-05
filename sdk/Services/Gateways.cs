using Newtonsoft.Json;
using Paydock_dotnet_sdk.Models;
using Paydock_dotnet_sdk.Tools;
using System;

namespace Paydock_dotnet_sdk.Services
{
    public class Gateways : IGateways
    {
        protected IServiceHelper _serviceHelper;
        protected string _overrideConfigSecretKey = null;

        /// <summary>
        /// Service locator style constructor
        /// <param name="overrideConfigSecretKey">Use a custom secret key rather than the value in shared config, defaults to null</param>
        /// </summary>
        public Gateways(string overrideConfigSecretKey = null)
        {
            _serviceHelper = new ServiceHelper();
            _overrideConfigSecretKey = overrideConfigSecretKey;
        }

        /// <summary>
        /// Dependency injection constructor to enable testing
        /// <param name="serviceHelper">Service helper class to perform HTTP requests</param>
        /// <param name="overrideConfigSecretKey">Use a custom secret key rather than the value in shared config, defaults to null</param>
        /// </summary>
        public Gateways(IServiceHelper serviceHelper, string overrideConfigSecretKey = null)
        {
            _serviceHelper = serviceHelper;
            _overrideConfigSecretKey = overrideConfigSecretKey;
        }


        /// <summary>
        /// Adds a gateway to your account
        /// </summary>
        /// <param name="request">details of the gateway</param>
        /// <returns>gateway data</returns>
        [RequiresConfig]
        public GatewayResponse Add(GatewayRequest request)
        {
            var requestData = SerializeHelper.Serialize(request);
            var responseJson = _serviceHelper.CallPaydock("gateways", HttpMethod.POST, requestData, overrideConfigSecretKey: _overrideConfigSecretKey);

			var response = SerializeHelper.Deserialize<GatewayResponse>(responseJson);
            response.JsonResponse = responseJson;
            return response;
        }


        /// <summary>
        /// Retrieve details of a single gateway
        /// </summary>
        /// <param name="gatewayid">id of the gateway</param>
        /// <returns>gateway data</returns>
        [RequiresConfig]
        public GatewayItemResponse Get(string gatewayid)
		{
			gatewayid = Uri.EscapeUriString(gatewayid);
			var responseJson = _serviceHelper.CallPaydock("gateways/" + gatewayid, HttpMethod.GET, "", overrideConfigSecretKey: _overrideConfigSecretKey);

			var response = SerializeHelper.Deserialize<GatewayItemResponse>(responseJson);
            response.JsonResponse = responseJson;
            return response;
        }

        /// <summary>
        /// Retrieve details of a single gateway
        /// </summary>
        /// <param name="gatewayid">id of the gateway</param>
        /// <returns>gateway data</returns>
        [RequiresConfig]
        public GatewayItemResponse Delete(string gatewayid)
		{
			gatewayid = Uri.EscapeUriString(gatewayid);
			var responseJson = _serviceHelper.CallPaydock("gateways/" + gatewayid, HttpMethod.DELETE, "", overrideConfigSecretKey: _overrideConfigSecretKey);

			var response = SerializeHelper.Deserialize<GatewayItemResponse>(responseJson);
            response.JsonResponse = responseJson;
            return response;
        }

        /// <summary>
        /// Update details of a gateway
        /// </summary>
        /// <param name="request">details of the gateway</param>
        /// <returns>gateway data</returns>
        [RequiresConfig]
        public GatewayItemResponse Update(GatewayUpdateRequest request)
		{
			var gatewayid = Uri.EscapeUriString(request._id);
			var requestData = SerializeHelper.Serialize(request);
            var responseJson = _serviceHelper.CallPaydock("gateways/" + gatewayid, HttpMethod.PUT, requestData, overrideConfigSecretKey: _overrideConfigSecretKey);

			var response = SerializeHelper.Deserialize<GatewayItemResponse>(responseJson);
            response.JsonResponse = responseJson;
            return response;
        }

        /// <summary>
        /// Retrieve list of gateways
        /// </summary>
        /// <returns>list of gateway data</returns>
        [RequiresConfig]
        public GatewayItemsResponse Get()
        {
            var responseJson = _serviceHelper.CallPaydock("gateways", HttpMethod.GET, "", overrideConfigSecretKey: _overrideConfigSecretKey);

			var response = SerializeHelper.Deserialize<GatewayItemsResponse>(responseJson);
            response.JsonResponse = responseJson;
            return response;
        }
    }
}
