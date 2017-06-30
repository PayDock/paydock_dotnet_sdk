using System;

namespace Paydock.Net.Sdk.Models
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
