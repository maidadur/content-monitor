using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Maid.Auth.API
{
	public class Startup
	{
		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services) {
			services.AddControllers();
			services.AddDbContext<AppIdentityDbContext>(options => options.UseMySql(Configuration.GetConnectionString("Maid_Auth_ConnectionString")));

			services.AddIdentity<AppUser, IdentityRole>()
			  .AddEntityFrameworkStores<AppIdentityDbContext>()
			  .AddDefaultTokenProviders();

			services.AddIdentityServer().AddDeveloperSigningCredential()
			   .AddOperationalStore(options => {
				   options.ConfigureDbContext = builder => builder.UseMySql(Configuration.GetConnectionString("Maid_Auth_ConnectionString"));
				   options.EnableTokenCleanup = true;
				   options.TokenCleanupInterval = 30;
			   })
			   .AddInMemoryIdentityResources(Config.GetIdentityResources())
			   .AddInMemoryApiResources(Config.GetApiResources())
			   .AddInMemoryClients(Config.GetClients())
			   .AddAspNetIdentity<AppUser>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();
			app.UseRouting();
			//app.UseAuthorization();

			app.UseCors("AllowAll");
			app.UseIdentityServer();

			app.UseEndpoints(endpoints => {
				endpoints.MapControllers();
			});
		}
	}
}
