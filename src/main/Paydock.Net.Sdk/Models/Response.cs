﻿namespace Paydock.Net.Sdk.Models
{
    public class Response
    {
        public int status { get; set; }
        public string error { get; set; }
        public bool IsSuccess
        {
            get { return status == 200 || status == 201; }
        }
        public string JsonResponse { get; set; }
    }
}
