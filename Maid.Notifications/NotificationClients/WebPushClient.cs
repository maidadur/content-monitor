using Lib.Net.Http.WebPush;
using Lib.Net.Http.WebPush.Authentication;
using Maid.Notifications.DB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Maid.Notifications
{
	public class WebPushClient : INotificationClient
	{
		private readonly IConfiguration _configuration;
		private readonly ILogger<WebPushClient> _logger;
		private readonly PushServiceClient _pushClient;

		public WebPushClient(
				IConfiguration configuration,
				PushServiceClient pushClient,
				ILogger<WebPushClient> logger) {
			_configuration = configuration;
			_pushClient = pushClient;
			_logger = logger;
			_pushClient.DefaultAuthentication = new VapidAuthentication(
				_configuration["Push_Public_Key"],
				_configuration["Push_Private_Key"]) {
				Subject = _configuration["Ui_Url"]
			};
		}

		public async Task SendNotificationsAsync(Subscription subscription, Notification notification) {
			PushMessage message = notification.ToPushMessage();
			var pushSubscription = subscription.GetPushSubscription();
			_logger.LogInformation($"Sending push notification to {subscription.Id} with content: {notification.Title}");
			try {
				await _pushClient.RequestPushMessageDeliveryAsync(pushSubscription, message);
			} catch (Exception ex) {
				_logger.LogError(ex, "Error while sending push notification");
			}
		}
	}
}