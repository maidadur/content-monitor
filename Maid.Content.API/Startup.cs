namespace Maid.Content.API
{
	using Maid.Core;
	using Maid.Core.DB;
	using Maid.Core.Exceptions;
	using Maid.Content;
	using Maid.Content.DB;
	using Maid.Content.Html;
	using Maid.RabbitMQ;
	using Microsoft.AspNetCore.Authentication.JwtBearer;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Hosting;
	using Microsoft.Identity.Web;
	using Microsoft.IdentityModel.Logging;
	using System;
	using System.Reflection;
	using Maid.Content.API.Messaging;
	using Maid.Content.Content;

	public class Startup
	{
		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		private void SetupDbServices(IServiceCollection services) {
			var connection = Configuration["MaidContent_ConnectionString"];
			services.AddDbContext<ContentDbContext>(options =>
				options.UseMySql(connection, ServerVersion.AutoDetect(connection))
			);
			services.AddScoped<DbContext, ContentDbContext>();
		}

		private void SetupServicesBindings(IServiceCollection services) {
			services.AddTransient<IHtmlDocumentLoader, HtmlDocumentLoader>();
			services.AddTransient<IParsersFactory, ParsersFactory>();
			services.AddTransient<IEntityRepository<ContentInfo>, EntityRepository<ContentInfo>>();
			services.AddTransient<IEntityRepository<ContentSource>, EntityRepository<ContentSource>>();
			services.AddTransient<IEntityRepository<ContentItemInfo>, EntityRepository<ContentItemInfo>>();
			services.AddTransient<IEntityRepository<ContentItemNotification>, EntityRepository<ContentItemNotification>>();
			services.AddTransient<IEntityRepository, EntityRepository>();
			services.AddTransient<IContentLoader, ContentLoader>();
			services.AddTransient<ConfigHelper, ConfigHelper>();
			services.AddTransient<ContentLoadTask, ContentLoadTask>();
			services.AddTransient<LoadContentQuartzSubscriber, LoadContentQuartzSubscriber>();
			services.AddTransient<SaveImageToEntitySubscriber, SaveImageToEntitySubscriber>();
			services.AddTransient<IMessageClient, MessageClient>();
			services.AddTransient<SaveImageToEntityTask, SaveImageToEntityTask>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			if (env.IsDevelopment()) {
				Console.WriteLine("env.IsDevelopment()");
				app.UseDeveloperExceptionPage();
				app.UseHttpsRedirection();
				IdentityModelEventSource.ShowPII = true;
			} 
			//else {
			//	app.UseHsts();
			//}

			//app.UseExceptionHandler(c => c.Run(async context =>
			//{
			//	var exception = context.Features
			//		.Get<IExceptionHandlerPathFeature>()
			//		.Error;
			//	await context.Response.WriteAsync(exception.Message + "\n" + exception.StackTrace);
			//}));

			app.UseRouting();
			app.UseCors();
			
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints => {
				endpoints.MapControllers();
			});

			LookupTypeManager.Instance.LoadLookupTypes(Assembly.GetAssembly(typeof(ContentDbContext)));

			app.UseMiddleware<ExceptionMiddleware>();

			try {
				MessageQueuesManager.Instance
						.Init(app.ApplicationServices, Configuration["Maid_RabbitMQ_Host"], int.Parse(Configuration["Maid_RabbitMQ_Port"]))
						.ConnectToQueue("quartz")
						.ConnectToQueue("notifications")
						.ConnectToQueue("save_image")
						.ConnectToQueue("load_image")
						.Subscribe<SaveImageToEntitySubscriber>("load_image")
						.Subscribe<LoadContentQuartzSubscriber>("quartz");
			} catch {
				Console.WriteLine("Error. Could not connect to RabbitMQ");
			}
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {
			string uiUrl = Configuration["UI_Url"];

			services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

			SetupServicesBindings(services);
			SetupDbServices(services);

			// IdentityServer4 auth
			//services.AddAuthentication("Bearer")
			//	.AddJwtBearer("Bearer", options => {
			//		options.Authority = Configuration["Authority"];
			//		options.Audience = "client";
			//	});

			// AzureB2C auth
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
						.AddMicrosoftIdentityWebApi(options => {
							Configuration.Bind("AzureAdB2C", options);

							//options.TokenValidationParameters.NameClaimType = "name";
						},
				options => { Configuration.Bind("AzureAdB2C", options); });

			services.AddCors(setup => {
				setup.AddDefaultPolicy(policy => {
					policy.AllowAnyHeader();
					policy.AllowAnyMethod();
					policy.WithOrigins(uiUrl);
				});
			});

			services.AddLogging();
		}
	}
}