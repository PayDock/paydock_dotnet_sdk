namespace Paydock.Net.Sdk.Models
{
    public class ExternalCheckoutResponse : Response
    {
        public Resource resource { get; set; }
        
        public class Resource
        {
            public string type { get; set; }
            public Data data { get; set; }
        }

        public class Data
        {
            public string checkout_type { get; set; }
            public string link { get; set; }
            public string reference_id { get; set; }
            public string mode { get; set; }
            public string token { get; set; }
        }


    }
}
