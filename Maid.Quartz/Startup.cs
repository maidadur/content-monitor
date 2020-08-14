﻿using Maid.RabbitMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;s
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
						s.WithInterval(TimeSpan.FromSeconds(60 * 5))
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

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IScheduler scheduler) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			} else {
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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