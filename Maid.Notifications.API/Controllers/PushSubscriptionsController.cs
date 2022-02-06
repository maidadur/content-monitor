using Lib.Net.Http.WebPush;
using Maid.Core.DB;
using Maid.Notifications.DB;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Maid.Notifications.Api.Controllers
{
	[Route("api/subscriptions")]
	[ApiController]
	[EnableCors()]
	public class PushSubscriptionsController : ControllerBase
	{
		private readonly INotificationClient _notificationClient;
		private readonly IEntityRepository<Subscription> _pushSubscriptionsRepository;

		public PushSubscriptionsController(IEntityRepository<Subscription> pushSubscriptionsRepository,
				INotificationClient notificationClient) {
			_pushSubscriptionsRepository = pushSubscriptionsRepository;
			_notificationClient = notificationClient;
		}

		private async Task SendTestMessage(Subscription subscription) {
			await _notificationClient.SendNotificationsAsync(
				subscription,
				new Notification {
					Title = "Test title",
					Body = "Test message",
					Icon = "https://maidadur.com/assets/loli-maid.png",
					Vibrate = { 200, 100, 200 }
				});
		}

		[HttpDelete()]
		public void Delete([FromBody] PushSubscription subscription) {
			_pushSubscriptionsRepository.Delete(new Subscription(subscription));
			_pushSubscriptionsRepository.Save();
		}

		[HttpPost]
		public async Task Post([FromBody] PushSubscription pushSubscription) {
			if (pushSubscription == null) {
				throw new ArgumentNullException(nameof(pushSubscription));
			}
			var subscription = new Subscription(pushSubscription);
			_pushSubscriptionsRepository.Create(subscription);
			_pushSubscriptionsRepository.Save();
			await SendTestMessage(subscription);
		}
	}
}