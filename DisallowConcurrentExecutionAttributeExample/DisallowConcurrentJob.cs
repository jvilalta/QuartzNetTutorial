using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Common.Logging;
using System.Threading;

namespace DisallowConcurrentExecutionAttributeExample
{
    [DisallowConcurrentExecutionAttribute]
    class DisallowConcurrentJob : BaseLongRunningJob
    {

    }
}
