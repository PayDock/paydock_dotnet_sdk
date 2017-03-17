# Welcome to Paydock dot net SDK

This SDK provides a wrapper around the PayDock REST API.

You can see our current documentation around the paydock API [here](https://docs.paydock.com).

## Usage

Simple example to create a single change.

``` C#
Config.Initialise(Paydock_dotnet_sdk.Services.Environment.Sandbox, "insert your secret key");

var charge = new ChargeRequest() // populate your charge object

try
{
    var result = new Charges().Add(charge);
}
catch (ResponseException ex)
{
    // handle possible error
}
```