namespace Paydock_dotnet_sdk.Models
{
    public class Response
    {
        public int status { get; set; }
        public object error { get; set; }
        public object error_summary { get; set; }
        public bool IsSuccess
        {
            get { return status == 200 || status == 201; }
        }
        public string JsonResponse { get; set; }
    }
}
