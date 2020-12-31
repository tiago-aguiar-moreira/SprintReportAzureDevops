using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;
using SprintReport.WorkService.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SprintReport.WorkService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private StdSchedulerFactory _schedulerFactory;
        private CancellationToken _stopppingToken;
        private IScheduler _scheduler;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("O serviço está iniciando.");

            stoppingToken.Register(() => _logger.LogInformation("Tarefa de segundo plano está parando."));

            await StartJobs();

            _stopppingToken = stoppingToken;

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }

            await _scheduler.Shutdown();

            _logger.LogInformation("O serviço está parando.");
        }

        protected async Task StartJobs()
        {
            _schedulerFactory = new StdSchedulerFactory();
 
            _scheduler = await _schedulerFactory.GetScheduler();
            await _scheduler.Start();
 
            // IJobDetail sendReportJob = JobBuilder.Create<SendReportJob>()
            //     .WithIdentity("SendReportJob", "group")
            //     .Build();
 
            // ITrigger trigger1 = TriggerBuilder.Create()
            //     .WithIdentity("trigger_everyday_seven_oclock", "group")
            //     .StartNow()
            //     .WithCronSchedule("0 0 7 1/1 * ? *")
            //     .Build();
 
 
            IJobDetail takeSnapshotJob = JobBuilder.Create<TakeSnapshotJob>()
               .WithIdentity("TakeSnapshotJob", "group")
               .Build();
 
            ITrigger trigger2 = TriggerBuilder.Create()
                .WithIdentity("trigger_30_min", "group")
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInMinutes(30).RepeatForever())
                .Build();
 
 
            //await _scheduler.ScheduleJob(sendReportJob, trigger1, _stopppingToken);
            await _scheduler.ScheduleJob(takeSnapshotJob, trigger2, _stopppingToken);
        }
    }
}
