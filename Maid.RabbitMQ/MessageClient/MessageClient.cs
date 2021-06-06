using Maid.Core;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Maid.RabbitMQ
{
	public class MessageClient : IMessageClient
	{
		private readonly ILogger<MessageClient> _logger;

		public MessageClient(ILogger<MessageClient> logger) {
			_logger = logger;
		}

		public void SendMessage(string queueName, object data) {
			string obj = "";
			if (data != null) {
				obj = JsonConvert.SerializeObject(data);
			}
			_logger.LogInformation($"Sending queue message to {queueName} with content: {obj}");
			MessageQueuesManager.Instance.Publish(queueName, obj.ToBytesArray());
		}
	}
}