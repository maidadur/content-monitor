namespace MaidAPI
{
	using Maid.Core;
	using Maid.Core.DB;
	using Maid.Manga;
	using Maid.Manga.DB;
	using Maid.Manga.Html;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using System.Reflection;

	public class Startup
	{
		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		private void SetupDbServices(IServiceCollection services) {
			var connection = Configuration["DbConnectionString"];
			services.AddDbContext<MangaDbContext>(options => 
				options
					.UseSqlServer(connection)
			);
			services.AddScoped<DbContext, MangaDbContext>();
			services.AddScoped<IMangaDbContext, MangaDbContext>();
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
			services.AddTransient<IHtmlDocumentLoader, HtmlDocumentLoader>();
			services.AddTransient<IParsersFactory, ParsersFactory>();
			services.AddTransient<IEntityRepository<MangaInfo>, EntityRepository<MangaInfo>>();
			services.AddTransient<IEntityRepository<MangaSource>, EntityRepository<MangaSource>>();
			services.AddTransient<IEntityRepository<MangaChapterInfo>, EntityRepository<MangaChapterInfo>>();
			services.AddTransient<IEntityRepository, EntityRepository>();
			services.AddTransient<IMangaLoader, MangaLoader>();
			services.AddTransient<ConfigHelper, ConfigHelper>();
			SetupDbServices(services);
			LookupTypeManager.Instance.LoadLookupTypes(Assembly.GetAssembly(typeof(MangaDbContext)));
			services.AddCors(options =>
			{
				options.AddPolicy("AllowOrigin",
					builder => {
						builder
							.WithOrigins("http://localhost:4200")
							.AllowAnyHeader()
							.AllowAnyMethod();
					}
				);
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			} else {
				app.UseHsts();
			}

			app.UseCors();
			app.UseHttpsRedirection();
			app.UseMvc();
		}
	}
}
