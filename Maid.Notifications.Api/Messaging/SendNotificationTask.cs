using Maid.Core;
using Maid.Core.DB;
using Maid.Notifications.DB;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Maid.Notifications.Api
{
	public class SendNotificationTask : ISendNotificationTask
	{
		public SendNotificationTask(INotificationClient pushClient,
				IEntityRepository<Subscription> pushSubscriptionsRepository) {
			PushClient = pushClient;
			PushSubscriptionsRepository = pushSubscriptionsRepository;
		}

		public INotificationClient PushClient { get; }
		public IEntityRepository<Subscription> PushSubscriptionsRepository { get; }

		public async Task SendNotificationFromTask(byte[] data) {
			string strData = System.Text.Encoding.UTF8.GetString(data);
			Notification notification = JsonConvert.DeserializeObject<Notification>(strData);
			var subscriptions = await PushSubscriptionsRepository.GetAllAsync();
			await subscriptions.ForEachAsync(async (subscription) => {
				await PushClient.SendNotificationsAsync(subscription, notification);
			});
		}
	}
}