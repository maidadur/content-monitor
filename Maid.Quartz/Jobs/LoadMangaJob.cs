namespace Schedule.WebApiCore.Sample.Schedule
{
	using Maid.RabbitMQ;
	using Microsoft.Extensions.Logging;
	using Quartz;
	using System;
	using System.Threading.Tasks;


	public class LoadMangaJob : IJob
	{
		private readonly ILogger<LoadMangaJob> logger;

		public LoadMangaJob(ILogger<LoadMangaJob> logger) {
			this.logger = logger;
		}

		public async Task Execute(IJobExecutionContext context) {
			logger.LogInformation($">> LoadMangaJob fired:  {DateTime.Now}");
			MessageQueuesManager.Instance.Publish("quartz", "");
			logger.LogInformation($">> LoadMangaJob published message");
			await Task.CompletedTask;
		}
	}
}
