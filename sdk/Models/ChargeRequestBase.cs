﻿using System.Collections.Generic;

namespace Paydock_dotnet_sdk.Models
{
    public abstract class ChargeRequestBase
    {
        public decimal amount { get; set; }
        public string currency { get; set; }
        public string token { get; set; }
        public string reference { get; set; }
        public string reference2 { get; set; }
        public string description { get; set; }
        public string descriptor { get; set; }
        public string customer_id { get; set; }
		public string payment_source_id { get; set; }
        public string fraud_charge_id { get; set; }
        public bool? bypass_3ds { get; set; }
        public string _3ds_charge_id { get; set; }
        public string initialization_source { get; set; }
        public Customer customer { get; set; }
        public ThreeDSecure  _3ds { get; set; }
        public FraudData fraud { get; set; }
        public ShippingData shipping { get; set; }
        
    }
}