
using Quartz;
using Quartz.Impl;

namespace WebMvcSandbox.Quartz
{
    public class JobScheduler
    {
        public static void Start()
        {
            ISchedulerFactory schedFact = new StdSchedulerFactory();

            IScheduler scheduler = (IScheduler)schedFact.GetScheduler();

            scheduler.Start();

            IJobDetail job = JobBuilder.Create<TestJob>()
        .WithIdentity("test", "testJobs")
        .Build();

            // Trigger the job to run now, and then every 40 seconds
            ITrigger trigger = TriggerBuilder.Create()
              .WithIdentity("myTrigger", "group1")
              .StartNow()
              .WithSimpleSchedule(x => x
                  .WithIntervalInSeconds(1)
                  .RepeatForever())
              .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }
}