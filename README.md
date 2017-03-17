# Welcome to Paydock dot net SDK

This SDK provides a wrapper around the PayDock REST API.

You can see our current documentation around the paydock API [here](https://docs.paydock.com).

## Usage

``` C#
Config.Initialise(Paydock_dotnet_sdk.Services.Environment.Sandbox, "insert your secret key");

try
{
	var result = new Charges().Get(new GetChargeRequest { gateway_id = gatewayId });
}
catch (ResponseException ex)
{
	// handle possible error
}