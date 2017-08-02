using Newtonsoft.Json;
using Paydock_dotnet_sdk.Models;
using Paydock_dotnet_sdk.Tools;
using System.Threading.Tasks;

namespace Paydock_dotnet_sdk.Services
{
	public class ExternalCheckout : IExternalCheckout
	{
		protected IServiceHelper _serviceHelper;
		protected string _overrideConfigSecretKey = null;

		/// <summary>
		/// Service locator style constructor
		/// <param name="overrideConfigSecretKey">Use a custom secret key rather than the value in shared config, defaults to null</param>
		/// </summary>
		public ExternalCheckout(string overrideConfigSecretKey = null)
		{
			_serviceHelper = new ServiceHelper();
			_overrideConfigSecretKey = overrideConfigSecretKey;
		}

		/// <summary>
		/// Dependency injection constructor to enable testing
		/// <param name="serviceHelper">Service helper class to perform HTTP requests</param>
		/// <param name="overrideConfigSecretKey">Use a custom secret key rather than the value in shared config, defaults to null</param>
		/// </summary>
		public ExternalCheckout(IServiceHelper serviceHelper, string overrideConfigSecretKey = null)
		{
			_serviceHelper = serviceHelper;
			_overrideConfigSecretKey = overrideConfigSecretKey;
		}

		/// <summary>
		/// makes an external checkout request, eg for paypal express
		/// </summary>
		/// <param name="request">checkout information</param>
		/// <returns>Checkout link</returns>
		[RequiresConfig]
		public async Task<ExternalCheckoutResponse> Create(ExternalCheckoutRequest request)
		{
			return await _serviceHelper.Post<ExternalCheckoutResponse, ExternalCheckoutRequest>(request, "payment_sources/external_checkout", overrideConfigSecretKey: _overrideConfigSecretKey);
		}
	}
}
