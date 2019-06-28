namespace Paydock_dotnet_sdk.Models
{
	public class VaultRequest
	{
		public string token { get; set; }
		public string card_name { get; set; }
		public string card_number { get; set; }
		public string expire_year { get; set; }
		public string expire_month { get; set; }
		public string account_name { get; set; }
		public string account_number { get; set; }
		public string account_bsb { get; set; }
	}
}
