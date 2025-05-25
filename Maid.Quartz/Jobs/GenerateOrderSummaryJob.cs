namespace Schedule.WebApiCore.Sample.Schedule
{
	using Maid.RabbitMQ;
	using Microsoft.Extensions.Logging;
	using Quartz;
	using System;
	using System.Threading.Tasks;

	public class GenerateOrderSummaryJob : IJob
	{
		private readonly IMessageClient _client;
		private readonly ILogger<LoadContentJob> _logger;

		public GenerateOrderSummaryJob(ILogger<LoadContentJob> logger, IMessageClient messageClient) {
			_logger = logger;
			_client = messageClient;
		}

		public async Task Execute(IJobExecutionContext context) {
			_logger.LogInformation($">> GenerateTradeSummaryJob fired:  {DateTime.Now}");
			_client.SendMessage("quartz_binance_order_ai_summary", null);
			_logger.LogInformation($">> GenerateTradeSummaryJob published message");
			await Task.CompletedTask;
		}
	}
}