using Paydock_dotnet_sdk.Tools;

namespace Paydock_dotnet_sdk.Models
{
    public class ErrorResponse : ErrorResponseBase
    {
        public string StatusCode { get; set; }
        public string StatusCodeDescription { get; set; }
        public Details ErrorDetails { get; set; }        
        public ChargeResponse ExceptionChargeResponse { get; set; }
    }

    public class Details
    {
        public string gateway_specific_code { get; set; }
        public string gateway_specific_description { get; set; }
        public string path { get; set; }
        public string description { get; set; }
        public string[] messages { get; set; }

    }

}
