using Paydock_dotnet_sdk.Models;
using Paydock_dotnet_sdk.Services;
using Paydock_dotnet_sdk.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Paydock_dotnet_sdk.Services
{
    public class Charges : ICharges
	{
		protected IServiceHelper _serviceHelper;
		protected string _overrideConfigSecretKey = null;

		/// <summary>
		/// Service locator style constructor
		/// <param name="overrideConfigSecretKey">Use a custom secret key rather than the value in shared config, defaults to null</param>
		/// </summary>
		public Charges(string overrideConfigSecretKey = null)
		{
			_serviceHelper = new ServiceHelper();
			_overrideConfigSecretKey = overrideConfigSecretKey;
		}

		/// <summary>
		/// Dependency injection constructor to enable testing
		/// <param name="serviceHelper">Service helper class to perform HTTP requests</param>
		/// <param name="overrideConfigSecretKey">Use a custom secret key rather than the value in shared config, defaults to null</param>
		/// </summary>
		public Charges(IServiceHelper serviceHelper, string overrideConfigSecretKey = null)
		{
			_serviceHelper = serviceHelper;
			_overrideConfigSecretKey = overrideConfigSecretKey;
		}


		/// <summary>
		/// Add a charge
		/// </summary>
		/// <param name="request">Charge data</param>
		/// <returns>Charge response</returns>
		[RequiresConfig]
		public async Task<ChargeResponse> Add(ChargeRequest request)
		{
			return await _serviceHelper.Post<ChargeResponse, ChargeRequest>(request, "charges", overrideConfigSecretKey: _overrideConfigSecretKey);
		}
	}
}
