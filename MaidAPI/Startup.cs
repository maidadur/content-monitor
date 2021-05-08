namespace Maid.Manga.API
{
	using Maid.Core;
	using Maid.Core.DB;
	using Maid.Manga;
	using Maid.Manga.DB;
	using Maid.Manga.Html;
	using Maid.RabbitMQ;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Hosting;
	using System;
	using System.Reflection;

	public class Startup
	{
		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		private void SetupDbServices(IServiceCollection services) {
			var connection = Configuration["MaidManga_ConnectionString"];
			services.AddDbContext<MangaDbContext>(options =>
				options.UseMySql(connection)
			);
			services.AddScoped<DbContext, MangaDbContext>();
		}

		private void SetupServicesBindings(IServiceCollection services) {
			services.AddTransient<IHtmlDocumentLoader, HtmlDocumentLoader>();
			services.AddTransient<IParsersFactory, ParsersFactory>();
			services.AddTransient<IEntityRepository<MangaInfo>, EntityRepository<MangaInfo>>();
			services.AddTransient<IEntityRepository<MangaSource>, EntityRepository<MangaSource>>();
			services.AddTransient<IEntityRepository<MangaChapterInfo>, EntityRepository<MangaChapterInfo>>();
			services.AddTransient<IEntityRepository<MangaChapterNotification>, EntityRepository<MangaChapterNotification>>();
			services.AddTransient<IEntityRepository, EntityRepository>();
			services.AddTransient<IMangaLoader, MangaLoader>();
			services.AddTransient<ConfigHelper, ConfigHelper>();
			services.AddTransient<MangaLoadTask, MangaLoadTask>();
			services.AddTransient<LoadMangaQuartzSubscriber, LoadMangaQuartzSubscriber>();
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {
			services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
			SetupServicesBindings(services);
			SetupDbServices(services);
			services.AddCors(options => {
				options.AddPolicy("AllowOrigin",
					builder => {
						builder
							.WithOrigins("https://localhost", "https://localhost:4200")
							.AllowAnyHeader()
							.AllowAnyMethod();
					}
				);
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			} else {
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseCors();
			app.UseEndpoints(endpoints => {
				endpoints.MapControllers();
			});

			LookupTypeManager.Instance.LoadLookupTypes(Assembly.GetAssembly(typeof(MangaDbContext)));

			try {
				MessageQueuesManager.Instance
						.Init(app.ApplicationServices, Configuration["Maid_RabbitMQ_Host"], int.Parse(Configuration["Maid_RabbitMQ_Port"]))
						.ConnectToQueue("quartz")
						.Subsribe<LoadMangaQuartzSubscriber>("quartz");
			} catch {
				Console.WriteLine("Error. Could not connect to to RabbitMQ");
			}
		}
	}
}
