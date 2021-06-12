using IdentityServer4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace Maid.Auth.API
{
	public class Startup
	{
		public Startup(IConfiguration configuration, IWebHostEnvironment environment) {
			Configuration = configuration;
			Environment = environment;
		}

		public IConfiguration Configuration { get; }
		public IWebHostEnvironment Environment { get; }

		public void ConfigureServices(IServiceCollection services) {
			string uiUrl = Configuration["UI_Url"];
			services.AddTransient<IReturnUrlParser, ReturnUrlParser>();
			services.AddControllers();
			services.AddDbContext<AppIdentityDbContext>(options => options.UseMySql(Configuration["Maid_Auth_ConnectionString"]));

			services.AddIdentity<AppUser, IdentityRole>()
				.AddEntityFrameworkStores<AppIdentityDbContext>()
				.AddDefaultTokenProviders();

			services.AddCors(setup => {
				setup.AddDefaultPolicy(policy => {
					policy.AllowAnyHeader();
					policy.AllowAnyMethod();
					policy.WithOrigins(uiUrl);
					policy.AllowCredentials();
				});
			});

			services.Configure<CookiePolicyOptions>(options => {
				options.CheckConsentNeeded = context => false;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			SetupIdentityServer(services, uiUrl);
		}

		private void SetupIdentityServer(IServiceCollection services, string uiUrl) {
			var isBuilder = services.AddIdentityServer((options) => {
				options.UserInteraction.LoginUrl = $"{uiUrl}/auth";
				options.UserInteraction.LogoutUrl = $"{uiUrl}/auth";
				options.Events.RaiseErrorEvents = true;
				options.Events.RaiseInformationEvents = true;
				options.Events.RaiseFailureEvents = true;
				options.Events.RaiseSuccessEvents = true;
			})
				.AddOperationalStore(options => {
					options.ConfigureDbContext = builder =>
						builder.UseMySql(Configuration["Maid_Auth_ConnectionString"],
						sql => sql.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name));
					options.EnableTokenCleanup = true;
					options.TokenCleanupInterval = 360;
				})
				.AddInMemoryPersistedGrants()
				.AddInMemoryIdentityResources(Config.GetIdentityResources())
				.AddInMemoryApiResources(Config.GetApiResources())
				.AddInMemoryClients(Config.GetClients(uiUrl))
				.AddAspNetIdentity<AppUser>();

			if (Environment.IsDevelopment()) {
				isBuilder.AddDeveloperSigningCredential();
			} else {
				string certificate = Configuration["Cert_Path"];
				string password = Configuration["Cert_Password"];
				var cert = new X509Certificate2(
				  certificate,
				  password,
				  X509KeyStorageFlags.MachineKeySet |
				  X509KeyStorageFlags.PersistKeySet |
				  X509KeyStorageFlags.Exportable
				);
				isBuilder.AddSigningCredential(cert);
			}
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory log) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
				log.CreateLogger("Trace");
			}

			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseAuthentication()
				.UseCookiePolicy(new CookiePolicyOptions {
					HttpOnly = HttpOnlyPolicy.Always,
					MinimumSameSitePolicy = SameSiteMode.None,
					Secure = CookieSecurePolicy.Always
				});

			app.UseCors();
			app.UseIdentityServer();

			app.UseEndpoints(endpoints => {
				endpoints.MapControllers();
			});
		}
	}
}