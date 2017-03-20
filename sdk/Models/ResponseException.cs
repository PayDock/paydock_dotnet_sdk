using System;

namespace Paydock_dotnet_sdk.Models
{
    public class ResponseException : Exception
    {
        public ErrorResponse ErrorResponse { get; private set; }

        public ResponseException(ErrorResponse errorResponse, string error)
            : base(error)
        {
            this.ErrorResponse = errorResponse;
        }
    }
}
