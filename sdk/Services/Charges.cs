using System;
using Paydock_dotnet_sdk.Models;
using Newtonsoft.Json;

namespace Paydock_dotnet_sdk.Services
{
    public class Charges : ICharges
    {
        // TODO: add function signature definitions and interfaces

        protected IServiceHelper _serviceHelper;

        public Charges()
        {
            _serviceHelper = new ServiceHelper();
        }

        public Charges(IServiceHelper serviceHelper)
        {
            _serviceHelper = serviceHelper;
        }

        [RequiresConfig]
        public ChargeResponse Add(ChargeRequest request)
        {
            var requestData = JsonConvert.SerializeObject(request, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var response = _serviceHelper.CallPaydock("charges", HttpMethod.POST, requestData);

            // TODO: review serialisation of the data
            return (ChargeResponse) JsonConvert.DeserializeObject(response, typeof(ChargeResponse));
        }

        [RequiresConfig]
        public ChargeItemsResponse Get()
        {
            var response = _serviceHelper.CallPaydock("charges", HttpMethod.GET, "");

            // TODO: review serialisation of the data
            return (ChargeItemsResponse)JsonConvert.DeserializeObject(response, typeof(ChargeItemsResponse));
        }

        [RequiresConfig]
        public ChargeItemsResponse Get(GetChargeRequest request)
        {
            var url = "charges";

            if (request.skip.HasValue)
                url = addUrlParameter(url, "skip", request.skip.Value.ToString());

            if (request.limit.HasValue)
                url = addUrlParameter(url, "limit", request.limit.Value.ToString());

            if (request.subscription_id != null)
                url = addUrlParameter(url, "subscription_id", request.subscription_id);

            if (request.gateway_id != null)
                url = addUrlParameter(url, "gateway_id", request.gateway_id);

            if (request.company_id != null)
                url = addUrlParameter(url, "company_id", request.company_id);

            if (request.created_at_from.HasValue)
                url = addUrlParameter(url, "created_at.from", request.created_at_from.Value.ToString());

            if (request.created_at_to.HasValue)
                url = addUrlParameter(url, "created_at.to", request.created_at_to.Value.ToString());

            if (request.search != null)
                url = addUrlParameter(url, "search", request.search);

            if (request.status != null)
                url = addUrlParameter(url, "status", request.status);

            if (request.archived.HasValue)
                url = addUrlParameter(url, "archived", request.archived.Value.ToString());

            var response = _serviceHelper.CallPaydock("charges", HttpMethod.GET, "");

            // TODO: review serialisation of the data
            return (ChargeItemsResponse)JsonConvert.DeserializeObject(response, typeof(ChargeItemsResponse));
        }

        private string addUrlParameter(string currrentUrl, string name, string value)
        {
            if (!currrentUrl.Contains("?"))
                currrentUrl += "?";
            else
                currrentUrl += "&";

            return Uri.EscapeUriString(name) + "=" + Uri.EscapeUriString(value);
        }

        [RequiresConfig]
        public ChargeItemsResponse Get(string chargeId)
        {
            chargeId = Uri.EscapeUriString(chargeId);
            var response = _serviceHelper.CallPaydock("charges/" + chargeId, HttpMethod.GET, "");

            // TODO: review serialisation of the data
            return (ChargeItemsResponse)JsonConvert.DeserializeObject(response, typeof(ChargeItemsResponse));
        }

        [RequiresConfig]
        public ChargeResponse Refund(RefundRequest request)
        {
            var chargeId = Uri.EscapeUriString(request.id);
            var requestData = JsonConvert.SerializeObject(request, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var response = _serviceHelper.CallPaydock("charges/" + chargeId + "/refunds", HttpMethod.POST, requestData);

            // TODO: review serialisation of the data
            return (ChargeResponse)JsonConvert.DeserializeObject(response, typeof(ChargeResponse));
        }

        [RequiresConfig]
        public ChargeResponse Archive(string chargeId)
        {
            chargeId = Uri.EscapeUriString(chargeId);
            var response = _serviceHelper.CallPaydock("charges/" + chargeId, HttpMethod.DELETE, "");

            // TODO: review serialisation of the data
            return (ChargeResponse)JsonConvert.DeserializeObject(response, typeof(ChargeResponse));
        }
    }
}
