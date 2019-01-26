namespace MaidAPI
{
	using MaidAPI.Html;
	using MaidAPI.Manga;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	public class Startup
	{
		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
			services.AddTransient<IHtmlDocumentLoader, HtmlDocumentLoader>();
			services.AddTransient<IParsersFactory, ParsersFactory>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			} else {
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseMvc();
		}
	}
}
