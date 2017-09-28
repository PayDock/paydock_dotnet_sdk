namespace Paydock_dotnet_sdk.Models
{
    public class ChargeRequestStripeConnect : ChargeRequestBase
	{
		public MetaData meta;
	}

	public class MetaData
	{
		public string stripe_direct_account_id;
		public string stripe_destination_account_id;
		public string stripe_transfer_group;
		public Transfer[] stripe_transfer;
	}

	public class Transfer
	{
		public decimal amount;
		public string currency;
		public string destination;
	}
}