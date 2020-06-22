using Paydock_dotnet_sdk.Tools;

namespace Paydock_dotnet_sdk.Models
{
    public class ErrorResponse : ErrorResponseBase
    {
        public string ErrorCode { get; set; }
        public Details[] ErrorDetails { get; set; }        
        public ChargeResponse ExceptionChargeResponse { get; set; }
    }

    public class Details
    {
        public string gateway_specific_code { get; set; }
        public string gateway_specific_description { get; set; }
        public string param_name { get; set; }
        public string description { get; set; }

    }

}
