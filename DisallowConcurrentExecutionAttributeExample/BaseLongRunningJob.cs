using log4net;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DisallowConcurrentExecutionAttributeExample
{
    class BaseLongRunningJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            _Log.InfoFormat("BEGIN job {0} in group {1}", context.JobDetail.Key.Name, context.JobDetail.Key.Group);
            var runningJobs = string.Join(",", context.Scheduler.GetCurrentlyExecutingJobs().Select(j => j.JobDetail.Key.Name ));
            _Log.InfoFormat("Running jobs: {0}",runningJobs);
            Thread.Sleep(5000);
            _Log.InfoFormat("END job {0} in group {1}", context.JobDetail.Key.Name, context.JobDetail.Key.Group);

        }
        private static ILog _Log = LogManager.GetLogger(typeof(BaseLongRunningJob));
    }
}
