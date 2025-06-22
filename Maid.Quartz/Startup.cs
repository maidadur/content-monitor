using Maid.Core.Utilities;
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
	public class JobSchedule
	{
		public Type JobType { get; }
		public ITrigger Trigger { get; }

		public JobSchedule(Type jobType, ITrigger trigger) {
			JobType = jobType;
			Trigger = trigger;
		}
	}

	public class Startup
	{
		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IScheduler scheduler) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
				app.UseHttpsRedirection();
			}

			_ = TaskUtils.RepeatActionUntilSuccess(() => {
				MessageQueuesManager.Instance
					.Init(app.ApplicationServices, Configuration["Maid_RabbitMQ_Host"], int.Parse(Configuration["Maid_RabbitMQ_Port"]))
					.ConnectToQueue("quartz")
					.ConnectToQueue("quartz_binance_trades", true)
					.ConnectToQueue("quartz_binance_order_ai_summary");

				var jobSchedules = app.ApplicationServices.GetServices<JobSchedule>();

				foreach (var schedule in jobSchedules) {
					var jobDetail = JobBuilder.Create(schedule.JobType)
						.WithIdentity($"{schedule.JobType.Name}.job")
						.Build();

					scheduler.ScheduleJob(jobDetail, schedule.Trigger);
				}
			});
		}

		public void ConfigureServices(IServiceCollection services) {
			services.AddLogging();

			// Register jobs
			services.AddTransient<LoadContentJob>();
			services.AddTransient<LoadTradesJob>();
			services.AddTransient<GenerateOrderSummaryJob>();

			services.AddSingleton<IJobFactory, ScheduledJobFactory>();
			services.AddTransient<IMessageClient, MessageClient>();

			// Register JobSchedules
			services.AddSingleton(new JobSchedule(
				typeof(LoadContentJob),
				TriggerBuilder.Create()
					.WithIdentity("LoadContent.trigger")
					.StartNow()
					.WithSimpleSchedule(s => s.WithInterval(TimeSpan.FromSeconds(Convert.ToInt32(Configuration["LoadContentIntervalSeconds"]))).RepeatForever())
					.Build()
			));

			services.AddSingleton(new JobSchedule(
				typeof(LoadTradesJob),
				TriggerBuilder.Create()
					.WithIdentity("LoadTrades.trigger")
					.StartNow()
					.WithSimpleSchedule(s => s.WithInterval(TimeSpan.FromSeconds(Convert.ToInt32(Configuration["LoadTradesIntervalSeconds"]))).RepeatForever())
					.Build()
			));

			services.AddSingleton(new JobSchedule(
				typeof(GenerateOrderSummaryJob),
				TriggerBuilder.Create()
					.WithIdentity("GenerateOrderSummary.trigger")
					.StartNow()
					.WithSimpleSchedule(s => s.WithInterval(TimeSpan.FromSeconds(Convert.ToInt32(Configuration["GenerateOrderSummaryIntervalSeconds"]))).RepeatForever())
					.Build()
			));

			services.AddTransient(provider => {
				var schedulerFactory = new StdSchedulerFactory();
				var scheduler = schedulerFactory.GetScheduler().Result;
				scheduler.JobFactory = provider.GetService<IJobFactory>();
				scheduler.Start();
				return scheduler;
			});
		}
	}
}
