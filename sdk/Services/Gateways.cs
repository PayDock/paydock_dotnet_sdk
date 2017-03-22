using Newtonsoft.Json;
using Paydock_dotnet_sdk.Models;
using Paydock_dotnet_sdk.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paydock_dotnet_sdk.Services
{
    public class Gateways
    {
        protected IServiceHelper _serviceHelper;

        /// <summary>
        /// Service locator style constructor
        /// </summary>
        public Gateways()
        {
            _serviceHelper = new ServiceHelper();
        }

        /// <summary>
        /// Dependency injection constructor to enable testing
        /// </summary>
        public Gateways(IServiceHelper serviceHelper)
        {
            _serviceHelper = serviceHelper;
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
            var responseJson = _serviceHelper.CallPaydock("gateways", HttpMethod.POST, requestData);

            var response = (GatewayResponse)JsonConvert.DeserializeObject(responseJson, typeof(GatewayResponse));
            response.JsonResponse = responseJson;
            return response;
        }


        /// <summary>
        /// Retrieve details of a single gateway
        /// </summary>
        /// <param name="request">details of the gateway</param>
        /// <returns>gateway data</returns>
        [RequiresConfig]
        public GatewayItemResponse Get(string gatewayid)
        {
            var responseJson = _serviceHelper.CallPaydock("gateways/" + gatewayid, HttpMethod.GET, "");

            var response = (GatewayItemResponse)JsonConvert.DeserializeObject(responseJson, typeof(GatewayItemResponse));
            response.JsonResponse = responseJson;
            return response;
        }


        /// <summary>
        /// Retrieve details of a single gateway
        /// </summary>
        /// <param name="request">details of the gateway</param>
        /// <returns>gateway data</returns>
        [RequiresConfig]
        public GatewayItemResponse Delete(string gatewayid)
        {
            var responseJson = _serviceHelper.CallPaydock("gateways/" + gatewayid, HttpMethod.DELETE, "");

            // TODO: review the response for this. It's odd that it would return the details of the gateway that has just been deleted
            var response = (GatewayItemResponse)JsonConvert.DeserializeObject(responseJson, typeof(GatewayItemResponse));
            response.JsonResponse = responseJson;
            return response;
        }
    }
}
