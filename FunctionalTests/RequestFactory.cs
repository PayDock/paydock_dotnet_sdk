using Paydock_dotnet_sdk.Models;
using System.Collections.Generic;

namespace FunctionalTests
{
	public static class RequestFactory
	{
		public static ChargeRequest CreateChargeRequest(decimal amount, string gatewayId, string customerEmail = "test@test.com")
		{
			return new ChargeRequest
			{
				amount = amount,
				currency = "AUD",
				customer = new Customer
				{
					email = customerEmail,
					payment_source = new PaymentSource
					{
						gateway_id = gatewayId,
						card_name = "Test Nametest",
						card_number = "4242424242424242",
						card_ccv = "123",
						expire_month = "12",
						expire_year = "2021"
					}
				},
				custom_fields = new {
					key1 = "customValue1",
					key2 = "customValue2"
				},
				fraud = new FraudData
				{
					token = "devicetoken",
					data = new
					{
						transaction = new 
						{
							transactionId = "transactionID",
							cncStoreID = "123123",
							billing = new
							{
								billingEmail = "johnsmith@email.com"
							}
						}

					}
				}
			};
		}


		public static ChargeRequest Init3DSRequest(decimal amount, string token)
		{
			return new ChargeRequest
			{
				token = token,
				amount = amount,
				currency = "AUD",
				_3ds = new ThreeDSecure
				{
					browser_details = new Dictionary<string, string> 
					{
						{ "name","CHROME" },
						{ "java_enabled","true" },
						{ "language","en-US" },
						{ "screen_height","640" },
						{ "screen_width","480" },
						{ "time_zone", "273" },
						{ "color_depth", "24" }
					}
				}
			};
		}

		public static ChargeRequest CreateChargeRequest3DS(string chargeId)
		{
			return new ChargeRequest
			{
				_3ds = new ThreeDSecure
				{
					charge_id = chargeId
				}
			};
		}

		public static ChargeRequest CreateChargeRequest3DSwithUUID(string id)
		{
			return new ChargeRequest
			{
				amount = 10m,
				currency = "AUD",
				_3ds = new ThreeDSecure
				{
					id = id
				}
			};
		}

		public static CustomerRequest CreateCustomerRequest(string email = "")
		{
			return new CustomerRequest
			{
				first_name = "john",
				last_name = "smith",
				email = email,
				payment_source = new PaymentSource
				{
					gateway_id = TestConfig.GatewayId,
					card_name = "John Smith",
					card_number = "4111111111111111",
					card_ccv = "123",
					expire_month = "12",
					expire_year = "2021"
				}
			};
		}

		public static ExternalCheckoutRequest CreateExternalCheckoutRequest()
		{
			return new ExternalCheckoutRequest
			{
				gateway_id = TestConfig.PaypalGatewayId,
				mode = "test",
				type = "paypal",
				success_redirect_url = "http://success.com",
				error_redirect_url = "http://failure.com"
			};
		}

		public static GatewayRequest CreateGatewayRequest()
		{
			return new GatewayRequest
			{
				type = "Brain",
				name = "BraintreeTesting",
				merchant = "r7pcwvkbkgjfzk99",
				username = "n8nktcb42fy8ttgt",
				password = "c865e194d750148b93284c0c026e5f2a"
			};
		}

		public static NotificationTemplateRequest CreateNotificationTemplateRequest()
		{
			return new NotificationTemplateRequest
			{
				body = "body",
                label = "test",
                notification_event = NotificationEvent.card_expiration_warning,
                html = true
			};
		}

		public static NotificationTriggerRequest CreateNotificationTriggerRequest(string templateId)
		{
			return new NotificationTriggerRequest
			{
				type = NotificationTriggerType.email,
				destination = "email@email.com",
				template_id = templateId,
				eventTrigger = NotificationEvent.card_expiration_warning
			};
		}

		public static SubscriptionRequest CreateSubscriptionRequest()
		{
			return new SubscriptionRequest
			{
				amount = 20.0M,
				currency = "AUD",
				description = "this is a test",
				customer = new Paydock_dotnet_sdk.Models.Customer
				{
					first_name = "first",
					last_name = "last",
					email = "test@test.com",
					payment_source = new PaymentSource
					{
						gateway_id = TestConfig.GatewayId,
						card_name = "John Smith",
						card_number = "4111111111111111",
						card_ccv = "123",
						expire_month = "10",
						expire_year = "2020"
					}
				},
				schedule = new SubscriptionSchedule
				{
					interval = "month",
					frequency = 1
				}
			};
		}

		public static ChargeRequestStripeConnect CreateBasicStripeConnectCharge()
		{
			return new ChargeRequestStripeConnect
			{
				amount = 200.1M,
				currency = "AUD",
				customer = new Customer
				{
					payment_source = new PaymentSource
					{
						gateway_id = TestConfig.StripeGatewayId,
						card_name = "Test Name",
						card_number = "4111111111111111",
						card_ccv = "123",
						expire_month = "10",
						expire_year = "2020"
					}
				}
			};
		}

		public static VaultRequest CreateVaultRequest()
		{
			return new VaultRequest
			{
				card_name = "John Smith",
				card_number = "4111111111111111",
				expire_month = "10",
				expire_year = "2020"
			};
		}

		public static ChargeRequest CreateWalletRequest()
		{
			return new ChargeRequest
			{
				amount = 200.1M,
				currency = "AUD",
				customer = new Customer
				{
					first_name = "First",
					last_name = "Last",
					email = "test@test.com",
					phone = "+123456789",
					payment_source = new PaymentSource
					{
						gateway_id = TestConfig.FlypayGatewayId,
						
					}
				},
				meta = new Dictionary<string, string>()
				{
					{ "store_name", "123" },
					{ "store_id", "123" }
				}
			};
		}
	}
}
