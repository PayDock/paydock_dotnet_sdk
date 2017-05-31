using Newtonsoft.Json;
using Paydock_dotnet_sdk.Models;
using Paydock_dotnet_sdk.Tools;
using System;

namespace Paydock_dotnet_sdk.Services
{
    public class Tokens
    {
        protected IServiceHelper _serviceHelper;

        /// <summary>
        /// Service locator style constructor
        /// </summary>
        public Tokens()
        {
            _serviceHelper = new ServiceHelper();
        }

        /// <summary>
        /// Dependency injection constructor to enable testing
        /// </summary>
        public Tokens(IServiceHelper serviceHelper)
        {
            _serviceHelper = serviceHelper;
        }

        /// <summary>
        /// Creates a one-time token on Paydock
        /// </summary>
        /// <param name="request">payment data to create the token</param>
        /// <returns>One-Time token</returns>
        [RequiresConfig]
        public TokenResponse Create(TokenRequest request)
        {
            var requestData = JsonConvert.SerializeObject(request, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var responseJson = _serviceHelper.CallPaydock("payment_sources/tokens?public_key=" + Uri.EscapeUriString(Config.PublicKey), HttpMethod.POST, requestData, excludeSecretKey: true);

            var response = (TokenResponse)JsonConvert.DeserializeObject(responseJson, typeof(TokenResponse));
            response.JsonResponse = responseJson;
            return response;

        }
    }
}
