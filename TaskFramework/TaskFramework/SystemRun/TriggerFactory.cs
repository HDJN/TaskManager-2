using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace TaskFramework.SystemRun
{
    public class TriggerFactory
    {
        public static Trigger Create(NodeTaskRuntimeInfo nodetask)
        {
            CronTrigger triger = new CronTrigger(nodetask.TaskModel.Id.ToString(), nodetask.TaskModel.CategoryId.ToString());
            triger.CronExpressionString = nodetask.TaskModel.TaskCron;
            return triger;
        }
    }
}
