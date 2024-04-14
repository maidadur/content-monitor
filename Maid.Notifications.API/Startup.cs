using Lib.Net.Http.WebPush;
using Maid.Core.DB;
using Maid.Core.Exceptions;
using Maid.Core.Utilities;
using Maid.Notifications.DB;
using Maid.RabbitMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Maid.Notifications.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		private void SetupDbServices(IServiceCollection services) {
			var connection = Configuration["Maid_Push_ConnectionString"];
			services.AddDbContext<NotificationsDbContext>(options =>
				options.UseMySql(connection, ServerVersion.AutoDetect(connection))
			);
			services.AddScoped<INotificationsDbContext, NotificationsDbContext>();
			services.AddScoped<DbContext, NotificationsDbContext>();
			services.AddTransient<IEntityRepository<Subscription>, EntityRepository<Subscription>>();
			services.AddTransient<INotificationClient, WebPushClient>();
			services.AddTransient<PushServiceClient, PushServiceClient>();
			services.AddTransient<ISendNotificationTask, SendNotificationTask>();
			services.AddTransient<IMessageClient, MessageClient>();
			services.AddTransient<SendNotificationsSubscriber, SendNotificationsSubscriber>();

			services.AddCors(setup => {
				setup.AddDefaultPolicy(policy => {
					policy.AllowAnyHeader();
					policy.AllowAnyMethod();
					policy.WithOrigins(Configuration["Ui_Url"]);
				});
			});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			if (env.IsDevelopment()) {
				Console.WriteLine("env.IsDevelopment()");
				app.UseDeveloperExceptionPage();
				app.UseHttpsRedirection();
			}

			app.UseRouting();
			app.UseCors();

			app.UseAuthorization();
			//app.UseAuthentication();

			app.UseEndpoints(endpoints => {
				endpoints.MapControllers();
			});

			app.UseMiddleware<ExceptionMiddleware>();

			_ = TaskUtils.RepeatActionUntilSuccess(() => {
				MessageQueuesManager.Instance
						.Init(app.ApplicationServices, Configuration["Maid_RabbitMQ_Host"], int.Parse(Configuration["Maid_RabbitMQ_Port"]))
						.ConnectToQueue("notifications")
						.Subscribe<SendNotificationsSubscriber>("notifications");
			});
		}

		public void ConfigureServices(IServiceCollection services) {
			services.AddControllers();
			services.AddLogging();
			SetupDbServices(services);
		}
	}
}

