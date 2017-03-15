using System;
using Paydock_dotnet_sdk.Models;
using Newtonsoft.Json;

namespace Paydock_dotnet_sdk.Services
{
    public class Charges
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
        public ChargeResponse Add(ChargeRequest data)
        {
            var requestData = JsonConvert.SerializeObject(data, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var response = _serviceHelper.CallPaydock("charges", HttpMethod.POST, requestData);

            // TODO: pull out the service logs independently
            return (ChargeResponse) JsonConvert.DeserializeObject(response, typeof(ChargeResponse));
        }
    }
}
