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
	public class Startup
	{
		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IScheduler scheduler) {
			if (env.IsDevelopment()) {
				Console.WriteLine("env.IsDevelopment()");
				app.UseDeveloperExceptionPage();
				app.UseHttpsRedirection();
			}
			//else {
			//	app.UseHsts();
			//}
			_ = TaskUtils.RepeatActionUntilSuccess(() => {
				MessageQueuesManager.Instance
					.Init(app.ApplicationServices, Configuration["Maid_RabbitMQ_Host"], int.Parse(Configuration["Maid_RabbitMQ_Port"]))
					.ConnectToQueue("quartz")
					.ConnectToQueue("quartz_binance_trades")
					.ConnectToQueue("quartz_binance_order_ai_summary");
				scheduler.ScheduleJob(app.ApplicationServices.GetService<IJobDetail>(), app.ApplicationServices.GetService<ITrigger>());
			});
		}

		public void ConfigureServices(IServiceCollection services) {
			services.AddLogging();
			services.Add(new ServiceDescriptor(typeof(IJob), typeof(LoadContentJob), ServiceLifetime.Transient));
			services.Add(new ServiceDescriptor(typeof(IJob), typeof(LoadTradesJob), ServiceLifetime.Transient));
			services.Add(new ServiceDescriptor(typeof(IJob), typeof(GenerateOrderSummaryJob), ServiceLifetime.Transient));
			services.AddSingleton<IJobFactory, ScheduledJobFactory>();
			services.AddTransient<IMessageClient, MessageClient>();
			services.AddTransient(provider => {
				return JobBuilder.Create<LoadContentJob>()
				  .WithIdentity("LoadContent.job", "ContentGroup")
				  .Build();
			});

			services.AddTransient(provider => {
				return JobBuilder.Create<LoadTradesJob>()
				  .WithIdentity("LoadTradesJob.job", "TradesGroup")
				  .Build();
			});

			services.AddTransient(provider => {
				return JobBuilder.Create<GenerateOrderSummaryJob>()
				  .WithIdentity("GenerateOrderSummaryJob.job", "AITradesGroup")
				  .Build();
			});

			Console.WriteLine("LoadContentIntervalSeconds: " + Configuration["LoadContentIntervalSeconds"]);
			services.AddTransient(provider => {
				return TriggerBuilder.Create()
					.WithIdentity($"LoadContent.trigger", "ContentGroup")
					.StartNow()
					.WithSimpleSchedule
					 (s =>
						s.WithInterval(TimeSpan.FromSeconds(Convert.ToInt32(Configuration["LoadContentIntervalSeconds"])))
						.RepeatForever()
					 )
					 .Build();
			});

			services.AddTransient(provider => {
				return TriggerBuilder.Create()
					.WithIdentity($"LoadTradesJob.trigger", "TradesGroup")
					.StartNow()
					.WithSimpleSchedule
					 (s =>
						s.WithInterval(TimeSpan.FromSeconds(Convert.ToInt32(Configuration["LoadTradesIntervalSeconds"])))
						.RepeatForever()
					 )
					 .Build();
			});

			services.AddTransient(provider => {
				return TriggerBuilder.Create()
					.WithIdentity($"GenerateOrderSummaryJob.trigger", "AITradesGroup")
					.StartNow()
					.WithSimpleSchedule
					 (s =>
						s.WithInterval(TimeSpan.FromSeconds(Convert.ToInt32(Configuration["GenerateOrderSummaryIntervalSeconds"])))
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
	}
}