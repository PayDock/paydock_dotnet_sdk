using Newtonsoft.Json;
using Paydock.Net.Sdk.Models;
using Paydock.Net.Sdk.Tools;

namespace Paydock.Net.Sdk.Services
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
            var requestData = JsonConvert.SerializeObject(request, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var responseJson = _serviceHelper.CallPaydock("gateways", HttpMethod.POST, requestData, overrideConfigSecretKey: _overrideConfigSecretKey);

            var response = (GatewayResponse)JsonConvert.DeserializeObject(responseJson, typeof(GatewayResponse));
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
            var responseJson = _serviceHelper.CallPaydock("gateways/" + gatewayid, HttpMethod.GET, "", overrideConfigSecretKey: _overrideConfigSecretKey);

            var response = (GatewayItemResponse)JsonConvert.DeserializeObject(responseJson, typeof(GatewayItemResponse));
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
            var responseJson = _serviceHelper.CallPaydock("gateways/" + gatewayid, HttpMethod.DELETE, "", overrideConfigSecretKey: _overrideConfigSecretKey);

            var response = (GatewayItemResponse)JsonConvert.DeserializeObject(responseJson, typeof(GatewayItemResponse));
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
            var requestData = JsonConvert.SerializeObject(request, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var responseJson = _serviceHelper.CallPaydock("gateways/" + request._id, HttpMethod.PUT, requestData, overrideConfigSecretKey: _overrideConfigSecretKey);

            var response = (GatewayItemResponse)JsonConvert.DeserializeObject(responseJson, typeof(GatewayItemResponse));
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

            var response = (GatewayItemsResponse)JsonConvert.DeserializeObject(responseJson, typeof(GatewayItemsResponse));
            response.JsonResponse = responseJson;
            return response;
        }
    }
}
