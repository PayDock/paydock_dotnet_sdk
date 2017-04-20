using Newtonsoft.Json;
using Paydock_dotnet_sdk.Models;
using Paydock_dotnet_sdk.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paydock_dotnet_sdk.Services
{
    public class ExternalCheckout
    {
        protected IServiceHelper _serviceHelper;

        /// <summary>
        /// Service locator style constructor
        /// </summary>
        public ExternalCheckout()
        {
            _serviceHelper = new ServiceHelper();
        }

        /// <summary>
        /// Dependency injection constructor to enable testing
        /// </summary>
        public ExternalCheckout(IServiceHelper serviceHelper)
        {
            _serviceHelper = serviceHelper;
        }

        /// <summary>
        /// makes an external checkout request, eg for paypal express
        /// </summary>
        /// <param name="request">checkout information</param>
        /// <returns>Checkout link</returns>
        [RequiresConfig]
        public ExternalCheckoutResponse Create(ExternalCheckoutRequest request)
        {
            var requestData = JsonConvert.SerializeObject(request, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var responseJson = _serviceHelper.CallPaydock("payment_sources/external_checkout", HttpMethod.POST, requestData);

            var response = (ExternalCheckoutResponse)JsonConvert.DeserializeObject(responseJson, typeof(ExternalCheckoutResponse));
            response.JsonResponse = responseJson;
            return response;

        }
    }
}
