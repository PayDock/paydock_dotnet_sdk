using Paydock_dotnet_sdk.Models;
using Paydock_dotnet_sdk.Tools;
using System;
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
		public async Task<ChargeResponse> Add(ChargeRequestBase request)
		{
			return await _serviceHelper.Post<ChargeResponse, ChargeRequestBase>(request, "charges", overrideConfigSecretKey: _overrideConfigSecretKey);
		}
		/// <summary>
		/// Retrieve full list of charges, limited to 1000
		/// </summary>
		/// <returns>List of charges</returns>
		[RequiresConfig]
		public async Task<ChargeItemsResponse> Get()
		{
			return await _serviceHelper.Get<ChargeItemsResponse>("charges", overrideConfigSecretKey: _overrideConfigSecretKey);
		}

		/// <summary>
		/// Retrieve a filtered list of charges, limited to 1000
		/// </summary>
		/// <param name="request">filter parameters</param>
		/// <returns>List of charges</returns>
		[RequiresConfig]
		public async Task<ChargeItemsResponse> Get(ChargeSearchRequest request)
		{
			var url = "charges/";
			url = url.AppendParameter("skip", request.skip);
			url = url.AppendParameter("limit", request.limit);
			url = url.AppendParameter("subscription_id", request.subscription_id);
			url = url.AppendParameter("gateway_id", request.gateway_id);
			url = url.AppendParameter("company_id", request.company_id);
			url = url.AppendParameter("created_at.from", request.created_at_from);
			url = url.AppendParameter("created_at.to", request.created_at_to);
			url = url.AppendParameter("search", request.search);
			url = url.AppendParameter("status", request.status);
			url = url.AppendParameter("archived", request.archived);
			url = url.AppendParameter("transaction_external_id", request.transaction_external_id);

			return await _serviceHelper.Get<ChargeItemsResponse>(url, overrideConfigSecretKey: _overrideConfigSecretKey);
		}

		/// <summary>
		/// Retrive a single charge
		/// </summary>
		/// <param name="chargeId">id of the charge to retreive</param>
		/// <returns>charge data</returns>
		[RequiresConfig]
		public async Task<ChargeItemResponse> Get(string chargeId)
		{
			return await _serviceHelper.Get<ChargeItemResponse>("charges/" + chargeId, overrideConfigSecretKey: _overrideConfigSecretKey);
		}

		/// <summary>
		/// Refund a transaction
		/// </summary>
		/// <param name="chargeId">id of the charge to refund</param>
		/// <param name="amount">amount to refund, can be used to issue partial refunds</param>
		/// <returns>information on the transaction</returns>
		[RequiresConfig]
		public async Task<ChargeRefundResponse> Refund(string chargeId, decimal amount)
		{
			chargeId = Uri.EscapeUriString(chargeId);
			var json = string.Format("{{\"amount\" : \"{0}\"}}", amount);
			return await _serviceHelper.Get<ChargeRefundResponse>(string.Format("charges/{0}/refunds", chargeId), overrideConfigSecretKey: _overrideConfigSecretKey);
		}

		/// <summary>
		/// Archive a transaction
		/// </summary>
		/// <param name="chargeId">id of the charge to archive</param>
		/// <returns>information on the transaction</returns>
		[RequiresConfig]
		public async Task<ChargeRefundResponse> Archive(string chargeId)
		{
			chargeId = Uri.EscapeUriString(chargeId);
			return await _serviceHelper.Delete<ChargeRefundResponse>(string.Format("charges/{0}", chargeId), overrideConfigSecretKey: _overrideConfigSecretKey);
		}
	}
}
