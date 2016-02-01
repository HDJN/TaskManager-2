using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using ZF.Log;

namespace TaskFramework.SystemRun
{
    public class TaskJob : IJob
    {
        private object lock_node = new object();
        public void Execute(JobExecutionContext context)
        {
            string taskid = context.JobDetail.Name;
            NodeTaskRuntimeInfo nodetask = TaskPool.Instance().Get(taskid);
            lock (lock_node)
            {
                LogHelper.WriteDebug("准备运行一次:" + nodetask.TaskDLL.GetType().ToString());
                nodetask.TaskDLL.Run();
                nodetask.TaskDLL.StartRun();
            }
        }
    }
}
