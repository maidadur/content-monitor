using Maid.Core;
using Maid.Core.DB;
using Maid.Notifications.DB;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Maid.Notifications.Api
{
	public class SendNotificationTask : ISendNotificationTask
	{
		private readonly ILogger<SendNotificationTask> _logger;

		public SendNotificationTask(INotificationClient pushClient,
				IEntityRepository<Subscription> pushSubscriptionsRepository,
				ILogger<SendNotificationTask> logger
				) {
			PushClient = pushClient;
			PushSubscriptionsRepository = pushSubscriptionsRepository;
			_logger = logger;
		}

		public INotificationClient PushClient { get; }
		public IEntityRepository<Subscription> PushSubscriptionsRepository { get; }

		public async Task SendNotificationFromTask(byte[] data) {
			try {
				string strData = System.Text.Encoding.UTF8.GetString(data);
				Notification notification = JsonConvert.DeserializeObject<Notification>(strData);
				var subscriptions = await PushSubscriptionsRepository.GetAllAsync();
				await subscriptions.ForEachAsync(async (subscription) => {
					await PushClient.SendNotificationsAsync(subscription, notification);
				});
			} catch (Exception ex) {
				_logger.LogError($"{ex.Message}; {ex.StackTrace}");
			}
		}
	}
}