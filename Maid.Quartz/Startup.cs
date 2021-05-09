using Maid.RabbitMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using Schedule.WebApiCore.Sample.Schedule;
using System;

namespace Maid.Quartz
{
	public class Startup
	{
		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {
			services.AddLogging();

			services.Add(new ServiceDescriptor(typeof(IJob), typeof(LoadMangaJob), ServiceLifetime.Transient));
			services.AddSingleton<IJobFactory, ScheduledJobFactory>();
			services.AddTransient(provider => {
				return JobBuilder.Create<LoadMangaJob>()
				  .WithIdentity("LoadManga.job", "MangaGroup")
				  .Build();
			});

			services.AddTransient(provider => {
				return TriggerBuilder.Create()
					.WithIdentity($"LoadManga.trigger", "MangaGroup")
					.StartNow()
					.WithSimpleSchedule
					 (s =>
						s.WithInterval(TimeSpan.FromSeconds(Convert.ToInt32(Configuration["LoadMangaIntervalSeconds"] ?? (60 * 3).ToString())))
						.RepeatForever()
					 )
					 .Build();
			});

			services.AddTransient(provider => {
				var schedulerFactory = new StdSchedulerFactory();
				var scheduler = schedulerFactory.GetScheduler().Result;
				scheduler.JobFactory = provider.GetService<IJobFactory>();
				scheduler.Start();
				return scheduler;
			});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IScheduler scheduler) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			} else {
				app.UseHsts();
			}

			app.UseHttpsRedirection();

			MessageQueuesManager.Instance
				.Init(app.ApplicationServices)
				.ConnectToQueue("quartz");

			scheduler.ScheduleJob(app.ApplicationServices.GetService<IJobDetail>(), app.ApplicationServices.GetService<ITrigger>());
		}
	}
}
