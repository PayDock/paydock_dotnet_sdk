﻿using Paydock_dotnet_sdk.Models;
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
		/// Initiate a 3DS authentication for charge
		/// </summary>
		/// <param name="request">Charge data</param>
		/// <returns>Charge response</returns>
		[RequiresConfig]
		public async Task<ChargeResponse> Init3DS(ChargeRequestBase request)
		{
			return await _serviceHelper.Post<ChargeResponse, ChargeRequestBase>(request, "charges/3ds", overrideConfigSecretKey: _overrideConfigSecretKey);
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
		/// Initialise a wallet charge
		/// </summary>
		/// <param name="request">Wallet Charge data</param>
		/// <returns>Wallet response</returns>
		[RequiresConfig]
		public async Task<WalletResponse> InitializeWallet(ChargeRequest request)
		{
			return await _serviceHelper.Post<WalletResponse, ChargeRequest>(request, "charges/wallet", overrideConfigSecretKey: _overrideConfigSecretKey);
		}

		/// <summary>
		/// Initialise a wallet charge
		/// </summary>
		/// <param name="request">Wallet Charge data</param>
		/// <param name="isCaptured">Should Charge be catpured</param>
		/// <returns>Wallet response</returns>
		[RequiresConfig]
		public async Task<WalletResponse> InitializeWallet(ChargeRequest request, Boolean isCaptured)
		{
			return await _serviceHelper.Post<WalletResponse, ChargeRequest>(request, isCaptured?"charges/wallet":"charges/wallet?capture=false", overrideConfigSecretKey: _overrideConfigSecretKey);
		}

		/// <summary>
		/// Authorise a charge
		/// </summary>
		/// <param name="request">Charge data</param>
		/// <returns>Charge response</returns>
		[RequiresConfig]
		public async Task<ChargeResponse> Authorise(ChargeRequestBase request)
		{
			return await _serviceHelper.Post<ChargeResponse, ChargeRequestBase>(request, "charges?capture=false", overrideConfigSecretKey: _overrideConfigSecretKey);
		}

		/// <summary>
		/// Capture a charge
		/// </summary>
		/// <param name="chargeId">id for the charge to capture</param>
		/// <param name="amount">amount to capture</param>
		/// <param name="customFields">Custom fields free-form object</param>
		/// <returns>Charge response</returns>
		[RequiresConfig]
		public async Task<ChargeResponse> Capture(string chargeId, decimal? amount = null, dynamic customFields = null)
		{
			object requestData = new { custom_fields = customFields };
			if (amount.HasValue)
            {
				requestData = new {
					custom_fields = customFields,
					amount = amount.Value
				};
			}

			chargeId = Uri.EscapeUriString(chargeId);
			return await _serviceHelper.Post<ChargeResponse, object>(requestData, string.Format("charges/{0}/capture", chargeId), overrideConfigSecretKey: _overrideConfigSecretKey);
		}


		/// <summary>
		/// Send charge payload to stanalone fraud check
		/// </summary>
		/// <param name="request">Charge data</param>
		/// <returns>Charge response</returns>
		[RequiresConfig]
		public async Task<ChargeResponse> FraudCheck(ChargeRequest request)
		{
			return await _serviceHelper.Post<ChargeResponse, ChargeRequest>(request, "charges/fraud", overrideConfigSecretKey: _overrideConfigSecretKey);
		}

		/// <summary>
		/// Send charge payload to standalone 3DS
		/// </summary>
		/// <param name="request">Charge data</param>
		/// <returns>Charge response</returns>
		[RequiresConfig]
		public async Task<ChargeResponse> ThreeDSStandalone(ChargeRequest request)
		{
			return await _serviceHelper.Post<ChargeResponse, ChargeRequest>(request, "charges/standalone-3ds", overrideConfigSecretKey: _overrideConfigSecretKey);
		}


		/// <summary>
		/// Attach fraud transaction to a charge
		/// </summary>
		/// <param name="chargeId">id of the charge</param>
		/// <param name="fraudCheckChargeId">Id of the fraud check charge</param>
		/// <returns>information on the transaction</returns>
		[RequiresConfig]
		public async Task<ChargeResponse> FraudCheckAttach(string chargeId, string fraudCheckChargeId)
		{
			chargeId = Uri.EscapeUriString(chargeId);
			object requestData = new { fraud_charge_id = fraudCheckChargeId };

			return await _serviceHelper.Post<ChargeResponse, object>(requestData, string.Format("charges/{0}/fraud/attach", chargeId), overrideConfigSecretKey: _overrideConfigSecretKey);
		}

		/// <summary>
		/// Update custom fields for transaction in a charge
		/// </summary>
		/// <param name="chargeId">id for the charge</param>
		/// <param name="transactionId">id for the transaction in charge to update custom fields</param>
		/// <param name="customFields">Custom fields free-form object</param>
		/// <returns>Charge response</returns>
		[RequiresConfig]
		public async Task<TransactionResponse> UpdateCustomFields(string chargeId, string transactionId, object customFields)
		{

			chargeId = Uri.EscapeUriString(chargeId);
			transactionId = Uri.EscapeUriString(transactionId);
			return await _serviceHelper.Post<TransactionResponse, object>(customFields, string.Format("charges/{0}/transactions/{1}/custom-fields", chargeId, transactionId), overrideConfigSecretKey: _overrideConfigSecretKey);
		}


		/// <summary>
		/// cancel a previously authorised charge
		/// </summary>
		/// <param name="chargeId">id for the charge to capture</param>
		/// <returns>Charge response</returns>
		[RequiresConfig]
		public async Task<ChargeResponse> CancelAuthorisation(string chargeId)
		{
			chargeId = Uri.EscapeUriString(chargeId);
			return await _serviceHelper.Delete<ChargeResponse>(string.Format("charges/{0}/capture", chargeId), overrideConfigSecretKey: _overrideConfigSecretKey);
		}

		/// <summary>
		/// cancel a previously authorised charge
		/// </summary>
		/// <param name="chargeId">id for the charge to capture</param>
		/// <param name="customFields">Custom fields free-form object</param>
		/// <returns>Charge response</returns>
		[RequiresConfig]
		public async Task<ChargeResponse> CancelAuthorisation(string chargeId, dynamic customFields)
		{
			object requestData = new { custom_fields = customFields };
			
			chargeId = Uri.EscapeUriString(chargeId);
			return await _serviceHelper.Delete<ChargeResponse, object>(requestData, string.Format("charges/{0}/capture", chargeId), overrideConfigSecretKey: _overrideConfigSecretKey);
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
			if (string.IsNullOrEmpty(request.reference))
			{
#pragma warning disable 0612
				request.reference = request.transaction_external_id;
#pragma warning restore 0612
			}

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
			url = url.AppendParameter("reference", request.reference);
			url = url.AppendParameter("authorization", request.authorization);

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
			chargeId = Uri.EscapeUriString(chargeId);
			return await _serviceHelper.Get<ChargeItemResponse>(string.Format("charges/{0}", chargeId), overrideConfigSecretKey: _overrideConfigSecretKey);
		}

		/// <summary>
		/// Retrive a single charge with 3DS authorisaton Id
		/// </summary>
		/// <param name="threeDSId">id of the 3ds authorisation</param>
		/// <returns>charge data</returns>
		[RequiresConfig]
		public async Task<ChargeItemResponse> GetWith3DSId(string threeDSId)
		{
			threeDSId = Uri.EscapeUriString(threeDSId);
			return await _serviceHelper.Get<ChargeItemResponse>(string.Format("charges/3ds/{0}", threeDSId), overrideConfigSecretKey: _overrideConfigSecretKey);
		}

		/// <summary>
		/// Refund a transaction
		/// </summary>
		/// <param name="chargeId">id of the charge to refund</param>
		/// <param name="amount">amount to refund, can be used to issue partial refunds</param>
		/// <returns>information on the transaction</returns>
		[RequiresConfig]
		public async Task<ChargeRefundResponse> Refund(string chargeId, decimal? amount = null)
		{
			chargeId = Uri.EscapeUriString(chargeId);
			object requestData = new { amount = amount };
		
			return await _serviceHelper.Post<ChargeRefundResponse, object>(requestData, string.Format("charges/{0}/refunds", chargeId), overrideConfigSecretKey: _overrideConfigSecretKey);
		}




		/// <summary>
		/// Refund a transaction
		/// </summary>
		/// <param name="chargeId">id of the charge to refund</param>
		/// <param name="amount">amount to refund, can be used to issue partial refunds</param>
		/// <param name="customFields">Custom fields free-form object</param>
		/// <returns>information on the transaction</returns>
		[RequiresConfig]
		public async Task<ChargeRefundResponse> Refund(string chargeId, decimal? amount = null, dynamic customFields = null)
		{
			chargeId = Uri.EscapeUriString(chargeId);
			object requestData = new { amount = amount, custom_fields = customFields };

			return await _serviceHelper.Post<ChargeRefundResponse, object>(requestData, string.Format("charges/{0}/refunds", chargeId), overrideConfigSecretKey: _overrideConfigSecretKey);
		}


		/// <summary>
		/// Send payload for standalone Refund
		/// </summary>
		/// <param name="request">Charge refund data</param>
		/// <returns>Charge Refund response</returns>
		[RequiresConfig]
		public async Task<ChargeRefundResponse> Refund(ChargeRefundRequest request)
		{
			return await _serviceHelper.Post<ChargeRefundResponse, ChargeRefundRequest>(request, "charges/refunds", overrideConfigSecretKey: _overrideConfigSecretKey);
		}

		/// <summary>
		/// Refund a transaction
		/// </summary>
		/// <param name="chargeId">id of the charge to refund</param>
		/// <param name="request">Charge refund data</param>
		/// <returns>information on the transaction</returns>
		[RequiresConfig]
		public async Task<ChargeRefundResponse> Refund(string chargeId, ChargeRefundRequest request)
		{
			return await _serviceHelper.Post<ChargeRefundResponse, ChargeRefundRequest>(request, string.Format("charges/{0}/refunds", chargeId), overrideConfigSecretKey: _overrideConfigSecretKey);
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
