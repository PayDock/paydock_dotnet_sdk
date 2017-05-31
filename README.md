# Welcome to Paydock dot net SDK

This SDK provides a wrapper around the PayDock REST API.

For more info on the Paydock API, see our [full documentation](https://docs.paydock.com).

## Usage

# Simple example to create a single charge.

``` C#
var charge = new ChargeRequest
{
    amount = amount,
    currency = "AUD",
    customer = new Paydock_dotnet_sdk.Models.Customer
    {
        email = customerEmail,
        payment_source = new PaymentSource
        {
            gateway_id = "<your gateway id here>",
            card_name = "Test Name",
            card_number = "4111111111111111",
            card_ccv = "123",
            expire_month = "10",
            expire_year = "2020"
        }
    }
};

try
{
    Config.Initialise(Environment.Sandbox, "<your secret key here>", "<your public key here>");
    var result = new Charges().Add(charge);

    if (!result.IsSuccess) {
        // handle failed payment
    }
}
catch (ResponseException ex)
{
    // handle possible error
}
```

# Simple example to create a customer


``` C#
var customer = new CustomerRequest
{
    first_name = "john",
    last_name = "smith",
    email = "email@email.com",
    payment_source = new PaymentSource
    {
        gateway_id = TestConfig.GatewayId,
        card_name = "John Smith",
        card_number = "4111111111111111",
        card_ccv = "123",
        expire_month = "10",
        expire_year = "2020"
    }
};

try
{
    Config.Initialise(Environment.Sandbox, "<your secret key here>", "<your public key here>");
    var result = Customers().Add(request);

    if (!result.IsSuccess) {
        // handle failure to create customer
    }
}
catch (ResponseException ex)
{
    // handle possible error
}
```

# Use a different config token per request

In most cases, you'll be looking to use just one PayDock Account. However we support using multiple PayDock account, choosing which one you use when create the service classes.


``` C#
var charge = new ChargeRequest
{
    amount = amount,
    currency = "AUD",
    customer = new Paydock_dotnet_sdk.Models.Customer
    {
        email = customerEmail,
        payment_source = new PaymentSource
        {
            gateway_id = "<your gateway id here>",
            card_name = "Test Name",
            card_number = "4111111111111111",
            card_ccv = "123",
            expire_month = "10",
            expire_year = "2020"
        }
    }
};

try
{
    Config.Initialise(Environment.Sandbox, "<this is the default secret key>", "<your public key here>");
    var result = new Charges("<this is a different secret key>").Add(charge);

    if (!result.IsSuccess) {
        // handle failed payment
    }
}
catch (ResponseException ex)
{
    // handle possible error
}
```