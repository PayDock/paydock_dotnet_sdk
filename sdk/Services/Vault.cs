using Paydock_dotnet_sdk.Models;
using Newtonsoft.Json;
using Paydock_dotnet_sdk.Tools;
using System;

namespace Paydock_dotnet_sdk.Services
{
    /// <summary>
    /// Provides abstraction over the /charges endpoint for the APU
    /// </summary>
    public class Vault : IVault
	{
        protected IServiceHelper _serviceHelper;
        protected string _overrideConfigSecretKey = null;

        /// <summary>
        /// Service locator style constructor
        /// <param name="overrideConfigSecretKey">Use a custom secret key rather than the value in shared config, defaults to null</param>
        /// </summary>
        public Vault(string overrideConfigSecretKey = null)
        {
            _serviceHelper = new ServiceHelper();
            _overrideConfigSecretKey = overrideConfigSecretKey;
        }

        /// <summary>
        /// Dependency injection constructor to enable testing
        /// <param name="serviceHelper">Service helper class to perform HTTP requests</param>
        /// <param name="overrideConfigSecretKey">Use a custom secret key rather than the value in shared config, defaults to null</param>
        /// </summary>
        public Vault(IServiceHelper serviceHelper, string overrideConfigSecretKey = null)
        {
            _serviceHelper = serviceHelper;
            _overrideConfigSecretKey = overrideConfigSecretKey;
        }

        /// <summary>
        /// Add a vault token
        /// </summary>
        /// <param name="request">Payment data</param>
        /// <returns>Vault response</returns>
        [RequiresConfig]
        public VaultResponse CreateToken(VaultRequest request)
        {
            var requestData = SerializeHelper.Serialize(request);
            var responseJson = _serviceHelper.CallPaydock("vault/payment_sources", HttpMethod.POST, requestData, overrideConfigSecretKey :_overrideConfigSecretKey);

			var response = SerializeHelper.Deserialize<VaultResponse>(responseJson);
            response.JsonResponse = responseJson;
            return response;
		}

		/// <summary>
		/// get information about a single vault token
		/// </summary>
		/// <param name="vaultToken">vault token id</param>
		/// <returns>Vault response</returns>
		[RequiresConfig]
		public VaultResponse GetToken(string vaultToken)
		{
			vaultToken = Uri.EscapeUriString(vaultToken);

			var responseJson = _serviceHelper.CallPaydock("vault/payment_sources/" + vaultToken, HttpMethod.GET, "", overrideConfigSecretKey: _overrideConfigSecretKey);

			var response = SerializeHelper.Deserialize<VaultResponse>(responseJson);
			response.JsonResponse = responseJson;
			return response;
		}

		/// <summary>
		/// get iget a list of vault tokens
		/// </summary>
		/// <returns>Vault response</returns>
		[RequiresConfig]
		public VaultItemsResponse GetTokens()
		{
			var responseJson = _serviceHelper.CallPaydock("vault/payment_sources", HttpMethod.GET, "", overrideConfigSecretKey: _overrideConfigSecretKey);

			var response = SerializeHelper.Deserialize<VaultItemsResponse>(responseJson);
			response.JsonResponse = responseJson;
			return response;
		}
	}
}
