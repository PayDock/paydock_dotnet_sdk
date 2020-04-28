using Paydock_dotnet_sdk.Models;
using Paydock_dotnet_sdk.Tools;
using System;
using System.Threading.Tasks;

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
        public async Task<GatewayResponse> Add(GatewayRequest request)
		{
			return await _serviceHelper.Post<GatewayResponse, GatewayRequest>(request, "gateways", overrideConfigSecretKey: _overrideConfigSecretKey);
        }


        /// <summary>
        /// Retrieve details of a single gateway
        /// </summary>
        /// <param name="gatewayid">id of the gateway</param>
        /// <returns>gateway data</returns>
        [RequiresConfig]
        public async Task<GatewayItemResponse> Get(string gatewayid)
		{
			gatewayid = Uri.EscapeUriString(gatewayid);
			return await _serviceHelper.Get<GatewayItemResponse>("gateways/" + gatewayid, overrideConfigSecretKey: _overrideConfigSecretKey);
        }

        /// <summary>
        /// Retrieve details of a single gateway
        /// </summary>
        /// <param name="gatewayid">id of the gateway</param>
        /// <returns>gateway data</returns>
        [RequiresConfig]
        public async Task<GatewayItemResponse> Delete(string gatewayid)
		{
			gatewayid = Uri.EscapeUriString(gatewayid);
			return await _serviceHelper.Delete<GatewayItemResponse>("gateways/" + gatewayid, overrideConfigSecretKey: _overrideConfigSecretKey);
        }

        /// <summary>
        /// Update details of a gateway
        /// </summary>
        /// <param name="request">details of the gateway</param>
        /// <returns>gateway data</returns>
        [RequiresConfig]
        public async Task<GatewayItemResponse> Update(GatewayUpdateRequest request)
		{
			var gatewayid = Uri.EscapeUriString(request._id);
			return await _serviceHelper.Put<GatewayItemResponse, GatewayUpdateRequest>(request, "gateways/" + gatewayid, overrideConfigSecretKey: _overrideConfigSecretKey);
        }

        /// <summary>
        /// Retrieve list of gateways
        /// </summary>
        /// <returns>list of gateway data</returns>
        [RequiresConfig]
        public async Task<GatewayItemsResponse> Get()
		{
			return await _serviceHelper.Get<GatewayItemsResponse>("gateways", overrideConfigSecretKey: _overrideConfigSecretKey);
        }
    }
}
