# Welcome to Paydock dot net SDK

This SDK provides a wrapper around the PayDock REST API.

For more info on the Paydock API, see our [full documentation](https://docs.paydock.com).

In order to support the different ways our customers use PayDock, we've released 2 versions of the SDK:
* .Net 4.0 SDK (for pre .NET 4.5.1)
* .Net Standard SDK - with async support (for net451+ and netcoreapp1.0+)

## SDK Usage

Get the SDK assembly from [nuget](https://www.nuget.org/packages/PaydockSdk/)

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
    var result = new Customers().Add(request);

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

# Parsing a webhook

Webhooks are POSTed to the URL, once you've captured the payload, you can parse this:

``` C#
// transaction webhook
var tran = (new Webhook()).Parse<TransactionWebhook>(tranJson);

// subscription webhook
var subscription = (new Webhook()).Parse<SubscriptionWebhook>(subscriptionJson);
```

The different webhook types map to different parsing function:
* Transaction Success -> Webhook.ParseTransaction()
* Transaction by Subscription Success -> Webhook.ParseTransaction()
* Transaction by Subscription Failed -> Webhook.ParseTransaction()
* Subscription Creation Success -> Webhook.ParseSubscription()
* Subscription Finished -> Webhook.ParseSubscription()
* Subscription Updated -> Webhook.ParseSubscription()
* Subscription Failed -> Webhook.ParseSubscription()
* Refund Requested -> Webhook.ParseRefund()
* Refund Success -> Webhook.ParseRefund()
* Refund Failure -> Webhook.ParseRefund()
* Card Expiration Warning -> Webhook.ParseCardExpiry()

## .Net Core SDK Usage

The best way to get this is on [nuget](https://www.nuget.org/packages/PaydockSdk.Core/)

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
    var result = await new Charges().Add(charge);

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
    var result = await Customers().Add(request);

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
    var result = await new Charges("<this is a different secret key>").Add(charge);

    if (!result.IsSuccess) {
        // handle failed payment
    }
}
catch (ResponseException ex)
{
    // handle possible error
}
```

# Parsing a webhook

Webhooks are POSTed to the URL, once you've captured the payload, you can parse this:

``` C#
// transaction webhook
var tran = (new Webhook()).Parse<TransactionWebhook>(tranJson);

// subscription webhook
var subscription = (new Webhook()).Parse<SubscriptionWebhook>(subscriptionJson);
```

The different webhook types map to different data objects:
* Transaction Success -> Webhook.Parse<TransactionWebhook>()
* Transaction by Subscription Success -> Webhook.Parse<TransactionWebhook>()
* Transaction by Subscription Failed -> Webhook.Parse<TransactionWebhook>()
* Subscription Creation Success -> Webhook.Parse<SubscriptionWebhook>()
* Subscription Finished -> Webhook.Parse<SubscriptionWebhook>()
* Subscription Updated -> Webhook.Parse<SubscriptionWebhook>()
* Subscription Failed -> Webhook.Parse<SubscriptionWebhook>()
* Refund Requested -> Webhook.Parse<TransactionWebhook>()
* Refund Success -> Webhook.Parse<TransactionWebhook>()
* Refund Failure -> Webhook.Parse<TransactionWebhook>()
* Card Expiration Warning -> Webhook.Parse<CardExpirationWebhook>()
