using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paydock_dotnet_sdk.Models
{
    public class ErrorResponse
    {
        public int Status { get; set; }
        public string ErrorMessage { get; set; }
        public dynamic ExtendedInformation { get; set; }
        public string JsonResponse { get; set; }
    }
}
