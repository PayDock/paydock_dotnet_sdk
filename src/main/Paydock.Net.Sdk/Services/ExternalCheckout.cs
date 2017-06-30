using Newtonsoft.Json;
using Paydock.Net.Sdk.Models;
using Paydock.Net.Sdk.Tools;

namespace Paydock.Net.Sdk.Services
{
    public class ExternalCheckout
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
        public ExternalCheckoutResponse Create(ExternalCheckoutRequest request)
        {
            var requestData = JsonConvert.SerializeObject(request, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var responseJson = _serviceHelper.CallPaydock("payment_sources/external_checkout", HttpMethod.POST, requestData, overrideConfigSecretKey: _overrideConfigSecretKey);

            var response = (ExternalCheckoutResponse)JsonConvert.DeserializeObject(responseJson, typeof(ExternalCheckoutResponse));
            response.JsonResponse = responseJson;
            return response;

        }
    }
}
