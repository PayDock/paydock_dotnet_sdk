using Paydock_dotnet_sdk.Models;
using Paydock_dotnet_sdk.Tools;
using System;
using System.Threading.Tasks;

namespace Paydock_dotnet_sdk.Services
{
    public class Vault : IVault
	{
        protected IServiceHelper _serviceHelper;
        protected string _overrideConfigSecretKey = null;

        /// <summary>
        /// Service locator style constructor
        /// </summary>
        public Vault(string overrideConfigSecretKey = null)
        {
            _serviceHelper = new ServiceHelper();
			_overrideConfigSecretKey = overrideConfigSecretKey;
        }

        /// <summary>
        /// Dependency injection constructor to enable testing
        /// <param name="serviceHelper">Service helper class to perform HTTP requests</param>
        /// <param name="overrideConfigSecretKey">Use a custom public key rather than the value in shared config, defaults to null</param>
        /// </summary>
        public Vault(IServiceHelper serviceHelper, string overrideConfigSecretKey = null)
        {
            _serviceHelper = serviceHelper;
			_overrideConfigSecretKey = overrideConfigSecretKey;
        }

        /// <summary>
        /// Create a vault token
        /// </summary>
        /// <param name="request">payment data to create the vault token</param>
        /// <returns>vault token</returns>
        [RequiresConfig]
        public async Task<VaultResponse> Create(VaultRequest request)
		{
			return await _serviceHelper.Post<VaultResponse, VaultRequest>(request, "vault/payment_sources", overrideConfigSecretKey: _overrideConfigSecretKey);
		}

		/// <summary>
		/// get a vault token
		/// </summary>
		/// <param name="vaultToken">vault token</param>
		/// <returns>vault token data</returns>
		[RequiresConfig]
		public async Task<VaultResponse> Get(string vaultToken)
		{
			vaultToken = Uri.EscapeUriString(vaultToken);
			var url = "vault/payment_sources/" + vaultToken;
			return await _serviceHelper.Get<VaultResponse>(url, overrideConfigSecretKey: _overrideConfigSecretKey);
		}

		/// <summary>
		/// get a list of vault token
		/// </summary>
		/// <returns>vault token data</returns>
		[RequiresConfig]
		public async Task<VaultItemsResponse> Get()
		{
			return await _serviceHelper.Get<VaultItemsResponse>("vault/payment_sources", overrideConfigSecretKey: _overrideConfigSecretKey);
		}
	}
}
