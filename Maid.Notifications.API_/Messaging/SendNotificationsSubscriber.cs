using Maid.Core;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Maid.Notifications.Api
{
	public class SendNotificationsSubscriber : IMessageConsumer
	{
		private readonly ILogger<SendNotificationsSubscriber> _logger;
		private readonly ISendNotificationTask _sendNotificationTask;

		public SendNotificationsSubscriber(ISendNotificationTask sendNotificationTask,
				ILogger<SendNotificationsSubscriber> logger) {
			_sendNotificationTask = sendNotificationTask;
			_logger = logger;
		}

		public async Task ProcessAsync(byte[] data) {
			_logger.LogInformation("Received message from queue.");
			await _sendNotificationTask.SendNotificationFromTask(data);
		}
	}
}