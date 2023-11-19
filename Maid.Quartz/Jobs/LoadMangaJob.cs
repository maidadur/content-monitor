namespace Schedule.WebApiCore.Sample.Schedule
{
	using Maid.RabbitMQ;
	using Microsoft.Extensions.Logging;
	using Quartz;
	using System;
	using System.Threading.Tasks;

	public class LoadContentJob : IJob
	{
		private readonly IMessageClient _client;
		private readonly ILogger<LoadContentJob> _logger;

		public LoadContentJob(ILogger<LoadContentJob> logger, IMessageClient messageClient) {
			_logger = logger;
			_client = messageClient;
		}

		public async Task Execute(IJobExecutionContext context) {
			_logger.LogInformation($">> LoadContentJob fired:  {DateTime.Now}");
			_client.SendMessage("quartz", null);
			_logger.LogInformation($">> LoadContentJob published message");
			await Task.CompletedTask;
		}
	}
}