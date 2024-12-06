using System.Collections.Generic;
using Newtonsoft.Json.Converters;

namespace Paydock_dotnet_sdk.Models
{
    public class ThreeDSecure
    {
        public string id { get; set; }
        public string charge_id { get; set; }
        public string authentication_id { get; set; }
        public string token { get; set; }
        public string status { get; set; }
        public string version { get; set; }
        public bool? custom_implementation { get; set; }
        public string service_id { get; set; }
        public IReadOnlyDictionary<string, string> browser_details { get; set; }
        public Authentication authentication { get; set; }
        public Decoupled decoupled { get; set; }
        public Recurring recurring { get; set; }
        [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
        public ChallengePreference challenge_preference { get; set; }

    }

    public class Authentication
    {
        public string id { get; set; }
        public string account_id { get; set; }
        public string type { get; set; }
        public string challenge_type { get; set; }
        public string merchant_name { get; set; }
        public string previous_authentication_id { get; set; }

        public string date { get; set; }
        public string version { get; set; }
        public string method { get; set; }
        public string auth_data { get; set; }
        public AuthCustomer customer { get; set; }
        public Risk risk { get; set; }

        public bool? whitelisted { get; set; }
       
    }


    public class AuthCustomer
    {
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string credentials_updated_at { get; set; }
        public bool? suspicious { get; set; }
        public AuthCustomerPaymentSource payment_source { get; set; }
        public ActivityHistory activity_history { get; set; }

    }

    public class AuthCustomerPaymentSource
    {
        public string created_at { get; set; }
        public string[] add_attempts { get; set; }
        public string card_type { get; set; }
    }

    public class ActivityHistory
    {
        public string shipping_address_created_at { get; set; }
        public int? transactions_count_last_day { get; set; }
        public int? transactions_count_last_six_months { get; set; }
        public int? transactions_count_last_year { get; set; }
    }

    public class Risk
    {
        public decimal? redeem_amount { get; set; }
        public string redeem_currency { get; set; }
        public string pre_order_date { get; set; }
        public string reorder { get; set; }
        public string shipping { get; set; }
        public int? redeem_count { get; set; }
       
    }

    public class Decoupled
    {
        public string timeout { get; set; }
        public bool? enabled { get; set; }

    }

    public class Recurring
    {
        public string timeout { get; set; }
        public int? frequency_days { get; set; }

    }

    public enum ChallengePreference
    {
        CHALLENGE_MANDATED,           
        CHALLENGE_PREFERRED,          
        NO_CHALLENGE,                 
        NO_PREFERENCE,                
        REQUEST_TRUSTED_MERCHANT_LISTING
    }

}



/** 
   {
    "_3ds": {
        "authentication": {
          "account_id": string,
          "type": string,
          "challenge_type": string,
          "merchant_name": string,
          "previous_authentication_id": string,
          "date": string,
          "whitelisted": boolean,
          "version": string,
          "method": string,
          "auth_data": string,
          "customer": {
            "created_at": string,
            "updated_at": string,
            "credentials_updated_at": string,
            "suspicious": boolean,
            "payment_source": {
                "created_at": string,
                "add_attempts": []string,
                "card_type": string
            },
            "activity_history": {
                "transactions_count_last_day": number,
                "transactions_count_last_six_months": number,
                "transactions_count_last_year": number,
                "shipping_address_created_at": string
          },
          "risk": {
            "redeem_amount": string,
            "redeem_currency": string,
            "redeem_count": number,
            "pre_order_date": string,
            "reorder": string,
            "shipping": string
        },
        "decoupled": {
            "timeout": string,
            "enabled": boolean
        },
        "recurring": {
            "expiry": string,
            "frequency_days": number
        }
    },
        "service_id": MongoObjectId,
        "browser_details": {
            "screen_height": number,
            "screen_width": number
        }
    },
    "customer": {
        "first_name": string,
        "last_name": string,
        "email": string, 
        "phone": string,
        "phone2": string,
        "payment_source": {
            "vault_token": string,
            "address_city": string,
            "address_country": string,
            "address_line1": string,
            "address_line2": string,
            "address_line3": string,
            "address_postcode": string,
            "address_state": string
        }
    },
    "shipping": {
        "address_city": string,
        "address_country": string,
        "address_line1": string,
        "address_line2": string,
        "address_line3": string,
        "address_postcode": string,
        "address_state": string,
        "method": string,
        "contact": {
            "email": string,
            "first_name": string,
            "last_name": string
        }
    },
    
}

*/