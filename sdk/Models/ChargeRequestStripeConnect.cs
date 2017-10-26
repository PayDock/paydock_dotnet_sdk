namespace Paydock_dotnet_sdk.Models
{
    public class ChargeRequestStripeConnect : ChargeRequestBase
	{
		public MetaData meta;
		public Transfer transfer;
	}

	public class MetaData
	{
		public string stripe_direct_account_id;
		public decimal stripe_application_fee;
		public string stripe_destination_account_id;
		public decimal stripe_destination_amount;
	}

	public class Transfer
	{
		public string stripe_transfer_group;
		public TransferItems[] items;

		public class TransferItems
		{
			public decimal amount;
			public string currency;
			public string destination;
		}
	}
}