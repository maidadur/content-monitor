namespace Schedule.WebApiCore.Sample.Schedule
{
	using Maid.RabbitMQ;
	using Microsoft.Extensions.Logging;
	using Quartz;
	using System;
	using System.Threading.Tasks;

	public class LoadMangaJob : IJob
	{
		private readonly IMessageClient _client;
		private readonly ILogger<LoadMangaJob> _logger;

		public LoadMangaJob(ILogger<LoadMangaJob> logger, IMessageClient messageClient) {
			_logger = logger;
			_client = messageClient;
		}

		public async Task Execute(IJobExecutionContext context) {
			_logger.LogInformation($">> LoadMangaJob fired:  {DateTime.Now}");
			_client.SendMessage("quartz", null);
			_logger.LogInformation($">> LoadMangaJob published message");
			await Task.CompletedTask;
		}
	}
}