using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paydock_dotnet_sdk.Models
{
    public class Response
    {
        public int status { get; set; }
        public string error { get; set; }
        public bool IsSuccess
        {
            get { return status == 200 || status == 201; }
        }
    }
}
