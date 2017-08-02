using Paydock_dotnet_sdk.Models;

namespace FunctionalTests
{
	public static class RequestFactory
	{
		public static ChargeRequest CreateChargeRequest(decimal amount, string gatewayId, string customerEmail = "")
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
						card_name = "Test Name",
						card_number = "4111111111111111",
						card_ccv = "123",
						expire_month = "10",
						expire_year = "2020"
					}
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
					expire_month = "10",
					expire_year = "2020"
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
	}
}
