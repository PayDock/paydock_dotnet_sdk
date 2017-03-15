using Paydock_dotnet_sdk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paydock_dotnet_sdk.Services
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
