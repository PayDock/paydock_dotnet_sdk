using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paydock_dotnet_sdk.Models
{
    public class Response
    {
        public int ResponseCode { get; set; }
        public string Error { get; set; }
        public bool IsSuccess
        {
            get { return ResponseCode == 200 || ResponseCode == 201; }
        }
    }
}
