using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisallowConcurrentExecutionAttributeExample
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create the scheduler factory
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();

            //Ask the scheduler factory for a scheduler
            IScheduler scheduler = schedulerFactory.GetScheduler();

            //Start the scheduler so that it can start executing jobs
            scheduler.Start();

            // Create a job of Type WriteToConsoleJob
            IJobDetail job1 = JobBuilder.Create(typeof(DisallowConcurrentJob)).WithIdentity("DisallowConcurrentJob", "DisallowConcurrentJobGroup").Build();

            //Schedule this job to execute every second, a maximum of 10 times
            ITrigger trigger1 = TriggerBuilder.Create().WithSchedule(SimpleScheduleBuilder.RepeatSecondlyForTotalCount(10)).StartNow().WithIdentity("DisallowConcurrentJobTrigger", "DisallowConcurrentJobTriggerGroup").Build();
            scheduler.ScheduleJob(job1, trigger1);

            // Create a job of Type WriteToConsoleJob
            IJobDetail job2 = JobBuilder.Create(typeof(AllowConcurrentJob)).WithIdentity("AllowConcurrentJob", "AllowConcurrentJobGroup").Build();

            //Schedule this job to execute every second, a maximum of 10 times
            ITrigger trigger2 = TriggerBuilder.Create().WithSchedule(SimpleScheduleBuilder.RepeatSecondlyForTotalCount(10)).StartNow().WithIdentity("AllowConcurrentJobTrigger", "AllowConcurrentJobTriggerGroup").Build();
            scheduler.ScheduleJob(job2, trigger2);

            //Wait for a key press. If we don't wait the program exits and the scheduler gets destroyed
            Console.ReadKey();

            //A nice way to stop the scheduler, waiting for jobs that are running to finish
            scheduler.Shutdown(true);
        }
    }
}
