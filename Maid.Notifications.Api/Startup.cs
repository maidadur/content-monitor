using Maid.Notifications.DB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
				options.UseMySql(connection)
			);
			services.AddScoped<INotificationsDbContext, NotificationsDbContext>();
		}

		public void ConfigureServices(IServiceCollection services) {
			services.AddControllers();
			services.AddLogging();
			SetupDbServices(services);
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseCors();

			app.UseAuthorization();
			//app.UseAuthentication();

			app.UseEndpoints(endpoints => {
				endpoints.MapControllers();
			});
		}
	}
}
