namespace Schedule.WebApiCore.Sample.Schedule
{
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.Logging;
	using Quartz;
	using System;
	using System.Threading.Tasks;


	public class LoadMangaJob : IJob
	{
		private readonly IConfiguration configuration;
		private readonly ILogger<LoadMangaJob> logger;

		public LoadMangaJob(IConfiguration configuration, ILogger<LoadMangaJob> logger) {
			this.logger = logger;
			this.configuration = configuration;
		}

		public async Task Execute(IJobExecutionContext context) {
			logger.LogWarning($"Hello from scheduled task {DateTime.Now}");
			await Task.CompletedTask;

		}
	}
}
