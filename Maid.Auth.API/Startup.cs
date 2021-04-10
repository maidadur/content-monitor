using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Maid.Auth.API
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));

			services.AddIdentity<AppUser, IdentityRole>()
			  .AddEntityFrameworkStores<AppIdentityDbContext>()
			  .AddDefaultTokenProviders();

			services.AddIdentityServer().AddDeveloperSigningCredential()
			   // this adds the operational data from DB (codes, tokens, consents)
			   .AddOperationalStore(options =>
			   {
				   options.ConfigureDbContext = builder => builder.UseSqlServer(Configuration.GetConnectionString("Default"));
				   // this enables automatic token cleanup. this is optional.
				   options.EnableTokenCleanup = true;
				   options.TokenCleanupInterval = 30; // interval in seconds
			   })
			   .AddInMemoryIdentityResources(Config.GetIdentityResources())
			   .AddInMemoryApiResources(Config.GetApiResources())
			   .AddInMemoryClients(Config.GetClients())
			   .AddAspNetIdentity<AppUser>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
