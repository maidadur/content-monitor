namespace Schedule.WebApiCore.Sample.Schedule
{
	using Maid.RabbitMQ;
	using Microsoft.Extensions.Logging;
	using Quartz;
	using System;
	using System.Threading.Tasks;

	public class LoadTradesJob : IJob
	{
		private readonly IMessageClient _client;
		private readonly ILogger<LoadTradesJob> _logger;

		public LoadTradesJob(ILogger<LoadTradesJob> logger, IMessageClient messageClient) {
			_logger = logger;
			_client = messageClient;
		}

		public async Task Execute(IJobExecutionContext context) {
			_logger.LogInformation($">> LoadTradesJob fired:  {DateTime.Now}");
			_client.SendMessage("quartz_binance_trades", null);
			_logger.LogInformation($">> LoadTradesJob published message");
			await Task.CompletedTask;
		}
	}
}
