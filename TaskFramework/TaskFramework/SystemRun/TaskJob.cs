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
            LogHelper.WriteInfo("TaskJob运行Execute;context.JobDetail.Name" + context.JobDetail.Name + "nodetask" + nodetask);
            nodetask.Tasklock.Invoke(() =>
            {
                try
                {
                    nodetask.TaskDLL.StartRun();
                }
                catch (Exception exp)
                {
                    LogHelper.WriteError("任务" + taskid + "TaskJob回调时执行失败");
                }
            });
            //lock (lock_node)
            //{
            //    LogHelper.WriteDebug("准备运行一次:" + nodetask.TaskDLL.GetType().ToString());
            //    //nodetask.TaskDLL.StartRun();
            //}
        }
    }
}
