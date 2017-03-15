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

            // TODO: pull out the service logs independently
            return (ChargeResponse) JsonConvert.DeserializeObject(response, typeof(ChargeResponse));
        }

        [RequiresConfig]
        public ChargeItemsResponse Get()
        {
            throw new NotImplementedException();
        }

        [RequiresConfig]
        public ChargeItemsResponse Get(GetChargeRequest request)
        {
            throw new NotImplementedException();
        }

        [RequiresConfig]
        public ChargeItemsResponse Get(string chargeId)
        {
            throw new NotImplementedException();
        }

        [RequiresConfig]
        public ChargeResponse Refund(RefundRequest request)
        {
            throw new NotImplementedException();
        }

        [RequiresConfig]
        public ChargeResponse Archive(string id)
        {
            throw new NotImplementedException();
        }

        [RequiresConfig]
        public ChargeResponse Get(bool isArchived)
        {
            throw new NotImplementedException();
        }
    }
}
