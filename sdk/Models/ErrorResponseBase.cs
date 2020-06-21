namespace Paydock_dotnet_sdk.Models
{
    public class ErrorResponseBase
    {
        public int Status { get; set; }
        public string ErrorMessage { get; set; }
        public dynamic ExtendedInformation { get; set; }
        public string JsonResponse { get; set; }
    }

}
