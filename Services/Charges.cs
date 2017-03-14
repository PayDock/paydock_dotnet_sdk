using System;
using Paydock_dotnet_sdk.Models;
using Newtonsoft.Json;

namespace Paydock_dotnet_sdk.Services
{
    public class Charges
    {
        protected IServiceHelper _serviceHelper;

        public Charges()
        {
            _serviceHelper = new ServiceHelper();
        }

        public Charges(IServiceHelper serviceHelper)
        {
            _serviceHelper = serviceHelper;
        }

        public ChargeResponse Add(ChargeRequest data)
        {
            // TODO: throw an error if missing required config

            var requestData = JsonConvert.SerializeObject(data);
            var response = _serviceHelper.CallPaydock("charges", HttpMethod.POST, requestData);

            // TODO: pull out the service logs independantly
            // TODO: review exception handling
            return (ChargeResponse) JsonConvert.DeserializeObject(response, typeof(ChargeResponse));
        }
    }
}
