using Lib.Net.Http.WebPush;
using Lib.Net.Http.WebPush.Authentication;
using Maid.Notifications.Api.Entities;
using Maid.Notifications.DB;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Maid.Notifications.Api.NotificationClients
{
	public class WebPushClient : INotificationClient
	{
		private readonly IConfiguration _configuration;
		private readonly PushServiceClient _pushClient;

		public WebPushClient(
				IConfiguration configuration,
				PushServiceClient pushClient) {
			_configuration = configuration;
			_pushClient = pushClient;
			_pushClient.DefaultAuthentication = new VapidAuthentication(
				_configuration["Push_Public_Key"], 
				_configuration["Push_Private_Key"]) {
					Subject = _configuration["Ui_Url"]
			};
		}

		public async Task SendNotificationsAsync(Subscription subscription, Notification notification) {
			PushMessage message = notification.ToPushMessage();
			var pushSubscription = subscription.GetPushSubscription();
			await _pushClient.RequestPushMessageDeliveryAsync(pushSubscription, message);
		}
	}
}
