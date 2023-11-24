namespace Maid.Storage.API
{
	using Maid.AzureStorage;
	using Maid.IStorage;
	using Maid.RabbitMQ;
	using Microsoft.Extensions.DependencyInjection;

	public class Program
	{

		private static void SetupServicesBindings(WebApplicationBuilder builder) {
			builder.Services.AddTransient<IStorageProvider, AzureStorageProvider>((storageProvider) => 
				new AzureStorageProvider(
					builder.Configuration["AzureConnectionString"], 
					builder.Configuration["AzureStorageAccountName"], 
					builder.Configuration["AzureContainerName"]
				)
			);
			builder.Services.AddTransient<SaveImageToStorageSubscriber, SaveImageToStorageSubscriber>();
			builder.Services.AddTransient<SaveImageToStorageTask, SaveImageToStorageTask>();
		}

		public static void Main(string[] args) {
			var builder = WebApplication.CreateBuilder(args);

			builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
			builder.Configuration.AddEnvironmentVariables();

			builder.Services.AddControllers();

			SetupServicesBindings(builder);

			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			if (app.Environment.IsDevelopment()) {
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.MapControllers();


			try {
				MessageQueuesManager.Instance
						.Init(app.Services, builder.Configuration["Maid_RabbitMQ_Host"], int.Parse(builder.Configuration["Maid_RabbitMQ_Port"]))
						.ConnectToQueue("save_image")
						.ConnectToQueue("load_image")
						.Subscribe<SaveImageToStorageSubscriber>("save_image");
			} catch {
				Console.WriteLine("Error. Could not connect to RabbitMQ");
			}

			app.Run();
		}
	}
}