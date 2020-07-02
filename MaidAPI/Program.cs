namespace Maid.Manga.API
{
	using Microsoft.AspNetCore;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.Extensions.Configuration;

	public class Program
	{
		public static void Main(string[] args) {
			CreateWebHostBuilder(args).Build().Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
			.ConfigureAppConfiguration((context, config) => {
				config.AddJsonFile("webRequestsConfig.json", false, true);
				config.AddEnvironmentVariables(prefix: "Maid_");
			}).UseStartup<Startup>();
	}
}
