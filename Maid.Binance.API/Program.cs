
using Maid.Binance.DB;
using Maid.ChatGPT;
using Maid.Core;
using Maid.Core.DB;
using Maid.Core.Exceptions;
using Maid.Core.Utilities;
using Maid.RabbitMQ;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;

namespace Maid.Binance.API
{
	public class Program
	{

		private static void SetupServicesBindings(WebApplicationBuilder builder) {
			builder.Services.AddTransient<IBinanceRequestHandler, BinanceRequestHandler>();
			builder.Services.AddTransient<IBinanceLoadDataTask, BinanceLoadDataTask>();
			builder.Services.AddTransient<IBinanceTradesLoader, BinanceTradesLoader>();
			builder.Services.AddTransient<IBinanceTradesRequest, BinanceTradesRequest>();
			builder.Services.AddTransient<IBinanceTradesRequest, BinanceTradesRequest>();
			builder.Services.AddTransient<LoadBinanceTradesQuartzSubscriber, LoadBinanceTradesQuartzSubscriber>();
			builder.Services.AddTransient<IEntityRepository<BinanceTrade>, EntityRepository<BinanceTrade>>();
			builder.Services.AddTransient<IEntityRepository<BinanceOrder>, EntityRepository<BinanceOrder>>();
			builder.Services.AddTransient<IEntityRepository<BinanceOrderTag>, EntityRepository<BinanceOrderTag>>();
			builder.Services.AddTransient<IEntityRepository<BinanceOrderEmotion>, EntityRepository<BinanceOrderEmotion>>();
			builder.Services.AddTransient<IEntityRepository, EntityRepository>();
			builder.Services.AddTransient<SaveImageToEntityTask, SaveImageToEntityTask>();
			builder.Services.AddTransient<SaveImageToEntitySubscriber, SaveImageToEntitySubscriber>();
			builder.Services.AddTransient<GenerateOrderAISummarySubscriber, GenerateOrderAISummarySubscriber>();
			builder.Services.AddTransient<IGenerateOrderAISummaryTask, GenerateOrderAISummaryTask>();
			builder.Services.AddTransient<IMessageClient, MessageClient>();
			builder.Services.AddTransient<ChatGptClient, ChatGptClient>((client) =>
				new ChatGptClient(builder.Configuration["ChatGPTApiKey"])
			);
		}

		private static void SetupDbServices(WebApplicationBuilder builder) {
			var connection = builder.Configuration["Maid_Binance_ConnectionString"];
			builder.Services.AddDbContext<BinanceDbContext>(options =>
				options.UseMySql(connection, ServerVersion.AutoDetect(connection))
			);
			builder.Services.AddScoped<DbContext, BinanceDbContext>();
		}

		public static void Main(string[] args) {
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
					.AddMicrosoftIdentityWebApi(options => {
						builder.Configuration.Bind("AzureAdB2C", options);

						//options.TokenValidationParameters.NameClaimType = "name";
					},
			options => { builder.Configuration.Bind("AzureAdB2C", options); });

			SetupServicesBindings(builder);
			SetupDbServices(builder);

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment()) {
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseMiddleware<ExceptionMiddleware>();

			_ = TaskUtils.RepeatActionUntilSuccess(() => {
				MessageQueuesManager.Instance
						.Init(app.Services, builder.Configuration["Maid_RabbitMQ_Host"], int.Parse(builder.Configuration["Maid_RabbitMQ_Port"]))
						.ConnectToQueue("quartz_binance_trades")
						.ConnectToQueue("quartz_binance_order_ai_summary")
						.ConnectToQueue("save_image_binance")
						.ConnectToQueue("load_image_binance")
						.Subscribe<SaveImageToEntitySubscriber>("load_image_binance")
						.Subscribe<GenerateOrderAISummarySubscriber>("quartz_binance_order_ai_summary")
						.Subscribe<LoadBinanceTradesQuartzSubscriber>("quartz_binance_trades");
			});

			string uiUrl = builder.Configuration["UI_Url"];
			app.UseCors(c => {
				c.AllowAnyHeader();
				c.AllowAnyMethod();
				c.WithOrigins(uiUrl);
			});
			app.UseAuthentication();
			app.UseAuthorization();



			app.UseHttpsRedirection();

			app.MapControllers();

			app.Run();
		}
	}
}
